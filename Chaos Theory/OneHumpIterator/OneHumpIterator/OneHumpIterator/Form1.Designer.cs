namespace OneHumpIterator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cbIterator = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.graph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.sliderR = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.graph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderR)).BeginInit();
            this.SuspendLayout();
            // 
            // cbIterator
            // 
            this.cbIterator.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.cbIterator.FormattingEnabled = true;
            this.cbIterator.Location = new System.Drawing.Point(105, 17);
            this.cbIterator.Name = "cbIterator";
            this.cbIterator.Size = new System.Drawing.Size(184, 34);
            this.cbIterator.TabIndex = 0;
            this.cbIterator.SelectedIndexChanged += new System.EventHandler(this.cbIterator_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Iterator:";
            // 
            // graph
            // 
            chartArea3.Name = "ChartArea1";
            this.graph.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.graph.Legends.Add(legend3);
            this.graph.Location = new System.Drawing.Point(16, 98);
            this.graph.Name = "graph";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.graph.Series.Add(series3);
            this.graph.Size = new System.Drawing.Size(609, 416);
            this.graph.TabIndex = 2;
            this.graph.Text = "graph";
            // 
            // sliderR
            // 
            this.sliderR.Location = new System.Drawing.Point(322, 12);
            this.sliderR.Name = "sliderR";
            this.sliderR.Size = new System.Drawing.Size(303, 45);
            this.sliderR.TabIndex = 3;
            this.sliderR.ValueChanged += new System.EventHandler(this.sliderR_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 526);
            this.Controls.Add(this.sliderR);
            this.Controls.Add(this.graph);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbIterator);
            this.Name = "Form1";
            this.Text = "OneHumpIterators";
            ((System.ComponentModel.ISupportInitialize)(this.graph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbIterator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart graph;
        private System.Windows.Forms.TrackBar sliderR;
    }
}

