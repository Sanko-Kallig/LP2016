// <copyright file="Product.cs" company="Fontys">
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
    /// A product used for rental purposes.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the ID from the database.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="id">The database ID of the product.</param>
        /// <param name="name">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        public Product(int id, string name, double price)
        {
            this.ID = id;
            this.Name = name;
            this.Price = price;
        }
    }
}
