using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using AutoStartHub.Managers;
using AutoStartHub.Models;

namespace AutoStartHub.Services;

public class VirtualMachineService
{
    private readonly LogManager _logger;
    
    public VirtualMachineService(LogManager logger)
    {
        _logger = logger;
    }
    
    public async Task StartAsync(VirtualMachineConfig vmConfig)
    {
        try
        {
            _logger.LogInfo($"开始启动虚拟机: {vmConfig.Name}");
            Console.WriteLine($"启动虚拟机: {vmConfig.Name}");
            
            if (!ValidateVirtualBoxPath(vmConfig.VBoxManagePath))
            {
                return;
            }
            
            var process = CreateVirtualMachineProcess(vmConfig);
            if (process == null)
            {
                return;
            }
            
            await ExecuteVirtualMachineStart(process, vmConfig);
        }
        catch (Exception ex)
        {
            _logger.LogError($"虚拟机启动异常: {ex.Message}");
            Console.WriteLine($"✗ 虚拟机启动失败: {ex.Message}");
        }
    }
    
    private bool ValidateVirtualBoxPath(string vboxPath)
    {
        if (!File.Exists(vboxPath))
        {
            _logger.LogError($"VBoxManage.exe 不存在: {vboxPath}");
            Console.WriteLine($"✗ VBoxManage.exe 不存在: {vboxPath}");
            return false;
        }
        return true;
    }
    
    private Process? CreateVirtualMachineProcess(VirtualMachineConfig vmConfig)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = vmConfig.VBoxManagePath,
            Arguments = $"startvm \"{vmConfig.Name}\" --type {(vmConfig.Headless ? "headless" : "gui")}",
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        
        return Process.Start(startInfo);
    }
    
    private async Task ExecuteVirtualMachineStart(Process process, VirtualMachineConfig vmConfig)
    {
        var output = await process.StandardOutput.ReadToEndAsync();
        var error = await process.StandardError.ReadToEndAsync();
        
        await process.WaitForExitAsync();
        
        if (process.ExitCode == 0)
        {
            _logger.LogSuccess($"虚拟机 {vmConfig.Name} 启动成功");
            Console.WriteLine($"✓ 虚拟机 {vmConfig.Name} 启动成功");
            await Task.Delay(vmConfig.StartupDelayMs);
        }
        else
        {
            _logger.LogError($"虚拟机启动失败，退出码: {process.ExitCode}, 错误: {error}");
            Console.WriteLine($"✗ 虚拟机启动失败: {error}");
        }
    }
}