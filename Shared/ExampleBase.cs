namespace Shared;
public abstract class ExampleBase : IExample
{
    public string SectionName { get; set; } = "Undefined";

    public void RunExample() {
        Console.WriteLine($"== {SectionName} ==\n");
        this.ExecuteCode();
    }

    protected abstract void ExecuteCode();

}
