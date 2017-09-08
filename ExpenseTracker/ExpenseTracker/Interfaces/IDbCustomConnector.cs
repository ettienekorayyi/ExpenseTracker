using ExpenseTracker.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExpenseTracker.Interfaces
{
    public interface IDbCustomConnector
    {
        IDbConnection ConnectToDatabase(ConnectionStringSettings connectionString);
        List<TransactionHistory> ViewTransactionRecords(IDbConnection database);
    }
}
