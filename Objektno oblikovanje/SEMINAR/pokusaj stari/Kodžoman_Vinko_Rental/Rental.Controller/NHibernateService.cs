using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Rental
{
    static class NHibernateService
    {
        public static ISession OpenSession()
        {
            Configuration c = new Configuration();
            c.Configure();

            c.AddAssembly(Assembly.GetCallingAssembly());
            ISessionFactory f = c.BuildSessionFactory();
            return f.OpenSession();
        }
    }
}
