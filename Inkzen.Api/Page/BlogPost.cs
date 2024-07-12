using Piranha.AttributeBuilder;
using Piranha.Models;

namespace Inkzen.Api.Page;

[PostType(Title = "Standard post")]
public class BlogPost : Post<BlogPost>
{
}
