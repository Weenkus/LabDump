using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class Client : Person
    {
        private Employee _dedicatedAgent;

        public Client() { }

        public Client(String name, String lastName, Employee emp)
        {
            this.Name = name;
            this.LastName = lastName;
            this._dedicatedAgent = emp;
        }

        public virtual Employee DedicatedAgent
        {
            get
            {
                return _dedicatedAgent;
            }

            set
            {
                _dedicatedAgent = value;
            }
        }
    }
}
