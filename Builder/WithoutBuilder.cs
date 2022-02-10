using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Builder
{
    public class WithoutBuilder : ExampleBase, IExample
    {
        public WithoutBuilder()
        {
            SectionName = "Without Builder";
        }
        protected override void ExecuteCode()
        {
            Console.WriteLine("When construction gets too complex");
            Console.WriteLine("Lets output a chink of HTML");

            var hello = "hello";
            //turn to html <p><p>
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");

            Console.WriteLine(sb);

            Console.WriteLine("Lets output a list");
            var words  = new[]  { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach(var w in words)
                sb.AppendFormat("<li>{0}></li>", w);
            sb.Append("</ul>");
            Console.WriteLine(sb);

            Console.WriteLine("This is overly verbose in the code and low level, ideally we'd like something more succinct");
        }
    }
}