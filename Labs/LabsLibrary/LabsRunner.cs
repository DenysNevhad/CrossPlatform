using System;
using System.IO;
using Lab1;
using Lab2;
using Lab3;

namespace LabsLibrary
{
    public static class LabsRunner
    {
        public static void RunLab(int labNumber, string inputFile, string outputFile)
        {
            switch (labNumber)
            {
                case 1:
                    Console.WriteLine("Running Lab 1...");
                    Lab1.Program.RunLab(inputFile, outputFile);
                    break;

                case 2:
                    Console.WriteLine("Running Lab 2...");
                    Lab2.Program.RunLab(inputFile, outputFile);
                    break;

                case 3:
                    Console.WriteLine("Running Lab 3...");
                    Lab3.Program.RunLab(inputFile, outputFile);
                    break;

                default:
                    Console.WriteLine("Error: Invalid lab number. Available labs: 1, 2, 3.");
                    break;
            }
        }

        public static (string inputPath, string outputPath) ResolvePaths(string? inputPath, string? outputPath)
        {
            string defaultInput = "INPUT.txt";
            string defaultOutput = "OUTPUT.txt";

            string rootDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

            string resolvedInputPath = !string.IsNullOrEmpty(inputPath)
                ? inputPath
                : Path.Combine(rootDirectory, defaultInput);

            string resolvedOutputPath = !string.IsNullOrEmpty(outputPath)
                ? outputPath
                : Path.Combine(rootDirectory, defaultOutput);

            return (resolvedInputPath, resolvedOutputPath);
        }
    }
}
