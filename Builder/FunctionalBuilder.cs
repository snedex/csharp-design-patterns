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
    public class FunctionalBuilder : ExampleBase, IExample
    {
        public FunctionalBuilder()
        {
            SectionName = "Functional Builder";
        }
    
        protected override void ExecuteCode()
        {
            Console.WriteLine("Lets create a functional builder");

            var person = new FunctionalPersonBuilder()
                .Called("Bond, James Bond")
                .Build();

            Console.WriteLine($"Built: {person}");

            //Ok, now extending without violating open/closed principle
            //Extensions!
            person = new FunctionalPersonBuilder()
                .Called("Bond, James Bond")
                .WorksAs("Intelligence")
                .Build();

            Console.WriteLine($"Built: {person}");

            //Generalise! GenericFunctionalBuilder<TSubject, Self>
            person = new EnhancedFunctionalBuilder()
                .Called("Ripley")
                .Build();

            Console.WriteLine($"Built: {person}");
        }
    }

    public sealed class EnhancedFunctionalBuilder : GenericFunctionalBuilder<Person, EnhancedFunctionalBuilder>
    {
        public EnhancedFunctionalBuilder Called(string name)
            => Do(p => p.Name = name);
    }

    //We specify TSelf here as we need to return the implementing type for a fluent interface
    public abstract class GenericFunctionalBuilder<TSubject, TSelf>
        where TSelf : GenericFunctionalBuilder<TSubject, TSelf>
        where TSubject : new() //We need a default constructor 
    {
        //Copied implementation
        //List of mutating functions that changes a person
        private readonly List<Func<TSubject, TSubject>> actions = 
            new List<Func<TSubject, TSubject>>();

        //Public method to add to the action  list
        public TSelf Do(Action<TSubject> action)
            => AddAction(action);

        //Now we expose a method to execute the actions and return the person
        //Aggregate compiles the actions into a single appliction
        //At each step, you have a pair, a person and a function to apply to the person
        public TSubject Build()
            => actions.Aggregate((TSubject)new(), (p, f) => f(p));

        private TSelf AddAction(Action<TSubject> action)
        {
            //Convert this to a function and return it to make it fluent
            actions.Add(p => { action(p); return p; });
            return (TSelf)this;
        }
    }

    public static class ExtensionsFunction 
    {
        public static FunctionalPersonBuilder WorksAs(this FunctionalPersonBuilder builder, string position)
        {
            return builder.Do(p => p.Position = position);
        }
    }

    public sealed class FunctionalPersonBuilder 
    {
        //List of mutating functions that changes a person
        private readonly List<Func<Person, Person>> actions = 
            new List<Func<Person, Person>>();

        //Setting a name
        public FunctionalPersonBuilder Called(string name)
            => Do(p => p.Name = name);

        //Public method to add to the action  list
        public FunctionalPersonBuilder Do(Action<Person> action)
            => AddAction(action);

        //Now we expose a method to execute the actions and return the person
        //Aggregate compiles the actions into a single appliction
        //At each step, you have a pair, a person and a function to apply to the person
        public Person Build()
            => actions.Aggregate(new Person(), (p, f) => f(p));


        private FunctionalPersonBuilder AddAction(Action<Person> action)
        {
            //Convert this to a function and return it to make it fluent
            actions.Add(p => { action(p); return p; });
            return this;
        }
    }
}