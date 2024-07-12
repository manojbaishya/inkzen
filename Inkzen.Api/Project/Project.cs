using System.Collections.Generic;

using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace Inkzen.Api;

[ContentGroup(Id = "Projects", Title = "Projects", Icon = "fas fa-building")]
public abstract class Projects<T> : Content<T> where T : Projects<T>;

[ContentType(Id = "Project", Title = "Project")]
public class Project : Projects<Project>, ITaggedContent
{
    public const string ContentGroup = "Projects";
    [Region] public TextField Description { get; set; }
    [Region] public StringField Client { get; set; }
    [Region] public NumberField Year { get; set; }
    [Region] public IList<ImageField> Images { get; set; }
    [Region] public StringField Video { get; set; }
    [Region] public IList<Taxonomy> Tags { get; set; }
}