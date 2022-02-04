using System;
using static System.Console;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace NameSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> names, sortedNames;
            
            if (args.Length == 0)
            {
                names = EnterNames();
                if (names.Count == 0)
                    WriteLine("No names were entered");
                else
                {
                    sortedNames = SortNames(names);
                    PrintNames(sortedNames);
                    SaveNamesToFile(sortedNames);
                }
            } else
            {
                WriteLine(args[0]);
                names = LoadNamesFromFile(args);
                if (names != null )
                {
                    if (names.Count == 0)
                        WriteLine("File was blank");
                    else
                    {
                        sortedNames = SortNames(names);
                        PrintNames(sortedNames);
                        SaveNamesToFile(sortedNames);
                    }
                }
            }
        }
        static List<string> EnterNames()
        {
            List<string> names = new List<string>();
            string entered = "";
            bool containsInt;
            WriteLine("#################################################################");
            WriteLine("Welcome to the Name Sorter.");
            WriteLine("Please enter names you wish to sort and type 'stop' when done.");
            WriteLine("Please note that given and last names must be separated by a space and any more than three given names will be ignored");
            WriteLine("#################################################################");
            while (entered != "stop")
            {
                Write("Enter a name (or 'stop'): ");
                entered = ReadLine().Trim();
                if (entered != "stop")
                {                    
                    entered = Regex.Replace(entered, " {2,}", " ");
                    if (entered.Contains(" ")) 
                    {
                        if (containsInt = entered.Any(c => char.IsDigit(c)))
                            WriteLine("Incorrect name format. Names cannot contain numbers");
                        else
                            names.Add(entered);
                    } 
                    else
                        WriteLine("Incorrect name format. Name must contain at least 1 last name and given name.");
                }
            }
            return names;
        }
        static List<string> LoadNamesFromFile(string[] args)
        {
            if (File.Exists(args[0]))
            {
                string fileName = Path.GetFileName(args[0]);
                FileStream infile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(infile);
                string recordIn;
                List<string> names = new List<string>();
                recordIn = reader.ReadLine();
                while (recordIn != null)
                {
                    names.Add(recordIn);
                    recordIn = reader.ReadLine();
                }
                reader.Close();
                infile.Close();
                return names;
            } 
            else
            {
                WriteLine("File not found!");
                return null;
            }
        }
        static List<string> SortNames(List<string> names)
        {
            List<string> sortedNames = new List<string>();
            List<Name> namesList = new List<Name>();
            foreach ( string name in names) 
                namesList.Add(new Name(name));
            namesList.Sort(new NameComparer());
            foreach (Name name in namesList)
            {
                sortedNames.Add(name.givenName + " " + name.lastName);
            }
                
            return sortedNames;
        }
        static void PrintNames(List<string> names)
        {
            WriteLine("\n Sorted Names: ");
            WriteLine("-----------------------");
            names.ForEach(WriteLine);
        }
        static void SaveNamesToFile(List<string> names)
        {
            string saveFileName =  "sorted-names-list.txt";
            FileStream outfile = new FileStream(saveFileName, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outfile);
            foreach (string name in names)
                writer.WriteLine(name);
            writer.Close();
            outfile.Close();
        }
        
    }

    class Name
    {
        public string lastName { get; set; }
        public string givenName { get; set; }

        public Name(string name)
        {
            string[] splitName = name.Split(' ');
            string tempGivenName;
            lastName = splitName[splitName.Length - 1];
            tempGivenName = splitName[0];
            if (splitName.Length > 2)
                tempGivenName += " " + splitName[1];
            if (splitName.Length > 3)
                tempGivenName += " " + splitName[2];
            givenName = tempGivenName;
        }
    }

    class NameComparer:IComparer<Name>
    {
        public int Compare(Name a, Name b)
        {
            int lastName = a.lastName.CompareTo(b.lastName);
            if (lastName == 0)
                return a.givenName.CompareTo(b.givenName);
            return lastName;
        }
    }
}
