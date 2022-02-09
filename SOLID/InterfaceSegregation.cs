using Shared;

namespace SOLID
{
    public class InterfaceSegregation : ExampleBase
    {
        public InterfaceSegregation()
        {
            SectionName = "Interface Segregation Principle";
        }

        protected override void ExecuteCode()
        {
            //Implementing an interface for printers, single and multifunction, IMachine.
            Console.WriteLine("Similar to single responsibility, only have interfaces for what you need, not a monolith");
            Console.WriteLine("Synthetic code example");
        }
    }

    //We can also delegate implementations
    public class MultiFunctionMachine : IMultifunctionDevice
    {
        private readonly IPrinter printer;
        private readonly IScanner scanner;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner)
        {
            this.printer = printer;
            this.scanner = scanner;
        }
        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }
    }

    //So we split this out into atomic interfaces or we can have higher order interfaces, like IMultifunctionDevice
    public interface IMultifunctionDevice : IPrinter, IScanner 
    {
        //Other implementation here 
    }

    public interface IPrinter 
    {
        public void Print(Document d);
    }

    public interface IScanner 
    {
        public void Scan(Document d);
    }

    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    public class Document 
    {

    }

    //this isn't great as we only implement one of these functions and we have a monolith interface.
    public class OldFashionedPrinter : IMachine
    {
        public void Fax(Document d)
        {
            throw new NotImplementedException();
        }

        public void Print(Document d)
        {
            //We can only do this.
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    //This is ok as this printer supports everything
    public class MultifunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    public interface IMachine 
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }
}