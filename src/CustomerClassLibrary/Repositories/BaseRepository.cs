using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary.Repositories
{
    public abstract class BaseRepository
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection("Server=.\\SQLEXPRESS;Database=customer_db_Levanova;Trusted_Connection=True;");
        }
    }
}
