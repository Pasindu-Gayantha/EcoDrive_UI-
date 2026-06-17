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

        //LOAD DATA GRID VIEW & COUNTERS FROM DATABASE ---
        public void LoadAdminDashboardData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // 🎯 1. Load full tabular booking tracking registry logs into DataGridView
                    string queryTable = "SELECT BookingId AS [Booking ID], NIC AS [Customer NIC], ReferenceNo AS [Reference Number], BookingDate AS [Date], TimeSlot AS [Time Slot], SlotNumber AS [Lane/Slot], Status AS [Payment Status] FROM Bookings";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(queryTable, connection))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvAdminBookings.DataSource = dt;
                    }

                    // 🎯 2. Admin Panel metrics calculations logic using Safe Date Objects
                    DateTime todayDate = DateTime.Today;

                    // Counter A: Today's Total Bookings
                    string queryTodayBookings = "SELECT COUNT(*) FROM Bookings WHERE CAST(BookingDate AS DATE) = @Today";
                    using (SqlCommand cmd = new SqlCommand(queryTodayBookings, connection))
                    {
                        cmd.Parameters.Add("@Today", SqlDbType.Date).Value = todayDate;
                        lblAdminTodayBookings.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                    }

                    // Counter B: Total Revenue (System Fee: Rs. 100 per billing instance)
                    string queryRevenue = "SELECT COUNT(*) FROM Bookings WHERE Status = 'Paid'";
                    using (SqlCommand cmd = new SqlCommand(queryRevenue, connection))
                    {
                        int systemPaidCount = Convert.ToInt32(cmd.ExecuteScalar());
                        int totalSystemRevenue = systemPaidCount * 100;
                        lblTotalRevenue.Text = $"Rs. {totalSystemRevenue:N2}";
                    }

                    // Counter C: Completed Tests Today
                    string queryCompleted = "SELECT COUNT(*) FROM Bookings WHERE Status = 'Paid' AND CAST(BookingDate AS DATE) = @Today";
                    using (SqlCommand cmd = new SqlCommand(queryCompleted, connection))
                    {
                        cmd.Parameters.Add("@Today", SqlDbType.Date).Value = todayDate;
                        lblAdminCompletedTests.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                    }

                    // Counter D: Pending Bookings Today
                    string queryPending = "SELECT COUNT(*) FROM Bookings WHERE Status = 'Pending' AND CAST(BookingDate AS DATE) = @Today";
                    using (SqlCommand cmd = new SqlCommand(queryPending, connection))
                    {
                        cmd.Parameters.Add("@Today", SqlDbType.Date).Value = todayDate;
                        lblAdminPendingBookings.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                    }

                    // Counter E: Total Registered Vehicles
                    string queryTotalVehicles = "SELECT COUNT(*) FROM CustomersAndVehicles";
                    using (SqlCommand cmd = new SqlCommand(queryTotalVehicles, connection))
                    {
                        lblAdminTotalVehicles.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                    }

                    // Counter F: Total Unique Customers
                    string queryTotalCustomers = "SELECT COUNT(DISTINCT NIC) FROM CustomersAndVehicles";
                    using (SqlCommand cmd = new SqlCommand(queryTotalCustomers, connection))
                    {
                        lblAdminTotalCustomers.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading admin data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //DATABASE SAFE BACKUP LOGIC ---
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
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "SQL Backup Files (*.bak)|*.bak";
                sfd.FileName = "EcoDriveDB_FullBackup.bak";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string dbFilePath = AppDomain.CurrentDomain.BaseDirectory + "EcoDriveDB.mdf";
                        string backupPath = sfd.FileName;

                        if (System.IO.File.Exists(dbFilePath))
                        {
                            System.IO.File.Copy(dbFilePath, backupPath, true);
                            MessageBox.Show("Database Full Backup created successfully!", "Backup Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Database primary data file (.mdf) not found!", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error creating database backup: " + ex.Message, "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        //REFRESH & SYNCHRONIZE METRICS ---
        private void btnRefreshAdmin_Click(object sender, EventArgs e)
        {
            LoadAdminDashboardData();
            MessageBox.Show("Admin data grid and summary metrics synchronized successfully!", "Refreshed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //CSV DATA EXPORT LOGIC ---
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
            if (dgvAdminBookings.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export!", "Export Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV file|*.csv", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false, Encoding.UTF8))
                        {
                            string[] headers = { "Booking ID", "Customer NIC", "Reference Number", "Date", "Time Slot", "Lane/Slot", "Payment Status" };
                            sw.WriteLine(string.Join(",", headers));

                            foreach (DataGridViewRow row in dgvAdminBookings.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    string[] cells = {
                                        row.Cells[0].Value?.ToString() ?? "",
                                        row.Cells[1].Value?.ToString() ?? "",
                                        row.Cells[2].Value?.ToString() ?? "",
                                        row.Cells[3].Value?.ToString() ?? "",
                                        row.Cells[4].Value?.ToString() ?? "",
                                        row.Cells[5].Value?.ToString() ?? "",
                                        row.Cells[6].Value?.ToString() ?? ""
                                    };
                                    sw.WriteLine(string.Join(",", cells));
                                }
                            }
                        }
                        MessageBox.Show("Data successfully exported to CSV file!", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //UNUSED DESIGNER EVENTS ---
        private void dgvAdminBookings_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void lblAdminTodayBookings_Click(object sender, EventArgs e) { }
    }
}