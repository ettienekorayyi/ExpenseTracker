using ExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Interfaces
{
    public interface ISqlServerDbClient
    {
        List<TransactionHistory> ViewTransactionRecords(IDbConnection database);
        void UpdateDataFromTransaction(IDbConnection database, TransactionHistory transactionHistory);
        void InsertDataToTransaction(IDbConnection database, TransactionHistory transaction);
        void DeleteDataFromTransaction(IDbConnection database, int primaryKey);
        List<Establishment> ViewEstablishments(IDbConnection database);
        void InsertDataToEstablishment(IDbConnection database, string paramOne, string paramTwo);
    }
}
