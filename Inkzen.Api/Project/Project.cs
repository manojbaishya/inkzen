using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Piranha;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace Inkzen.Api.Project;

[ContentGroup(Id = "Projects", Title = "Projects", Icon = "fas fa-building")]
public abstract class Projects<T> : Content<T> where T : Projects<T>
{
}

[ContentType(Id = "Project", Title = "Project")]
public class Project : Projects<Project>, IBlockContent
{
    [Region]
    public HtmlField Description { get; set; }
    public IList<Block> Blocks { get; set; }
}