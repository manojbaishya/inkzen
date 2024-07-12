using Inkzen.Api.Shared;

namespace Inkzen.Api;

public class MemberDto
{
    public ImageMetadataDto? Photo { get; set; }
    public string? Name {get; set; }
    public string? Role  {get; set; }
    public string? Bio {get; set; }
    public string? Link {get; set; }
}
