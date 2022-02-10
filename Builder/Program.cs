using System.Reflection;
using Shared;


Console.WriteLine("Gamma Categorisation of Patterns:");
Console.WriteLine("\t1. Creational Patterns");
Console.WriteLine("\t2. Structural Patterns");
Console.WriteLine("\t3. Behavioural Patterns\n");

Console.WriteLine("Builder Pattern:\n");

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
