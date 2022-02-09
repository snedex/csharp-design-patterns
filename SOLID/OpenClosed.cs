using System;
using System.Threading.Tasks;
using Shared;

namespace SOLID
{
    public enum Colour
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Yuuuge
    }

    public class Product
    {
        public string name;
        public Colour colour;
        public Size size;

        public Product(string name, Colour colour, Size size)
        {
            this.size = size;
            this.colour = colour;
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(paramName: nameof(name));
            this.name = name;
        }
    }

    public class OpenClosed : ExampleBase
    {
        public OpenClosed()
        {
            SectionName = "Open-Closed Principle";
        }

        protected override void ExecuteCode()
        {
            //We've been asked initially to write something to allow customers to filter by size
            //Lets make some products to filter

            var apple = new Product("Apple", Colour.Green, Size.Small);
            var tree = new Product("Tree", Colour.Green, Size.Large);
            var house = new Product("House", Colour.Blue, Size.Large);

            Product[] products = { apple, tree, house }; 

            //Now we only want the green products
            //We need to change the filter to allow this.
            var pf = new ProductFilter();
            Console.WriteLine($"Green Products (old)");

            foreach(var i in pf.FilterByColour(products, Colour.Green))
            {
                Console.WriteLine($"{i.name} is {i.colour}");
            }

            //But now the spec wants us to filter by size and colour!
            //Instead of changing the ProductFilter let us extend it using the enterprise pattern specification pattern.
            var betterFilter = new BetterFilter();
            var spec = new ColourSpecification(Colour.Green);
            
            Console.WriteLine($"Green Products (spec)");
            foreach(var i in betterFilter.Filter(products, spec))
            {
                Console.WriteLine($"{i.name} is {i.colour}");
            }

            var blueSpec = new ColourSpecification(Colour.Blue);
            var largeSpec = new SizeSpecification(Size.Large);
            var andSpec = new AndSpecification<Product>(blueSpec, largeSpec);
            //Ok now size and colour with combinator
            Console.WriteLine($"Large Blue Products (and)");
            foreach(var i in betterFilter.Filter(products, andSpec))
            {
                Console.WriteLine($"{i.name} is {i.colour}");
            }

            //If we need more, we are open for extension but closed for modification
        }
    }

    //So lets implement a product filter
    public class ProductFilter {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size) {
            foreach(var p in products)
                if(p.size == size)
                    yield return p;
        }

        //We add a new duplicate changing for the colour instead of size
        public IEnumerable<Product> FilterByColour(IEnumerable<Product> products, Colour colour) {
            foreach(var p in products)
                if(p.colour == colour)
                    yield return p;
        }

        //Adding more filters
        public IEnumerable<Product> FilterBySizeAndColour(IEnumerable<Product> products, Size size, Colour colour) {
            foreach(var p in products)
                if(p.colour == colour && p.size == size)
                    yield return p;
        }
    }

    //Pattern Interfaces
    //Specification can work on a given type
    public interface ISpecification<T>
    {
        //Check if the item t satisfies the implementation criteria
        bool IsSatisfied(T t);
    }

    public interface IFilter<T> 
    {
        //Returns a filtered list based on the specification that is passed in, spec determines how to filter the objects
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColourSpecification : ISpecification<Product>
    {
        private readonly Colour c;

        public ColourSpecification(Colour c)
        {
            this.c = c;
        }

        public bool IsSatisfied(Product t)
        {
            return t.colour == c; 
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private readonly Size s;

        public SizeSpecification(Size s)
        {
            this.s = s;
        }        

        public bool IsSatisfied(Product t)
        {
            return t.size == s;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> leftSpec;
        private readonly ISpecification<T> rightSpec;
        public AndSpecification(ISpecification<T> leftSpec, ISpecification<T> rightSpec)
        {
            this.rightSpec = rightSpec;
            this.leftSpec = leftSpec;
            
        }
        public bool IsSatisfied(T t)
        {
            return leftSpec.IsSatisfied(t) && rightSpec.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach(var p in items)
                if(spec.IsSatisfied(p))
                    yield return p;
        }
    }
}