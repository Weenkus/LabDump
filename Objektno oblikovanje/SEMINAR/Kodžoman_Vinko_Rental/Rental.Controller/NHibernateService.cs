using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using System.Data.SQLite;

namespace Rental
{
    public class NHibernateService
    {
        public static ISession OpenSession()
        {
            //SQLiteConnection.CreateFile("MyDatabase.sqlite"); // Run it only once

            Configuration c = new Configuration();
            c.Configure();


            c.AddAssembly(Assembly.GetCallingAssembly());   // The below is useless since this finds all mappings
            //c.AddAssembly(typeof(Rental).Assembly);
            //c.AddAssembly(typeof(Person).Assembly);
            //c.AddAssembly(typeof(RentalInformation).Assembly);

            //var schema = new SchemaExport(c);
           // schema.Create(true, true);

            ISessionFactory f = c.BuildSessionFactory();
            return f.OpenSession();
        }
    }
}
