using System;
using System.IO;
using System.Text.Json;
using AutoStartHub.Models;

namespace AutoStartHub.Managers;

public class ConfigurationManager
{
    private readonly string _configPath;
    private readonly LogManager _logger;
    
    public ConfigurationManager(string configPath = "startup-config.json", LogManager? logger = null)
    {
        _configPath = configPath;
        _logger = logger ?? new LogManager();
    }
    
    public StartupConfig LoadConfig()
    {
        try
        {
            if (!File.Exists(_configPath))
            {
                _logger.LogInfo("配置文件不存在，创建默认配置");
                var defaultConfig = CreateDefaultConfig();
                SaveConfig(defaultConfig);
                Console.WriteLine($"已创建默认配置文件: {_configPath}");
                Console.WriteLine("请根据实际情况修改配置文件后重新运行程序");
                return defaultConfig;
            }
            
            var configJson = File.ReadAllText(_configPath);
            var config = JsonSerializer.Deserialize<StartupConfig>(configJson, GetJsonOptions());
            
            _logger.LogInfo("配置文件加载成功");
            return config ?? CreateDefaultConfig();
        }
        catch (Exception ex)
        {
            _logger.LogError($"配置文件加载失败: {ex.Message}");
            Console.WriteLine($"配置文件加载失败，使用默认配置: {ex.Message}");
            return CreateDefaultConfig();
        }
    }
    
    public void SaveConfig(StartupConfig config)
    {
        try
        {
            var json = JsonSerializer.Serialize(config, GetJsonWriteOptions());
            File.WriteAllText(_configPath, json);
            _logger.LogInfo("配置文件保存成功");
        }
        catch (Exception ex)
        {
            _logger.LogError($"配置文件保存失败: {ex.Message}");
        }
    }
    
    private StartupConfig CreateDefaultConfig()
    {
        return new StartupConfig
        {
            AutoExit = false,
            AutoExitDelayMs = 3000,
            VirtualMachine = new VirtualMachineConfig
            {
                Enabled = true,
                Name = "Windows10",
                Type = "VirtualBox",
                Headless = false,
                VBoxManagePath = @"C:\Program Files\Oracle\VirtualBox\VBoxManage.exe",
                StartupDelayMs = 5000
            },
            UnityProjects = [
                new UnityProjectConfig
                {
                    Enabled = true,
                    Name = "Match2023",
                    ProjectPath = @"C:\Users\GM19\myfile\Projects\match2023",
                    UnityVersion = "2022.3.0f1",
                    AdditionalArgs = "",
                    StartupDelayMs = 3000
                }
            ],
            Browser = new BrowserConfig
            {
                Enabled = true,
                BrowserType = "Firefox",
                FirefoxPath = @"C:\Program Files\Mozilla Firefox\firefox.exe",
                Urls = [
                    "https://github.com",
                    "https://unity.com",
                    "https://stackoverflow.com",
                    "https://docs.microsoft.com"
                ],
                TabDelayMs = 500
            },
            OtherProjects = [
                new OtherProjectConfig
                {
                    Enabled = false,
                    Name = "Trae AI",
                    ExecutablePath = @"C:\Program Files\Trae\Trae.exe",
                    Arguments = "",
                    WorkingDirectory = @"C:\Users\GM19\myfile\Projects",
                    DelayMs = 2000
                }
            ]
        };
    }
    
    private JsonSerializerOptions GetJsonOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };
    }
    
    private JsonSerializerOptions GetJsonWriteOptions()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
    }
}