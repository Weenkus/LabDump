using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Rental
{
    public class Tests
    {
        [Fact]
        public void PersonCreation()
        {
            Employee employee = new Employee("Vinko", "Zadric");
            Client client = new Client("Marin", "Veljko", employee);

            PersonRepository.Instance.Add(employee);
            PersonRepository.Instance.Add(client);

            // Check if the repository saved successfully
            Assert.Equal(client, PersonRepository.Instance.Get(client));
            Assert.Equal(employee, PersonRepository.Instance.Get(employee));

            // Test the client link created via the constructor
            Assert.Equal(client.DedicatedAgent, employee);
        }
    }
}
