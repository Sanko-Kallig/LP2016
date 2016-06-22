// <copyright file="WaterEntity.cs" company="Fontys">
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
    /// A body of water used in the rental contract.
    /// </summary>
    public class WaterEntity
    {
        /// <summary>
        /// Gets or sets the ID of a water entity.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of a water entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaterEntity"/> class.
        /// Used for unnamed bodies of water.
        /// </summary>
        /// <param name="id">The water entity's database id.</param>
        public WaterEntity(int id)
        {
            this.ID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaterEntity"/> class.
        /// </summary>
        /// <param name="id">The water entity's database id.</param>
        /// <param name="name">The name of a water entity, if it is named.</param>
        public WaterEntity(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
