namespace NewsAndMedia.Core
{
    public class RabbitMqConfiguration
    {
        public const string Position = "RabbitMQ";
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
