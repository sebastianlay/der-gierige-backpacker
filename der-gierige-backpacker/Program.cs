using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace der_gierige_backpacker
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// A program to solve the bounded knapsack problem
        /// </summary>
        /// <param name="filepath">Path to the CSV file containing the items</param>
        /// <param name="trucks">Comma-separated list of maximum truck capacities (in g)</param>
        static int Main(string filepath = "code_for_bwi.csv", string trucks = "1027600,1014300")
        {
            var items = ReadItemsFromCsvFile(filepath);
            var capacities = ReadCapacitiesFromString(trucks);

            if (items == null || capacities == null)
            {
                Console.WriteLine("The input was invalid. Make sure the path to the csv file is correct.");
                return 1;
            }

            var results = Knapsack.Solve(items, capacities);
            PrintResults(items, capacities, results);

            return 0;
        }

        /// <summary>
        /// Reads the items from a given CSV file
        /// </summary>
        /// <param name="filePath">The path to the file</param>
        /// <returns>A list containing all items</returns>
        private static List<Item> ReadItemsFromCsvFile(string filePath)
        {
            try
            {
                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                return csv.GetRecords<Item>().ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Parses the capacities for the trucks
        /// </summary>
        /// <param name="capacities">A comma-separated list of numbers</param>
        /// <returns>A list containing the parsed numeric values</returns>
        private static List<int> ReadCapacitiesFromString(string capacities)
        {
            try
            {
                return capacities
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(capacity => int.Parse(capacity))
                    .ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Prints the results in a nice table with some ASCII art sprinkled on top
        /// </summary>
        /// <param name="items">A list containing all items</param>
        /// <param name="capacities">A list containing the capacities of the trucks</param>
        /// <param name="shippingLists">A list for each truck with the number of the corresponding items</param>
        private static void PrintResults(List<Item> items, List<int> capacities, List<List<int>> shippingLists)
        {
            var topRow =    "╔══════════════════════════════════════╦══════════════════════════════════════╗";
            var emptyRow =  "║                                      ║                                      ║";
            var middleRow = "╠══════════════════════════════════════╬══════════════════════════════════════╣";
            var bottomRow = "╚══════════════════════════════════════╩══════════════════════════════════════╝";

            var truckArt = new List<string>() {
                "",
                "        ____________________",
                "  ___  |                    |",
                " /_| | |     get in {IT}    |",
                "|    |_|__________________  |",
               "\"-O----O-O' `         `O`O'-'"
            };

            Console.WriteLine(topRow);

            for (int index = 0; index < shippingLists.Count; index++)
            {
                var shippingList = shippingLists[index];
                var truckValue = 0;
                var truckWeight = 0;

                var truckInfo = new List<string>();
                truckInfo.Add(" Truck #" + (index + 1));
                truckInfo.Add("");

                for (int i = 0; i < shippingList.Count; i++)
                {
                    if(shippingList[i] > 0)
                    {
                        var name = items[i].Name.PadRight(26);
                        var units = ("x " + shippingList[i].ToString()).PadLeft(6);
                        truckInfo.Add("  " + name + units);

                        truckValue += shippingList[i] * items[i].Value;
                        truckWeight += shippingList[i] * items[i].Weight;
                    }
                }

                var usedCapacity = Math.Round(truckWeight / 1000.0, 2);
                var maxCapacity = Math.Round(capacities[index] / 1000.0, 2);

                truckInfo.Add("");
                truckInfo.Add(" Value:  " + truckValue);
                truckInfo.Add(" Weight: " + usedCapacity + " / " + maxCapacity + " kg");

                Console.WriteLine(emptyRow);
                for (int i = 0; i < Math.Max(truckArt.Count, truckInfo.Count); i++)
                {
                    var line = emptyRow;
                    if (i < truckArt.Count)
                        line = Combine(line, truckArt[i], 5);
                    if (i < truckInfo.Count)
                        line = Combine(line, truckInfo[i], 41);
                    Console.WriteLine(line);
                }
                Console.WriteLine(emptyRow);

                if (index < shippingLists.Count - 1)
                    Console.WriteLine(middleRow);
            }

            Console.WriteLine(bottomRow);

            // overrides the first string with the second one, starting at the specified index
            static string Combine(string original, string overlay, int index)
            {
                return original.Remove(index, overlay.Length).Insert(index, overlay);
            }
        }
    }
}
