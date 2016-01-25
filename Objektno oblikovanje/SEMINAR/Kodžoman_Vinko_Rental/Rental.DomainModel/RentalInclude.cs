using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class RentalInclude
    {
        private Offer _offer;
        private int _number;

        public RentalInclude(Offer offer, int number)
        {
            this._offer = offer;
            this._number = number;
        }
    }
}
