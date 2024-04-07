namespace PixelHotel.Infra.Options;

public class ElasticsearchOptions
{
    public const string Elasticsearch = "Elasticsearch";

    public string Uri { get; set; }
    public string IndexFormat { get; set; }
}
