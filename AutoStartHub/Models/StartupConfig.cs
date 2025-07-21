namespace AutoStartHub.Models;

public class StartupConfig
{
    public bool AutoExit { get; set; } = false;
    public int AutoExitDelayMs { get; set; } = 3000;
    public VirtualMachineConfig VirtualMachine { get; set; } = new();
    public UnityProjectConfig[] UnityProjects { get; set; } = [];
    public BrowserConfig Browser { get; set; } = new();
    public OtherProjectConfig[] OtherProjects { get; set; } = [];
}