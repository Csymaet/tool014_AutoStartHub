using System;
using System.IO;

namespace AutoStartHub.Managers;

public class LogManager
{
    private readonly string _logPath;
    
    public LogManager(string logPath = "startup.log")
    {
        _logPath = logPath;
    }
    
    public void LogMessage(string message)
    {
        try
        {
            var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            File.AppendAllText(_logPath, logEntry + Environment.NewLine);
        }
        catch
        {
            // 忽略日志写入错误
        }
    }
    
    public void LogSuccess(string message)
    {
        LogMessage($"✓ {message}");
    }
    
    public void LogError(string message)
    {
        LogMessage($"✗ {message}");
    }
    
    public void LogInfo(string message)
    {
        LogMessage(message);
    }
}