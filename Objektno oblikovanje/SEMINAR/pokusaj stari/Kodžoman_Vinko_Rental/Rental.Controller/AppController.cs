using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using System.Text.RegularExpressions;

namespace Rental
{
    public class AppController : IController
    {
        public void ShowEmplyoees()
        {
            using (ISession session = NHibernateService.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Employee employee1 = new Employee("Vinko", "Zadric");
                    Employee employee2 = new Employee("Mlako", "Vader");
                    Employee employee3 = new Employee("Hesimono", "Kaero");
                    Client client = new Client("Marin", "Veljko", employee1);
                    session.Save(employee1);
                    session.Save(employee2);
                    session.Save(employee3);
                    session.Save(client);
                    transaction.Commit();
                }
            }

           
            

           /* // Clean repos (because they are singeltons, they might still have some data left in them)
            PersonRepository.Instance.Clear();

            // Fill the repos
            PersonRepository.Instance.Add(employee1);
            PersonRepository.Instance.Add(employee2);
            PersonRepository.Instance.Add(employee3);
            PersonRepository.Instance.Add(client);*/

            FormEmployeeView f = new FormEmployeeView(this, PersonRepository.Instance);
            f.Show();
        }
    }
}
