using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public interface IRentable
    {
        double getMonthlyPrice();
        Client getOwner();
        String getDescription();
        String getName();
    }
}
