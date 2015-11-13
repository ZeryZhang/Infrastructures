using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Hk.Infrastructures.Common.Extensions;
using Hk.Infrastructures.Common.Utility;
using Hk.Infrastructures.Fastdfs.Configs;
using org.csource.fastdfs;
namespace Hk.Infrastructures.Fastdfs
{
    public class FileClient
    {
        private bool _isInited = false;
        private ConfigItem _configItem = null;

        public FileClient()
        {
            Init();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="contentByte">文件内容</param>
        /// <param name="fileExt">文件扩展名(注意:不包含".")</param>
        /// <returns>文件名</returns>
        public string Upload(byte[] contentByte, string fileExt)
        {
            string result = string.Empty;
            if (_isInited)
            {
                try
                {
                    if (_configItem != null && _configItem.FileGroups.IsNotNull())
                    {
                        var trackerClient = new TrackerClient();
                        var trackerServer = trackerClient.getConnection();

                        org.csource.fastdfs.StorageServer storageServer = trackerClient.getStoreStorage(trackerServer);
                        var groupName = GetFileGroupName();
                        var selectedGroup = _configItem.FileGroups.FirstOrDefault(u => u.Name == groupName);
                        if (storageServer != null && !string.IsNullOrWhiteSpace(groupName) &&
                            selectedGroup != null)
                        {
                            var storageClient = new StorageClient(trackerServer, storageServer);
                            var uploadResult = storageClient.upload_file(groupName, contentByte, fileExt, null);

                            if (uploadResult.IsNotNull() && uploadResult.Length == 2)
                            {
                                var domainItem = selectedGroup.StorageServers.FirstOrDefault(u => storageServer.getInetSocketAddress().getAddress().toString().Contains(u.Address));
                                if (domainItem != null && !string.IsNullOrWhiteSpace(domainItem.DomainName))
                                {
                                    result = string.Format("{0}/{1}/{2}", domainItem.DomainName, uploadResult[0], uploadResult[1]);
                                }
                            }

                            storageClient = null;
                        }

                        trackerServer.close();
                        if (storageServer != null)
                        {
                            storageServer.close();
                        }
                        trackerClient = null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        public ImageUrl ImageUpload(byte[] contentByte, string fileExt,int width = 120, int height=120)
        {
            ImageUrl result = new ImageUrl();
            switch (fileExt)
            {
                case "jpg":
                case "jpeg":
                case "gif":
                case "png":
                case "bmp"://图片
                    result.LargerLength = contentByte.Length;
                    result.LargerImageUrl = Upload(contentByte, fileExt);//上传大图
                    byte[] thumbnailContent = GetThumbnailContent(contentByte, fileExt, width, height);//获取缩略图的字节数组
                    if (thumbnailContent != null && thumbnailContent.Length > 0)
                    {
                        result.SmallLength = thumbnailContent.Length;
                        result.SmallImageUrl = Upload(thumbnailContent, fileExt);//上传缩略图
                        thumbnailContent = null;
                    }
                    break;
                default://其他文件          
                    break;
            }
            return result;
        }
        /// <summary>
        /// 等比上传图片
        /// </summary>
        /// <param name="contentByte"></param>
        /// <param name="fileExt"></param>
        /// <param name="plax"></param>
        /// <param name="isZip">true 压缩图片，false 不压缩</param>
        /// <returns></returns>
        public ImageUrl ImageUpload(byte[] contentByte, string fileExt, int plax,bool isZip=true)
        {
            ImageUrl result = new ImageUrl();
            switch (fileExt)
            {
                case "jpg":
                case "jpeg":
                case "gif":
                case "png":
                case "bmp"://图片
                    result.LargerLength = contentByte.Length;
                    result.LargerImageUrl = Upload(contentByte, fileExt);//上传大图
                    byte[] thumbnailContent = isZip?GetThumbnailContent(contentByte, fileExt, plax):contentByte;//获取缩略图的字节数组
                    if (thumbnailContent != null && thumbnailContent.Length > 0)
                    {
                        result.SmallLength = thumbnailContent.Length;
                        result.SmallImageUrl = Upload(thumbnailContent, fileExt);//上传缩略图
                        thumbnailContent = null;
                    }
                    break;
                default://其他文件          
                    break;
            }
            return result;
        }
        private byte[] GetThumbnailContent(byte[] oStreamoStreamBytes, string fileSuffix, int width = 120, int height=120)
        {
            byte[] bytes = null;
            Stream oStream = new MemoryStream(oStreamoStreamBytes);
            //生成原图 
            System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);
            int owidth = oImage.Width; //原图宽度 
            int oheight = oImage.Height; //原图高度
            int twidth = width;
            int theight = height;
            //按比例计算出缩略图的宽度和高度 
            if (owidth >= oheight)
            {
                theight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(120) / Convert.ToDouble(owidth)));
                //等比设定高度
            }
            else
            {
                twidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(120) / Convert.ToDouble(oheight)));
                //等比设定宽度
            }
            Bitmap tImage = null;
            Graphics g = null;
            try
            {
                //生成缩略原图 
                tImage = new Bitmap(twidth, theight);
                g = Graphics.FromImage(tImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //设置高质量插值法 
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //设置高质量,低速度呈现平滑程度 
                g.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                g.DrawImage(oImage, new Rectangle(0, 0, twidth, theight), new Rectangle(0, 0, owidth, oheight),
                    GraphicsUnit.Pixel);
                using (MemoryStream stream = new MemoryStream())
                {
                    switch (fileSuffix)
                    {
                        case "jpeg":
                        case "jpg":
                            {
                                tImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            }
                        case "gif":
                            {
                                tImage.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                            }
                        case "png":
                            {
                                tImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            }
                        case "bmp":
                            {
                                tImage.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            }
                    }
                    bytes = new byte[stream.Length];
                    bytes = stream.ToArray();
                }
                return bytes;
            }
            catch (Exception ex)
            {
                return bytes;
            }
            finally
            {
                oImage.Dispose();
                if (tImage != null)
                    tImage.Dispose();
                if (g != null)
                    g.Dispose();
            }
        }
        /// <summary>
        /// 等比缩小图片
        /// </summary>
        /// <param name="oStreamoStreamBytes"></param>
        /// <param name="fileSuffix"></param>
        /// <param name="Plax"></param>
        /// <returns></returns>
        private byte[] GetThumbnailContent(byte[] oStreamoStreamBytes, string fileSuffix, int Plax)
        {
            byte[] bytes = null;
            Stream oStream = new MemoryStream(oStreamoStreamBytes);
            //生成原图 
            System.Drawing.Image oImage = System.Drawing.Image.FromStream(oStream);
            int owidth = oImage.Width; //原图宽度 
            int oheight = oImage.Height; //原图高度
            int twidth = Plax;
            int theight = Plax;
            theight = (int)Math.Floor(Convert.ToDouble(oheight) * (Convert.ToDouble(100) / Convert.ToDouble(Plax)));
            twidth = (int)Math.Floor(Convert.ToDouble(owidth) * (Convert.ToDouble(100) / Convert.ToDouble(Plax)));
            Bitmap tImage = null;
            Graphics g = null;
            try
            {
                //生成缩略原图 
                tImage = new Bitmap(twidth, theight);
                g = Graphics.FromImage(tImage);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //设置高质量插值法 
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; //设置高质量,低速度呈现平滑程度 
                g.Clear(Color.Transparent); //清空画布并以透明背景色填充 
                g.DrawImage(oImage, new Rectangle(0, 0, twidth, theight), new Rectangle(0, 0, owidth, oheight),
                    GraphicsUnit.Pixel);
                using (MemoryStream stream = new MemoryStream())
                {
                    switch (fileSuffix)
                    {
                        case "jpeg":
                        case "jpg":
                            {
                                tImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            }
                        case "gif":
                            {
                                tImage.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                            }
                        case "png":
                            {
                                tImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            }
                        case "bmp":
                            {
                                tImage.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            }
                    }
                    bytes = new byte[stream.Length];
                    bytes = stream.ToArray();
                }
                return bytes;
            }
            catch (Exception ex)
            {
                return bytes;
            }
            finally
            {
                oImage.Dispose();
                if (tImage != null)
                    tImage.Dispose();
                if (g != null)
                    g.Dispose();
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>      
        /// <param name="fileUrl">文件名</param>
        /// <returns>文件内容</returns>
        public byte[] Download(string fileUrl)
        {
            byte[] result = null;
            if (_isInited)
            {
                try
                {
                    var trackerClient = new TrackerClient();
                    var trackerServer = trackerClient.getConnection();

                    org.csource.fastdfs.StorageServer storageServer = trackerClient.getStoreStorage(trackerServer);
                    var groupName = GetFileGroupName();
                    if (storageServer != null && !string.IsNullOrWhiteSpace(groupName))
                    {
                        var storageClient = new StorageClient(trackerServer, storageServer);
                        Uri uriValue = null;
                        if (Uri.TryCreate(fileUrl, UriKind.RelativeOrAbsolute, out uriValue))
                        {
                            var realFileName = StringUtil.ReplaceString(uriValue.AbsolutePath, "/" + groupName + "/", string.Empty, false);
                            result = storageClient.download_file(groupName, realFileName);
                        }
                        storageClient = null;
                    }

                    trackerServer.close();
                    if (storageServer != null)
                    { storageServer.close(); }
                    trackerClient = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// 增量下载文件
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="offset">从文件起始点的偏移量</param>
        /// <param name="length">要读取的字节数</param>
        /// <returns></returns>
        public byte[] Download(string fileUrl, long offset, long length)
        {
            byte[] result = null;
            if (_isInited)
            {
                try
                {
                    var trackerClient = new TrackerClient();
                    var trackerServer = trackerClient.getConnection();

                    org.csource.fastdfs.StorageServer storageServer = trackerClient.getStoreStorage(trackerServer);
                    var groupName = GetFileGroupName();
                    if (storageServer != null && !string.IsNullOrWhiteSpace(groupName))
                    {
                        var storageClient = new StorageClient(trackerServer, storageServer);
                        Uri uriValue = null;
                        if (Uri.TryCreate(fileUrl, UriKind.RelativeOrAbsolute, out uriValue))
                        {
                            var realFileName = StringUtil.ReplaceString(uriValue.AbsolutePath, "/" + groupName + "/", string.Empty, false);
                            result = storageClient.download_file(groupName, realFileName, offset, length);
                        }
                        storageClient = null;
                    }

                    trackerServer.close();
                    if (storageServer != null) { storageServer.close(); }
                    trackerClient = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public bool Remove(string fileUrl)
        {
            bool result = false;
            if (_isInited)
            {
                try
                {
                    var trackerClient = new TrackerClient();
                    var trackerServer = trackerClient.getConnection();

                    org.csource.fastdfs.StorageServer storageServer = trackerClient.getStoreStorage(trackerServer);
                    var groupName = GetFileGroupName();
                    if (storageServer != null && !string.IsNullOrWhiteSpace(groupName))
                    {
                        var storageClient = new StorageClient(trackerServer, storageServer);
                        Uri uriValue = null;
                        if (Uri.TryCreate(fileUrl, UriKind.RelativeOrAbsolute, out uriValue))
                        {
                            var realFileName = StringUtil.ReplaceString(uriValue.AbsolutePath, "/" + groupName + "/", string.Empty, false);
                            result = storageClient.delete_file(groupName, realFileName) == 0;
                        }
                        storageClient = null;
                    }

                    trackerServer.close();
                    if (storageServer != null) { storageServer.close(); }
                    trackerClient = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        private void Init()
        {
            try
            {
                _configItem = Fastdfs.Configs.Config.GetConfig();
                if (_configItem != null && _configItem.FileGroups.IsNotNull() && _configItem.TrackerServers.IsNotNull())
                {
                    var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "FastdfsClient.config");
                    ClientGlobal.init(configPath);
                    _isInited = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetFileGroupName()
        {
            string fileGroupName = string.Empty;
            try
            {
                _configItem = Fastdfs.Configs.Config.GetConfig();
                if (_configItem != null && _configItem.FileGroups.IsNotNull())
                {
                    var random = new System.Random();
                    var index = random.Next(0, _configItem.FileGroups.Count);
                    fileGroupName = _configItem.FileGroups[index].Name;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fileGroupName;
        }
    }
}

