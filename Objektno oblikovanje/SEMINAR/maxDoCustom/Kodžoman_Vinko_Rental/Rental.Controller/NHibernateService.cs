using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using System.IO;
using System.Data.SQLite;

namespace Rental
{
    public class NHibernateService
    {
        private static Configuration _cfg;

        public static void Init()
        {
            // Create the database
            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            Configuration c = new Configuration();
            c.Configure();

            c.AddAssembly(Assembly.GetCallingAssembly());
            _cfg = c;

            // SchemaUpdate creates tables in db if they don't exists
            // SchemaCreate will erase all previous tables (getting fresh database every run)
            new SchemaUpdate(c).Execute(true, true);
        }

        public static ISession OpenSession()
        {
            ISessionFactory f = _cfg.BuildSessionFactory();
            return f.OpenSession();
        }
    }
}
