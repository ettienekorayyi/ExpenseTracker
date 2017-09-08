using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpenseTracker.Interfaces;
using ExpenseTracker.DbClasses;

namespace ExpenseTracker.FactoryPattern
{
    public class DbSelectorFactory
    {
        //public static IDbCustomConnector DbClient { get; set; }
        public IDbCustomConnector CreateDbClasses(string DbType)
        {
            return new SqlServerDbClient();
        }
    }
}
