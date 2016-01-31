using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public interface IController
    {
        void ShowEmplyoees();
        void ShowClients();
        void ShowApartmants();
        void AddClient();
        void AddEmployee();
        void EditClient();
    }
}
