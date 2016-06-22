// <copyright file="Account.cs" company="Fontys">
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
    /// A employee's account.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the account's database ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the account's Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the account's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <param name="id">The account's ID</param>
        /// <param name="email">The account's Email</param>
        /// <param name="name">The account's name</param>
        public Account(int id, string email, string name)
        {
            this.ID = id;
            this.Email = email;
            this.Name = name;
        }
    }
}
