using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using ExpenseTracker.Model;
using System.Windows;
using System.Configuration;
using ExpenseTracker.Classes;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Common
{
    public class Utility
    {
        public IDbConnection connection { get; private set; }
        private ISqlServerDbClient _sqlServerDbClient;
        

        public Utility(ISqlServerDbClient sqlServerDbClient = null)
        {
            _sqlServerDbClient = sqlServerDbClient ?? new SqlServerDbClient();
            
        }

        public List<TransactionHistory> UseTransactionHistory(string dbType, string connectionString)
        {
            var configurationManager = ConfigurationManager.ConnectionStrings[connectionString];
            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(configurationManager);

            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;

            var result = _sqlServerDbClient.ViewTransactionRecords(connection);
            return result;
        }

        public void TransactionHistoryUpdate(string dbType, string connectionString,
            TransactionHistory history)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;

            _sqlServerDbClient.UpdateDataFromTransaction(connection, history);
        }

        public void TransactionHistoryDelete(string dbType, string connectionString,
            int primaryKey)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;

            _sqlServerDbClient.DeleteDataFromTransaction(connection, primaryKey);
        }

        public void TransactionHistoryCreate(string dbType, string connectionString,
            TransactionHistory transaction)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;

            _sqlServerDbClient.InsertDataToTransaction(connection, transaction);
        }

        // Establishments
        public List<Establishment> UseEstablishment(string dbType, string connectionString)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;

            var result = _sqlServerDbClient.ViewEstablishments(connection);
            return result;
        }

        public void CreateNewEstablishment(string dbType, string connectionString,string paramOne, string paramTwo)
        {
            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;

            _sqlServerDbClient.InsertDataToEstablishment(connection, paramOne, paramTwo);
        }



    }
}
