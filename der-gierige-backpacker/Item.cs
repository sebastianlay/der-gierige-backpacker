using CsvHelper.Configuration.Attributes;
using System;

namespace der_gierige_backpacker
{
    /// <summary>
    /// Item
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Name (has to be unique)
        /// </summary>
        [Index(0)]
        public string Name { get; set; }

        /// <summary>
        /// Number of available units
        /// </summary>
        [Index(1)]
        public int Units { get; set; }

        /// <summary>
        /// Weight per unit (in g)
        /// </summary>
        [Index(2)]
        public int Weight { get; set; }

        /// <summary>
        /// Value per unit
        /// </summary>
        [Index(3)]
        public int Value { get; set; }
    }
}
