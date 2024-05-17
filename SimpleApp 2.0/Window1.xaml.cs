using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Management;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;
using MessageBox = System.Windows.Forms.MessageBox;
using HardwareInfo;



namespace SimpleApp_2._0
{

    public partial class Window1 : Window
    {

        public async Task initComponents()
        {
            Task[] tasks = new Task[] {
                HardwareInfoParser.GetProcessorInfo(),
                HardwareInfoParser.GetGraphicInfo(),
                HardwareInfoParser.GetPhysicalDiskInfo(),
                HardwareInfoParser.GetRAM(),
                HardwareInfoParser.GetDisk(),
                HardwareInfoParser.GetMonitor()
            };
            await Task.WhenAll(tasks);

            var arr_cpu = await (Task<string[]>)tasks[0];
            cpu_model_label.Content = arr_cpu[1];
            cores_label.Content = arr_cpu[2] + " Ядер " + arr_cpu[3];
            arr_full_info.Add(arr_cpu[1]);
            arr_full_info.Add(arr_cpu[2] + " Ядер " + arr_cpu[3]);

            var arr_gpu = await (Task<string[]>)tasks[1];
            model_label_gpu.Content = arr_gpu[1];
            memory_gpu.Content = arr_gpu[2];
            arr_full_info.Add(arr_gpu[1]);
            arr_full_info.Add(arr_gpu[2]);

            /// чіпай тільки знизу
            DispatcherTimer cpu_timer = new DispatcherTimer();
            cpu_timer.Interval = TimeSpan.FromMilliseconds(500);
            cpu_timer.Tick += Timer_Tick;
            cpu_timer.Start();

            DispatcherTimer ram_timer = new DispatcherTimer();
            ram_timer.Interval = TimeSpan.FromMilliseconds(500);
            ram_timer.Tick += DisplayRAMUsage;
            ram_timer.Start();

            computer.GPUEnabled = true;
            computer.Open();

            Thread updateThread = new Thread(UpdateProgressBar);
            updateThread.IsBackground = true;
            updateThread.Start();
            /// чіпай тільки зверху

            var physDiskInfo = await (Task<string>)tasks[2];
            var ramInfo = await (Task<string>)tasks[3];
            var diskInfo = await (Task<string>)tasks[4];

            logical_disk_label.Content = diskInfo;
            ram_label.Content = ramInfo;
            phisical_disk_label.Content = diskInfo;
            arr_full_info.Add(physDiskInfo);
            arr_full_info.Add(ramInfo);
            arr_full_info.Add(diskInfo);

            string[] arr_monitor = await (Task<string[]>)tasks[5];
            px_horizontal_label.Content = arr_monitor[2];
            px_vertical_label.Content = arr_monitor[1];
            arr_full_info.Add(arr_monitor[2]);
            arr_full_info.Add(arr_monitor[1]);
        }
        public Window1()
        {
            this.
            InitializeComponent();

            this.initComponents().GetAwaiter().GetResult();
        }
        List<string> arr_full_info = new List<string>();
        Computer computer = new Computer();
        PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        private void UpdateProgressBar()
        {
            while (true)
            {
                foreach (var hardware in computer.Hardware)
                {
                    if (hardware.HardwareType == HardwareType.GpuNvidia || hardware.HardwareType == HardwareType.GpuAti)
                    {
                        hardware.Update();
                        foreach (var sensor in hardware.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Load && sensor.Name == "GPU Core")
                            {
                                double gpuLoad = sensor.Value.GetValueOrDefault();
                                Dispatcher.Invoke(() => { gpu_bar.Value = gpuLoad; });
                            }
                        }
                    }
                }

                Thread.Sleep(1000);
            }
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            float cpuUsage = cpuCounter.NextValue();
            cpu_bar.Value = cpuUsage;
        }

        protected static ManagementObjectCollection moc = new ManagementClass("Win32_ComputerSystem").GetInstances();
        void DisplayRAMUsage(object sender, EventArgs e)
        {

            double availableMemory = ramCounter.NextValue();
            double totalMemory = 0;

            foreach (ManagementObject mo in moc)
            {
                if (mo["TotalPhysicalMemory"] != null)
                {
                    totalMemory = Convert.ToDouble(mo["TotalPhysicalMemory"]);
                    totalMemory = totalMemory / (1024 * 1024);
                    break;
                }
            }

            double memoryUsagePercent = ((totalMemory - availableMemory) / totalMemory) * 100;

            ram_bar.Minimum = 0;
            ram_bar.Maximum = 100;
            ram_bar.Value = (int)memoryUsagePercent;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void memory_button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("mmc.exe", "diskmgmt.msc");
        }

        private void mon_button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("control", "desk.cpl");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GeneratePDF(arr_full_info);
        }

        public static void GeneratePDF(List<string> data)
        {
            
        }
    }
}
