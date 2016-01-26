using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class AppController : IController
    {
        public void ShowEmplyoees()
        {
            Employee employee1 = new Employee("Vinko", "Zadric");
            Employee employee2 = new Employee("Mlako", "Vader");
            Employee employee3 = new Employee("Hesimono", "Kaero");
            Client client = new Client("Marin", "Veljko", employee1);

            // Clean repos (because they are singeltons, they might still have some data left in them)
            PersonRepository.Instance.Clear();

            // Fill the repos
            PersonRepository.Instance.Add(employee1);
            PersonRepository.Instance.Add(employee2);
            PersonRepository.Instance.Add(employee3);
            PersonRepository.Instance.Add(client);

            FormEmployeeView f = new FormEmployeeView(this, PersonRepository.Instance);
            f.Show();
        }
    }
}
