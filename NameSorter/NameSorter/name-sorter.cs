using System;
using static System.Console;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace NameSorter
{
    //The Sorter interface could be used to sort strings for different purposes
    interface Sorter
    {
        List<string> EnterStrings();
        List<string> LoadFromFile(string[] args);
        List<string> Sort(List<string> names);
        void PrintStrings(List<string> names);
        bool SaveToFile(List<string> names);
    }

    //The NameSorter is an implementation of the Sorter interface
    public class NameSorter : Sorter
    {
        static void Main(string[] args)
        {
            NameSorter nameSorter = new NameSorter();
            List<string> names, sortedNames;
            bool successfulSave = false;
            
            //if no argument is used when the program is executed
            if (args.Length == 0)
            {
                names = nameSorter.EnterStrings();
                //if no names were manually entered
                if (names.Count == 0)
                    WriteLine("No names were entered");
                else
                {
                    //sort, print and save the list of names
                    sortedNames = nameSorter.Sort(names);
                    nameSorter.PrintStrings(sortedNames);
                    successfulSave = nameSorter.SaveToFile(sortedNames);
                }
            } 
            //An argument was used at the program execution (a filename)
            else
            {
                //Loads the names from the file
                names = nameSorter.LoadFromFile(args);
                //Checks if the file was loaded successfully
                if (names != null )
                {
                    if (names.Count == 0)
                        WriteLine("File was blank");
                    else
                    {
                        //sort, print and save the list of names
                        sortedNames = nameSorter.Sort(names);
                        nameSorter.PrintStrings(sortedNames);
                        successfulSave = nameSorter.SaveToFile(sortedNames);
                    }
                }
            }
            //terminates the program based on whether or not the save was successful
            if (successfulSave)
            {
                WriteLine("\nSorted names saved successfully to sorted-names-list.txt");
                WriteLine("The program will now terminate. Press any key to close...");
                ReadKey();
            }
            else
            {
                WriteLine("\nAn error has occured.");
                WriteLine("The program will now terminate. Press any key to close...");
                ReadKey();
            }
        }
        //The EnterStrings method continually prompts the user to enter names and adds them to a list which is returned
        public List<string> EnterStrings()
        {
            List<string> names = new List<string>();
            string entered = "";
            bool containsInt;
            WriteLine("#################################################################");
            WriteLine("Welcome to the Name Sorter.");
            WriteLine("Please enter names you wish to sort and type 'stop' when done.");
            WriteLine("Please note that given and last names must be separated by a space and any more than three given names will be ignored");
            WriteLine("#################################################################");
            //loops until the "stop" keyphrase is used
            while (entered != "stop")
            {
                Write("Enter a name (or 'stop'): ");
                entered = ReadLine().Trim();
                //checks if entered name contains "stop" keyphrase
                if (entered != "stop")
                {                    
                    //removes extra whitespaces from name
                    entered = Regex.Replace(entered, " {2,}", " ");
                    //checks if name entered contains at least a given and last name (separated by a space)
                    if (entered.Contains(" ")) 
                    {
                        //checks if name entered contains a number
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
        //The LoadfromFile method reads the names from the textfile used in the program execution argument and returns them as a list
        public List<string> LoadFromFile(string[] args)
        {
            //Checks if the file exists
            if (File.Exists(args[0]))
            {
                string fileName = Path.GetFileName(args[0]);
                FileStream infile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(infile);
                string recordIn;
                List<string> names = new List<string>();
                recordIn = reader.ReadLine();
                //Adds the records from the stream reader to a list
                while (recordIn != null)
                {
                    names.Add(recordIn);
                    recordIn = reader.ReadLine();
                }
                reader.Close();
                infile.Close();
                return names;
            } 
            //If the file is not found in the directory
            else
            {
                WriteLine("File not found!");
                return null;
            }
        }
        //The Sort method converts the list of names into Name objects and uses and NameComparer IComparer class to sort them alphabetically
        public List<string> Sort(List<string> names)
        {
            List<string> sortedNames = new List<string>();
            List<Name> namesList = new List<Name>();
            //converting the name strings into name objects
            foreach ( string name in names) 
                namesList.Add(new Name(name));
            //Sorting the list using the IComparer class
            namesList.Sort(new NameComparer());
            //Converting the Name objects back into strings
            foreach (Name name in namesList)
            {
                sortedNames.Add(name.givenName + " " + name.lastName);
            }
                
            return sortedNames;
        }
        //The Print Strings method prints the sorted list of names to the console
        public void PrintStrings(List<string> names)
        {
            WriteLine("\nSorted Names: ");
            WriteLine("-----------------------");
            names.ForEach(WriteLine);
        }
        //The SaveToFile method saves the list of sorted names to a file in the directory and returns wether or not it was successful
        public bool SaveToFile(List<string> names)
        {
            //An openended try/catch is used to detect if an error occurs during the saving process and return a false value
            try 
            {
                string saveFileName = "sorted-names-list.txt";
                FileStream outfile = new FileStream(saveFileName, FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(outfile);
                foreach (string name in names)
                    writer.WriteLine(name);
                writer.Close();
                outfile.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    //Name class used in the Sort Method. Consists only of last and given name string variables and a single constructor 
    class Name
    {
        public string lastName { get; set; }
        public string givenName { get; set; }

        //The constructor uses the last part of the string as the last name and all other parts as the given name
        //After using 3 given names, the rest of the name is ignored.
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

    //IComparer class used in the Sort method 
    class NameComparer:IComparer<Name>
    {
        public int Compare(Name a, Name b)
        {
            //compares two last names
            int lastName = a.lastName.CompareTo(b.lastName);
            //if both last names are the same, compare given names
            if (lastName == 0)
                return a.givenName.CompareTo(b.givenName);
            return lastName;
        }
    }
}
