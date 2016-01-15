namespace KinectGUI
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnFileName = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.btnFileTime = new System.Windows.Forms.Button();
            this.lblAngleTitle = new System.Windows.Forms.Label();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.lblAngleTitle2 = new System.Windows.Forms.Label();
            this.recordBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.recordBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.kinectSensorClassBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kinectSensorClassBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(9, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(223, 29);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start Sensor";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(9, 47);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(223, 29);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop Sensor";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(12, 79);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(112, 24);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Sensor is off";
            // 
            // chart
            // 
            chartArea2.AxisX.Maximum = 300D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisY.Interval = 20D;
            chartArea2.AxisY.Maximum = 160D;
            chartArea2.AxisY.Minimum = -20D;
            chartArea2.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea2);
            this.chart.Location = new System.Drawing.Point(256, 12);
            this.chart.Name = "chart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "Series1";
            this.chart.Series.Add(series2);
            this.chart.Size = new System.Drawing.Size(504, 468);
            this.chart.TabIndex = 6;
            this.chart.Text = "chart2";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(12, 169);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(220, 20);
            this.txtFile.TabIndex = 10;
            // 
            // btnFileName
            // 
            this.btnFileName.Location = new System.Drawing.Point(12, 195);
            this.btnFileName.Name = "btnFileName";
            this.btnFileName.Size = new System.Drawing.Size(220, 26);
            this.btnFileName.TabIndex = 11;
            this.btnFileName.Text = "Set File Name";
            this.btnFileName.UseVisualStyleBackColor = true;
            this.btnFileName.Click += new System.EventHandler(this.btnFileName_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Location = new System.Drawing.Point(13, 287);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(136, 18);
            this.lblFileName.TabIndex = 12;
            this.lblFileName.Text = "File name is not set";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnFileTime
            // 
            this.btnFileTime.Location = new System.Drawing.Point(12, 227);
            this.btnFileTime.Name = "btnFileTime";
            this.btnFileTime.Size = new System.Drawing.Size(220, 26);
            this.btnFileTime.TabIndex = 13;
            this.btnFileTime.Text = "Set File Name as Time";
            this.btnFileTime.UseVisualStyleBackColor = true;
            this.btnFileTime.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // lblAngleTitle
            // 
            this.lblAngleTitle.AutoSize = true;
            this.lblAngleTitle.Location = new System.Drawing.Point(13, 361);
            this.lblAngleTitle.Name = "lblAngleTitle";
            this.lblAngleTitle.Size = new System.Drawing.Size(40, 13);
            this.lblAngleTitle.TabIndex = 14;
            this.lblAngleTitle.Text = "Angle: ";
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(16, 400);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(100, 20);
            this.txtOffset.TabIndex = 15;
            this.txtOffset.Text = "155";
            // 
            // lblAngleTitle2
            // 
            this.lblAngleTitle2.AutoSize = true;
            this.lblAngleTitle2.Location = new System.Drawing.Point(12, 384);
            this.lblAngleTitle2.Name = "lblAngleTitle2";
            this.lblAngleTitle2.Size = new System.Drawing.Size(76, 13);
            this.lblAngleTitle2.TabIndex = 16;
            this.lblAngleTitle2.Text = "Starting Angle:";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // kinectSensorClassBindingSource
            // 
            this.kinectSensorClassBindingSource.DataSource = typeof(KinectGUI.KinectSensorClass);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 575);
            this.Controls.Add(this.lblAngleTitle2);
            this.Controls.Add(this.txtOffset);
            this.Controls.Add(this.lblAngleTitle);
            this.Controls.Add(this.btnFileTime);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnFileName);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Elbow Angle";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kinectSensorClassBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource kinectSensorClassBindingSource;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.BindingSource recordBindingSource;
        private System.Windows.Forms.BindingSource recordBindingSource1;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnFileName;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button btnFileTime;
        private System.Windows.Forms.Label lblAngleTitle;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.Label lblAngleTitle2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
    }
}

