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
            FormEmployeeView f = new FormEmployeeView(this);
            f.Show();
        }
    }
}
