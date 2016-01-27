using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class ApartmanFactory
    {
        public static Apartment createApartman(Client owner, String name, String description, String postal,
            String address, Double monthlyPrice, List<RentalInclude> rFeatures, List<SpecialFeatures> sFeatures)
        {
            Apartment apartmant = new Apartment(owner, name, description, postal, address,
                monthlyPrice, rFeatures, sFeatures);
            return apartmant;
        }
    }
}
