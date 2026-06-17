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
using System.Windows.Forms.DataVisualization.Charting;

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
            tabControlPages.SelectedTab = tabDashboard;
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
            string query = "INSERT INTO CustomersAndVehicles (NIC, CustomerName, Phone, VehicleNo, VehicleType, EngineCC, FuelType) " +
                            "VALUES (@NIC, @Name, @Phone, @VehicleNo, @VehicleType, @EngineCC, @FuelType)";

            if (string.IsNullOrEmpty(txtNIC.Text) || string.IsNullOrEmpty(txtCustomerName.Text) || string.IsNullOrEmpty(txtVehicleNo.Text))
            {
                MessageBox.Show("Please fill all the required fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NIC", txtNIC.Text.Trim());
                    command.Parameters.AddWithValue("@Name", txtCustomerName.Text.Trim());
                    command.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                    command.Parameters.AddWithValue("@VehicleNo", txtVehicleNo.Text.Trim());
                    command.Parameters.AddWithValue("@VehicleType", cmbVehicleType.SelectedItem?.ToString() ?? "");
                    command.Parameters.AddWithValue("@EngineCC", int.Parse(txtEngineCC.Text.Trim()));
                    command.Parameters.AddWithValue("@FuelType", cmbFuelType.SelectedItem?.ToString() ?? "");

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Customer & Vehicle Registered Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Capture values for the next step summary strip (Step 3 UX)
                        currentNIC = txtNIC.Text.Trim();
                        currentCustomerName = txtCustomerName.Text.Trim();
                        currentVehicleNo = txtVehicleNo.Text.Trim();
                        currentVehicleType = cmbVehicleType.SelectedItem?.ToString() ?? "";

                        // Bind labels in Booking Tab Summary Strip dynamically
                        lblBookSummaryNIC.Text = $"Customer NIC: {currentNIC}";
                        lblBookSummaryName.Text = $"Name: {currentCustomerName}";
                        lblBookSummaryVehicle.Text = $"Vehicle: {currentVehicleNo}";
                        lblBookSummaryType.Text = $"Type: {currentVehicleType}";

                        // Redirect smoothly
                        tabControlPages.Selecting -= tabControlPages_Selecting;
                        tabControlPages.SelectedTab = tabBooking;
                        tabControlPages.Selecting += tabControlPages_Selecting;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while saving data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBookSlot_Click(object sender, EventArgs e)
        {
            if (cmbTimeSlots.SelectedItem == null)
            {
                MessageBox.Show("Please select a time slot!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedDate = dtpBookingDate.Value.ToString("yyyy-MM-dd");
            string selectedTime = cmbTimeSlots.SelectedItem.ToString();
            int allocatedSlot = -1;

            if (rdoSlot2.Checked) allocatedSlot = 2;
            else if (rdoSlot3.Checked) allocatedSlot = 3;
            else if (rdoSlot4.Checked) allocatedSlot = 4;
            else if (rdoSlot5.Checked) allocatedSlot = 5;

            if (allocatedSlot == -1)
            {
                MessageBox.Show("Please select an available Slot/Lane to book!", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Bookings (NIC, BookingDate, TimeSlot, SlotNumber, Status) " +
                                         "VALUES (@NIC, @Date, @Time, @Slot, @Status)";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@NIC", currentNIC);
                        insertCommand.Parameters.AddWithValue("@Date", selectedDate);
                        insertCommand.Parameters.AddWithValue("@Time", selectedTime);
                        insertCommand.Parameters.AddWithValue("@Slot", allocatedSlot);
                        insertCommand.Parameters.AddWithValue("@Status", "Pending"); // Matches Step 3 workflow state

                        insertCommand.ExecuteNonQuery();

                        string idQuery = "SELECT TOP 1 BookingId FROM Bookings ORDER BY BookingId DESC";
                        using (SqlCommand idCommand = new SqlCommand(idQuery, connection))
                        {
                            currentBookingId = Convert.ToInt32(idCommand.ExecuteScalar());
                        }
                    }

                    MessageBox.Show($"Slot {allocatedSlot} Booked Successfully! Status marked as Pending.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    tabControlPages.Selecting -= tabControlPages_Selecting;
                    tabControlPages.SelectedTab = tabPayments;
                    tabControlPages.Selecting += tabControlPages_Selecting;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during booking: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbTimeSlots_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControlPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnPayAndPrint_Click(object sender, EventArgs e)
        {
            if (cmbPaymentMethod.SelectedItem == null)
            {
                MessageBox.Show("Please select a Payment Method (Cash/Card)!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string method = cmbPaymentMethod.SelectedItem.ToString();
            string refNumber = "REF" + DateTime.Now.ToString("yyyyMMdd") + currentBookingId.ToString("D4");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string updateQuery = "UPDATE Bookings SET Status = 'Paid', ReferenceNo = @RefNo WHERE BookingId = @BookingId";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@BookingId", currentBookingId);
                        command.Parameters.AddWithValue("@RefNo", refNumber);
                        command.ExecuteNonQuery();
                    }

                    // Prepares preview block layout exactly mirroring step 4 panel specifications
                    rtxtReceipt.Clear();
                    rtxtReceipt.AppendText("=========================================\n");
                    rtxtReceipt.AppendText("         ECODRIVE EMISSION CENTER        \n");
                    rtxtReceipt.AppendText("             OFFICIAL RECEIPT            \n");
                    rtxtReceipt.AppendText("=========================================\n\n");
                    rtxtReceipt.AppendText($" Date/Time   : {DateTime.Now.ToString()}\n");
                    rtxtReceipt.AppendText($" Reference No: {refNumber}\n");
                    rtxtReceipt.AppendText($" Customer NIC: {currentNIC}\n");
                    rtxtReceipt.AppendText($" Vehicle No  : {currentVehicleNo}\n");
                    rtxtReceipt.AppendText($" Vehicle Type: {currentVehicleType}\n");
                    rtxtReceipt.AppendText($" Allocated    : {lblSummarySlot.Text}\n");
                    rtxtReceipt.AppendText($" Payment via : {method}\n");
                    rtxtReceipt.AppendText("-----------------------------------------\n");
                    rtxtReceipt.AppendText($" TOTAL PAID  : {lblSummaryAmount.Text}\n");
                    rtxtReceipt.AppendText("-----------------------------------------\n\n");
                    rtxtReceipt.AppendText("   Please present this receipt and your   \n");
                    rtxtReceipt.AppendText("   original NIC at the testing counter.  \n");
                    rtxtReceipt.AppendText("=========================================\n");

                    MessageBox.Show("Payment Successful! Booking Status updated to Completed/Paid.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateDashboardChart();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating payment: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void CustomizeChartAppearance(Series series)
        {
            // Make the bars a bit thicker
            series["PointWidth"] = "0.8";

            series.Points[0].Color = Color.FromArgb(18, 201, 24);  
            series.Points[1].Color = Color.FromArgb(18, 201, 24);  
            series.Points[2].Color = Color.FromArgb(201, 18, 55);  
            series.Points[3].Color = Color.FromArgb(201, 18, 55);

            // Hide the ugly grid lines on the chart background for a cleaner look
            barChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            barChart.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        }


        //---------------load bar chart data-------------------
        private void loadBarChart()
        {
            dtpBookingDate.Value = DateTime.Today;

            // 1. Clear existing data
            barChart.Series.Clear();
            barChart.Titles.Clear();

            // 2. Create the data series
            Series emissionSeries = new Series("Bookings");
            emissionSeries.ChartType = SeriesChartType.Column;

            // 3. Variables to hold our real data counts (Default to 0)
            int lightSlot1Count = 0; // Slot 2
            int lightSlot2Count = 0; // Slot 3
            int heavySlot1Count = 0; // Slot 4
            int heavySlot2Count = 0; // Slot 5

            string query = "SELECT SlotNumber, COUNT(SlotNumber) AS TotalBookings FROM Bookings GROUP BY SlotNumber";

            // 5. Connect to the database and execute the query
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Read each row returned by the database
                            while (reader.Read())
                            {
                                // Get the slot number and the total count for that slot
                                int slotNum = Convert.ToInt32(reader["SlotNumber"]);
                                int count = Convert.ToInt32(reader["TotalBookings"]);

                                // Match the database SlotNumber to the correct variable
                                switch (slotNum)
                                {
                                    case 2: lightSlot1Count = count; break;
                                    case 3: lightSlot2Count = count; break;
                                    case 4: heavySlot1Count = count; break;
                                    case 5: heavySlot2Count = count; break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading chart data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // 6. Add the retrieved data points to the chart
            emissionSeries.Points.AddXY("Light slot 1", lightSlot1Count);
            emissionSeries.Points.AddXY("Light slot 2", lightSlot2Count);
            emissionSeries.Points.AddXY("Heavy slot 1", heavySlot1Count);
            emissionSeries.Points.AddXY("Heavy slot 2", heavySlot2Count);

            // 7. Assign the series to your barChart control
            barChart.Series.Add(emissionSeries);

            // Visual styling
            CustomizeChartAppearance(emissionSeries);
        }

        private void loadPeiChart()
        {
            // 1. Clear any default or placeholder data
            pieChart.Series.Clear();
            pieChart.Titles.Clear();
            pieChart.Legends.Clear();

            // 2. Set up the Legend on the right side
            Legend legend = pieChart.Legends.Add("SlotsLegend");
            legend.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            legend.BackColor = System.Drawing.Color.Transparent;

            // 3. Create the Data Series and set to Donut style
            Series donutSeries = new Series("Slots");
            donutSeries.ChartType = SeriesChartType.Doughnut;
            donutSeries["DoughnutRadius"] = "60";

            // 4. Variables to hold our real data counts from the database
            int slot2Count = 0;
            int slot3Count = 0;
            int slot4Count = 0;
            int slot5Count = 0;

            // 5. Connect to the database and get the counts
            string query = "SELECT SlotNumber, COUNT(SlotNumber) AS TotalBookings FROM Bookings GROUP BY SlotNumber";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Get the slot number and the total count
                                int slotNum = Convert.ToInt32(reader["SlotNumber"]);
                                int count = Convert.ToInt32(reader["TotalBookings"]);

                                // Match the database SlotNumber to our variables
                                switch (slotNum)
                                {
                                    case 2: slot2Count = count; break;
                                    case 3: slot3Count = count; break;
                                    case 4: slot4Count = count; break;
                                    case 5: slot5Count = count; break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading pie chart data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // 6. Add the retrieved data points to the chart
            donutSeries.Points.AddXY("Slot 2 (Light)", slot2Count);
            donutSeries.Points.AddXY("Slot 3 (Light)", slot3Count);
            donutSeries.Points.AddXY("Slot 4 (Heavy)", slot4Count);
            donutSeries.Points.AddXY("Slot 5 (Heavy)", slot5Count);

            // 7. Styling the slices individually (Your custom colors)
            donutSeries.Points[0].Color = System.Drawing.Color.FromArgb(46, 204, 113);  // Emerald Green
            donutSeries.Points[1].Color = System.Drawing.Color.FromArgb(52, 152, 219);  // Peter River Blue
            donutSeries.Points[2].Color = System.Drawing.Color.FromArgb(155, 89, 182);  // Amethyst Purple
            donutSeries.Points[3].Color = System.Drawing.Color.FromArgb(241, 196, 15);  // Sunflower Yellow

            // 8. Display data values directly on the slices
            donutSeries.IsValueShownAsLabel = true;
            donutSeries.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            donutSeries.LabelForeColor = System.Drawing.Color.White;

         
            pieChart.Series.Add(donutSeries);
        }

        private void loadTotalBookings()
        {

            
            string query = "SELECT COUNT(*) FROM Bookings";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();

                        
                        int totalCount = Convert.ToInt32(cmd.ExecuteScalar());

                        
                        totalBookings.Text = totalCount.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading total bookings: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void loadOccupiedSlots()
        {
            string query = @"
        SELECT COUNT(*) 
        FROM (
            SELECT SlotNumber 
            FROM Bookings 
            GROUP BY SlotNumber 
            HAVING COUNT(SlotNumber) = 42
        ) AS FullSlots";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();

                        // ExecuteScalar grabs that single number representing the fully booked slots
                        int fullSlotsCount = Convert.ToInt32(cmd.ExecuteScalar());

                        
                        slotsOcc.Text = $"{fullSlotsCount}/4";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading occupied slots: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void UpdateDashboard()
        {
            loadBarChart();
            loadPeiChart();
            loadTotalBookings();
            loadOccupiedSlots();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtpBookingDate.Value = DateTime.Today;
            UpdateDashboardChart();
        }

        private void dtpBookingDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpBookingDate.Value.Date != DateTime.Today)
            {
                MessageBox.Show("Direct booking is restricted to TODAY only! For future reservations, please contact our helpline.", "Date Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBookingDate.Value = DateTime.Today;
            }
        }

        private void btnDownloadReceipt_Click_1(object sender, EventArgs e)
        {
           
        }

        private void cmbTimeSlots_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtxtReceipt.Text))
            {
                MessageBox.Show("No receipt generated yet to download!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text file|*.txt", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(sfd.FileName, rtxtReceipt.Text, Encoding.UTF8);
                        MessageBox.Show("Receipt successfully downloaded to your computer!", "Download Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving receipt file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}