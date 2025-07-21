using AutoStartHub.Managers;
using AutoStartHub.Models;
using AutoStartHub.Services;

namespace AutoStartHub;

#region 程序入口
class Program
{
    static async Task Main(string[] args)
    {
        var logger = new LogManager();
        var configManager = new ConfigurationManager(logger: logger);
        
        try
        {
            logger.LogInfo("=== 开机自动启动程序开始 ===");
            Console.WriteLine("开始启动具体项目和虚拟机...");
            
            var config = configManager.LoadConfig();
            
            var startupTasks = new List<Task>();
            
            await AddStartupTasks(startupTasks, config, logger);
            await Task.WhenAll(startupTasks);
            
            logger.LogInfo("所有项目启动完成！");
            Console.WriteLine("所有项目启动完成！按任意键退出...");
            
            await HandleProgramExit(config);
        }
        catch (Exception ex)
        {
            logger.LogError($"程序执行出错: {ex.Message}");
            Console.WriteLine($"程序执行出错: {ex.Message}");
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
    }
    
    #region 任务管理
    private static Task AddStartupTasks(List<Task> startupTasks, StartupConfig config, LogManager logger)
    {
        if (config.VirtualMachine.Enabled)
        {
            var vmService = new VirtualMachineService(logger);
            startupTasks.Add(vmService.StartAsync(config.VirtualMachine));
        }
        
        foreach (var unityProject in config.UnityProjects.Where(p => p.Enabled))
        {
            var unityService = new UnityProjectService(logger);
            startupTasks.Add(unityService.StartAsync(unityProject));
        }
        
        if (config.Browser.Enabled)
        {
            var browserService = new BrowserService(logger);
            startupTasks.Add(browserService.StartAsync(config.Browser));
        }
        
        foreach (var project in config.OtherProjects.Where(p => p.Enabled))
        {
            var otherService = new OtherProjectService(logger);
            startupTasks.Add(otherService.StartAsync(project));
        }
        
        return Task.CompletedTask;
    }
    
    private static async Task HandleProgramExit(StartupConfig config)
    {
        if (config.AutoExit)
        {
            await Task.Delay(config.AutoExitDelayMs);
        }
        else
        {
            Console.ReadKey();
        }
    }
    #endregion
}
#endregion
