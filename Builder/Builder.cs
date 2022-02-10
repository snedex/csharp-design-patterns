using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.BasicBuilder;
using Shared;

namespace Builder
{

    

    public class Builder : ExampleBase, IExample
    {
        public Builder()
        {
            SectionName = "Builder";
        }
    
        protected override void ExecuteCode()
        {
            Console.WriteLine("Lets create a HTML builder HtmlElement");

            var words = new[] {"hello", "world"};

            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            Console.WriteLine(builder);
        }
    }
}