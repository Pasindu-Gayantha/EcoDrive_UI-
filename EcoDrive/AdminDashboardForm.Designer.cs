namespace EcoDrive
{
    partial class AdminDashboardForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminDashboardForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.dgvAdminBookings = new System.Windows.Forms.DataGridView();
            this.btnRefreshAdmin = new System.Windows.Forms.Button();
            this.btnBackupDB = new System.Windows.Forms.Button();
            this.btnExportCSV = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblAdminTotalCustomers = new System.Windows.Forms.Label();
            this.lblAdminTotalVehicles = new System.Windows.Forms.Label();
            this.lblAdminPendingBookings = new System.Windows.Forms.Label();
            this.lblAdminCompletedTests = new System.Windows.Forms.Label();
            this.lblAdminTodayBookings = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminBookings)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 181);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(311, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Revenue (System Fees Included):";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblTotalRevenue.Location = new System.Drawing.Point(331, 181);
            this.lblTotalRevenue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(73, 24);
            this.lblTotalRevenue.TabIndex = 1;
            this.lblTotalRevenue.Text = "Rs. 0.00";
            // 
            // dgvAdminBookings
            // 
            this.dgvAdminBookings.AllowUserToAddRows = false;
            this.dgvAdminBookings.BackgroundColor = System.Drawing.Color.LightCyan;
            this.dgvAdminBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdminBookings.Location = new System.Drawing.Point(7, 223);
            this.dgvAdminBookings.Margin = new System.Windows.Forms.Padding(4);
            this.dgvAdminBookings.Name = "dgvAdminBookings";
            this.dgvAdminBookings.RowHeadersWidth = 51;
            this.dgvAdminBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdminBookings.Size = new System.Drawing.Size(1032, 306);
            this.dgvAdminBookings.TabIndex = 2;
            this.dgvAdminBookings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAdminBookings_CellContentClick);
            // 
            // btnRefreshAdmin
            // 
            this.btnRefreshAdmin.BackColor = System.Drawing.Color.SpringGreen;
            this.btnRefreshAdmin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRefreshAdmin.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshAdmin.Location = new System.Drawing.Point(0, 636);
            this.btnRefreshAdmin.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefreshAdmin.Name = "btnRefreshAdmin";
            this.btnRefreshAdmin.Size = new System.Drawing.Size(1067, 39);
            this.btnRefreshAdmin.TabIndex = 3;
            this.btnRefreshAdmin.Text = "Refresh Data";
            this.btnRefreshAdmin.UseVisualStyleBackColor = false;
            this.btnRefreshAdmin.Click += new System.EventHandler(this.btnRefreshAdmin_Click);
            // 
            // btnBackupDB
            // 
            this.btnBackupDB.BackColor = System.Drawing.Color.SpringGreen;
            this.btnBackupDB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBackupDB.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackupDB.Location = new System.Drawing.Point(0, 597);
            this.btnBackupDB.Margin = new System.Windows.Forms.Padding(4);
            this.btnBackupDB.Name = "btnBackupDB";
            this.btnBackupDB.Size = new System.Drawing.Size(1067, 39);
            this.btnBackupDB.TabIndex = 4;
            this.btnBackupDB.Text = "Backup Database";
            this.btnBackupDB.UseVisualStyleBackColor = false;
            this.btnBackupDB.Click += new System.EventHandler(this.btnBackupDB_Click_1);
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.BackColor = System.Drawing.Color.SpringGreen;
            this.btnExportCSV.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExportCSV.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCSV.Location = new System.Drawing.Point(0, 558);
            this.btnExportCSV.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(1067, 39);
            this.btnExportCSV.TabIndex = 5;
            this.btnExportCSV.Text = "Export to CSV";
            this.btnExportCSV.UseVisualStyleBackColor = false;
            this.btnExportCSV.Click += new System.EventHandler(this.btnExportCSV_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Today\'s Total Bookings";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(242, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 22);
            this.label3.TabIndex = 7;
            this.label3.Text = "Completed Tests (Today)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(495, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 22);
            this.label4.TabIndex = 8;
            this.label4.Text = "Pending Bookings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(716, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 22);
            this.label5.TabIndex = 9;
            this.label5.Text = "Total Vehicles";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(915, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 22);
            this.label6.TabIndex = 10;
            this.label6.Text = "Total Customers";
            // 
            // lblAdminTotalCustomers
            // 
            this.lblAdminTotalCustomers.AutoSize = true;
            this.lblAdminTotalCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminTotalCustomers.Location = new System.Drawing.Point(915, 137);
            this.lblAdminTotalCustomers.Name = "lblAdminTotalCustomers";
            this.lblAdminTotalCustomers.Size = new System.Drawing.Size(18, 20);
            this.lblAdminTotalCustomers.TabIndex = 15;
            this.lblAdminTotalCustomers.Text = "0";
            // 
            // lblAdminTotalVehicles
            // 
            this.lblAdminTotalVehicles.AutoSize = true;
            this.lblAdminTotalVehicles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminTotalVehicles.Location = new System.Drawing.Point(716, 137);
            this.lblAdminTotalVehicles.Name = "lblAdminTotalVehicles";
            this.lblAdminTotalVehicles.Size = new System.Drawing.Size(18, 20);
            this.lblAdminTotalVehicles.TabIndex = 14;
            this.lblAdminTotalVehicles.Text = "0";
            // 
            // lblAdminPendingBookings
            // 
            this.lblAdminPendingBookings.AutoSize = true;
            this.lblAdminPendingBookings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminPendingBookings.Location = new System.Drawing.Point(495, 137);
            this.lblAdminPendingBookings.Name = "lblAdminPendingBookings";
            this.lblAdminPendingBookings.Size = new System.Drawing.Size(18, 20);
            this.lblAdminPendingBookings.TabIndex = 13;
            this.lblAdminPendingBookings.Text = "0";
            // 
            // lblAdminCompletedTests
            // 
            this.lblAdminCompletedTests.AutoSize = true;
            this.lblAdminCompletedTests.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminCompletedTests.Location = new System.Drawing.Point(242, 137);
            this.lblAdminCompletedTests.Name = "lblAdminCompletedTests";
            this.lblAdminCompletedTests.Size = new System.Drawing.Size(18, 20);
            this.lblAdminCompletedTests.TabIndex = 12;
            this.lblAdminCompletedTests.Text = "0";
            // 
            // lblAdminTodayBookings
            // 
            this.lblAdminTodayBookings.AutoSize = true;
            this.lblAdminTodayBookings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminTodayBookings.Location = new System.Drawing.Point(20, 137);
            this.lblAdminTodayBookings.Name = "lblAdminTodayBookings";
            this.lblAdminTodayBookings.Size = new System.Drawing.Size(18, 20);
            this.lblAdminTodayBookings.TabIndex = 11;
            this.lblAdminTodayBookings.Text = "0";
            this.lblAdminTodayBookings.Click += new System.EventHandler(this.lblAdminTodayBookings_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial Narrow", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Green;
            this.label18.Location = new System.Drawing.Point(14, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(279, 22);
            this.label18.TabIndex = 64;
            this.label18.Text = "System overview and management panel.";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial Narrow", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Green;
            this.label19.Location = new System.Drawing.Point(12, 18);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(213, 33);
            this.label19.TabIndex = 63;
            this.label19.Text = "Admin Dashboard";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 16);
            this.label7.TabIndex = 62;
            this.label7.Text = "0";
            // 
            // AdminDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1067, 675);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblAdminTotalCustomers);
            this.Controls.Add(this.lblAdminTotalVehicles);
            this.Controls.Add(this.lblAdminPendingBookings);
            this.Controls.Add(this.lblAdminCompletedTests);
            this.Controls.Add(this.lblAdminTodayBookings);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExportCSV);
            this.Controls.Add(this.btnBackupDB);
            this.Controls.Add(this.btnRefreshAdmin);
            this.Controls.Add(this.dgvAdminBookings);
            this.Controls.Add(this.lblTotalRevenue);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AdminDashboardForm";
            this.Text = "AdminDashboardForm";
            this.Load += new System.EventHandler(this.AdminDashboardForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminBookings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.DataGridView dgvAdminBookings;
        private System.Windows.Forms.Button btnRefreshAdmin;
        private System.Windows.Forms.Button btnBackupDB;
        private System.Windows.Forms.Button btnExportCSV;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblAdminTotalCustomers;
        private System.Windows.Forms.Label lblAdminTotalVehicles;
        private System.Windows.Forms.Label lblAdminPendingBookings;
        private System.Windows.Forms.Label lblAdminCompletedTests;
        private System.Windows.Forms.Label lblAdminTodayBookings;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label7;
    }
}