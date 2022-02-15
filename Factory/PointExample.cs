using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;

namespace Factory
{
    public class PointExample : ExampleBase, IExample
    {

        public enum CoordinateType 
        {
            Cartesian,
            Polar
        }

        public class Point 
        {

            private double x, y;
           
            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            // Now we replace this constructor with one specifying a type
            // And we need some form of processing logic in the constructor
            // also the coordinate variables will now be semi ambiguous 
            // In addition the contructor name is restrcited so we can't be descriptive
            public Point(double a, double b, CoordinateType type = CoordinateType.Cartesian)
            {
                switch(type)
                {
                    case CoordinateType.Cartesian:
                        this.x = a; //This is not clear in the constructor, you can infer it but it can be the other way around
                        this.y = b;
                        break;
                    case CoordinateType.Polar:
                        x = a * Math.Cos(b);
                        y = a * Math.Sin(b);
                        break;
                    default:
                        throw new ArgumentException("Invalid coordinate type");
                }
            }

            // This Constructor is not possible as we can't overload the constructor
            // with the same type parameters
            // public Point(float rho, float theta)
            // {

            // }
        }

        public PointExample()
        {
            this.SectionName = "Point Example";
        }

        protected override void ExecuteCode()
        {
            Console.WriteLine("This is an example showing parameter hell with constructors.");
            Console.WriteLine("We have defined a Point class, for both cartesian and polar points.");
            Console.WriteLine("However we'd favour Factory methods rather than constructor overload for clarity");
        }
    }
}