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

        private void Form1_Load(object sender, EventArgs e)
        {
            dtpBookingDate.Value = DateTime.Today;
            UpdateDashboard();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            tabControlPages.SelectedTab = tabDashboard;
            UpdateDashboard();
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

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            tabControlPages.SelectedTab = tabAdmin;
        }

        //CUSTOMER AND VEHICLE REGISTRATION LOGIC ---
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

        //STEP 3: SLOT BOOKING LOGIC ---
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
                        insertCommand.Parameters.AddWithValue("@Status", "Pending");

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

        // Llive Blocking Logic inside the active dynamic TimeSlot drop-down handler
        private void cmbTimeSlots_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbTimeSlots.SelectedItem == null) return;

            DateTime selectedDateTime = dtpBookingDate.Value.Date;
            string rawTime = cmbTimeSlots.SelectedItem.ToString().Trim();
            string selectedTime = rawTime;

            // Normalize formats safely (e.g. "9:00 AM" to "09:00 AM") to prevent database string mismatches
            if (DateTime.TryParse(rawTime, out DateTime parsedTime))
            {
                selectedTime = parsedTime.ToString("hh:mm tt");
            }

            // 1. Reset ONLY eligible lane pairs back to Green based on registered vehicle type specifications
            if (currentVehicleType == "Light Vehicle")
            {
                rdoSlot2.Enabled = true; rdoSlot2.BackColor = Color.LightGreen; rdoSlot2.Text = "Machine/Slot 2 (Light Vehicle's owner need to select)";
                rdoSlot3.Enabled = true; rdoSlot3.BackColor = Color.LightGreen; rdoSlot3.Text = "Machine/Slot 3 (Light Vehicle's owner need to select)";
                rdoSlot4.Enabled = false; rdoSlot4.BackColor = Color.Gray; rdoSlot4.Text = "Machine/Slot 4 (Heavy Vehicle) - Blocked";
                rdoSlot5.Enabled = false; rdoSlot5.BackColor = Color.Gray; rdoSlot5.Text = "Machine/Slot 5 (Heavy Vehicle) - Blocked";
            }
            else if (currentVehicleType == "Heavy Vehicle")
            {
                rdoSlot4.Enabled = true; rdoSlot4.BackColor = Color.LightGreen; rdoSlot4.Text = "Machine/Slot 4 (Heavy Vehicle's owner need to select)";
                rdoSlot5.Enabled = true; rdoSlot5.BackColor = Color.LightGreen; rdoSlot5.Text = "Machine/Slot 5 (Heavy Vehicle's owner need to select)";
                rdoSlot2.Enabled = false; rdoSlot2.BackColor = Color.Gray; rdoSlot2.Text = "Machine/Slot 2 (Light Vehicle) - Blocked";
                rdoSlot3.Enabled = false; rdoSlot3.BackColor = Color.Gray; rdoSlot3.Text = "Machine/Slot 3 (Light Vehicle) - Blocked";
            }

            // Walk-in configurations are explicitly forced into locked states
            rdoSlot1.Enabled = false; rdoSlot1.BackColor = Color.Gray; rdoSlot1.Text = "Machine/Slot 1 (Walk-in CANNOT BOOK)";
            rdoSlot1.Checked = false; rdoSlot2.Checked = false; rdoSlot3.Checked = false; rdoSlot4.Checked = false; rdoSlot5.Checked = false;

            // 2. Query DB live with strict date and string-matching synchronization
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string checkQuery = "SELECT SlotNumber FROM Bookings WHERE CAST(BookingDate AS DATE) = @Date AND (TimeSlot = @Time OR TimeSlot LIKE @TimeLike)";

                    using (SqlCommand command = new SqlCommand(checkQuery, connection))
                    {
                        command.Parameters.Add("@Date", SqlDbType.Date).Value = selectedDateTime;
                        command.Parameters.AddWithValue("@Time", selectedTime);
                        command.Parameters.AddWithValue("@TimeLike", "%" + rawTime + "%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int takenSlot = Convert.ToInt32(reader["SlotNumber"]);

                                // Instantly mask pre-allocated selections into locked gray states for the next incoming user session
                                if (takenSlot == 2) { rdoSlot2.Enabled = false; rdoSlot2.BackColor = Color.Gray; rdoSlot2.Text = "Machine/Slot 2 - Already Booked"; }
                                if (takenSlot == 3) { rdoSlot3.Enabled = false; rdoSlot3.BackColor = Color.Gray; rdoSlot3.Text = "Machine/Slot 3 - Already Booked"; }
                                if (takenSlot == 4) { rdoSlot4.Enabled = false; rdoSlot4.BackColor = Color.Gray; rdoSlot4.Text = "Machine/Slot 4 - Already Booked"; }
                                if (takenSlot == 5) { rdoSlot5.Enabled = false; rdoSlot5.BackColor = Color.Gray; rdoSlot5.Text = "Machine/Slot 5 - Already Booked"; }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading slots: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //PAYMENTS AND RECEIPT GENERATION ---
        private void tabControlPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            // PART 1: SLOT BOOKING TAB INITIAL STATIC BLOCKING ===
            if (tabControlPages.SelectedTab == tabBooking)
            {
                rdoSlot1.Enabled = false; rdoSlot1.BackColor = Color.Gray; rdoSlot1.Text = "Machine/Slot 1 (Walk-in CANNOT BOOK)";
                rdoSlot2.Enabled = true; rdoSlot2.BackColor = Color.LightGreen; rdoSlot2.Text = "Machine/Slot 2 (Light Vehicle's owner need to select)";
                rdoSlot3.Enabled = true; rdoSlot3.BackColor = Color.LightGreen; rdoSlot3.Text = "Machine/Slot 3 (Light Vehicle's owner need to select)";
                rdoSlot4.Enabled = true; rdoSlot4.BackColor = Color.LightGreen; rdoSlot4.Text = "Machine/Slot 4 (Heavy Vehicle's owner need to select)";
                rdoSlot5.Enabled = true; rdoSlot5.BackColor = Color.LightGreen; rdoSlot5.Text = "Machine/Slot 5 (Heavy Vehicle's owner need to select)";

                rdoSlot1.Checked = false; rdoSlot2.Checked = false; rdoSlot3.Checked = false; rdoSlot4.Checked = false; rdoSlot5.Checked = false;

                if (currentVehicleType == "Light Vehicle")
                {
                    rdoSlot4.Enabled = false; rdoSlot4.BackColor = Color.Gray;
                    rdoSlot5.Enabled = false; rdoSlot5.BackColor = Color.Gray;
                }
                else if (currentVehicleType == "Heavy Vehicle")
                {
                    rdoSlot2.Enabled = false; rdoSlot2.BackColor = Color.Gray;
                    rdoSlot3.Enabled = false; rdoSlot3.BackColor = Color.Gray;
                }
            }

            // PART 2: PAYMENTS SUMMARY LOGIC ===
            if (tabControlPages.SelectedTab == tabPayments)
            {
                lblSummaryNIC.Text = currentNIC;
                lblSummaryName.Text = currentCustomerName;
                lblSummaryVehicleNo.Text = currentVehicleNo;
                lblSummaryVehicleType.Text = currentVehicleType;
                lblSummaryDate.Text = dtpBookingDate.Value.ToString("yyyy-MM-dd");
                lblSummaryTime.Text = cmbTimeSlots.SelectedItem?.ToString() ?? "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "SELECT SlotNumber FROM Bookings WHERE BookingId = @BookingId";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@BookingId", currentBookingId);
                            var result = command.ExecuteScalar();
                            if (result != null) lblSummarySlot.Text = $"Slot {result} ({(int.Parse(result.ToString()) <= 3 ? "Light" : "Heavy")})";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading payment summary: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                int baseFee = (currentVehicleType == "Light Vehicle") ? 1500 : 2000;
                int systemFee = 100;
                int totalAmount = baseFee + systemFee;

                lblBaseTestFee.Text = $"Rs. {baseFee}.00";
                lblAdditionalSystemFee.Text = $"Rs. {systemFee}.00";
                lblSummaryAmount.Text = $"Rs. {totalAmount:N2}";
            }
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
                    rtxtReceipt.AppendText($" Allocated    : Slot " + currentBookingId + "\n");
                    rtxtReceipt.AppendText($" Payment via : {method}\n");
                    rtxtReceipt.AppendText("-----------------------------------------\n");
                    int finalAmt = (currentVehicleType == "Light Vehicle") ? 1600 : 2100;
                    rtxtReceipt.AppendText($" TOTAL PAID  : Rs. " + finalAmt + ".00\n");
                    rtxtReceipt.AppendText("-----------------------------------------\n\n");
                    rtxtReceipt.AppendText("   Please present this receipt and your   \n");
                    rtxtReceipt.AppendText("   original NIC at the testing counter.  \n");
                    rtxtReceipt.AppendText("=========================================\n");

                    MessageBox.Show("Payment Successful! Booking Status updated to Completed/Paid.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateDashboard();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating payment: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Active save button file exporter
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

        // Decoupled admin dashboard form invoker
        private void btnAdminLogin_Click_1(object sender, EventArgs e)
        {
            string inputUsername = txtAdminUsername.Text.Trim();
            string inputPassword = txtAdminPassword.Text;

            if ((inputUsername == "admin" && inputPassword == "admin123") ||
                (inputUsername == "admin1" && inputPassword == "pm123"))
            {
                MessageBox.Show("Welcome back, Administrator!", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAdminUsername.Clear();
                txtAdminPassword.Clear();

                AdminDashboardForm adminForm = new AdminDashboardForm();
                adminForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password! Access Denied.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdminPassword.Clear();
                txtAdminUsername.Focus();
            }
        }

        private void tabControlPages_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabBooking || e.TabPage == tabPayments)
            {
                e.Cancel = true;
            }
        }

        private void dtpBookingDate_ValueChanged_1(object sender, EventArgs e)
        {
            if (dtpBookingDate.Value.Date != DateTime.Today)
            {
                MessageBox.Show("You can only select today only!", "Date Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBookingDate.Value = DateTime.Today;
            }
        }

        //PUBLIC DASHBOARD LIVE UPDATES ---
        public void UpdateDashboard()
        {
            loadBarChart();
            loadPeiChart();
            loadTotalBookings();
            loadOccupiedSlots();
        }

        private void CustomizeChartAppearance(Series series)
        {
            series["PointWidth"] = "0.8";
            series.Points[0].Color = Color.FromArgb(18, 201, 24);
            series.Points[1].Color = Color.FromArgb(18, 201, 24);
            series.Points[2].Color = Color.FromArgb(201, 18, 55);
            series.Points[3].Color = Color.FromArgb(201, 18, 55);

            barChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            barChart.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
        }

        private void loadBarChart()
        {
            barChart.Series.Clear();
            barChart.Titles.Clear();

            Series emissionSeries = new Series("Bookings");
            emissionSeries.ChartType = SeriesChartType.Column;

            int lightSlot1Count = 0; int lightSlot2Count = 0;
            int heavySlot1Count = 0; int heavySlot2Count = 0;

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
                                int slotNum = Convert.ToInt32(reader["SlotNumber"]);
                                int count = Convert.ToInt32(reader["TotalBookings"]);

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

            emissionSeries.Points.AddXY("Light slot 1", lightSlot1Count);
            emissionSeries.Points.AddXY("Light slot 2", lightSlot2Count);
            emissionSeries.Points.AddXY("Heavy slot 1", heavySlot1Count);
            emissionSeries.Points.AddXY("Heavy slot 2", heavySlot2Count);

            barChart.Series.Add(emissionSeries);
            CustomizeChartAppearance(emissionSeries);
        }

        private void loadPeiChart()
        {
            pieChart.Series.Clear();
            pieChart.Titles.Clear();
            pieChart.Legends.Clear();

            Legend legend = pieChart.Legends.Add("SlotsLegend");
            legend.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            legend.BackColor = System.Drawing.Color.Transparent;

            Series donutSeries = new Series("Slots");
            donutSeries.ChartType = SeriesChartType.Doughnut;
            donutSeries["DoughnutRadius"] = "60";

            int slot2Count = 0; int slot3Count = 0;
            int slot4Count = 0; int slot5Count = 0;

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
                                int slotNum = Convert.ToInt32(reader["SlotNumber"]);
                                int count = Convert.ToInt32(reader["TotalBookings"]);

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

            donutSeries.Points.AddXY("Slot 2 (Light)", slot2Count);
            donutSeries.Points.AddXY("Slot 3 (Light)", slot3Count);
            donutSeries.Points.AddXY("Slot 4 (Heavy)", slot4Count);
            donutSeries.Points.AddXY("Slot 5 (Heavy)", slot5Count);

            donutSeries.Points[0].Color = System.Drawing.Color.FromArgb(46, 204, 113);
            donutSeries.Points[1].Color = System.Drawing.Color.FromArgb(52, 152, 219);
            donutSeries.Points[2].Color = System.Drawing.Color.FromArgb(155, 89, 182);
            donutSeries.Points[3].Color = System.Drawing.Color.FromArgb(241, 196, 15);

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

        // Explicitly bypassed unused residual handlers
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }
        private void label48_Click(object sender, EventArgs e) { }
        private void label47_Click(object sender, EventArgs e) { }
        private void tabPayments_Click(object sender, EventArgs e) { }
        private void tabDashboard_Click(object sender, EventArgs e) { }
    }
}