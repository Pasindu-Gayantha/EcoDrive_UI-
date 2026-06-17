using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EcoDrive
{
    public partial class Form1 : Form
    {
        // Global variables to transfer state across tabs
        public string currentNIC = "";
        public string currentCustomerName = "";
        public string currentVehicleNo = "";
        public string currentVehicleType = "";
        public int currentBookingId = 0;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\EcoDriveDB.mdf;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            tabControlPages.SelectedTab = tabRegistration;
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            tabControlPages.SelectedTab = tabBooking;
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            tabControlPages.SelectedTab = tabPayments;
        }

        private void btnSubmitAndNext_Click(object sender, EventArgs e)
        {
           
        }

        private void btnBookSlot_Click(object sender, EventArgs e)
        {
           
        }

        private void cmbTimeSlots_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControlPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnPayAndPrint_Click(object sender, EventArgs e)
        {
           
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            tabControlPages.SelectedTab = tabAdmin;
        }

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {

        }

        private void tabControlPages_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabBooking || e.TabPage == tabPayments)
            {
                e.Cancel = true;
            }
        }

        public void UpdateDashboardChart()
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtpBookingDate.Value = DateTime.Today;
            UpdateDashboardChart();
        }

        private void dtpBookingDate_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void btnDownloadReceipt_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}