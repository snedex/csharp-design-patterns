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
    public class FacetPerson 
    {
        public string Address, Postcode, City;
        public string CompanyName, Position;

        public int Income; 

        public override string ToString()
        {
            return $"Person: {Address}, {Postcode}, {City}, {CompanyName}, {Position}, {Income}";
        }
    }

    public class FacetedBuilder : ExampleBase, IExample
    {
        public FacetedBuilder()
        {
            SectionName = "Faceted Builder";
        }
    
        protected override void ExecuteCode()
        {
            Console.WriteLine("Lets create a faceted builder, initialising properties with separate builders");

            //Lets build our first one with a job!
            var person1 = new FacadePersonBuilder()
                .Works.At("Chocolate Factory")
                    .AsA("Owner")
                    .Earning(100000);
          
            //Add an address builder!
            var person2 = new FacadePersonBuilder()
                .Works.At("United Nations of Earth")
                    .AsA("Secretary General")
                    .Earning(1000000)
                .Lives.At("Luna")
                    .In("Sea of Tranquility")
                    .Postcode("LUNA 1");

            //This is also possible
            //var person3 = new FacadePersonBuilder().Lives.Lives.Lives.Lives
        }
    }

    public class FacadePersonBuilder //Facade 
    {
        //For other builders, keeps a reference to the person being built
        //Allows access to the builder facets

        //This is for reference types, structs would be problematic 
        protected FacetPerson person = new FacetPerson();

        //We pass in the person we have initialised which it also inherits
        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        //You can expose the person like this, not great though
        public static implicit operator FacetPerson(FacadePersonBuilder pb)
        {
            return pb.person;
        }
    }

    //This builds the job details
    public class PersonJobBuilder : FacadePersonBuilder
    {
        public PersonJobBuilder(FacetPerson person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }
        public PersonJobBuilder Earning(int income)
        {
            person.Income = income;
            return this;
        }
    }

    //This builds the address details
    public class PersonAddressBuilder : FacadePersonBuilder
    {
        public PersonAddressBuilder(FacetPerson person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string address)
        {
            person.Address = address;
            return this;
        }

        public PersonAddressBuilder Postcode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }
        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }
}