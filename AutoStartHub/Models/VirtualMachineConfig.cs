namespace AutoStartHub.Models;

public class VirtualMachineConfig
{
    public bool Enabled { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "VirtualBox";
    public bool Headless { get; set; } = false;
    public string VBoxManagePath { get; set; } = @"C:\Program Files\Oracle\VirtualBox\VBoxManage.exe";
    public int StartupDelayMs { get; set; } = 5000;
}