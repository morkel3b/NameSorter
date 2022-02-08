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
            //Tests the LoadFromFile method by loading a short list of names and comparing it to the expected list.
            //arrange
            string[] fileName = new string[] { "unsorted-names-list.txt" };
            NameSorter.NameSorter nameSorter = new NameSorter.NameSorter();
            List<string> expected = new List<string>
            {
                "Janet Parsons", "Vaughn Lewis", "Adonis Julius Archer", "Shelby Nathan Yoder", "Marin Alvarez",
                "London Lindsey", "Beau Tristan Bentley", "Leo Gardner", "Hunter Uriah Mathew Clarke", "Mikayla Lopez",
                "Frankie Conner Ritter"
            };
            //act
            List<string> loadedNames = nameSorter.LoadFromFile(fileName);
            //assert
            CollectionAssert.AreEqual(expected, loadedNames);
        }
        [TestMethod]
        public void Test_Sort()
        {
            //Tests the Sort method by providing it an unsorted list and comparing it to the expected sorted list
            //arrange
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
            //act
            List<string> sortedNames = nameSorter.Sort(names);
            //assert
            CollectionAssert.AreEqual(expected, sortedNames);
        }
        [TestMethod]
        public void Test_SaveToFile()
        {
            //Tests the SaveToFile method by providing it a sorted name list and comparing the returned boolean value
            //arrange
            NameSorter.NameSorter nameSorter = new NameSorter.NameSorter();
            List<string> sortedNames = new List<string>
            {
                "Marin Alvarez", "Adonis Julius Archer", "Beau Tristan Bentley", "Hunter Uriah Mathew Clarke",
                "Leo Gardner", "Vaughn Lewis", "London Lindsey", "Mikayla Lopez", "Janet Parsons","Frankie Conner Ritter",
                "Shelby Nathan Yoder"
            };
            bool expected = true;
            //act
            bool result = nameSorter.SaveToFile(sortedNames);
            //assert
            Assert.AreEqual(expected, result);
        }
    }
}
