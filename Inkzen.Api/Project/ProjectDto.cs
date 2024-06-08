using System.Collections.Generic;

using Piranha.Models;

namespace Inkzen.Api.Project;
public class ProjectDto
{
    public DynamicContent project { get; set; }
    public IList<ImageMetadataDto> gallery;

}

public class ImageMetadataDto
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string altText { get; set; }
}