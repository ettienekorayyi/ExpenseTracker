using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Common
{
    public static class Constants
    {
        // Stored Procedures
        public static string ShowEstablishmentList = "sp_ShowEstablishmentList";
        public static string  CreateEstablishment = "sp_CreateEstablishment";
        public static string ShowTransactionHistories = "sp_ShowTransactionHistories";
        public static string CreateTransactionRecord = "sp_CreateTransactionRecord";
        public static string UpdateTransactionRecord = "sp_UpdateTransactionRecord";
        public static string DeleteTransactionRecord = "sp_DeleteTransactionRecord";

        // Database Clients
        public const string SqlServerClient = "System.Data.SqlClient";
        public const string SqlServerConnection = "SqlServer";
    }
}
