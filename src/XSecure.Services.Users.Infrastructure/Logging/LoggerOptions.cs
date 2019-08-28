using XSecure.Services.Users.Infrastructure.Logging.Options;

namespace XSecure.Services.Users.Infrastructure.Logging
{
    public class LoggerOptions
    {
        public string Level { get; set; }
        public ConsoleOptions Console { get; set; }
        public FileOptions File { get; set; }
        public GrayLogOptions GrayLog { get; set; }
    }
}