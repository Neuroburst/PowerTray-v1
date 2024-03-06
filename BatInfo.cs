using System;
using System.Windows.Forms;
using Windows.Devices.Power;
using System.Management;
using Windows.UI.ViewManagement;
using System.Drawing;
using System.Reflection;

using System.Windows.Media.Imaging;

using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;
//using Windows.UI.Xaml.Media.Imaging;


namespace PowerTray
{
    public partial class BatInfo : Form
    {
        public BatInfo()
        {
            InitializeComponent();
            Timer timer = new Timer
            {
                Interval = PowerTray.batInfoRefreshRate,
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
            if (sender != null && PowerTray.batInfoAutoRefresh == false)
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

            var batteryInfo = PowerTray.GetBatteryInfo();
            var designChargeCapMwh = batteryInfo["designChargeCapMwh"];
            var fullChargeCapMwh = batteryInfo["fullChargeCapMwh"];
            var remainChargeCapMwh = batteryInfo["remainChargeCapMwh"];
            var chargeRateMwh = batteryInfo["chargeRateMwh"];

            double health = ((double)fullChargeCapMwh / (double)designChargeCapMwh) * 100;

            double batteryPercent = (remainChargeCapMwh / (double)fullChargeCapMwh) * 100;

            double timeLeft = 0;
            if (chargeRateMwh < 0)
            {
                timeLeft = (remainChargeCapMwh / -(double)chargeRateMwh) * 60;
            }
            else if (chargeRateMwh > 0)
            {
                timeLeft = ((fullChargeCapMwh - remainChargeCapMwh) / (double)chargeRateMwh) * 60;
            }
            // ---
            Values.Items.Clear();
            Items.Items.Clear();

            Items.Items.Add("Percent");
            Values.Items.Add(batteryPercent.ToString() + "%");
            Items.Items.Add(chargeRateMwh > 0 ? "Full Recharge Time" : "Full Discharge Time");
            Values.Items.Add(PowerTray.EasySecondsToTime((int)timeLeft));
            Items.Items.Add("Power Status");
            Values.Items.Add(SystemInformation.PowerStatus.PowerLineStatus.ToString());

            Items.Items.Add("---");
            Values.Items.Add("");

            Items.Items.Add("Design Capacity");
            Values.Items.Add(designChargeCapMwh.ToString() + " mWh");
            Items.Items.Add("Current Capacity");
            Values.Items.Add(fullChargeCapMwh.ToString() + " mWh");
            Items.Items.Add("Current Charge");
            Values.Items.Add(remainChargeCapMwh.ToString() + " mWh");
            Items.Items.Add(chargeRateMwh > 0 ? "Charge Rate" : "Discharge Rate");
            Values.Items.Add(Math.Abs(chargeRateMwh).ToString() + " mWh");
            Items.Items.Add("Battery Health");
            Values.Items.Add(health.ToString() + "%");

            Items.Items.Add("- - - - -");
            Values.Items.Add("- - - - -");
            Items.Items.Add("Other Battery Stats:");
            Values.Items.Add("");
            Items.Items.Add("");
            Values.Items.Add("");

            // add extra info to bottom
            foreach (PropertyData property in main_battery.Properties)
            {
                if (property.Value != null)
                {
                    Items.Items.Add(property.Name);
                    Values.Items.Add(property.Value.ToString());
                }
            }
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
            PowerTray.batInfoAutoRefresh = !PowerTray.batInfoAutoRefresh;
        }
    }
}
