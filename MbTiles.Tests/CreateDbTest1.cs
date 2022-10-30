using MBTiles.Core;

namespace MbTiles.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        File.Delete("test.mbtiles");
        var metadataJson = File.ReadAllText("testfixtures/metadata.json");
        var metadata = System.Text.Json.JsonSerializer.Deserialize<Metadata>(metadataJson);
        var bbox = "4.814930,52.320757,4.988308,52.396763";
        metadata.bounds = bbox;
        var xmin = Double.Parse(bbox.Split(",")[0]);
        var ymin = Double.Parse(bbox.Split(",")[1]);
        var xmax = Double.Parse(bbox.Split(",")[2]);
        var ymax = Double.Parse(bbox.Split(",")[3]);

        var levelFrom = 0;
        var levelTo = 17;

        var city = "amsterdam";

        metadata.center = $"{xmin + (xmax - xmin) / 2},{ymin + (ymax - ymin) / 2},{levelTo}";
        metadata.name = city;
        metadata.description = city;

        var conn = MBTilesWriter.CreateDatabase("test.mbtiles", metadata);
        Assert.That(conn, Is.Not.Null);
    }
}