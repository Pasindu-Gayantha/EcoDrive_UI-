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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

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

        private void btnAdminLogin_Click_1(object sender, EventArgs e)
        {
            // 1. chiking empty boxes
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please enter both Username and Password!", "Required Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Cheking Username Password 
            if (txtUsername.Text == "admin" && txtPassword.Text == "admin123")
            {
                MessageBox.Show("Login Successful! Welcome to Admin Panel.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Admin Dashboard Form එක ඕපන් කරනවා
                AdminDashboardForm dashboard = new AdminDashboardForm();
                dashboard.Show();

              
                 this.Hide(); 
            }
            else
            {
                // 3. Showing Error massage
                MessageBox.Show("Wrong Username or Password! Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtPassword.Clear(); 
                txtPassword.Focus(); 
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Enter Username...")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black; 
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                txtUsername.Text = "Enter Username...";
                txtUsername.ForeColor = Color.Gray;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Enter Password...")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black; 

                
                txtPassword.UseSystemPasswordChar = !checkBoxShowPassword.Checked;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.Text = "Enter Password...";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.UseSystemPasswordChar = false; 
            }
        }
    }
}