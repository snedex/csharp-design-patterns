using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Builder.Common;

namespace Builder.BasicBuilder
{
    public class FluentHtmlBuilder {
        private readonly string rootName;
        HtmlElement root = new HtmlElement();

        public FluentHtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        public FluentHtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootName};
        }
    }
}