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
    public partial class MainForm : Form
    {
        private IController _controller;

        public MainForm(IController con)
        {
            _controller = con;
            InitializeComponent();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ShowEmplyoees();
        }

        private void viewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _controller.ShowClients();
        }

        private void viewToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _controller.ShowApartmants();
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _controller.AddClient();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.AddEmployee();
        }
    }
}
