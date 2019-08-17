using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Classes
{
    public class DbSelectorFactory
    {
        public IDbCustomConnector CreateDbClasses(string DbType)
        {
            return new SqlServerDbClient();
        }
    }
}
