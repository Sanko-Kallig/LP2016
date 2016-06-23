// <copyright file="Administration.cs" company="Fontys">
//      Copyright (c) Fontyss. All rights reserved.
// </copyright>
// <author>Sander Koch</author>

using System.IO;
using System.Text.RegularExpressions;

namespace Administration_Sloepke
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// An administration class used to communicate between the GUI code and models.
    /// </summary>
    public class Administration
    {
        /// <summary>
        /// Gets or sets a list of products from the database.
        /// </summary>
        public List<Product> Products { get; set; }


        /// <summary>
        /// Gets or sets a list of constracts from the database.
        /// </summary>
        public List<HiringContract> Constracts { get; set; }

        /// <summary>
        /// Gets or sets a list of boats from the database
        /// </summary>
        public List<IBoat> Boats { get; set; }

        public List<WaterEntity> WaterEntities { get; set; }

        public List<MotorBoat> TempMotorBoats { get; set; }

        public List<MuscleBoat> TempMuscleBoats { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Administration"/> class.
        /// </summary>
        public Administration()
        {
        }

        /// <summary>
        /// Gets an account from the Database manager, by using the email and password.
        /// </summary>
        /// <param name="Email">Account email.</param>
        /// <param name="Password">Account password</param>
        /// <returns></returns>
        public Account GetAccount(string email, string password)
        {
            return DatabaseManager.GetAccount(email, password);
        }

        public List<HiringContract> GetHiringContracts()
        {
            return DatabaseManager.GetHiringContracts();
        }

        public List<Product> GetProducts()
        {
            return DatabaseManager.GetProducts();
        }

        public List<IBoat> GetBoats()
        {
            return DatabaseManager.GetBoats();
        }

        public List<WaterEntity> GetWaterEntities()
        {
            return DatabaseManager.GetWaterEntities();
        }
        public Customer GetCustomer(string email)
        {
            return DatabaseManager.GetCustomer(email);
        }

        public bool AddHiringContract(HiringContract hiringContract)
        {
            for(int i = 0; i < hiringContract.FrieseCount; i++)
            {
                hiringContract.WaterEntities = new List<WaterEntity>();
                hiringContract.WaterEntities.Add(WaterEntities[i]);
            }
            return DatabaseManager.AddHiringContract(hiringContract);
        }

        public bool CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            else
            {
                // Return true if strIn is in valid e-mail format.
                return Regex.IsMatch(email, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$", RegexOptions.IgnoreCase);
            }
        }

        public bool AddProduct(Product product)
        {
            return DatabaseManager.AddProduct(product);
        }

        public bool AddBoat(IBoat boat)
        {
            return DatabaseManager.AddBoat(boat);
        }

        public bool AddCustomer(Customer customer)
        {
            return customer.AddCustomer(customer);
        }

        public bool ChangeBoat(IBoat boat)
        {
            return DatabaseManager.ChangeBoat(boat);
        }

        public bool ChangeProduct(Product product)
        {
            return DatabaseManager.ChangeProduct(product);
        }

        public bool RemoveBoat(IBoat boat)
        {
            return DatabaseManager.RemoveBoat(boat);
        }

        public bool RemoveProduct(Product product)
        {
            return DatabaseManager.RemoveProduct(product);
        }

        public void CalculateTempFile(List<string> lines )
        {
            throw new NotImplementedException();
        }

        public List<MotorBoat> GetMotorBoats()
        {
            List<MotorBoat> returnMotorBoats = new List<MotorBoat>();
            foreach (IBoat b in Boats.Where(b => b is MotorBoat))
            {
                returnMotorBoats.Add(b as MotorBoat);
            }
            return returnMotorBoats;
        }


        public List<MuscleBoat>  GetMuscleBoats()
        {
            List<MuscleBoat> returnMuscleBoats = new List<MuscleBoat>();
            foreach (IBoat b in Boats.Where(b => b is MuscleBoat))
            {
                returnMuscleBoats.Add(b as MuscleBoat);
            }
            return returnMuscleBoats;
        }
    }
}
