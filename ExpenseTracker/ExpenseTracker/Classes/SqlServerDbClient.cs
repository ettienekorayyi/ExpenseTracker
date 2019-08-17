using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Configuration;

using ExpenseTracker.Interfaces;
using System.Data.SqlClient;
using ExpenseTracker.Model;
using ExpenseTracker.Common;
using System.Windows;

namespace ExpenseTracker.Classes
{
    public class SqlServerDbClient : IDbCustomConnector, ISqlServerDbClient 
    {
        public IDbConnection ConnectToDatabase(ConnectionStringSettings connectionString)
        { 
            DbProviderFactory providerFactory = 
                DbProviderFactories.GetFactory(connectionString.ProviderName);
            return providerFactory.CreateConnection();
        }

        public List<TransactionHistory> ViewTransactionRecords(IDbConnection database) 
        {
            List<TransactionHistory> list = new List<TransactionHistory>();
            try
            {
                using (IDbCommand command = database.CreateCommand())
                {
                    database.Open();

                    command.CommandType = CommandType.StoredProcedure; ;
                    command.CommandText = Constants.ShowTransactionHistories;

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(new TransactionHistory
                            {
                                primaryKey = reader[0].ToString(),
                                itemName = reader[1].ToString(),
                                qty = reader[2].ToString(),
                                amount = reader[3].ToString(),
                                total = reader[4].ToString(),
                                cash = reader[5].ToString(),
                                change = reader[6].ToString(),
                                tax = reader[7].ToString(),
                                transDate = reader[8].ToString(),
                                estName = reader[9].ToString()
                            });
                    }
                }
                
            }
            catch (NullReferenceException nullException)
            {
                MessageBox.Show(nullException.Message);
            }
            return list;
        }

        public void UpdateDataFromTransaction(IDbConnection database,TransactionHistory transactionHistory)
        {
            using (SqlCommand command = new SqlCommand(Constants.UpdateTransactionRecord,
                new SqlConnection(database.ConnectionString)))
            {
                command.Connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@pk", SqlDbType.NVarChar, 30).Value = transactionHistory.primaryKey;
                command.Parameters.Add("@item", SqlDbType.NVarChar, 30).Value = transactionHistory.itemName;
                command.Parameters.Add("@qty", SqlDbType.Int).Value = transactionHistory.qty;
                command.Parameters.Add("@amount", SqlDbType.Decimal, 18).Value = transactionHistory.amount;
                command.Parameters.Add("@total", SqlDbType.Decimal, 18).Value = transactionHistory.amount;
                command.Parameters.Add("@cash", SqlDbType.Decimal, 18).Value = transactionHistory.cash;
                command.Parameters.Add("@change", SqlDbType.Decimal, 18).Value = transactionHistory.change;
                command.Parameters.Add("@tax", SqlDbType.Decimal, 4).Value = transactionHistory.tax;
                command.Parameters.Add("@transaction_date", SqlDbType.DateTime).Value = transactionHistory.transDate;
                command.Parameters.Add("@establishment_id", SqlDbType.Int).Value = transactionHistory.establishment_Id;

                command.ExecuteNonQuery();
                
            }
        }
        //total,change
        public void InsertDataToTransaction(
            IDbConnection database, TransactionHistory transaction)
        {
            using (SqlCommand command = new SqlCommand(Constants.CreateTransactionRecord,
                new SqlConnection(database.ConnectionString)))
            {
                try
                {
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@item", SqlDbType.NVarChar, 30).Value = transaction.itemName;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = transaction.qty;
                    command.Parameters.Add("@amount", SqlDbType.Decimal, 18).Value = transaction.amount;

                    command.Parameters.Add("@total", SqlDbType.Decimal, 18).Value = transaction.total;

                    command.Parameters.Add("@cash", SqlDbType.Decimal, 18).Value = transaction.cash;

                    command.Parameters.Add("@change", SqlDbType.Decimal, 18).Value = transaction.change;

                    command.Parameters.Add("@tax", SqlDbType.Decimal, 4).Value = transaction.tax;
                    command.Parameters.Add("@transaction_date", SqlDbType.DateTime).Value = transaction.transDate;
                    command.Parameters.Add("@establishment_id", SqlDbType.Int).Value = transaction.establishment_Id;

                    command.ExecuteNonQuery();
                    
                }
                catch (FormatException formatException)
                {
                    MessageBox.Show(formatException.Message, "Record Creation Failed!");
                }
            }
        }

        public void DeleteDataFromTransaction(IDbConnection database, int primaryKey)
        {
             using (SqlCommand command = new SqlCommand(Constants.DeleteTransactionRecord,
                new SqlConnection(database.ConnectionString)))
                {
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@trans_id", SqlDbType.NVarChar, 30).Value = primaryKey;
                    command.ExecuteNonQuery();
                }
        }

        // Establishments
        public List<Establishment> ViewEstablishments(IDbConnection database)
        {
            using (IDbCommand command = database.CreateCommand())
            {
                List<Establishment> list = new List<Establishment>();
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = Constants.ShowEstablishmentList;
                    database.Open();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add
                            (
                                new Establishment(int.Parse(reader[0].ToString()), reader[1].ToString())
                            );
                        }
                        return list;
                    }
                }
                catch (SqlException sql)
                {
                    if (sql.Number == 2)
                    {
                        MessageBox.Show("An error has occurred while establishing a connection to the server. \n" +
                                    "Please check the SQL Service from Windows Services.");
                    }
                }
                return null;
            }
        }

        public void InsertDataToEstablishment(IDbConnection database,string paramOne, string paramTwo)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = new SqlConnection(database.ConnectionString);
                command.Connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Constants.CreateEstablishment;

                command.Parameters.Add("@establishment_name", SqlDbType.VarChar, 20).Value = paramOne;
                command.Parameters.Add("@establishment_description", SqlDbType.VarChar, 90).Value = paramTwo;
                
                command.ExecuteNonQuery();
            }
        }

    }
}
