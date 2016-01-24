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
            this.Name = name;
            this.LastName = lastName;
            this._advasingClients = clients;
        }

        public Employee(String name, String lastName, Client client)
        {
            this.Name = name;
            this.LastName = lastName;
            this._advasingClients.Add(client);
        }

        public Employee(String name, String lastName)
        {
            this.Name = name;
            this.LastName = lastName;
        }
    }
}
