﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class Employee : Person
    {
        private IList<Client> _advasingClients = new List<Client>();

        public Employee() { }

        public Employee(String name, String lastName, IList<Client> clients)
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

        public virtual IList<Client> AdvasingClients
        {
            get
            {
                return _advasingClients;
            }

            set
            {
                _advasingClients = value;
            }
        }
    }
}
