// <copyright file="IBoat.cs" company="Fontys">
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
    /// IBoat interface.
    /// </summary>
    public interface IBoat
    {
        /// <summary>
        /// Gets or sets the name of the Boat.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the price per day of the boat.
        /// </summary>
        double DayPrice { get; set; }
    }
}
