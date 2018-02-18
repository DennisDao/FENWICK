using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FENWICK
{
    class Program
    {
        private static string _option;
        private static string _filePath;
        private static bool isContinue = true;

        static void Main(string[] args)
        {
            Console.WriteLine("-----Welcome To Fenwick software-----");
            Console.WriteLine("-----Enter Help to view usage-----");

            while (isContinue)
            {
                Console.WriteLine("Type a command:");
                string inputs = Console.ReadLine();
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
                    case "QUIT":
                        Help();
                        isContinue = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid command, type help to view usage");
                        break;
                }
            }
        }

        /// <summary>
        /// Insert a list decimal values to a exsisting txt file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <param name="data">List of decimal values</param>
        public static void Record(string filePath, List<Decimal> data)
        {
            if (data == null)
            {
                Console.WriteLine("Please enter valid numerical values! example 12.1, 6, 25.1");
                return;
            }
             
            try
            {
                bool isExist = File.Exists(filePath);
                if (isExist)
                {
                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        foreach (decimal val in data)
                            writer.WriteLine(val);
                    }
                    Console.WriteLine("{0} new record inserted!", data.Count());
                }
                else
                {
                    throw new FileNotFoundException("File does not exsist or check the file extension");
                }          
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Print Average, Max & Total records in data file in a table format.
        /// </summary>
        public static void Summary(string filePath)
        {
            string data;
            StringBuilder table = new StringBuilder();
            List<Decimal> list = new List<Decimal>();
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                using (StreamReader file = new StreamReader(filePath))
                {
                    while ((data = file.ReadLine()) != null)
                    {
                        list.Add(Convert.ToDecimal(data));
                    }

                    if (list == null)
                    {
                        Console.WriteLine("No data");
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
                Console.WriteLine("File does not exsist or check the file extension");
            }
           
        }

        /// <summary>
        /// Prints software usage 
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
        public static string GetUserOption(string userInput)
        {
            //Add a space to input as user can type in help only
            string option = userInput.Split()[0];
            return option;
        }

        /// <summary>
        /// Function to return user file path from string input
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns>Filepath to file</returns>
        public static string GetUserFilePath(string userInput)
        {
            string filePath = userInput.Split(' ')[1];
            return filePath;
        }

        /// <summary>
        /// Function to retrieve decimal value from user inputs
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns>List of decimal numbers</returns>
        public static List<Decimal> GetDecimalValues(string userInput)
        {
            string[] strArray = userInput.Split().Skip(2).ToArray();
            List<Decimal> list = new List<Decimal>();
            decimal number;
            int i = 0;

            foreach(string value in strArray)
            {
                if (Decimal.TryParse(value, out number)){
                    list.Add(Convert.ToDecimal(value));
                }
                else
                {
                    return null;
                }
            }
            return list;
        }

    }
}
