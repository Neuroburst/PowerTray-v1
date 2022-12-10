using System;
using System.Windows.Forms;
using Windows.Devices.Power;
using System.Management;
using Windows.UI.ViewManagement;
using System.Drawing;
using System.Reflection;

//using Windows.Graphics.Imaging;
using System.Windows.Media.Imaging;

using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;
//using Windows.UI.Xaml.Media.Imaging;


namespace PowerTray
{
    public partial class BatInfo : Form
    {
        static public bool AutoRefresh = false;
        public BatInfo()
        {
            InitializeComponent();

            Timer timer = new Timer
            {
                Interval = PowerTray.refreshRate,
            };
            timer.Tick += new EventHandler(RefreshList);
            timer.Start();
        }

        private void BatInfo_Load(object sender, EventArgs e)
        {
            var background = new UISettings().GetColorValue(UIColorType.Background);

            bool darkModeEnabled = Color.FromArgb(background.A, background.R, background.G, background.B) ==
                Color.FromArgb(255, 0, 0, 0);

            //if (darkModeEnabled)
            //{

            //    Uri iconUri = new Uri("/BatteryFullFluentDark.ico", UriKind.RelativeOrAbsolute);
            //    Icon = BitmapFrame.Create(iconUri);
            //}

            RefreshList();
        }

        public void RefreshList(object sender = null, EventArgs e = null)
        {
            if (sender != null && AutoRefresh == false)
            {
                return;
            }

            var batteries = new ManagementObjectSearcher("SELECT * FROM CIM_Battery").Get(); //get advanced battery info
            ManagementObject main_battery = new ManagementObject();

            //gets the first battery using the dumbest way possible :)
            foreach (ManagementObject battery in batteries)
            {
                main_battery = battery;
                break;
            };

            var batteryReport = Battery.AggregateBattery.GetReport(); // get battery info

            // use battery info
            String description = "Blank";
            String caption = "Blank";
            String modelID = "Unknown";
            String expectedRunTime = "Unknown";
            String status = "Unknown";

            String voltage = "?";

            

            foreach (PropertyData property in main_battery.Properties){
                if (property.Name == "Description")
                {
                    description = (String)property.Value;
                }

                if (property.Name == "Caption")
                {
                    caption = (String)property.Value;
                }

                if (property.Name == "DeviceID") {
                    modelID = (String)property.Value;
                }

                if (property.Name == "DesignVoltage")
                {
                    voltage = property.Value.ToString();
                }

                if (property.Name == "ExpectedRunTime")
                {
                    expectedRunTime = property.Value.ToString();
                }

                if (property.Name == "Status")
                {
                    status = property.Value.ToString();
                }
            }

            var designChargeCapMwh = batteryReport.DesignCapacityInMilliwattHours;
            var fullChargeCapMwh = batteryReport.FullChargeCapacityInMilliwattHours;
            var remainChargeCapMwh = batteryReport.RemainingCapacityInMilliwattHours;
            var chargeRateMwh = batteryReport.ChargeRateInMilliwatts;

            double health = ((double)fullChargeCapMwh / (double)designChargeCapMwh) * 100;

            double batteryPercent = (remainChargeCapMwh.Value / (double)fullChargeCapMwh.Value) * 100;
            
            double timeLeft = 0;
            if (chargeRateMwh.Value < 0)
            {
                timeLeft = (remainChargeCapMwh.Value / -(double)chargeRateMwh.Value) * 60;
            }
            else if (chargeRateMwh.Value > 0)
            {
                timeLeft = ((fullChargeCapMwh.Value - remainChargeCapMwh.Value) / (double)chargeRateMwh.Value) * 60;
            }
            // ---

            Values.Items.Clear();
            Values.Items.Add(batteryPercent.ToString() + "%");
            Values.Items.Add(PowerTray.EasySecondsToTime((int)timeLeft));
            Values.Items.Add(SystemInformation.PowerStatus.PowerLineStatus.ToString());
            Values.Items.Add("");
            Values.Items.Add(designChargeCapMwh.ToString() + " mWh");
            Values.Items.Add(fullChargeCapMwh.ToString() + " mWh");
            Values.Items.Add(remainChargeCapMwh.ToString() + " mWh");
            Values.Items.Add(chargeRateMwh.ToString() + " mWh");
            Values.Items.Add(health.ToString() + "%");
            Values.Items.Add("");
            Values.Items.Add(description);
            Values.Items.Add(caption);
            Values.Items.Add(modelID);
            Values.Items.Add(expectedRunTime);
            Values.Items.Add(voltage + "V");
            Values.Items.Add(status);
        }


        private void Close_Click(object sender, EventArgs e)
        {

        }        
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Values_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void AutoRefreshToggle(object sender, EventArgs e)
        {
            AutoRefresh = !AutoRefresh;
        }
    }
}
