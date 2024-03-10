using wfcp;
using System;
using System.Diagnostics;

namespace ExampleProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Enter the parent program where the program will have child programs
            // You can just make your own, just use Process.Start() to external dummy program
            Console.Write("Enter parent program directory:\n> ");
            string parentDirectory = Console.ReadLine().Trim('"');

            Process parentProcess = new Process();
            parentProcess.StartInfo.FileName = parentDirectory;
            parentProcess.StartInfo.WorkingDirectory = parentDirectory;

            WaitForProcess.LaunchAndWait(parentProcess);
            Console.ReadLine();
        }
    }
}
