using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneHumpIterator
{
    public partial class Form1 : Form
    {
        // Content item for the combo box
        private class OneHumpIterator
        {
            public string name;
            public int id;
            public OneHumpIterator(string name, int id)
            {
                this.name = name; this.id = id;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return this.name;
            }

        }

        public Form1()
        {
            InitializeComponent();
            initialiseTheComboBox();
            initialiseTheGraph();
            initialiseTheSlider();
        }

        private void cbIterator_SelectedIndexChanged(object sender, EventArgs e)
        {
            draw();
        }

        private void sliderR_ValueChanged(object sender, EventArgs e)
        {
            draw();
        }

        private void draw()
        {
            // Clear all previous compositions
            graph.Series[1].Points.Clear();
            graph.Series[2].Points.Clear();
            graph.Series[3].Points.Clear();

            double r = (double)sliderR.Value / 100;
            if (cbIterator.SelectedItem.ToString().Equals("r*x*(1-x)"))
            {
                for (double x = 0; x <= 1; x = x + 0.001)
                {
                    // f(x)
                    graph.Series[1].Points.AddXY(x, (r * x * (1 - x)));

                    // f^2(x)
                    graph.Series[2].Points.AddXY(x, -r * r * (x - 1) * x * (r * x * x - r * x + 1));

                    // f^4(x)
                    graph.Series[3].Points.AddXY(x, r * r * r * r * (x - 1) * x * (r * (x - 1) * x + 1) * (r * r * (-(x - 1)) * x * (r * (x - 1) * x + 1) - 1) * (r * r * r * r * r * (x - 1) * (x - 1) * x * x * (r * (x - 1) * x + 1) * (r * (x - 1) * x + 1) + r * r * r * (x - 1) * x * (r * (x - 1) * x + 1) + 1));

                    // f^8(x)

                }
            }
            else if (cbIterator.SelectedItem.ToString().Equals("r*x*sqrt(1-x)"))
            {
                for (double x = 0; x <= 1; x = x + 0.001)
                {
                    // f(x)
                    graph.Series[1].Points.AddXY(x, (r * x * Math.Sqrt(1 - x)));

                    // f^2(x)
                    graph.Series[2].Points.AddXY(x, r * r * Math.Sqrt(1 - x) * x * Math.Sqrt(1 - r * Math.Sqrt(1 - x) * x));

                    // f^4(x)
                    graph.Series[3].Points.AddXY(x, r * r * r * r * Math.Sqrt(1 - x) * x * Math.Sqrt(1 - r * Math.Sqrt(1 - x) * x) * Math.Sqrt(1 - r * r * Math.Sqrt(1 - x) * x * Math.Sqrt(1 - r * Math.Sqrt(1 - x) * x)) * Math.Sqrt(1 - r * r * r * Math.Sqrt(1 - x) * x * Math.Sqrt(1 - r * Math.Sqrt(1 - x) * x) * Math.Sqrt(1 - r * r * Math.Sqrt(1 - x) * x * Math.Sqrt(1 - r * Math.Sqrt(1 - x) * x))));

                    // f^8(x)
                }
            }
            else if (cbIterator.SelectedItem.ToString().Equals("r - (x*x)")) {
                for (double x = 0; x <= 1; x = x + 0.001)
                {
                    // f(x)
                    graph.Series[1].Points.AddXY(x, r-x*x);

                    // f^2(x)
                    graph.Series[2].Points.AddXY(x, x*x*(2*r - x*x) + (1 - r)*r);

                    // f^4(x)
                    graph.Series[3].Points.AddXY(x, (-2*r*x*x + (r - 1)*r + x*x*x*x)* (-2 * r * x * x + (r - 1) * r + x * x * x * x)*(2*r - (-2*r*x*x + (r - 1)*r + x*x*x*x)* (-2 * r * x * x + (r - 1) * r + x * x * x * x))-(r - 1)*r);

                    // f^8(x)
                }
            }


        }



        private void initialiseTheGraph() {
            // Initialise the graph
            graph.Series.Clear();
            graph.Series.Add("f(x) = x");
            graph.Series.Add("f(x)");
            graph.Series.Add("f^2(x)");
            graph.Series.Add("f^4(x)");
            graph.Series.Add("f^8(x)");

            // Set the axis to proper values
            graph.ChartAreas[0].AxisX.Minimum = 0;
            graph.ChartAreas[0].AxisX.Maximum = 1;

            graph.ChartAreas[0].AxisY.Minimum = 0;
            graph.ChartAreas[0].AxisY.Maximum = 1;


            // Add the linear line
            graph.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            graph.Series[0].Points.AddXY(0, 0);
            graph.Series[0].Points.AddXY(1, 1);

            // Set function typesg
            graph.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            graph.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            graph.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            graph.Series[4].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        }

        private void initialiseTheComboBox() {
            // Fill the comobo box with one hump iterator equations
            cbIterator.Items.Add(new OneHumpIterator("r*x*(1-x)", 0));
            cbIterator.Items.Add(new OneHumpIterator("r*x*sqrt(1-x)", 1));
            cbIterator.Items.Add(new OneHumpIterator("r - (x*x)", 2));
            cbIterator.Items.Add(new OneHumpIterator("r*x*exp(-x)", 3));
        }

        private void initialiseTheSlider() {
            sliderR.SetRange(0, 1000);
        }


        

    }
}
