using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rental
{
    public partial class FormEmployeeView : Form
    {
        private IController _controller;
        private PersonRepository _repo;

        public FormEmployeeView(IController con)
        {
            _controller = con;
            InitializeComponent();
        }
    }
}
