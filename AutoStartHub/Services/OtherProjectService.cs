using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AutoStartHub.Managers;
using AutoStartHub.Models;

namespace AutoStartHub.Services;

public class OtherProjectService
{
    private readonly LogManager _logger;
    
    public OtherProjectService(LogManager logger)
    {
        _logger = logger;
    }
    
    public async Task StartAsync(OtherProjectConfig project)
    {
        try
        {
            _logger.LogInfo($"开始启动项目: {project.Name}");
            Console.WriteLine($"启动项目: {project.Name}");
            
            if (!ValidateExecutablePath(project.ExecutablePath))
            {
                return;
            }
            
            var process = CreateProjectProcess(project);
            Process.Start(process);
            
            _logger.LogSuccess($"{project.Name} 启动命令已执行");
            Console.WriteLine($"✓ {project.Name} 启动完成");
            
            await Task.Delay(project.DelayMs);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{project.Name} 启动异常: {ex.Message}");
            Console.WriteLine($"✗ {project.Name} 启动失败: {ex.Message}");
        }
    }
    
    private bool ValidateExecutablePath(string executablePath)
    {
        if (!File.Exists(executablePath))
        {
            _logger.LogError($"可执行文件不存在: {executablePath}");
            Console.WriteLine($"✗ 可执行文件不存在: {executablePath}");
            return false;
        }
        return true;
    }
    
    private ProcessStartInfo CreateProjectProcess(OtherProjectConfig project)
    {
        return new ProcessStartInfo
        {
            FileName = project.ExecutablePath,
            Arguments = project.Arguments ?? "",
            WorkingDirectory = project.WorkingDirectory ?? Path.GetDirectoryName(project.ExecutablePath),
            UseShellExecute = true
        };
    }
}