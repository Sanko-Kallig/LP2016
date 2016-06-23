// <copyright file="Customer.cs" company="Fontys">
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
    /// The customer who has rented equipment or is renting equipment.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the customer's database ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the customer's Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the customer's name.
        /// </summary>
        public string Name { get; set; }

        public Customer()
        {
            
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="id">The customer's ID</param>
        /// <param name="email">The customer's Email</param>
        /// <param name="name">The customer's name</param>
        public Customer(int id, string email, string name)
        {
            this.ID = id;
            this.Email = email;
            this.Name = name;
        }

        internal bool AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
