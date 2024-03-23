namespace PixelHotel.Infra.Options;

public class ElasticsearchOptions : IOptions
{
    public const string KEY = "Elasticsearch";

    public string Uri { get; set; }
    public string IndexFormat { get; set; }
}
