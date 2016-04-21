namespace kinectExpirement
{
    partial class KinectForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KinectForm));
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.velocityScale = new System.Windows.Forms.PictureBox();
            this.recordBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.recordBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.kinectSensorClassBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblArduinoStatus = new System.Windows.Forms.Label();
            this.btnZero = new System.Windows.Forms.Button();
            this.btnOffset = new System.Windows.Forms.Button();
            this.lblOffset = new System.Windows.Forms.Label();
            this.rdioVelocity1 = new System.Windows.Forms.RadioButton();
            this.rdioVelocity2 = new System.Windows.Forms.RadioButton();
            this.rdioVelocity3 = new System.Windows.Forms.RadioButton();
            this.lblVelocityTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.velocity_label = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.velocityScale)).BeginInit();
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
            this.btnStart.Text = "Start Recording";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(9, 47);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(223, 29);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop Recording";
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
            chartArea3.AxisX.Maximum = 300D;
            chartArea3.AxisX.Minimum = 0D;
            chartArea3.AxisY.Interval = 20D;
            chartArea3.AxisY.Maximum = 160D;
            chartArea3.AxisY.Minimum = -20D;
            chartArea3.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea3);
            this.chart.Location = new System.Drawing.Point(256, 12);
            this.chart.Name = "chart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Name = "Series1";
            this.chart.Series.Add(series3);
            this.chart.Size = new System.Drawing.Size(504, 468);
            this.chart.TabIndex = 6;
            this.chart.Text = "chart2";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(10, 249);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(220, 20);
            this.txtFile.TabIndex = 10;
            // 
            // btnFileName
            // 
            this.btnFileName.Location = new System.Drawing.Point(10, 275);
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
            this.lblFileName.Location = new System.Drawing.Point(4, 336);
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
            this.btnFileTime.Location = new System.Drawing.Point(10, 307);
            this.btnFileTime.Name = "btnFileTime";
            this.btnFileTime.Size = new System.Drawing.Size(220, 26);
            this.btnFileTime.TabIndex = 13;
            this.btnFileTime.Text = "Set File Name as Time";
            this.btnFileTime.UseVisualStyleBackColor = true;
            this.btnFileTime.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 8;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // velocityScale
            // 
            this.velocityScale.ErrorImage = null;
            this.velocityScale.Image = ((System.Drawing.Image)(resources.GetObject("velocityScale.Image")));
            this.velocityScale.Location = new System.Drawing.Point(314, 486);
            this.velocityScale.Name = "velocityScale";
            this.velocityScale.Size = new System.Drawing.Size(401, 77);
            this.velocityScale.TabIndex = 17;
            this.velocityScale.TabStop = false;
            // 
            // lblArduinoStatus
            // 
            this.lblArduinoStatus.AutoSize = true;
            this.lblArduinoStatus.Location = new System.Drawing.Point(97, 121);
            this.lblArduinoStatus.Name = "lblArduinoStatus";
            this.lblArduinoStatus.Size = new System.Drawing.Size(106, 13);
            this.lblArduinoStatus.TabIndex = 18;
            this.lblArduinoStatus.Text = "Ardunio not detected";
            // 
            // btnZero
            // 
            this.btnZero.Location = new System.Drawing.Point(9, 185);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(220, 26);
            this.btnZero.TabIndex = 19;
            this.btnZero.Text = "Zero Encoder";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // btnOffset
            // 
            this.btnOffset.Location = new System.Drawing.Point(10, 217);
            this.btnOffset.Name = "btnOffset";
            this.btnOffset.Size = new System.Drawing.Size(219, 26);
            this.btnOffset.TabIndex = 20;
            this.btnOffset.Text = "Set Kinect Offset";
            this.btnOffset.UseVisualStyleBackColor = true;
            this.btnOffset.Click += new System.EventHandler(this.btnOffset_Click);
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(11, 160);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(84, 13);
            this.lblOffset.TabIndex = 21;
            this.lblOffset.Text = "Current Offset: 0";
            // 
            // rdioVelocity1
            // 
            this.rdioVelocity1.AutoSize = true;
            this.rdioVelocity1.Location = new System.Drawing.Point(9, 417);
            this.rdioVelocity1.Name = "rdioVelocity1";
            this.rdioVelocity1.Size = new System.Drawing.Size(45, 17);
            this.rdioVelocity1.TabIndex = 22;
            this.rdioVelocity1.TabStop = true;
            this.rdioVelocity1.Text = "5°/s\r\n";
            this.rdioVelocity1.UseVisualStyleBackColor = true;
            // 
            // rdioVelocity2
            // 
            this.rdioVelocity2.AutoSize = true;
            this.rdioVelocity2.Location = new System.Drawing.Point(9, 440);
            this.rdioVelocity2.Name = "rdioVelocity2";
            this.rdioVelocity2.Size = new System.Drawing.Size(51, 17);
            this.rdioVelocity2.TabIndex = 23;
            this.rdioVelocity2.TabStop = true;
            this.rdioVelocity2.Text = "10°/s";
            this.rdioVelocity2.UseVisualStyleBackColor = true;
            // 
            // rdioVelocity3
            // 
            this.rdioVelocity3.AutoSize = true;
            this.rdioVelocity3.Location = new System.Drawing.Point(9, 463);
            this.rdioVelocity3.Name = "rdioVelocity3";
            this.rdioVelocity3.Size = new System.Drawing.Size(51, 17);
            this.rdioVelocity3.TabIndex = 24;
            this.rdioVelocity3.TabStop = true;
            this.rdioVelocity3.Text = "15°/s";
            this.rdioVelocity3.UseVisualStyleBackColor = true;
            // 
            // lblVelocityTitle
            // 
            this.lblVelocityTitle.AutoSize = true;
            this.lblVelocityTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVelocityTitle.Location = new System.Drawing.Point(9, 386);
            this.lblVelocityTitle.Name = "lblVelocityTitle";
            this.lblVelocityTitle.Size = new System.Drawing.Size(174, 25);
            this.lblVelocityTitle.TabIndex = 25;
            this.lblVelocityTitle.Text = "Velocity Wanted:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Encoder Value:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // velocity_label
            // 
            this.velocity_label.AutoSize = true;
            this.velocity_label.Location = new System.Drawing.Point(7, 504);
            this.velocity_label.Name = "velocity_label";
            this.velocity_label.Size = new System.Drawing.Size(0, 13);
            this.velocity_label.TabIndex = 27;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(100, 426);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(123, 31);
            this.btnOpen.TabIndex = 28;
            this.btnOpen.Text = "Open Sensor";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // KinectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 575);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.velocity_label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblVelocityTitle);
            this.Controls.Add(this.rdioVelocity3);
            this.Controls.Add(this.rdioVelocity2);
            this.Controls.Add(this.rdioVelocity1);
            this.Controls.Add(this.lblOffset);
            this.Controls.Add(this.btnOffset);
            this.Controls.Add(this.btnZero);
            this.Controls.Add(this.lblArduinoStatus);
            this.Controls.Add(this.velocityScale);
            this.Controls.Add(this.btnFileTime);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnFileName);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "KinectForm";
            this.Text = "Elbow Angle";
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.velocityScale)).EndInit();
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
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox velocityScale;
        public System.Windows.Forms.Label lblArduinoStatus;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button btnOffset;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.RadioButton rdioVelocity1;
        private System.Windows.Forms.RadioButton rdioVelocity2;
        private System.Windows.Forms.RadioButton rdioVelocity3;
        private System.Windows.Forms.Label lblVelocityTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label velocity_label;
        private System.Windows.Forms.Button btnOpen;
    }
}
