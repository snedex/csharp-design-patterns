using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.BasicBuilder;
using Shared;

namespace Builder
{
    public class FluentBuilder : ExampleBase, IExample
    {
        public FluentBuilder()
        {
            SectionName = "Fluent Builder with Generics";
        }
    
        protected override void ExecuteCode()
        {
            Console.WriteLine("Lets create a Fluent HTML builder HtmlElement");
            Console.WriteLine("Add child returns the instance of the builder class to chain calls");

            var words = new[] {"hello", "world"};

            var builder = new FluentHtmlBuilder("ul");
            builder.AddChild("li", "hello").AddChild("li", "world");
            Console.WriteLine(builder);
        }
    }
}