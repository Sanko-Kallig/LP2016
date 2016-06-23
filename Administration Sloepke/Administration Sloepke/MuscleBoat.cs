// <copyright file="MuscleBoat.cs" company="Fontys">
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
    /// A boat powered by the power of muscles.
    /// </summary>
    public class MuscleBoat : Boat
    {
        /// <summary>
        /// Gets or sets the type of boat
        /// </summary>
        public string Type { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="MuscleBoat"/> class.
        /// </summary>
        /// <param name="type"></param>
        public MuscleBoat(string name, double dayPrice, string type) : base(name, dayPrice)
        {
            this.Type = type;
        }

        public override string ToString()
        {
            return base.ToString() + ": " + Type;
        }
    }
}
