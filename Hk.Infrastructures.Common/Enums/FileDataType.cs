using System.ComponentModel;

namespace Hk.Infrastructures.Common.Enums
{
    /// <summary>
    /// 通过FastDFS上传的文件类型
    /// </summary>
    public enum FileDataType
    {
        [Description("图片")]
        Picture = 1,
        [Description("文本")]
        Text=2,
        [Description("语音")]
        Audio=3,
        [Description("其它")]
        Other=4
    }
}
