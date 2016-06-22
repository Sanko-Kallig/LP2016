// <copyright file="Boat.cs" company="Fontys">
//      Copyright (c) Fontyss. All rights reserved.
// </copyright>
// <author>Sander Koch</author>
namespace Administration_Sloepke
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A boat used for rental purposes.
    /// </summary>
    public class Boat : IBoat
    {
        /// <summary>
        /// Gets or sets the name of the Boat.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price per day of the boat.
        /// </summary>
        public double DayPrice { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Boat"/> class.
        /// </summary>
        /// <param name="name">The name of the boat.</param>
        /// <param name="dayPrice">The price per day of the boat.</param>
        public Boat(string name, double dayPrice)
        {
            this.Name = name;
            this.DayPrice = dayPrice;
        }
    }
}
