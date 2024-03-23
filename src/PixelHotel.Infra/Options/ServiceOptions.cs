namespace PixelHotel.Infra.Options;

public class ServiceOptions : IOptions
{
    public const string KEY = "Service";

    public string Name { get; set; }
    public string Version { get; set; }
}
