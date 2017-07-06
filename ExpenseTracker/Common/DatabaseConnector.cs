using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Collections.ObjectModel;
//Refactor -- The controls should not be visible to this class
using System.Windows.Controls;
using System.Collections;

namespace ExpenseTracker.Common
{
    static class DatabaseConnector
    {
        private static string connectionstring = "Data Source=DESKTOP-PBHHE1A;Initial Catalog=ExpenseManagementSystem;Integrated Security=True";
        private static DataTable dt { get; set; }

        /// <summary>
        /// The listView.Items.Clear() is used to clear the contents of the 
        /// ItemCollection to avoid item duplication when calling the stored procedure.
        /// </summary>
        /// <param name="listView">a listview control</param>
        public static void ViewData(ListView listView)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                listView.Items.Clear();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = Constants.ShowTransactionHistory;
                    cmd.Connection = conn;
                    TransactionHistory data = null;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            conn.Open();
                            adapter.SelectCommand.Connection = conn;

                            using (SqlDataReader read = cmd.ExecuteReader())
                            {
                                while (read.Read())
                                {
                                    listView.Items.Add
                                    (
                                        data = new TransactionHistory()
                                        {
                                            primaryKey = read[0].ToString(),
                                            itemName = read[1].ToString(),
                                            qty = read[2].ToString(),
                                            amount = read[3].ToString(),
                                            total = read[4].ToString(),
                                            cash = read[5].ToString(),
                                            change = read[6].ToString(),
                                            tax = read[7].ToString(),
                                            transDate = read[8].ToString(),
                                            estName = read[9].ToString()
                                        }
                                    );
                                }
                            }
                            conn.Close();
                        }
                        catch (SqlException sql)
                        {
                            if (sql.Number == 2)
                            {
                                MessageBox.Show("An error has occurred while establishing a connection to the server. \n" + 
                                    "Please check the SQL Service from Windows Services.");
                            }
                            
                        }
                    }
                }
            }
        }

        public static void PopulateComboBox()
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                dt = null;
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = Constants.ShowEstablishmentList;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            dt = new DataTable(cmd.CommandText);
                            adapter.SelectCommand.Connection = conn;
                            adapter.Fill(dt);
                            SqlDataReader read = cmd.ExecuteReader();

                            EstablishmentData.estList = new List<EstablishmentData>();

                            while (read.Read())
                            {
                                EstablishmentData.estList.Add
                                (
                                     new EstablishmentData(int.Parse(read[0].ToString()), read[1].ToString())
                                );
                            }

                            conn.Close();
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
                    


                }
            }   
        }
        
        public static void InsertDataToEstablishment(string storedproc, string paramOne, string paramTwo)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    conn.Open();

                    cmd.CommandText = storedproc;
                    cmd.Connection = conn;
                    
                    // CommandType.StoredProcedure means the command text 
                    // SHOULD ONLY contain the name of the stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@establishment_name", SqlDbType.VarChar, 20).Value = paramOne;
                    cmd.Parameters.Add("@establishment_description", SqlDbType.VarChar, 90).Value = paramTwo;
                    cmd.ExecuteNonQuery();
                    
                    conn.Close();
                }
            }
        }

        public static void UpdataDataFromTransaction(
            string storedproc,
            string primaryKey,
            string itemParam,
            string qtyParam,

            string amountParam,
            string totalParam,//
            string cashParam,

            string changeParam,//
            string taxParam,
            string dateParam,

            int? estId)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    conn.Open();

                        cmd.CommandText = storedproc;
                        cmd.Connection = conn;

                        // CommandType.StoredProcedure means the command text 
                        // SHOULD ONLY contain the name of the stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@pk", SqlDbType.NVarChar, 30).Value = primaryKey;
                        cmd.Parameters.Add("@item", SqlDbType.NVarChar, 30).Value = itemParam;
                        cmd.Parameters.Add("@qty", SqlDbType.Int).Value = qtyParam;
                        cmd.Parameters.Add("@amount", SqlDbType.Decimal, 18).Value = amountParam;

                        cmd.Parameters.Add("@total", SqlDbType.Decimal, 18).Value = totalParam;
                        cmd.Parameters.Add("@cash", SqlDbType.Decimal, 18).Value = cashParam;
                        cmd.Parameters.Add("@change", SqlDbType.Decimal, 18).Value = changeParam;

                        cmd.Parameters.Add("@tax", SqlDbType.Decimal, 4).Value = taxParam;
                        cmd.Parameters.Add("@transaction_date", SqlDbType.DateTime).Value = dateParam;
                        cmd.Parameters.Add("@establishment_id", SqlDbType.Int).Value = estId;

                        cmd.ExecuteNonQuery();

                        conn.Close();
                }
            }
        }

        public static void DeleteDataFromTransaction(string storedproc, int pk)
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    conn.Open();

                        cmd.CommandText = storedproc;
                        cmd.Connection = conn;

                        int ctr = 1;
                        // CommandType.StoredProcedure means the command text 
                        // SHOULD ONLY contain the name of the stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@trans_id", SqlDbType.NVarChar, 30).Value = pk;
                        cmd.Parameters.Add("@ctr", SqlDbType.Int).Value = ctr;

                        cmd.ExecuteNonQuery();

                        conn.Close();
                }
            }
        }

        public static void InsertDataToTransaction(
            string storedproc, 
            string itemParam, 
            string qtyParam, 
            string amountParam,

            string totalParam,//

            string cashParam,

            string changeParam,//

            string taxParam,
            string dateParam,
            int? est_idParam
        )
        {
            using (SqlConnection conn = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        conn.Open();

                        cmd.CommandText = storedproc;
                        cmd.Connection = conn;

                        // CommandType.StoredProcedure means the command text 
                        // SHOULD ONLY contain the name of the stored procedure
                        cmd.CommandType = CommandType.StoredProcedure;

                        // This implements parameterized query
                        cmd.Parameters.Add("@item", SqlDbType.NVarChar, 30).Value = itemParam;
                        cmd.Parameters.Add("@qty", SqlDbType.Int).Value = qtyParam;
                        cmd.Parameters.Add("@amount", SqlDbType.Decimal, 18).Value = amountParam;

                        cmd.Parameters.Add("@total", SqlDbType.Decimal, 18).Value = totalParam;

                        cmd.Parameters.Add("@cash", SqlDbType.Decimal, 18).Value = cashParam;

                        cmd.Parameters.Add("@change", SqlDbType.Decimal, 18).Value = changeParam;

                        cmd.Parameters.Add("@tax", SqlDbType.Decimal, 4).Value = taxParam;
                        cmd.Parameters.Add("@transaction_date", SqlDbType.DateTime).Value = dateParam;
                        cmd.Parameters.Add("@establishment_id", SqlDbType.Int).Value = est_idParam;

                        // Execute the query
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch(FormatException format)
                    {
                        MessageBox.Show("Please check the format of the payment and tax.", "Record Creation Failed!");
                    }
                }
            }
        }

    }
}
