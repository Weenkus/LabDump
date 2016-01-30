using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class AppController : IController
    {
        public void ShowEmplyoees()
        {
            FormEmployeeView f = new FormEmployeeView(this, PersonRepository.Instance);
            f.ShowDialog();
        }

        public void ShowClients()
        {
            FormClientView f = new FormClientView(this, PersonRepository.Instance);
            f.ShowDialog();
        }

        public void ShowApartmants()
        {
            FormApartmantView f = new FormApartmantView(this, RentalRepository.Instance);
            f.ShowDialog();
        }

        public void AddClient()
        {
            FormClientAdd f = new FormClientAdd(this, PersonRepository.Instance);
            f.ShowDialog();
        }


    }
}
