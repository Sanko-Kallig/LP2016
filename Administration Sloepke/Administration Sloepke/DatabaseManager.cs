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

        internal static Account GetAccount(string email, string password)
        {
            throw new NotImplementedException();
        }

        internal static List<HiringContract> GetHiringContracts()
        {
            throw new NotImplementedException();
        }

        internal static List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        internal static List<IBoat> GetBoats()
        {
            throw new NotImplementedException();
        }

        internal static Customer GetCustomer(string email)
        {
            throw new NotImplementedException();
        }

        internal static bool AddHiringContract(HiringContract hiringContract)
        {
            throw new NotImplementedException();
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

