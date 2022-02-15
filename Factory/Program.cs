using System.Reflection;
using Shared;

Console.WriteLine("Factory Patterns:\n");

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
