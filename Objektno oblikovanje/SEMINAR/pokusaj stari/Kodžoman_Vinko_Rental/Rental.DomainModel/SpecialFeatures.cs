using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class SpecialFeatures
    {
        private double _price;
        private String _description;

        public SpecialFeatures(Double price, String description)
        {
            this._price = price;
            this._description = description;
        }
    }
}
