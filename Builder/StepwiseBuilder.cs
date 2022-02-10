using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.BasicBuilder;
using Shared;

namespace Builder
{
    public class StepwiseBuilder : ExampleBase, IExample
    {
        public StepwiseBuilder()
        {
            SectionName = "Stepwise Builder";
        }
    
        protected override void ExecuteCode()
        {
            Console.WriteLine("Lets create a builder for the car that restricts wheel size depending on type");

            var car = CarBuilder.Create()
                .OfType(CarType.Sedan)
                .WithWheels(17)
                .Build();

            Console.WriteLine(car);
            try
            {
                car = CarBuilder.Create()
                    .OfType(CarType.Crossover)
                    .WithWheels(25)
                    .Build();

                Console.WriteLine(car);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Not a valid configuration");
            }
            
        }

        public class Car {
            
            public CarType Type;

            public int WheelSize;

            public override string ToString()
            {
                return $"Car {this.Type} with wheels {this.WheelSize}\"";
            }
        }

        public enum CarType 
        {
            Sedan,
            Crossover
        }

        //Define the interfaces for the builder functions using interface segregation
        public interface ISpecifyCarType
        {
            ISpecifyWheelSize OfType(CarType type);
        }

        public interface ISpecifyWheelSize
        {
            IBuildCar WithWheels(int size);
        }

        public interface IBuildCar
        {
            public Car Build();
        }

        //Lets build the builder
        public class CarBuilder
        {
            //We encapsulate the implementation internally with the interfaces to prevent exposing methods that could be invoked incorrectly.
            private class Impl : IBuildCar, ISpecifyCarType, ISpecifyWheelSize
            {
                //This object we are building up
                private Car car = new Car();

                public Car Build()
                {
                    return car;
                }

                public ISpecifyWheelSize OfType(CarType type)
                {
                    car.Type = type;
                    return this; //segmented this
                }

                //Now we can do  some validation
                public IBuildCar WithWheels(int size)
                {
                    switch (car.Type)
                    {
                        case CarType.Crossover when size < 17 || size > 20:
                        case CarType.Sedan when size < 15 || size > 17:
                            throw new ArgumentException($"Wrong size of wheel for {car.Type}");
                    }
                    car.WheelSize = size;
                    return this; //This is now an IBuildCar
                }
            }

            public static ISpecifyCarType Create()
            {
                //So how do we stop calling the build methods out of order?
                //The Impl method return types from each call allow you to specify the step order!
                return new Impl();
            }
        }
    }
}