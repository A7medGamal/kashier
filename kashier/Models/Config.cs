namespace kashier.Models
{
    public class Config
    {
        public string Mode { get; set; }
        public EnvironmentConfig Live { get; set; }
        public EnvironmentConfig Test { get; set; }
    }
}
