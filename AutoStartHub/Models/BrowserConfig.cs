namespace AutoStartHub.Models;

public class BrowserConfig
{
    public bool Enabled { get; set; }
    public string BrowserType { get; set; } = "Firefox";
    public string FirefoxPath { get; set; } = @"C:\Program Files\Mozilla Firefox\firefox.exe";
    public string[] Urls { get; set; } = [];
    public int TabDelayMs { get; set; } = 500;
}