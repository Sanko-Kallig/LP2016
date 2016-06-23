// <copyright file="HiringContract.cs" company="Fontys">
//      Copyright (c) Fontyss. All rights reserved.
// </copyright>
// <author>Sander Koch</author>
namespace Administration_Sloepke
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A object that represents a rental contract.
    /// </summary>
    public class HiringContract
    {
        /// <summary>
        /// Gets or sets the database ID of a rental contract.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the start date of a rental contract.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of a rental contract.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the amount of Friese lakes of a rental contract.
        /// </summary>
        public int FrieseCount { get; set; }

        /// <summary>
        /// Gets or sets the employee who helped the customer establish this contract.
        /// </summary>
        public Account Employee { get; set; }

        /// <summary>
        /// Gets or sets customer who rented the equipment.
        /// </summary>
        public Customer Renter { get; set; }

        public List<Product> Products { get; set; }

        public List<IBoat> Boats { get; set; }

        public List<WaterEntity> WaterEntities { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HiringContract"/> class.
        /// </summary>
        /// <param name="id">The database id of a rental contract.</param>
        /// <param name="startDate">The start date of a rental contract.</param>
        /// <param name="endDate">The end date of a rental contract.</param>
        /// <param name="employee">The employee who helped facilitate the customer.</param>
        /// <param name="renter">The customer who rented the equipment.</param>
        public HiringContract(int id, DateTime startDate, DateTime endDate, Account employee, Customer renter)
        {
            this.ID = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Employee = employee;
            this.Renter = renter;
        }

        public override string ToString()
        {
            return this.ID + " - " + this.StartDate.ToString() + " - " + this.Renter.Name;
        }
    }
}
