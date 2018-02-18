using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stats
{
    class Program
    {
        private static string _option;
        private static string _filePath;
        
        static void Main(string[] inputs)
        {
            if (inputs.Any())
            {
                _option = GetUserOption(inputs);

                switch (_option.ToUpper())
                {
                    case "RECORD":
                        _filePath = GetUserFilePath(inputs);
                        Record(_filePath, GetDecimalValues(inputs));
                        break;
                    case "SUMMARY":
                        _filePath = GetUserFilePath(inputs);
                        Summary(_filePath);
                        break;
                    case "HELP":
                        Help();
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        Help();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid command!");
            }        
        }

        /// <summary>
        /// Insert a list decimal values to a exsisting txt file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <param name="data">List of decimal values</param>
        public static void Record(string filePath, List<Decimal> data)
        {
            try
            {
                if (!CheckFile(filePath, ".txt"))
                {
                    throw new FileNotFoundException("File does not exsist or ensure the file is type of .txt extension");
                }

                if (data == null)
                {
                    Console.WriteLine("Please enter valid numerical values! example 12.1, 6, 25.1");
                    return;
                }
                else
                {
                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        foreach (decimal val in data)
                            writer.WriteLine(val);
                    }
                    Console.WriteLine("{0} new record inserted!", data.Count());
                } 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Method to Print Average, Max & Total records in data file
        /// and summarise data into a table format.
        /// </summary>
        public static void Summary(string filePath)
        {
            string data;
            StringBuilder table = new StringBuilder();
            List<Decimal> list = new List<Decimal>();

            try
            {
                if (!CheckFile(filePath, ".txt"))
                {
                    throw new FileNotFoundException("File does not exsist or ensure the file is type of .txt extension");
                }

                //Pass the file path and file name to the StreamReader constructor
                using (StreamReader file = new StreamReader(filePath))
                {
                    while ((data = file.ReadLine()) != null)
                    {
                        list.Add(Convert.ToDecimal(data));
                    }

                    if (list.Count == 0)
                    {
                        Console.WriteLine("No data");
                        return;
                    }
                    else
                    {
                        table.AppendLine("┌--------------------┐");
                        table.AppendLine("|# of Entries |" + list.Count().ToString().PadRight(6) + "|");
                        table.AppendLine("|Min. value   |" + list.Min().ToString().PadRight(6) + "|");
                        table.AppendLine("|Max. value   |" + list.Max().ToString().PadRight(6) + "|");
                        //Round Average to 1 decimal place
                        table.AppendLine("|Avg. value   |" + list.Average().ToString("0.0").PadRight(6) + "|");
                        table.AppendLine("└--------------------┘");
                        Console.WriteLine(table.ToString());
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        /// <summary>
        /// Method Prints software usage 
        /// </summary>
        public static void Help()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Usage:");
            sb.AppendLine("HELP ".PadLeft(10) + "  Display software usage");
            sb.AppendLine("RECORD ".PadLeft(10) + "  Insert data to txt file e.g RECORD + FILEPATH + VALUES");
            sb.AppendLine("SUMMARY ".PadLeft(10) + "  Display summarise data in txt file e.g SUMMARY + FILEPATH");
            sb.AppendLine("QUIT ".PadLeft(10) + "  EXIT APPLICATION");
            Console.WriteLine(sb);
        }

        /// <summary>
        /// Function to return user option from string input
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns>user option</returns>
        public static string GetUserOption(string[] userInput)
        {
            string option = userInput[0];
            return option;
        }

        /// <summary>
        /// Function to return user file path from string input
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns>Filepath to file</returns>
        public static string GetUserFilePath(string[] userInput)
        {
            string filePath = userInput.Length > 1 ? userInput[1] : "";
            return filePath;
        }

        /// <summary>
        /// Function to validate supplied filepath and extension
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <param name="extension">extension type</param>
        /// <returns>true or false base on condition</returns>
        public static bool CheckFile(string filePath, string extension)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine(@"Please enter a valid file path e.g C:\Users\Dennis\Desktop\test.txt");
            }
            if (File.Exists(filePath) && Path.GetExtension(filePath) == extension)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Function to retrieve decimal value from user inputs
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns>List of decimal numbers</returns>
        public static List<Decimal> GetDecimalValues(string[] userInput)
        {
            string[] strArray = userInput.Skip(2).ToArray();
            List<Decimal> list = new List<Decimal>();
            decimal number;
   
            foreach(string value in strArray)
            {
                if (Decimal.TryParse(value, out number)){
                    list.Add(Convert.ToDecimal(number));
                }
                else
                {
                    //Console.WriteLine("Please enter numeric values e.g 1.2 10 88");
                    return null;
                }
            }
            return list;
        }

    }
}
