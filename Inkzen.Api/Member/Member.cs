using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;
using Piranha.Extend.Fields;
using System.ComponentModel.DataAnnotations;

namespace Inkzen.Api;

[ContentGroup(Id = "Team", Title = "Team", Icon = "fas fa-users")]
public abstract class Team<T> : Content<T> where T : Team<T>;

[ContentType(Id = "Member", Title = "Member")]
public class Member : Team<Member>
{
    public const string ContentGroup = "Team";
    [Region] public ImageField Photo { get; set; }
    [Region] public StringField Name { get; set; }
    [Region] public SelectField<MemberRole> Role { get; set; }
    [Region] public TextField Bio { get; set; }
    [Region] public StringField Link { get; set; }
}

public enum MemberRole
{
    [Display(Description = "Graphic Designer")]
    GraphicDesigner,
    [Display(Description = "Interior Designer")]
    InteriorDeveloper,
    [Display(Description = "Architect")]
    Architect
}

