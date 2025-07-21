using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoStartHub.Managers;
using AutoStartHub.Models;

namespace AutoStartHub.Services;

public class BrowserService
{
    private readonly LogManager _logger;
    
    public BrowserService(LogManager logger)
    {
        _logger = logger;
    }
    
    public async Task StartAsync(BrowserConfig browserConfig)
    {
        if (browserConfig.Urls.Length == 0)
        {
            _logger.LogInfo("没有配置浏览器URL");
            return;
        }
        
        try
        {
            _logger.LogInfo("开始启动Firefox浏览器");
            Console.WriteLine("启动Firefox浏览器...");
            
            if (!ValidateFirefoxPath(browserConfig.FirefoxPath))
            {
                return;
            }
            
            var process = CreateFirefoxProcess(browserConfig);
            Process.Start(process);
            
            _logger.LogSuccess($"Firefox启动完成，打开了{browserConfig.Urls.Length}个标签页");
            Console.WriteLine($"✓ Firefox启动完成，打开了{browserConfig.Urls.Length}个标签页");
            
            await Task.Delay(browserConfig.TabDelayMs);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Firefox启动异常: {ex.Message}");
            Console.WriteLine($"✗ Firefox启动失败: {ex.Message}");
        }
    }
    
    private bool ValidateFirefoxPath(string firefoxPath)
    {
        if (!File.Exists(firefoxPath))
        {
            _logger.LogError($"Firefox不存在: {firefoxPath}");
            Console.WriteLine($"✗ Firefox不存在: {firefoxPath}");
            return false;
        }
        return true;
    }
    
    private ProcessStartInfo CreateFirefoxProcess(BrowserConfig browserConfig)
    {
        var urls = string.Join(" ", browserConfig.Urls.Select(url => $"\"{url}\""));
        
        return new ProcessStartInfo
        {
            FileName = browserConfig.FirefoxPath,
            Arguments = urls,
            UseShellExecute = true
        };
    }
}