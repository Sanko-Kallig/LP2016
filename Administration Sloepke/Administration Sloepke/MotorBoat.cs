// <copyright file="MotorBoat.cs" company="Fontys">
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
    /// A motorized boat used for rental.
    /// </summary>
    public class MotorBoat
    {
        /// <summary>
        /// Gets or sets what kind of motorized boat this is.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the fuel capacity of this motorized boat.
        /// </summary>
        public double FuelCapacity { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MotorBoat"/> class.
        /// </summary>
        /// <param name="type">The type of motorized boat.</param>
        /// <param name="fuelCapacity">The fuel capacity of this motorized boat.</param>
        public MotorBoat(string type, double fuelCapacity)
        {
            this.Type = type;
            this.FuelCapacity = fuelCapacity;
        }
    }
}
