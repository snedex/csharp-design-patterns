

using System.Reflection;
using Shared;

Console.WriteLine("SOLID Principles Examples\n");
Console.WriteLine("S = Single Responsibility");
Console.WriteLine("O = Open-Closed Principle");
Console.WriteLine("L = Liskov Substitution Principle");
Console.WriteLine("I = Interface Segregation Princple");
Console.WriteLine("D = Dependency Inversion");

foreach(var t in Assembly.GetExecutingAssembly().GetTypes())
{
    if(t.BaseType?.GetInterfaces().Contains(typeof(IExample)) ?? false)
    {
        var method = t.GetMethod("RunExample");
        var instance = Activator.CreateInstance(t);
        Console.WriteLine();
        method.Invoke(instance, null);
    }
}
