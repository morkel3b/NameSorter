# NameSorter
### Sorting a list of strings and saving to a file
**Language:** C# (.NET Core) 
**Author:** Kelly Green (morkel3b)  
**Date:** 08/02/2022 

**Description**: This is a small console application written in C# which can either be run directly and a list of names manually entered into the console one at a time or can take a text file as a command line argument. The program sorts the list of names, prints the sorted list to the console and saves the list to a file in the directory. Three unit tests have been made for the file loading, sorting, and file saving methods using MSTest and implemented in a Travis.CI testing pipeline.

## Instructions

1.  Clone or download the Github repository to a local location.

2. Navigate to the folder location which contains the name-sorter.exe file in the console (eg. command prompt)

![Screenshot](/docs/images/img1.png)

3. **Manual name entry:** Typing “name-sorter” will start the program with the manual name entry.
    - Names can now be entered manually until the “stop” command is entered.
    - The entered names are sorted, printed to the console, and saved in a file called “sorted-names-list.txt” in the same directory. This file will be overwritten when the program is next run.

![Screenshot](/docs/images/img2.png)

4. **Loading names from a file:** Using the name-sorter command in the console followed by the name of the file will start the program using the automatic name entry. The text file must be in the same directory as the name-sorter.exe file.
    - If successful, the names in the file will be sorted, printed to the console, and saved in the “sorted-names-list.txt” in the same directory. This file will be overwritten when the program is next run.



5. The program self terminates with either an error message or a successful save message. Pressing any key will close the program and return to the command prompt.
