using System.Collections.Generic;
using Inkzen.Api.Shared;

using Piranha.Models;

namespace Inkzen.Api;
public class ProjectDto
{
    public DynamicContent? project { get; set; }
    public IList<ImageMetadataDto>? gallery { get; set; }

}
