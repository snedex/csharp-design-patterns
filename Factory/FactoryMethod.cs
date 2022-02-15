using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;

namespace Factory
{
    public class FactoryMethod : ExampleBase, IExample
    {

        public class Point 
        {
            private double x, y;
        }

        public FactoryMethod()
        {
            this.SectionName = "Factory Method Example";
        }

        protected override void ExecuteCode()
        {
            Console.WriteLine("We will now take our class and construct it using a factory method.");
        }
    }
}