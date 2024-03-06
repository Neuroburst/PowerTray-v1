//using System;
//using System.Drawing;
//using System.Runtime.InteropServices;
//using System.Windows.Forms;
//namespace PowerTray
//{
//    static class Program
//    {
//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);

//            TrayIcon trayIcon = new TrayIcon();

//            Application.Run();
//        }
//    }
//}

// TODO: 
// Important: Support Situations in which:
// No System Battery
// Multiple Batteries
// Unknown Battery Condition

// TODO:
// fix flicker when updating
// list all battery stats
// make font size bigger
// more info in tooltip
// only get taskbar darkmode

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;

using Windows.Devices.Power;

using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.System.Power;
using Windows.ApplicationModel.Background;
using Windows.UI.ViewManagement;
using System.Drawing.Text;
using Windows.UI.Xaml.Media;

namespace PowerTray
{
    public class PowerTray
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _ = new PowerTray();
            Application.Run();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool DestroyIcon(IntPtr handle);
        private readonly NotifyIcon trayIcon;

        // Options ---
        static int trayFontSize = 11;
        static String trayFontType = "Microsoft Sans Serif";
        static float trayFontQualityMultiplier = 2.0f;

        public static int refreshRate = 100;

        static Color chargingColor = Color.Green;
        static Color highColor = Color.Black;
        static Color highDarkColor = Color.White;
        static Color mediumColor = Color.FromArgb(255, 220, 100, 20);
        static Color lowColor = Color.FromArgb(255, 232, 17, 35);

        static Color errorColor = Color.Black;

        static int highAmount = 60;
        static int mediumAmount = 40;
        static int lowAmount = 0;
        // ---

        static Font trayFont = new Font(trayFontType, trayFontSize * trayFontQualityMultiplier);

        public PowerTray()
        {
            // Create Context Menu
            ContextMenu contextMenu = new ContextMenu();
            
            MenuItem infoItem = new MenuItem();
            MenuItem settingsItem = new MenuItem();
            MenuItem exitItem = new MenuItem();

            contextMenu.MenuItems.AddRange(new MenuItem[] { infoItem, exitItem });

            infoItem.Click += new System.EventHandler(CreateInfoWindow);
            infoItem.Index = 0;
            infoItem.Text = "Battery Info";

            settingsItem.Index = 1;
            settingsItem.Text = "PowerTray Settings";

            exitItem.Click += new System.EventHandler(MenuItemClick);
            exitItem.Index = 1;
            exitItem.Text = "Exit";

            // Create tray button
            trayIcon = new NotifyIcon();
            trayIcon.ContextMenu = contextMenu;
            trayIcon.Visible = true;
            //trayIcon.Click += new System.EventHandler(CreateInfoWindow); // weirdly also imputs right clicks and context menu clicks

            // Create Update Timer
            Timer timer = new Timer
            {
                Interval = refreshRate,
            };
            timer.Tick += new EventHandler(UpdateTray);
            timer.Start();
        }
        private void UpdateTray(object sender, EventArgs e)
        {
            // check if dark mode is enabled ---
            var background = new UISettings().GetColorValue(UIColorType.Background);

            bool darkModeEnabled = Color.FromArgb(background.A, background.R, background.G, background.B) == 
                Color.FromArgb(255, 0, 0, 0);
            // ---

            //var batteries = new ManagementObjectSearcher("SELECT * FROM CIM_Battery").Get(); //get advanced battery info
            //ManagementObject main_battery = new ManagementObject();

            ////gets the first battery using the dumbest way possible :)
            //foreach (ManagementObject battery in batteries)
            //{
            //    main_battery = battery;
            //    break;
            //};

            var batteryReport = Battery.AggregateBattery.GetReport(); // get battery info

            // use battery info
            bool isPlugged = SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online;

            bool isCharging = batteryReport.Status == BatteryStatus.Charging;

            var fullChargeCapMwh = batteryReport.FullChargeCapacityInMilliwattHours;
            var remainChargeCapMwh = batteryReport.RemainingCapacityInMilliwattHours;
            var chargeRateMwh = batteryReport.ChargeRateInMilliwatts;

            double batteryPercent = (remainChargeCapMwh.Value / (double)fullChargeCapMwh.Value)*100;

            double timeLeft = 0;
            if (chargeRateMwh.Value < 0)
            {
                timeLeft = (remainChargeCapMwh.Value / -(double)chargeRateMwh.Value) * 60;
            }

            //double chargeTime = 0;
            if (chargeRateMwh.Value > 0)
            {
                timeLeft = ((fullChargeCapMwh.Value - remainChargeCapMwh.Value) / (double)chargeRateMwh.Value)*60;
            }
            // ---
            

            int roundPercent = (int)Math.Round(batteryPercent, 0);

            Color statusColor = highColor;

            if (isCharging)
            {
                statusColor = chargingColor;
            }

            else if (roundPercent >= highAmount)
            {
                statusColor = highColor;
            }
            else if (roundPercent >= mediumAmount)
            {
                statusColor = mediumColor;
            }
            else if (roundPercent >= lowAmount)
            {
                statusColor = lowColor;
            }

            // Lighter text for darkmode
            if (darkModeEnabled)
            {
                if (statusColor == highColor)
                {
                    statusColor = highDarkColor;
                }
                else
                {
                    statusColor = LightenColor(statusColor);
                }
            }
            
            String toolTipText =
                Math.Round(batteryPercent, 3).ToString() + "% " + (isPlugged ? "Connected to AC" : "On Battery\n" +
                EasySecondsToTime((int)timeLeft) + " Remaining") +

                (isPlugged ? (isCharging ? "\nCharging: " + EasySecondsToTime((int)timeLeft) + " until Fully Charged" : "\nNot Charging") + "" : "");
            SetNotifyIconText(trayIcon, toolTipText);

            String trayIconText = roundPercent == 100 ? ":)" : roundPercent.ToString();
            SolidBrush trayFontColor = new SolidBrush(statusColor);

            float dpi;
            int textWidth, textHeight;

            // Measure the rendered size of tray icon text under the current system DPI setting.
            using (var bitmap = new Bitmap(1, 1))
            {
                SizeF size;
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    // Measure the rendering size of the tray icon text using this font.
                    size = graphics.MeasureString(trayIconText, trayFont);
                    dpi = graphics.DpiX * trayFontQualityMultiplier;
                }

                // Round the size to integer.
                textWidth = (int)Math.Round(size.Width);
                textHeight = (int)Math.Round(size.Height);
            }

            var iconDimension = (int)Math.Round(16 * (dpi / 96));
            
            // Draw the tray icon
            using (var bitmap = new Bitmap(iconDimension, iconDimension))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    if (!darkModeEnabled)
                    {
                        // Anti-Aliasing looks the best when the taskbar is in light mode
                        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                    }

                    // Draw the text, centering it, and padding it with 1%
                    graphics.DrawString(trayIconText, trayFont, trayFontColor,
                        (iconDimension - textWidth) / 2f,
                        (iconDimension - textHeight) / 2f);

                    // The above scaling and start position alignments aim to remove the
                    // padding of the font so that the text fills the tray icon edge to edge.
                }

                // Set tray icon from the drawn bitmap image.
                var handle = ExecuteWithRetry(bitmap.GetHicon);
                try
                {
                    trayIcon.Icon?.Dispose();
                    trayIcon.Icon = Icon.FromHandle(handle);
                }
                finally
                {
                    // Destroy icon hand to release it from memory as soon as it's set to the tray.
                    DestroyIcon(handle);
                    // This should be the very last call when updating the tray icon.
                }
            }
        }

        private void CreateInfoWindow(object sender, System.EventArgs e)
        {
            BatInfo dialog = new BatInfo();
            var background = new UISettings().GetColorValue(UIColorType.Background);

            bool darkModeEnabled = Color.FromArgb(background.A, background.R, background.G, background.B) ==
                Color.FromArgb(255, 0, 0, 0);
            //dialog.SetTheme(darkModeEnabled);
            dialog.ShowDialog();
        }

        public static String EasySecondsToTime(int seconds) // Convert seconds to a readable format
        {
            String time = "_";

            if (seconds < 60)
            {
                time = seconds.ToString() + " minutes";
            }
            else
            {
                time = (seconds / 60).ToString() + " Hour" + (seconds / 60 == 1 ? "" : "s") + " and " + (seconds % 60).ToString() + " Minutes";
            }

            if (seconds == -1 || seconds == 0)
            {
                time = "Unknown minutes";
            }
            return time;
        }
        public static void SetNotifyIconText(NotifyIcon ni, string text) // bypass 63 character limit for tooltips
        {
            if (text.Length >= 128) throw new ArgumentOutOfRangeException("Text limited to 127 characters");
            Type t = typeof(NotifyIcon);
            BindingFlags hidden = BindingFlags.NonPublic | BindingFlags.Instance;
            t.GetField("text", hidden).SetValue(ni, text);
            if ((bool)t.GetField("added", hidden).GetValue(ni))
                t.GetMethod("UpdateIcon", hidden).Invoke(ni, new object[] { true });
        }

        public static Color LightenColor(Color color)
        {
            var amount = 30;
            Color lightColor = Color.FromArgb(color.A,
                Math.Min((int)(color.R + amount), 255), Math.Min((int)(color.G + amount), 255), Math.Min((int)(color.B + amount), 255));
            return lightColor;
        }

        // Tray Icon Helper Functions ---
        private void MenuItemClick(object sender, EventArgs e) // check if the exit button was pressed
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();
            Application.Exit();
        }
        T ExecuteWithRetry<T>(Func<T> function, bool throwWhenFail = true)
        {
            for (var i = 0; ;)
            {
                try
                {
                    return function();
                }
                catch when (i++ < 5)
                {
                    // Swallow exception if retry is possible.
                }
                catch when (!throwWhenFail)
                {
                    // Return default value if not throwing exception.
                    return default;
                }
            }
        }
        // ---
    }
}

