using Shared;

namespace SOLID
{
    public class SingleResponsibility : ExampleBase
    {
        public SingleResponsibility() : base()
        {
            SectionName = "Single Responsibility Principle";
        }
        protected override void ExecuteCode()
        {
            var j = new Journal();
            var p = new JournalPersistience();

            j.AddEntry("I ate a bug");
            j.AddEntry("I am a leaf on the wind");
            j.AddEntry("My ship don't crash");

            p.SaveToFile(j, "journal.txt", true);

            Console.WriteLine(j.ToString());
        }
    }

    public class Journal {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        //The following methods are violating the single responsibility principle
        //Journal is now responsible for more than just keeping journal entires
        //Which means you will have to change it more often
        //Better approach is to have a separate Persistience class
        #region SRP violation

        private void Save(string fileName)
        {
            File.WriteAllText(fileName, ToString());
        }

        private static Journal Load(string file)
        {
            return new Journal();
        }

        private void Load(Uri uri)
        {

        }

        #endregion
    }

    //This class separates out the concern of the load and saving of the journal instead of baking it in to the journal class
    //this means if we need to change the persistience from a disk to a DB, we only need modify this class and logic
    public class JournalPersistience {

        public void SaveToFile(Journal j, string fileName, bool overwrite = true)
        {
            if(overwrite || !File.Exists(fileName))
                File.WriteAllText(fileName, j.ToString());
        }
    }
}