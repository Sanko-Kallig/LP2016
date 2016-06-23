// <copyright file="DatabaseManager.cs" company="Fontys">
//      Copyright (c) Fontyss. All rights reserved.
// </copyright>
// <author>Sander Koch</author>
namespace Administration_Sloepke
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Oracle.DataAccess.Client;

    /// <summary>
    /// Static DatabaseManager class, used to connect to the database and manipulate the data in it via queries.
    /// Exceptions are thrown without any handling
    /// </summary>
    public static class DatabaseManager
    {
        #region Fields
        /// <summary>
        /// Database connection; uses Connectionstring
        /// </summary>
        private static OracleConnection connection = new OracleConnection(ConnectionString);

        /// <summary>
        /// Temp data for connecting to the database; [Username], [Password], [server-IP]
        /// </summary>
        private const string ConnectionId = "dbi330810",

            ConnectionPassword = "Jm3AQLBVXo",

            ConnectionAddress = "//fhictora01.fhict.local:1521/fhictora";
        #endregion

        #region Properties
        /// <summary>
        /// Data used to create the database connection 
        /// </summary>
        private static string ConnectionString
        {
            get
            {
                return string.Format("Data Source={0};Persist Security Info=True;User Id={1};Password={2}", ConnectionAddress, ConnectionId, ConnectionPassword);
            }
        }

        public static Account GetAccount(string email, string password)
        {
            try
            {
                Account returnAccount = null;
                OracleCommand selectCommand =
                    CreateOracleCommand("SELECT * FROM \"Account\" WHERE Email = :email and Wachtwoord = :password");
                selectCommand.Parameters.Add(":email", email);
                selectCommand.Parameters.Add(":password", password);

                OracleDataReader MainReader = ExecuteQuery(selectCommand);
                while (MainReader.Read())
                {
                    int id = Convert.ToInt32(MainReader["ID"].ToString());
                    string Email = MainReader["Email"].ToString();
                    string Name = MainReader["Naam"].ToString();
                    bool isAdmin = Convert.ToBoolean(Convert.ToInt32(MainReader["IsAdmin"].ToString()));
                    returnAccount = new Account(id, Email, Name, isAdmin);


                }
                return returnAccount;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static List<HiringContract> GetHiringContracts()
        {
            try
            {
                List<HiringContract> returnContracts = new List<HiringContract>();
                OracleCommand selectCommand = CreateOracleCommand("SELECT * FROM HUURCONTRACT");

                OracleDataReader MainReader = ExecuteQuery(selectCommand);

                while (MainReader.Read())
                {
                    int ID = Convert.ToInt32(MainReader["ID"].ToString());
                    DateTime startDate = Convert.ToDateTime(MainReader["BeginTijd"].ToString());
                    DateTime endDate = Convert.ToDateTime(MainReader["EindTijd"].ToString());
                    returnContracts.Add(new HiringContract(ID, startDate, endDate, null, null));
                }

                foreach (HiringContract h in returnContracts)
                {
                    h.Employee = GetAccount(h);
                    h.Renter = GetCustomer(h);
                    h.Boats = GetBoats(h);
                    h.Products = GetProducts(h);
                    h.WaterEntities = GetWaterEntities(h);

                }
                return returnContracts;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private static Account GetAccount(HiringContract contract)
        {
            try
            {
                Account returnAccount = new Account();
                OracleCommand accountCommand = CreateOracleCommand("Select * FROM \"Account\" WHERE ID = (select Verhuurder_ID FROM HuurContract where ID = :id)");
                accountCommand.Parameters.Add("id", contract.ID);

                OracleDataReader AccountReader = ExecuteQuery(accountCommand);
                while (AccountReader.Read())
                {
                    int id = Convert.ToInt32(AccountReader["ID"].ToString());
                    string email = AccountReader["Email"].ToString();
                    string name = AccountReader["Naam"].ToString();
                    bool isAdmin = Convert.ToBoolean(Convert.ToInt32(AccountReader["IsAdmin"].ToString()));
                    returnAccount = new Account(id, email, name, isAdmin);
                }
                return returnAccount;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        private static Customer GetCustomer(HiringContract contract)
        {
            try
            {
                Customer returnCustomer = new Customer();
                OracleCommand customerCommand =
                    CreateOracleCommand(
                        "Select * From Huurder Where ID = (Select Huurder_ID From HuurContract where ID = :id)");
                customerCommand.Parameters.Add(":id", contract.ID);

                OracleDataReader customerReader = ExecuteQuery(customerCommand);
                while (customerReader.Read())
                {
                    int id = Convert.ToInt32(customerReader["ID"].ToString());
                    string name = customerReader["Naam"].ToString();
                    string email = customerReader["Email"].ToString();
                    returnCustomer = new Customer(id, email, name);
                }
                return returnCustomer;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static List<Product> GetProducts(HiringContract contract)
        {
            try
            {
                List<Product> returnProducts = new List<Product>();
                OracleCommand productCommand = CreateOracleCommand("SELECT * FROM Artikel WHERE ID IN (SELECT Artikel_ID FROM Huur_Artikelen where HuurContract_ID = :id)");
                productCommand.Parameters.Add(":id", contract.ID);

                OracleDataReader productReader = ExecuteQuery(productCommand);
                while (productReader.Read())
                {
                    int id = Convert.ToInt32(productReader["ID"].ToString());
                    string name = productReader["Naam"].ToString();
                    double price = Convert.ToDouble(productReader["Prijs"].ToString());
                    returnProducts.Add(new Product(id, name, price));
                }
                return returnProducts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static List<IBoat> GetBoats(HiringContract contract)
        {
            try
            {
                List<IBoat> returnBoats = new List<IBoat>();
                OracleCommand boatCommand =
                    CreateOracleCommand(
                        "SELECT b.Naam as Naam, b.DagPrijs as DagPrijs, b.BOOTTYPE as BootType, m.\"Type\" as mType, m.TankInhoud as Tankinhoud, s.\"Type\" as sType  FROM BOOT b LEFT JOIN MotorBoot m ON b.Naam = m.Naam LEFT JOIN SpierBoot s on b.Naam = s.Naam WHERE b.Naam = (SELECT Boot_Naam From Huur_Boten WHERE HuurContract_ID = :ID)");
                boatCommand.Parameters.Add(":id", contract.ID);

                OracleDataReader boatReader = ExecuteQuery(boatCommand);

                while (boatReader.Read())
                {
                    string name = boatReader["Naam"].ToString();
                    double DayPrice = Convert.ToDouble(boatReader["DagPrijs"].ToString());
                    string bootType = boatReader["BOOTTYPE"].ToString();

                    switch (bootType)
                    {
                        case "SpierBoot":
                        {
                            string type = boatReader["STYPE"].ToString();
                                returnBoats.Add(new MuscleBoat(name, DayPrice, type));
                            break;
                        }
                        case "MotorBoot":
                        {
                                string type = boatReader["MTYPE"].ToString();
                            double fuel = Convert.ToDouble(boatReader["Tankinhoud"].ToString());
                                returnBoats.Add(new MotorBoat(name, DayPrice, type, fuel));
                                break;
                        }

                    }
                }
                return returnBoats;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        private static List<WaterEntity> GetWaterEntities(HiringContract contract)
        {
            try
            {
                List<WaterEntity> returnWaterEntities = new List<WaterEntity>();
                OracleCommand waterCommand =
                    CreateOracleCommand(
                        "SELECT * FROM WATERLICHAAM WHERE ID IN (SELECT WATERLICHAAM_ID FROM HUUR_WATERLICHAAM WHERE HUURCONTRACT_ID = :id)");
                waterCommand.Parameters.Add(":id", contract.ID);

                OracleDataReader waterReader = ExecuteQuery(waterCommand);

                while (waterReader.Read())
                {
                    int id = Convert.ToInt32(waterReader["ID"].ToString());
                    if (waterReader.IsDBNull(1))
                    {
                        returnWaterEntities.Add(new WaterEntity(id));
                    }
                    else
                    {
                        string name = waterReader["Naam"].ToString();
                        returnWaterEntities.Add(new WaterEntity(id, name));
                    }

                }
                return returnWaterEntities;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static List<WaterEntity> GetWaterEntities()
        {
            try
            {
                List<WaterEntity> returnWaterEntities = new List<WaterEntity>();
                OracleCommand waterCommand =
                    CreateOracleCommand(
                        "SELECT * FROM WATERLICHAAM");

                OracleDataReader waterReader = ExecuteQuery(waterCommand);

                while (waterReader.Read())
                {
                    int id = Convert.ToInt32(waterReader["ID"].ToString());
                    if (waterReader.IsDBNull(1))
                    {
                        returnWaterEntities.Add(new WaterEntity(id));
                    }
                    else
                    {
                        string name = waterReader["Naam"].ToString();
                        returnWaterEntities.Add(new WaterEntity(id, name));
                    }

                }
                return returnWaterEntities;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Product> GetProducts()
        {
            try
            {
                List<Product> returnProducts = new List<Product>();
                OracleCommand productCommand = CreateOracleCommand("SELECT * FROM Artikel");

                OracleDataReader productReader = ExecuteQuery(productCommand);
                while (productReader.Read())
                {
                    int id = Convert.ToInt32(productReader["ID"].ToString());
                    string name = productReader["Naam"].ToString();
                    double price = Convert.ToDouble(productReader["Prijs"].ToString());
                    returnProducts.Add(new Product(id, name, price));
                }
                return returnProducts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<IBoat> GetBoats()
        {
            try
            {
                List<IBoat> returnBoats = new List<IBoat>();
                OracleCommand boatCommand =
                    CreateOracleCommand(
                        "SELECT b.Naam as Naam, b.DagPrijs as DagPrijs, b.BOOTTYPE as BootType, m.\"Type\" as mType, m.TankInhoud as Tankinhoud, s.\"Type\" as sType  FROM BOOT b LEFT JOIN MotorBoot m ON b.Naam = m.Naam LEFT JOIN SpierBoot s on b.Naam = s.Naam");
                OracleDataReader boatReader = ExecuteQuery(boatCommand);

                while (boatReader.Read())
                {
                    string name = boatReader["Naam"].ToString();
                    double DayPrice = Convert.ToDouble(boatReader["DagPrijs"].ToString());
                    string bootType = boatReader["BOOTTYPE"].ToString();

                    switch (bootType)
                    {
                        case "SpierBoot":
                            {
                                string type = boatReader["STYPE"].ToString();
                                returnBoats.Add(new MuscleBoat(name, DayPrice, type));
                                break;
                            }
                        case "MotorBoot":
                            {
                                string type = boatReader["MTYPE"].ToString();
                                double fuel = Convert.ToDouble(boatReader["Tankinhoud"].ToString());
                                returnBoats.Add(new MotorBoat(name, DayPrice, type, fuel));
                                break;
                            }

                    }
                }
                return returnBoats;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static Customer GetCustomer(string mail)
        {
            try
            {
                Customer returnCustomer = new Customer();
                OracleCommand customerCommand =
                    CreateOracleCommand(
                        "Select * From Huurder Where Email = :email");
                customerCommand.Parameters.Add(":email", mail);

                OracleDataReader customerReader = ExecuteQuery(customerCommand);
                while (customerReader.Read())
                {
                    int id = Convert.ToInt32(customerReader["ID"].ToString());
                    string name = customerReader["Naam"].ToString();
                    string email = customerReader["Email"].ToString();
                    returnCustomer = new Customer(id, email, name);
                }
                return returnCustomer;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal static bool AddHiringContract(HiringContract hiringContract)
        {
            try
            {
                bool sub = false;
                OracleCommand contractCommand =
                    CreateOracleCommand(
                        "INSERT INTO HUURCONTRACT(BeginTijd, EindTijd, Huurder_ID, Verhuurder_ID) VALUES(:startDate, :endDate, :customerID, :employeeID)");
                contractCommand.Parameters.Add(":startDate", hiringContract.StartDate);
                contractCommand.Parameters.Add(":endDate", hiringContract.EndDate);
                contractCommand.Parameters.Add(":customerID", hiringContract.Renter.ID);
                contractCommand.Parameters.Add(":employeeID", hiringContract.Employee.ID);

                if (ExecuteNonQuery(contractCommand))
                {
                    OracleCommand selectCommand = CreateOracleCommand("SELECT MAX(ID) FROM HUURCONTRACT");
                    OracleDataReader selectReader = ExecuteQuery(selectCommand);
                    while (selectReader.Read())
                    {
                        hiringContract.ID = Convert.ToInt32(selectReader["MAX(ID)"].ToString());
                    }
                    foreach (IBoat boat in hiringContract.Boats)
                    {
                        OracleCommand boatCommand =
                            CreateOracleCommand("Insert into HUUR_BOTEN(Huurcontract_ID, Boot_Naam) VALUES (:id, :name)");
                        boatCommand.Parameters.Add(":id", hiringContract.ID);
                        boatCommand.Parameters.Add(":name", boat.Name);
                        sub = ExecuteNonQuery(boatCommand);
                    }
                    foreach (Product p in hiringContract.Products)
                    {
                        OracleCommand productCommand =
                            CreateOracleCommand("Insert into HUUR_ARTIKELEN(Huurcontract_ID, ARTIKEL_ID) VALUES (:id, :aid)");
                        productCommand.Parameters.Add(":id", hiringContract.ID);
                        productCommand.Parameters.Add(":aid", p.ID);
                        sub = ExecuteNonQuery(productCommand);
                    }
                    foreach (WaterEntity w in hiringContract.WaterEntities)
                    {
                        OracleCommand waterCommand =
                            CreateOracleCommand(
                                "Insert into HUUR_Waterlichaam(Huurcontract_ID, Waterlichaam_ID) VALUES (:id, :wid)");
                        waterCommand.Parameters.Add(":id", hiringContract.ID);
                        waterCommand.Parameters.Add(":wid", w.ID);
                        sub = ExecuteNonQuery(waterCommand);
                    }
                }
                return sub;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        internal static bool AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        internal static bool AddBoat(IBoat boat)
        {
            throw new NotImplementedException();
        }

        internal static bool ChangeBoat(IBoat boat)
        {
            throw new NotImplementedException();
        }

        internal static bool ChangeProduct(Product product)
        {
            throw new NotImplementedException();
        }

        internal static bool RemoveBoat(IBoat boat)
        {
            throw new NotImplementedException();
        }

        internal static bool RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods - Background Processes
        /// <summary>
        /// Creates the command with sql query and database connection information
        /// </summary>
        /// <param name="sql">The sql query.</param>
        /// <returns>A new Oracle command.</returns>
        private static OracleCommand CreateOracleCommand(string sql)
        {
            OracleCommand command = new OracleCommand(sql, connection);
            command.CommandType = System.Data.CommandType.Text;

            return command;
        }

        /// <summary>
        ///  Opens the connection with the database, returns the reader
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private static OracleDataReader ExecuteQuery(OracleCommand command)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (OracleException exc)
                    {
                        Debug.WriteLine("Database connection failed!\n" + exc.Message);
                        throw;
                    }
                }

                OracleDataReader reader = command.ExecuteReader();

                return reader;
            }
            catch (OracleException)
            {
                return null;
            }
        }

        /// <summary>
        /// Opens the connection with the database and inserts the given information, returns true if insert worked
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private static bool ExecuteNonQuery(OracleCommand command)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (OracleException exc)
                    {
                        Debug.WriteLine("Database connection failed!\n" + exc.Message);
                        throw;
                    }
                }

                command.ExecuteNonQuery();
                return true;
            }
            catch (OracleException)
            {
                return false;
            }
        }
    }
    #endregion
}

