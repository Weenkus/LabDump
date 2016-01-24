using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class Apartment : Rental
    {
        private List<SpecialFeatures> _features;
        private String _postalCode;
        private String _address;
    }
}
