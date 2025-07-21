# 开机自动启动项目解决方案

这是一个基于 .NET 10.0 的控制台应用程序，用于开机自动启动虚拟机、Unity项目、Firefox浏览器和其他可执行程序。

## 功能特性

- ✅ **虚拟机管理**: 支持 VirtualBox 虚拟机的自动启动
- ✅ **Unity项目启动**: 支持多个Unity项目的自动启动
- ✅ **浏览器管理**: 支持Firefox浏览器多标签页启动
- ✅ **其他程序启动**: 支持任意可执行程序的启动
- ✅ **配置化管理**: 通过JSON配置文件灵活配置
- ✅ **异步并发**: 所有启动任务并发执行，提高效率
- ✅ **错误处理**: 完善的错误处理和日志记录
- ✅ **控制台输出**: 实时显示启动状态和错误信息

## 快速开始

### 1. 编译项目

```bash
dotnet build
```

### 2. 运行程序

```bash
dotnet run
```

首次运行会自动创建默认配置文件 `startup-config.json`，请根据实际情况修改配置。

### 3. 配置文件说明

配置文件 `startup-config.json` 包含以下主要配置项：

#### 全局配置
- `AutoExit`: 是否自动退出程序
- `AutoExitDelayMs`: 自动退出延迟时间（毫秒）

#### 虚拟机配置
```json
"VirtualMachine": {
  "Enabled": true,
  "Name": "Windows10",
  "Type": "VirtualBox",
  "Headless": false,
  "VBoxManagePath": "C:\\Program Files\\Oracle\\VirtualBox\\VBoxManage.exe",
  "StartupDelayMs": 5000
}
```

#### Unity项目配置
```json
"UnityProjects": [
  {
    "Enabled": true,
    "Name": "Match2023",
    "ProjectPath": "C:\\Users\\GM19\\myfile\\Projects\\match2023",
    "UnityVersion": "2022.3.0f1",
    "AdditionalArgs": "",
    "StartupDelayMs": 3000
  }
]
```

#### 浏览器配置
```json
"Browser": {
  "Enabled": true,
  "BrowserType": "Firefox",
  "FirefoxPath": "C:\\Program Files\\Mozilla Firefox\\firefox.exe",
  "Urls": [
    "https://github.com",
    "https://unity.com"
  ],
  "TabDelayMs": 500
}
```

#### 其他程序配置
```json
"OtherProjects": [
  {
    "Enabled": false,
    "Name": "Visual Studio Code",
    "ExecutablePath": "C:\\Users\\GM19\\AppData\\Local\\Programs\\Microsoft VS Code\\Code.exe",
    "Arguments": "C:\\Users\\GM19\\myfile\\Projects",
    "WorkingDirectory": "C:\\Users\\GM19\\myfile\\Projects",
    "DelayMs": 1000
  }
]
```

## 设置开机自启

### 方法1: 任务计划程序（推荐）

1. 打开"任务计划程序"（Task Scheduler）
2. 创建基本任务
3. 设置触发器为"计算机启动时"
4. 设置操作为启动程序：`dotnet run --project "完整项目路径"`
5. 设置"起始于"为项目目录

### 方法2: 启动文件夹

1. 按 `Win + R`，输入 `shell:startup`
2. 创建批处理文件 `startup.bat`：
```batch
@echo off
cd /d "D:\myfile\desktop\001-projects\tool\tool014_\AutoStartupProjects"
dotnet run
pause
```
3. 将批处理文件放入启动文件夹

### 方法3: 注册表

1. 按 `Win + R`，输入 `regedit`
2. 导航到 `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run`
3. 新建字符串值，设置为批处理文件路径

## 日志文件

程序运行时会生成 `startup.log` 日志文件，记录详细的启动过程和错误信息。

## 故障排除

### 常见问题

1. **虚拟机启动失败**
   - 检查 VBoxManage.exe 路径是否正确
   - 确认虚拟机名称是否存在
   - 检查 VirtualBox 是否正确安装

2. **Unity项目启动失败**
   - 检查Unity编辑器路径是否正确
   - 确认项目路径是否存在
   - 检查Unity版本是否匹配

3. **Firefox启动失败**
   - 检查Firefox安装路径
   - 确认URL格式是否正确

### 调试建议

- 查看控制台输出信息
- 检查 `startup.log` 日志文件
- 手动测试各个程序路径是否正确
- 确认配置文件JSON格式正确

## 扩展功能

### 添加新的启动项

1. 在 `OtherProjects` 数组中添加新配置
2. 设置 `Enabled: true`
3. 配置可执行文件路径和参数

### 自定义启动顺序

可以通过调整 `StartupDelayMs` 参数来控制启动时间间隔。

### 支持更多浏览器

可以扩展 `BrowserConfig` 类，添加对Chrome、Edge等浏览器的支持。

## 技术栈

- .NET 10.0
- C# 12.0
- System.Text.Json
- System.Diagnostics.Process

## 许可证

本项目采用 MIT 许可证。