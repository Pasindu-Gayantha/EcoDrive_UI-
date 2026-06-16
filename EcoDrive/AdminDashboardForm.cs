using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoDrive
{
    public partial class AdminDashboardForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\EcoDriveDB.mdf;Integrated Security=True";

        public AdminDashboardForm()
        {
            InitializeComponent();
        }

        private void AdminDashboardForm_Load_1(object sender, EventArgs e)
        {
            LoadAdminDashboardData();
        }

        public void LoadAdminDashboardData()
        {
            
        }

        private void btnBackupDB_Click(object sender, EventArgs e)
        {
            ExecuteSafeBackup();
        }

        private void btnBackupDB_Click_1(object sender, EventArgs e)
        {
            ExecuteSafeBackup();
        }

        private void ExecuteSafeBackup()
        {
            
        }

        private void btnRefreshAdmin_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            ExecuteCSVExport();
        }

        private void btnExportCSV_Click_1(object sender, EventArgs e)
        {
            ExecuteCSVExport();
        }

        private void ExecuteCSVExport()
        {
            
        }

        private void dgvAdminBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblAdminTodayBookings_Click(object sender, EventArgs e)
        {

        }
    }
}