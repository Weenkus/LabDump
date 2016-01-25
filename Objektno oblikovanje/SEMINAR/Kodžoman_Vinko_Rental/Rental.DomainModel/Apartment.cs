using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class Apartment : Rental
    {
        private List<SpecialFeatures> _payedFeatures;
        private List<RentalInclude> _includedFeatures;
        private String _postalCode;
        private String _address;

        public Apartment(Client owner, String name, String description, String postal,
            String address, Double MonthlyPrice, List<RentalInclude> rFeatures, List<SpecialFeatures> sFeatures)
        {
            this.Id = this.Id++;
            this.Name = name;
            this.Description = description;
            this._postalCode = postal;
            this._address = address;
            this._payedFeatures = sFeatures;
            this._includedFeatures = rFeatures;
            this.Owner = owner;
        }
    }
}
