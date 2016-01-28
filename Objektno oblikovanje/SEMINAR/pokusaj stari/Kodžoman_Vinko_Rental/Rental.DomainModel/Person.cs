using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rental
{
    public abstract class Person
    {
        private static int _id = 0;
        private String _name;
        private String _lastName;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

    }
}
