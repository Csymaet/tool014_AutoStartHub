namespace AutoStartHub.Models;

public class OtherProjectConfig
{
    public bool Enabled { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ExecutablePath { get; set; } = string.Empty;
    public string Arguments { get; set; } = string.Empty;
    public string WorkingDirectory { get; set; } = string.Empty;
    public int DelayMs { get; set; } = 1000;
}