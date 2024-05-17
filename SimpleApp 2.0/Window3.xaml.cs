using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Windows;
using System.Windows.Media;

namespace SimpleApp_2._0
{
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
           
            adapterInfoList = GetNetworkAdaptersInfo();

            UpdateAdapterInfo();
        }
        private List<string[]> adapterInfoList;
        private int currentAdapterIndex = 0;
        public static List<string[]> GetNetworkAdaptersInfo()
        {
            var adapterInfoList = new List<string[]>();

            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionID IS NOT NULL"))
            {
                foreach (ManagementObject instance in searcher.Get())
                {
                    string deviceName = (string)instance["NetConnectionID"];
                    string macAddress = (string)instance["MACAddress"];
                    bool isConnected = false;

                    string ipAddress = "";
                    string subnetMask = "";
                    string defaultGateway = "";

                    using (var configSearcher = new ManagementObjectSearcher("root\\CIMV2", $"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE MACAddress = '{macAddress}'"))
                    {
                        foreach (ManagementObject configInstance in configSearcher.Get())
                        {
                            if ((bool)configInstance["IPEnabled"])
                            {
                                isConnected = true;

                                string[] ipAddresses = (string[])configInstance["IPAddress"];
                                string[] subnets = (string[])configInstance["IPSubnet"];
                                string[] gateways = (string[])configInstance["DefaultIPGateway"];

                                ipAddress = ipAddresses != null && ipAddresses.Length > 0 ? ipAddresses[0] : "";
                                subnetMask = subnets != null && subnets.Length > 0 ? subnets[0] : "";
                                defaultGateway = gateways != null && gateways.Length > 0 ? gateways[0] : "";
                            }
                        }
                    }
                    string connectionStatus = isConnected ? "Connected" : "Not Connected";
                    
                    string[] adapterInfo = new string[] { deviceName, macAddress, ipAddress, subnetMask, defaultGateway, connectionStatus };

                    adapterInfoList.Add(adapterInfo);
                }
            }

            return adapterInfoList;
        }

        private void left_btn_Click(object sender, RoutedEventArgs e)
        {
            if (currentAdapterIndex > 0)
            {
                currentAdapterIndex--;
                UpdateAdapterInfo();
            }
        }

        private void right_btn_Click(object sender, RoutedEventArgs e)
        {
            if (currentAdapterIndex < adapterInfoList.Count - 1)
            {
                currentAdapterIndex++;
                UpdateAdapterInfo();
            }
        }
        private void UpdateAdapterInfo()
        {
            if (adapterInfoList.Count > 0)
            {
                string[] adapterInfo = adapterInfoList[currentAdapterIndex];

                model_label.Content = adapterInfo[0];
                mac_label.Content = adapterInfo[1];
                ip_label.Content = adapterInfo[2];
                mask_label.Content = adapterInfo[3];
                way_label.Content = adapterInfo[4];

                if (adapterInfo[5] == "Connected")
                {
                    status.Background = Brushes.Green;
                }
                else
                {
                    status.Background= Brushes.Red;
                }
            }
            else
            {
                model_label.Content = "Мережеві адаптери не знайдені";
                mac_label.Content = "";
                ip_label.Content = "";
                mask_label.Content = "";
                way_label.Content = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("control", "ncpa.cpl");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
