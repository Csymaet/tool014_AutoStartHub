using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AutoStartHub.Managers;
using AutoStartHub.Models;

namespace AutoStartHub.Services;

public class UnityProjectService(LogManager logger)
{
    private readonly LogManager _logger = logger;

    public async Task StartAsync(UnityProjectConfig projectConfig)
    {
        try
        {
            _logger.LogInfo($"开始启动Unity项目: {projectConfig.Name}");
            Console.WriteLine($"启动Unity项目: {projectConfig.Name}");
            
            var unityPath = FindUnityEditor(projectConfig.UnityVersion);
            if (!ValidateUnityPath(unityPath, projectConfig.UnityVersion))
            {
                return;
            }
            
            if (!ValidateProjectPath(projectConfig.ProjectPath))
            {
                return;
            }
            
            var process = CreateUnityProcess(unityPath!, projectConfig);
            Process.Start(process);
            
            _logger.LogSuccess($"Unity项目 {projectConfig.Name} 启动命令已执行");
            Console.WriteLine($"✓ Unity项目 {projectConfig.Name} 启动中...");
            
            await Task.Delay(projectConfig.StartupDelayMs);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unity项目启动异常: {ex.Message}");
            Console.WriteLine($"✗ Unity项目启动失败: {ex.Message}");
        }
    }
    
    private bool ValidateUnityPath(string? unityPath, string version)
    {
        if (string.IsNullOrEmpty(unityPath))
        {
            _logger.LogError($"找不到Unity编辑器版本: {version}");
            Console.WriteLine($"✗ 找不到Unity编辑器版本: {version}");
            return false;
        }
        return true;
    }
    
    private bool ValidateProjectPath(string projectPath)
    {
        if (!Directory.Exists(projectPath))
        {
            _logger.LogError($"Unity项目路径不存在: {projectPath}");
            Console.WriteLine($"✗ Unity项目路径不存在: {projectPath}");
            return false;
        }
        return true;
    }
    
    private ProcessStartInfo CreateUnityProcess(string unityPath, UnityProjectConfig projectConfig)
    {
        var arguments = $"-projectPath \"{projectConfig.ProjectPath}\""
            + (string.IsNullOrEmpty(projectConfig.AdditionalArgs) ? "" : $" {projectConfig.AdditionalArgs}");
        
        return new ProcessStartInfo
        {
            FileName = unityPath,
            Arguments = arguments,
            UseShellExecute = true
        };
    }
    
    private string? FindUnityEditor(string version)
    {
        var possiblePaths = new[]
        {
            $@"C:\Program Files\Unity\Hub\Editor\{version}\Editor\Unity.exe",
            $@"C:\Program Files\Unity\Editor\Unity.exe",
            $@"C:\Program Files (x86)\Unity\Editor\Unity.exe",
            $@"D:\Unity\Hub\Editor\{version}\Editor\Unity.exe",
            $@"E:\Unity\Hub\Editor\{version}\Editor\Unity.exe"
        };
        
        foreach (var path in possiblePaths)
        {
            if (File.Exists(path))
            {
                _logger.LogInfo($"找到Unity编辑器: {path}");
                return path;
            }
        }
        
        _logger.LogError($"未找到Unity编辑器版本: {version}");
        return null;
    }
}