using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    internal class Database
    {
        System.Data.Linq.DataContext DbContext()
        {
            return new DbContext(ConfigurationManager.ConnectionStrings);
        }
    }
}
