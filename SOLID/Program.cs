

using System.Reflection;
using Shared;

Console.WriteLine("SOLID Principles Examples\n");

foreach(var t in Assembly.GetExecutingAssembly().GetTypes())
{
    if(t.BaseType.GetInterfaces().Contains(typeof(IExample)))
    {
        var method = t.GetMethod("RunExample");
        var instance = Activator.CreateInstance(t);
        method.Invoke(instance, null);
    }
}
