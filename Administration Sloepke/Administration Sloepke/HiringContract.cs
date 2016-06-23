// <copyright file="HiringContract.cs" company="Fontys">
//      Copyright (c) Fontyss. All rights reserved.
// </copyright>
// <author>Sander Koch</author>

using System.IO;
using System.Runtime.InteropServices;

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

        public HiringContract()
        {
            Boats = new List<IBoat>();
            WaterEntities = new List<WaterEntity>();
            Products = new List<Product>();
        }

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
            Boats = new List<IBoat>();
            WaterEntities = new List<WaterEntity>();
            Products = new List<Product>();
        }

        /// <summary>
        /// Used to calculate how many lakes you can visit according to this budget.
        /// </summary>
        /// <param name="budget">The amount of money you have to spend.</param>
        /// <returns>Returns the total price.</returns>
        public double CalculateBudget(double budget)
        {
            int days = EndDate.Day - StartDate.Day;
            double totalprice = 0;
            double leftover = 0;
            if (Products != null)
            {
                foreach (Product p in Products)
                {
                    totalprice += (p.Price * days);
                }
            }

            foreach (IBoat b in Boats.Where(b => b is MuscleBoat))
            {
                totalprice += (10*days);
            }
            foreach (IBoat b in Boats.Where(b => b is MotorBoat))
            {
                totalprice += (15*days);
            }
            if (WaterEntities != null)
            {
                foreach (WaterEntity w in WaterEntities.Where(b => b.Name != null))
                {
                    totalprice += (2 * days);
                }
            }

            leftover = budget - totalprice;

            if (leftover < 0)
            {
                throw new Exception("Zit over het budget heen.");
            }
            else
            {
                if((int)Math.Floor(leftover / (1 * days)) <= 5 && (int)Math.Floor(leftover / (1 * days)) > 0)
                {

                        FrieseCount = (int)Math.Floor(leftover / (1 * days));
                        totalprice += (FrieseCount * 1) * days;
                }
                else if ((int) Math.Floor(leftover/(1.5*days)) > 0)
                {
                    if ((int)Math.Floor(leftover / (1.5 * days)) > 12)
                    {
                        FrieseCount = 12;
                        totalprice += (FrieseCount * 1.5) * days;
                    }
                    else
                    {
                        FrieseCount = (int)Math.Floor(leftover / (1.5 * days));
                        totalprice += (FrieseCount * 1.5) * days;
                    }
                }
                else
                {
                    throw new Exception("Kan geen friese meren op met dit budget");
                }
            }
            return totalprice;
        }

        /// <summary>
        /// Calculates the total price of an hiring contract.
        /// </summary>
        /// <returns>The total price of the hiring contract.</returns>
        public double TotalPrice()
        {
            int days = EndDate.Day - StartDate.Day;
            double totalprice = 0;
            double leftover = 0;
            if (Products != null)
            {
                foreach (Product p in Products)
                {
                    totalprice += (p.Price * days);
                }
            }

            foreach (IBoat b in Boats.Where(b => b is MuscleBoat))
            {
                totalprice += (10 * days);
            }
            foreach (IBoat b in Boats.Where(b => b is MotorBoat))
            {
                totalprice += (15 * days);
            }
            if (WaterEntities != null)
            {
                foreach (WaterEntity w in WaterEntities.Where(b => b.Name != null))
                {
                    totalprice += (2 * days);
                }
            }
            if (FrieseCount > 0 && FrieseCount <= 5)
            {
                totalprice += FrieseCount*days;
            }
            if (FrieseCount > 0 && FrieseCount > 5)
            {
                totalprice += (FrieseCount*1.5)*days;
            }
            return totalprice;
        }

        /// <summary>
        /// Gets all the motor boats out of the generic list of boats.
        /// </summary>
        /// <returns>A list of motor boats.</returns>
        public List<MotorBoat> GetMotorBoats()
        {
            List<MotorBoat> returnMotorBoats = new List<MotorBoat>();
            foreach (IBoat b in Boats.Where(b => b is MotorBoat))
            {
                returnMotorBoats.Add(b as MotorBoat);
            }
            return returnMotorBoats;
        }


        /// <summary>
        /// Gets all the muscle boats out of the generic list of boats.
        /// </summary>
        /// <returns>A list of muscle boats.</returns>
        public List<MuscleBoat> GetMuscleBoats()
        {
            List<MuscleBoat> returnMuscleBoats = new List<MuscleBoat>();
            foreach (IBoat b in Boats.Where(b => b is MuscleBoat))
            {
                returnMuscleBoats.Add(b as MuscleBoat);
            }
            return returnMuscleBoats;
        }

        /// <summary>
        /// Exports the rental contract to a text file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="html">If it needs to be an html export or not.</param>
        public void ExportHiringContract(string path, bool html = false)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine("Huur contract 'T Sloepke");
            writer.WriteLine("Klant: " + Renter.Name);
            writer.WriteLine("Geholpen door: ", Employee.Name);

            writer.WriteLine("Verhuurde boten: ");

            foreach (MuscleBoat m in GetMuscleBoats())
            {
                writer.Write(m.ToString());
            }
            foreach (MotorBoat m in GetMotorBoats())
            {
                writer.Write(m.ToString());
            }

            writer.WriteLine("Verhuurde producten: ");

            foreach (Product p in Products)
            {
                writer.WriteLine(p.ToString());
            }

            writer.WriteLine("Aantal Friesse meren: " + FrieseCount.ToString());
            writer.WriteLine("Dag prijs Friese meren: 1");
            writer.WriteLine("Indien meer dan 5 Friese meren worden bezocht is de dag prijs 1.5");
            writer.WriteLine("Andere waterlichaamen: ");
            foreach (WaterEntity w in WaterEntities.Where(w => w.Name != null))
            {
                writer.WriteLine("Naam: ", w.Name);
                writer.WriteLine("Dag prijs: 2");
            }

            writer.WriteLine("Totaal prijs: " + TotalPrice());
            writer.Close();
        }

        public override string ToString()
        {
            return this.ID + " - " + this.StartDate.ToString() + " - " + this.Renter.Name;
        }
    }
}
