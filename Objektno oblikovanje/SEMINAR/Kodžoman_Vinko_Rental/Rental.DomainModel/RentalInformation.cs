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
        private DateTime _from;
        private DateTime  _to;
        private Double _dailyCost;

        public RentalInformation() { }

        public RentalInformation(Client c, Rental r, DateTime from, DateTime to, Double dailyCost)
        {
            this.Client = c;
            this.Rented = r;
            this.From = from;
            this.To = to;
            this.DailyCost = dailyCost;
        }

        public virtual Client Client
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

        public virtual Rental Rented
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

        public virtual DateTime From
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

        public virtual DateTime To
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

        public virtual double DailyCost
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

        public virtual int Id
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
