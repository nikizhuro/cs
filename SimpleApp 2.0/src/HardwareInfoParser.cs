using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardwareInfo
{
    internal static class HardwareInfoParser
    {
        private static ManagementObjectSearcher processorSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
        private static ManagementObjectSearcher graphicSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
        private static ManagementObjectSearcher diskSearcher = new ManagementObjectSearcher("SELECT DeviceID, Model, Size FROM Win32_DiskDrive");

        public static async Task<string[]> GetProcessorInfo()
        {
            var firstProcessor = processorSearcher.Get().Cast<ManagementObject>().FirstOrDefault();

            if (firstProcessor == null) return new string[0];

            string[] processorInfo = {
                firstProcessor["Manufacturer"].ToString(),
                firstProcessor["Name"].ToString(),
                firstProcessor["NumberOfCores"].ToString(),
                firstProcessor["MaxClockSpeed"].ToString() + " MHz"
            };

            return processorInfo;
        }

        public static async Task<string[]> GetGraphicInfo()
        {
            var firstGraphicUnit = graphicSearcher.Get().Cast<ManagementObject>().FirstOrDefault();

            if (firstGraphicUnit == null) return new string[0];

            string manufacturer = firstGraphicUnit["AdapterCompatibility"].ToString();
            string model = firstGraphicUnit["Name"].ToString();
            string memory = firstGraphicUnit["AdapterRAM"].ToString();
            string memoryType = firstGraphicUnit["Description"].ToString();

            ulong memoryBytes;
            if (ulong.TryParse(memory, out memoryBytes))
            {
                double memoryGB = memoryBytes / (1024.0 * 1024.0 * 1024.0);
                memory = memoryGB.ToString("F2") + " GB";
            }
            else
            {
                memory = "Unknown";
            }

            string[] graphicsInfo = {
                manufacturer,
                model,
                memory,
                memoryType
            };

            return graphicsInfo;
        }

        public static async Task<string> GetPhysicalDiskInfo()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            int physicalDiskCount = 0;
            string diskInfo = String.Empty;

            foreach (DriveInfo drive in allDrives)
            {
                if (drive.DriveType != DriveType.Fixed) continue;

                physicalDiskCount++;
                double usagePercentage = Math.Round((1 - ((double)drive.TotalFreeSpace / drive.TotalSize)) * 100, 2);
                string driveInfo = $"{drive.Name}: Об'єм - {drive.TotalSize / (1024 * 1024 * 1024)} ГБ, Вільно - {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} ГБ, Завантаженість - {usagePercentage}%";
                diskInfo += driveInfo + Environment.NewLine;
            }

            if (physicalDiskCount == 0)
            {
                return "Немає фізичних дисків";
            }
            else
            {
                return diskInfo;
            }
        }

        public static async Task<string> GetRAM()
        {
            ManagementObjectSearcher memorySlotSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemoryArray");
            ManagementObjectSearcher memoryModuleSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");

            int slotCount = 0;
            ulong totalCapacityBytes = 0;
            string memoryFrequency = String.Empty;

            foreach (ManagementObject queryObj in memorySlotSearcher.Get())
            {
                slotCount = Convert.ToInt32(queryObj["MemoryDevices"]);
                break;
            }

            foreach (ManagementObject queryObj in memoryModuleSearcher.Get())
            {
                ulong capacityBytes = Convert.ToUInt64(queryObj["Capacity"]);
                totalCapacityBytes += capacityBytes;

                if (queryObj["ConfiguredClockSpeed"] != null)
                {
                    int clockSpeed = Convert.ToInt32(queryObj["ConfiguredClockSpeed"]);
                    memoryFrequency = $"{clockSpeed} МГц";
                }
            }

            double totalCapacityGB = Math.Round(totalCapacityBytes / (1024.0 * 1024.0 * 1024.0), 2);

            return $"Слоти ОЗУ: {slotCount}, об'єм: {totalCapacityGB} ГБ, частота: {memoryFrequency}";
        }

        public static async Task<string> GetDisk()
        {
            string diskInfo = String.Empty;
            foreach (ManagementObject instance in diskSearcher.Get())
            {
                string model = (string)instance["Model"];
                ulong sizeBytes = (ulong)instance["Size"];
                double sizeGB = Math.Round(sizeBytes / (1024.0 * 1024.0 * 1024.0), 2);

                diskInfo += $"{model}, Розмір: {sizeGB} ГБ" + Environment.NewLine;
            }

            return diskInfo;
        }

        public static async Task<string[]> GetMonitor()
        {
            string[] primaryMonitorInfoArray = null;
            Screen primaryScreen = Screen.PrimaryScreen;

            if (primaryScreen == null) return null;

            primaryMonitorInfoArray = new string[3];
            primaryMonitorInfoArray[1] = primaryScreen.Bounds.Height.ToString() + " px";
            primaryMonitorInfoArray[2] = primaryScreen.Bounds.Width.ToString() + " px";

            return primaryMonitorInfoArray;
        }
    }
}
