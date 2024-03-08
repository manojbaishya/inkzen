using Piranha.AttributeBuilder;
using Piranha.Models;

namespace RazorWeb.Models;

[PostType(Title = "Standard post")]
public class BlogPost : Post<BlogPost>
{
}
