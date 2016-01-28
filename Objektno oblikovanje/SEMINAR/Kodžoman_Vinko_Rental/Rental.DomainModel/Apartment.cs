using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class Apartment : Rental
    {
        private IList<SpecialFeatures> _payedFeatures;
        private IList<RentalInclude> _includedFeatures;
        private String _postalCode;
        private String _address;

        public Apartment() { }

        public Apartment(Client owner, String name, String description, String postal,
            String address, Double MonthlyPrice, IList<RentalInclude> rFeatures, IList<SpecialFeatures> sFeatures)
        {
            this.Name = name;
            this.Description = description;
            this._postalCode = postal;
            this._address = address;
            this._payedFeatures = sFeatures;
            this._includedFeatures = rFeatures;
            this.Owner = owner;
        }

        public virtual IList<SpecialFeatures> PayedFeatures
        {
            get
            {
                return _payedFeatures;
            }

            set
            {
                _payedFeatures = value;
            }
        }

        public virtual IList<RentalInclude> IncludedFeatures
        {
            get
            {
                return _includedFeatures;
            }

            set
            {
                _includedFeatures = value;
            }
        }

        public virtual string PostalCode
        {
            get
            {
                return _postalCode;
            }

            set
            {
                _postalCode = value;
            }
        }

        public virtual string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
            }
        }
    }
}
