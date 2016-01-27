using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class Employee : Person
    {
        private List<Client> _advasingClients = new List<Client>();

        public Employee(String name, String lastName, List<Client> clients)
        {
            this.Id = this.Id++;
            this.Name = name;
            this.LastName = lastName;
            this._advasingClients = clients;
        }

        public Employee(String name, String lastName, Client client)
        {
            this.Id = this.Id++;
            this.Name = name;
            this.LastName = lastName;
            this._advasingClients.Add(client);
        }

        public Employee(String name, String lastName)
        {
            this.Id = this.Id++;
            this.Name = name;
            this.LastName = lastName;
        }
    }
}
