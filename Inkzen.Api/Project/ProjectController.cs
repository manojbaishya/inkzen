using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Piranha;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace Inkzen.Api.Project;

[ApiController]
[Route("api/project")]
public class ProjectController(IApi api, ILogger<ProjectController> logger) : ControllerBase
{
    [HttpGet("{id:Guid}")]
    public async Task<ProjectDto> GetByIdAsync([FromRoute(Name = "id")] Guid id, [FromQuery(Name = "imageSize")] int imageSize)
    {
        DynamicContent project = await api.Content.GetByIdAsync(id).ConfigureAwait(false);
        var images = project.Regions.Images as RegionList<ImageField>;
        IList<ImageMetadataDto> gallery = images.Select(image =>
            {
                string imageUrl = imageSize != 0 ? image.Resize(api, imageSize) : image.Resize(api, 500);
                imageUrl ??= image.Media.PublicUrl;

                return new ImageMetadataDto
                {
                    Id = image.Id.ToString(),
                    Url = imageUrl,
                    altText = image.Media.AltText
                };
            })
            .ToList();

        return new ProjectDto
        {
            project = project,
            gallery = gallery
        };
    }

    [HttpGet]
    [Route("list")]
    public Task<IEnumerable<ContentInfo>> GetAll() => api.Content.GetAllAsync<ContentInfo>(App.ContentGroups.GetById(Project.ContentGroup).Id);
}
