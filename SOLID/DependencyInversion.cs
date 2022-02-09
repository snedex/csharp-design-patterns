using System.Collections.Generic;
using Shared;

namespace SOLID
{
    public class DependencyInversion : ExampleBase
    {
        public DependencyInversion()
        {
            SectionName = "Dependency Inversion";
        }

        protected override void ExecuteCode()
        {
            //Performing queries on a geneology db
            //Lets do some research
            var parent = new Person { Name = "Anakin" };
            var child1 = new Person { Name = "Luke" };
            var child2 = new Person { Name = "Leia" };

            var relations = new RelationShips();
            relations.AddParentAndChild(parent, child1);
            relations.AddParentAndChild(parent, child2);

            //Now how do we get the data? Do we allow access to the internal list?
            //We could access a public list on the relationships class.
            //The internals cannot change now. 
            //Instead define an interface to define the interactions IRelationshipBrowser
            var research = new Research(relations);
            research.DoResearch("Anakin");
        }
    }

    public class Research
    {
        private readonly IRelationshipBrowser browser;
        public Research(IRelationshipBrowser browser)
        {
            this.browser = browser;
        }

        public void DoResearch(string parent)
        {
            foreach(var p in browser.FindAllChildrenOf(parent))
                Console.WriteLine($"{parent} has a child called {p.Name}");
        }
    }

    public interface IRelationshipBrowser 
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    public enum RelationShip {
        Parent, Child, Sibling
    }

    public class Person 
    {
        public string Name;
    }

    public class RelationShips : IRelationshipBrowser
    {
        private List<(Person, RelationShip, Person)> relations = 
            new List<(Person, RelationShip, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, RelationShip.Parent, child));
            relations.Add((child, RelationShip.Child, parent));
        }

        //Now we have the implementation without exposiing the low level
        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            foreach(var r in relations.Where(r => r.Item1.Name == name && r.Item2 == RelationShip.Parent))
                yield return r.Item3;
        }
    }
}