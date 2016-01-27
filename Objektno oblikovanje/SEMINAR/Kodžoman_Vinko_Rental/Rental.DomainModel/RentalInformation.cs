using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class RentalInformation
    {
        private int _id;
        private Client _client;
        private Rental _rented;
        private DateTime _from, _to;
        private Double _dailyCost;

        public Client Client
        {
            get
            {
                return _client;
            }

            set
            {
                _client = value;
            }
        }

        public Rental Rented
        {
            get
            {
                return _rented;
            }

            set
            {
                _rented = value;
            }
        }

        public DateTime From
        {
            get
            {
                return _from;
            }

            set
            {
                _from = value;
            }
        }

        public DateTime To
        {
            get
            {
                return _to;
            }

            set
            {
                _to = value;
            }
        }

        public double DailyCost
        {
            get
            {
                return _dailyCost;
            }

            set
            {
                _dailyCost = value;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
    }
}
