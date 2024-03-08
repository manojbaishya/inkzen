using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Piranha;
using Piranha.Models;

namespace Inkzen.Api.Project;

[ApiController]
[Route("api/project")]
public class ProjectController(IApi api, ILogger<ProjectController> logger) : ControllerBase
{
    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<DynamicContent> GetByIdAsync(Guid id) => await api.Content.GetByIdAsync(id);

    [HttpGet]
    [Route("list")]
    public async Task<IEnumerable<ContentInfo>> GetAll()
    {
        ContentGroup group = App.ContentGroups.GetById(Project.ContentGroup);
        return await api.Content.GetAllAsync<ContentInfo>(group.Id);
    }
}
