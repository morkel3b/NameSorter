using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace SorterTests
{
    [TestClass]
    public class NameSorterTests
    {
        [TestMethod]
        public void Test_LoadFromFile()
        {
            string[] fileName = new string[] { "unsorted-names-list.txt" };
            NameSorter.NameSorter nameSorter = new NameSorter.NameSorter();
            List<string> expected = new List<string>
            {
                "Janet Parsons", "Vaughn Lewis", "Adonis Julius Archer", "Shelby Nathan Yoder", "Marin Alvarez",
                "London Lindsey", "Beau Tristan Bentley", "Leo Gardner", "Hunter Uriah Mathew Clarke", "Mikayla Lopez",
                "Frankie Conner Ritter"
            };
            List<string> loadedNames = nameSorter.LoadFromFile(fileName);
            CollectionAssert.AreEqual(expected, loadedNames);
        }
        [TestMethod]
        public void Test_Sort()
        {
            NameSorter.NameSorter nameSorter = new NameSorter.NameSorter();
            List<string> names = new List<string>
            {
                "Janet Parsons", "Vaughn Lewis", "Adonis Julius Archer", "Shelby Nathan Yoder", "Marin Alvarez",
                "London Lindsey", "Beau Tristan Bentley", "Leo Gardner", "Hunter Uriah Mathew Clarke", "Mikayla Lopez",
                "Frankie Conner Ritter"
            };
            List<string> expected = new List<string>
            {
                "Marin Alvarez", "Adonis Julius Archer", "Beau Tristan Bentley", "Hunter Uriah Mathew Clarke",
                "Leo Gardner", "Vaughn Lewis", "London Lindsey", "Mikayla Lopez", "Janet Parsons","Frankie Conner Ritter",
                "Shelby Nathan Yoder"
            };
            List<string> sortedNames = nameSorter.Sort(names);
            CollectionAssert.AreEqual(expected, sortedNames);
        }
        [TestMethod]
        public void Test_SaveToFile()
        {
            NameSorter.NameSorter nameSorter = new NameSorter.NameSorter();
            List<string> sortedNames = new List<string>
            {
                "Marin Alvarez", "Adonis Julius Archer", "Beau Tristan Bentley", "Hunter Uriah Mathew Clarke",
                "Leo Gardner", "Vaughn Lewis", "London Lindsey", "Mikayla Lopez", "Janet Parsons","Frankie Conner Ritter",
                "Shelby Nathan Yoder"
            };
            bool expected = true;
            bool result = nameSorter.SaveToFile(sortedNames);
            Assert.AreEqual(expected, result);
        }
    }
}
