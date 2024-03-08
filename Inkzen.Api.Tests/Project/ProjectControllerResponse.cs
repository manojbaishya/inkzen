using System;

namespace Inkzen.Api.Tests.Project;

public class ProjectControllerResponse
{
    public Regions regions { get; set; }
    public string category { get; set; }
    public Tag[] tags { get; set; }
    public Block[] blocks { get; set; }
    public PrimaryImage primaryImage { get; set; }
    public string excerpt { get; set; }
    public string id { get; set; }
    public string typeId { get; set; }
    public string title { get; set; }
    public object[] permissions { get; set; }
    public DateTimeOffset created { get; set; }
    public DateTimeOffset lastModified { get; set; }
}

public class Block
{

}

public class Regions
{
    public string type { get; set; }
    public Description Description { get; set; }
    public Client Client { get; set; }
    public Year Year { get; set; }
    public Images Images { get; set; }
    public Video Video { get; set; }
}

public class Description
{
    public string type { get; set; }
    public string value { get; set; }
}

public class Client
{
    public string type { get; set; }
    public string value { get; set; }
}

public class Year
{
    public string type { get; set; }
    public int value { get; set; }
}

public class Images
{
    public string type { get; set; }
    public Image[] values { get; set; }
}

public class Image
{
    public string id { get; set; }
    public Media media { get; set; }
    public bool hasValue { get; set; }
}

public class Media
{
    public Properties properties { get; set; }
    public Version[] versions { get; set; }
    public string id { get; set; }
    public string folderId { get; set; }
    public int type { get; set; }
    public string filename { get; set; }
    public string contentType { get; set; }
    public string title { get; set; }
    public string altText { get; set; }
    public string description { get; set; }
    public int size { get; set; }
    public string publicUrl { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public DateTime created { get; set; }
    public DateTime lastModified { get; set; }
}

public class Properties
{
}

public class Version
{
    public string id { get; set; }
    public int size { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public string fileExtension { get; set; }
}

public class Video
{
    public string type { get; set; }
    public string value { get; set; }
}

public class PrimaryImage
{
    public string id { get; set; }
    public Media media { get; set; }
    public bool hasValue { get; set; }
}

public class Tag
{
    public string id { get; set; }
    public string title { get; set; }
    public string slug { get; set; }
    public int type { get; set; }
}
