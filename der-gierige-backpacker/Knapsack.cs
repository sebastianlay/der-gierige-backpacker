using System;
using System.Collections.Generic;
using System.Linq;

namespace der_gierige_backpacker
{
    /// <summary>
    /// Knapsack
    /// </summary>
    static class Knapsack
    {
        /// <summary>
        /// Approximates a solution for the Knapsack problem using a greedy approach
        /// </summary>
        /// <param name="items">A list containing all items</param>
        /// <param name="capacities">A list containing the capacities of the trucks</param>
        /// <returns>A list of containers with the number of the corresponding items</returns>
        public static List<List<int>> Solve(List<Item> items, List<int> capacities)
        {
            List<List<int>> containers = new List<List<int>>();

            foreach (var capacity in capacities)
            {
                var container = Enumerable.Repeat(0, items.Count).ToList();
                FillContainer(ref items, capacity, ref container);
                containers.Add(container);
            }

            return containers;
        }

        /// <summary>
        /// Fills the container with available items
        /// </summary>
        /// <param name="items">A list containing all available items</param>
        /// <param name="capacity">The capacity for the given container</param>
        /// <param name="container">A container with the number of the corresponding items</param>
        private static void FillContainer(ref List<Item> items, int capacity, ref List<int> container)
        {
            while (HasPossibleItems(items, capacity))
                PutItemsInContainer(ref items, ref capacity, ref container);
        }

        /// <summary>
        /// Checks whether there are suitable items left
        /// </summary>
        /// <param name="items">A list containing all available items</param>
        /// <param name="remaining">The remaining weight to fill for the container</param>
        /// <returns>Whether there is at least one suitable item left</returns>
        private static bool HasPossibleItems(List<Item> items, int remaining)
        {
            return items.Any(item => item.Weight <= remaining && item.Units > 0);
        }

        /// <summary>
        /// Find the best item based on the weight/value ratio and move as many units of it into the container as possible
        /// </summary>
        /// <param name="items">A list containing all available items</param>
        /// <param name="remaining">The remaining weight to fill for the container</param>
        /// <param name="container">A container with the number of the corresponding items</param>
        private static void PutItemsInContainer(ref List<Item> items, ref int remaining, ref List<int> container)
        {
            var maxWeight = remaining;

            // determine the item with the best weight/value ratio
            var bestItem = items
                .Where(item => item.Weight <= maxWeight && item.Units > 0)
                .OrderBy(item => item.Weight / item.Value)
                .First();

            // find out how many units of this item we can take
            var bestItemCount = Math.Min(remaining / bestItem.Weight, bestItem.Units);
            
            // reduce the remaining weight
            remaining -= bestItemCount * bestItem.Weight;

            // reduce number of available units for this item
            items.Single(item => item.Name == bestItem.Name).Units -= bestItemCount;

            // add number of units to the container
            container[items.FindIndex(item => item.Name == bestItem.Name)] += bestItemCount;
        }
    }
}
