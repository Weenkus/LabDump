using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public abstract class Rental
    {
        private static int _id = 0;
        private Client _owner;
        private String _name;
        private String _description;
        private Double _daylyPrice;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Client Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Double MonthlyPrice
        {
            get { return _daylyPrice; }
            set { _daylyPrice = value; }
        }
    }
}
