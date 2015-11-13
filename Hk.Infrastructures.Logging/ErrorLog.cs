using Hk.Infrastructures.Mongo.Repository;
using System;
using System.Text;

namespace Hk.Infrastructures.Logging
{
    public class ErrorLog : Entity
    {
        public string ErrorLogId { get; set; }
        public int PlatformType { get; set; }
        public string Module { get; set; }
        public string Version { get; set; }
        public int LevelValue { get; set; }
        public string LevelName { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionInformation { get; set; }
        public string CustomMessage { get; set; }

    }
}
