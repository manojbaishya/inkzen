using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inkzen.Api.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Piranha;
using Piranha.Manager;
using Piranha.Models;

namespace Inkzen.Api;

[ApiController]
[Route("api/team")]
public class TeamController(IApi api, ILogger<TeamController> logger) : ControllerBase
{
    [HttpGet("{id:Guid}")]
    public async Task<MemberDto> GetByIdAsync([FromRoute(Name = "id")] Guid id)
    {
        logger.LogInformation("Getting member details with Id: {Id}", id);

        Member? member = await api.Content.GetByIdAsync<Member>(id) ?? throw new InvalidOperationException($"Member with Guid {id} not present!");
        
        return new MemberDto
        {
            Photo = new ImageMetadataDto {
                Id = member.Photo?.Id.ToString(),
                Url = member.Photo?.Media.PublicUrl,
                AltText = member.Photo?.Media.AltText
            },
            Name = member.Name,
            Role = member.Role?.ToString(),
            Bio = member.Bio,
            Link = member.Link
        };
    }

    [HttpGet]
    [Route("list")]
    public Task<IEnumerable<Member>> GetAll() => api.Content.GetAllAsync<Member>(App.ContentGroups.GetById(Member.ContentGroup).Id);
}