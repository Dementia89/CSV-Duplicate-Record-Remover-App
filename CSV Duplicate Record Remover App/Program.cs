using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSV_Duplicate_Record_Remover_App
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("Please input file path: ");
            //string filePath = Console.ReadLine();
            string filePath = @"DuplicateRemoverSampleInput.csv";

            try
            {
                List<string> values = new List<string>();

                // Try reading from the filepath specified.
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while (!sr.EndOfStream)
                        {
                            bool duplicate = false;
                            line = sr.ReadLine();
                            foreach (string s in values)
                            {
                                if (line == s)
                                {
                                    duplicate = true;
                                }
                            }
                            if (!duplicate)
                            {
                                values.Add(line);
                            }
                        }
                        sr.Close();
                    }
                    Console.WriteLine("Successfully read " + filePath + "!");
                }
                catch
                {
                    Console.WriteLine("Stream Reader Section Failed");
                }

                // Try and write the file to the path specified earlier but a new file.
                try
                {
                    using (StreamWriter sw = new StreamWriter(RewriteOutputFile(filePath)))
                    {
                        foreach (string s in values)
                        {
                            sw.Write(s);
                            sw.WriteLine();
                        }
                    }

                    Console.WriteLine("output.csv is now located in the same folder as " + filePath);
                }
                catch
                {
                    Console.WriteLine("Stream Writer Section Failed.");
                }
            }
            catch
            {
                Console.WriteLine("Something went wrong. Bad file path?");
            }

            Console.WriteLine("Program complete!");
            Console.Read();
            
        }

        /// <summary>
        /// Helper method to change the output location. Could have hard coded
        /// but I added it so that if we allow user input file-paths, it would
        /// be easier to re-write.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        static string RewriteOutputFile(string filePath)
        {
            string[] splitFilePath = filePath.Split('\\');
            string newFilePath = "output.csv";

            // Check if we are in a different folder structure or the root location.
            // If it is root, we bypass this, if not, it rewrites the end of it.
            if (splitFilePath.Length > 1)
            {
                newFilePath = splitFilePath[0];
                splitFilePath[splitFilePath.Length - 1] = "output.csv";

                for (int i = 1; i < splitFilePath.Length; i++)
                {
                    newFilePath += "\\" + splitFilePath[i];
                }
            }

            return newFilePath;
        }
    }
}
