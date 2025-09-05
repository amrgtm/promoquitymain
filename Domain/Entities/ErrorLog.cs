using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ErrorLog : CommonFields
    {
        public required string Message { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
        public string LogLevel { get; set; }
        [MaxLength(2083)]
        public string RequestPath { get; set; }
        [MaxLength(10)]
        public string HttpMethod { get; set; }
        [MaxLength(512)]
        public string UserAgent { get; set; }
        [MaxLength(45)]
        public string IpAddress { get; set; }
        [MaxLength(256)]
        public string Source { get; set; }
    }
}
