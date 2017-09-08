using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using ExpenseTracker.FactoryPattern;
using ExpenseTracker.Model;
using System.Windows;
using System.Configuration;
using ExpenseTracker.DbClasses;

namespace ExpenseTracker.Common
{
    public class Utility
    {
        public IDbConnection connection { get; private set; }

        public List<TransactionHistory> UseTransactionHistory(string dbType, string connectionString)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            return new SqlServerDbClient().ViewTransactionRecords(connection);
        }

        public void TransactionHistoryUpdate(string dbType, string connectionString,
            TransactionHistory history)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            
            new SqlServerDbClient().UpdateDataFromTransaction(connection, history);
        }

        public void TransactionHistoryDelete(string dbType, string connectionString,
            int primaryKey)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            new SqlServerDbClient().DeleteDataFromTransaction(connection, primaryKey);
        }

        public void TransactionHistoryCreate(string dbType, string connectionString,
            TransactionHistory transaction)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            new SqlServerDbClient().InsertDataToTransaction(connection, transaction);
        }

        // Establishments
        public List<Establishment> UseEstablishment(string dbType, string connectionString)
        {

            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            return new SqlServerDbClient().ViewEstablishments(connection);
        }

        public void CreateNewEstablishment(string dbType, string connectionString,string paramOne, string paramTwo)
        {
            connection = new DbSelectorFactory().CreateDbClasses(dbType)
                .ConnectToDatabase(ConfigurationManager.ConnectionStrings[connectionString]);
            connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString]
                .ConnectionString;
            new SqlServerDbClient().InsertDataToEstablishment(connection, paramOne, paramTwo);
        }



    }
}
