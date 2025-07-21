namespace AutoStartHub.Models;

public class UnityProjectConfig
{
    public bool Enabled { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ProjectPath { get; set; } = string.Empty;
    public string UnityVersion { get; set; } = string.Empty;
    public string AdditionalArgs { get; set; } = string.Empty;
    public int StartupDelayMs { get; set; } = 3000;
}