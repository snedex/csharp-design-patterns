using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.BasicBuilder;
using Builder.Common;
using Shared;

namespace Builder
{
    public class FluentBuilderWithGenerics : ExampleBase, IExample
    {
        public FluentBuilderWithGenerics()
        {
            SectionName = "Fluent Builder with generics and inheritance";
        }
    
        protected override void ExecuteCode()
        {
            Console.WriteLine("Lets create a fluent builder with recursive generics");

            var builder = BuiltPerson.New
                .WorksAs("Renegade")
                .Called("Commander Shepard");

            Console.WriteLine(builder.Build());
        }

        public class BuiltPerson : Person
        {
            //We expose an API for the builder
            public class Builder : PersonJobBuilder<Builder> 
            {

            }

            public static Builder New => new Builder();

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }

        //Example builder base class
        public abstract class PersonBuilder 
        {
            protected BuiltPerson person = new BuiltPerson();

            public BuiltPerson Build()
            {
                return person;
            }
        }

        //Using recursive generics to enforce that any deriving builder inherits from the 
        //builder it is subclassing so it can be chained fluently and still retain the 
        //functions of the higher level builder class
        public class PersonInfoBuilder<T> : PersonBuilder
            where T : PersonInfoBuilder<T>
        {
           
            public T Called(string name)
            {
                person.Name = name;
                //Shouldn't need the cast but vscode complains
                return (T)this;
            }
        }

        //Now  we can inherit from the person info builder and specify we are using a job builder type
        public class PersonJobBuilder<T> : PersonInfoBuilder<PersonJobBuilder<T>>
            where T : PersonJobBuilder<T>
        {
            public T WorksAs(string position)
            {
                person.Position = position;
                return (T)this;
            }
        }
    }
}