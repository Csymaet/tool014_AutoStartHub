# AutoStartHub

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/platform-Windows-blue)](https://www.microsoft.com/windows)
[![License](https://img.shields.io/badge/license-MIT-green)](./LICENSE)

这是一个基于 .NET 10.0 的现代化控制台应用程序，专为开机自动启动管理而设计。支持虚拟机、Unity项目、Firefox浏览器和任意可执行程序的自动化启动。

## ✨ 功能特性

- 🖥️ **虚拟机管理**: 支持 VirtualBox 虚拟机的自动启动和管理
- 🎮 **Unity项目启动**: 支持多个Unity项目的并发自动启动
- 🌐 **浏览器管理**: 支持Firefox浏览器多标签页批量启动
- ⚡ **其他程序启动**: 支持任意可执行程序的灵活启动配置
- ⚙️ **配置化管理**: 通过JSON配置文件进行可视化配置管理
- 🚀 **异步并发**: 所有启动任务并发执行，大幅提升启动效率
- 🛡️ **错误处理**: 完善的错误处理机制和详细的日志记录
- 📊 **实时反馈**: 控制台实时显示启动状态和错误信息
- 📁 **模块化架构**: 采用分层架构设计，代码结构清晰易维护

## 🏗️ 项目架构

```
AutoStartHub/
├── Models/                          # 📁 配置模型层
│   ├── StartupConfig.cs            #   主配置模型
│   ├── VirtualMachineConfig.cs     #   虚拟机配置模型
│   ├── UnityProjectConfig.cs       #   Unity项目配置模型
│   ├── BrowserConfig.cs            #   浏览器配置模型
│   └── OtherProjectConfig.cs       #   其他程序配置模型
├── Managers/                        # 🔧 管理器层
│   ├── ConfigurationManager.cs     #   配置文件管理器
│   └── LogManager.cs               #   日志管理器
├── Services/                        # ⚙️ 服务层
│   ├── VirtualMachineService.cs    #   虚拟机启动服务
│   ├── UnityProjectService.cs      #   Unity项目启动服务
│   ├── BrowserService.cs           #   浏览器启动服务
│   └── OtherProjectService.cs      #   其他程序启动服务
└── Program.cs                       # 🚀 程序入口
```

## 🚀 快速开始

### 环境要求

- Windows 10/11
- .NET 10.0 Runtime 或更高版本
- VirtualBox（如需启动虚拟机）

### 1. 克隆项目

```bash
git clone <repository-url>
cd AutoStartHub
```

### 2. 编译项目

```bash
cd AutoStartHub
dotnet build
```

### 3. 运行程序

```bash
dotnet run
```

首次运行会自动创建默认配置文件 `startup-config.json`，请根据实际环境修改配置后重新运行。

### 4. 发布为可执行文件

```bash
# 发布为单文件可执行程序
dotnet publish -c Release

# 生成的文件位于：bin\Release\net10.0\win-x64\publish\
```

## ⚙️ 配置文件详解

配置文件 `startup-config.json` 采用 JSON 格式，支持丰富的注释说明：

### 全局配置

```json
{
  "AutoExit": false,              // 是否在启动完成后自动退出
  "AutoExitDelayMs": 3000,        // 自动退出延迟时间（毫秒）
}
```

### 虚拟机配置

```json
"VirtualMachine": {
  "Enabled": true,                // 是否启用虚拟机启动
  "Name": "arch",                 // 虚拟机名称（VirtualBox中显示的名称）
  "Type": "VirtualBox",           // 虚拟机类型（目前仅支持VirtualBox）
  "Headless": false,              // 是否无头模式启动（后台运行）
  "VBoxManagePath": "C:\\Program Files\\Oracle\\VirtualBox\\VBoxManage.exe",
  "StartupDelayMs": 5000          // 启动后等待时间
}
```

### Unity项目配置

支持配置多个Unity项目，每个项目可独立启用/禁用：

```json
"UnityProjects": [
  {
    "Enabled": true,              // 是否启用此Unity项目
    "Name": "Match2023",          // 项目显示名称
    "ProjectPath": "C:\\Users\\GM19\\myfile\\Projects\\match2023",
    "UnityVersion": "2022.3.37f1", // Unity版本号（用于自动查找编辑器）
    "AdditionalArgs": "",         // 启动Unity时的额外参数
    "StartupDelayMs": 3000        // 项目启动后等待时间
  },
  {
    "Enabled": false,             // 已禁用的项目示例
    "Name": "MyGame",
    "ProjectPath": "C:\\Users\\GM19\\myfile\\Projects\\MyGame",
    "UnityVersion": "2023.2.0f1",
    "AdditionalArgs": "-batchmode -quit", // 批处理模式
    "StartupDelayMs": 2000
  }
]
```

### 浏览器配置

```json
"Browser": {
  "Enabled": true,                // 是否启用浏览器启动
  "BrowserType": "Firefox",       // 浏览器类型
  "FirefoxPath": "C:\\Program Files\\Mozilla Firefox\\firefox.exe",
  "Urls": [                       // 批量打开的网址列表
    "https://github.com",
    "https://unity.com",
    "https://stackoverflow.com"
  ],
  "TabDelayMs": 500              // 打开标签页间隔时间
}
```

### 其他程序配置

支持配置任意可执行程序：

```json
"OtherProjects": [
  {
    "Enabled": false,             // 是否启用
    "Name": "Visual Studio Code", // 程序名称
    "ExecutablePath": "C:\\Users\\GM19\\AppData\\Local\\Programs\\Microsoft VS Code\\Code.exe",
    "Arguments": "C:\\Users\\GM19\\myfile\\Projects\\match2023", // 启动参数
    "WorkingDirectory": "C:\\Users\\GM19\\myfile\\Projects\\match2023", // 工作目录
    "DelayMs": 1000               // 启动延迟
  }
]
```

## 🔧 设置开机自启

### 方法1: Windows任务计划程序（推荐）

1. 按 `Win + R`，输入 `taskschd.msc` 打开任务计划程序
2. 点击"创建基本任务"
3. 输入任务名称："AutoStartHub"
4. 选择触发器："计算机启动时"
5. 选择操作："启动程序"
6. 程序路径：`<项目路径>\bin\Release\net10.0\win-x64\publish\AutoStartHub.exe`
7. 起始于：设置为程序所在目录
8. 完成创建并测试

### 方法2: 启动文件夹

1. 按 `Win + R`，输入 `shell:startup` 打开启动文件夹
2. 创建快捷方式指向编译后的可执行文件
3. 或创建批处理文件：

```batch
@echo off
cd /d "项目完整路径\AutoStartHub"
AutoStartHub.exe
pause
```

### 方法3: 注册表（高级用户）

1. 按 `Win + R`，输入 `regedit`
2. 导航到：`HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run`
3. 新建字符串值，数值数据设为可执行文件路径

## 📋 日志系统

程序运行时会自动生成 `startup.log` 日志文件，记录：

- 📊 **启动过程**: 详细的启动步骤和时间戳
- ⚠️ **错误信息**: 完整的错误堆栈和诊断信息
- ✅ **成功状态**: 各个组件的启动成功确认
- 🔍 **调试信息**: 配置加载、路径验证等调试数据

日志格式：
```
[2024-01-20 10:30:15] === 开机自动启动程序开始 ===
[2024-01-20 10:30:15] 配置文件加载成功
[2024-01-20 10:30:16] ✓ 虚拟机 arch 启动成功
[2024-01-20 10:30:19] ✓ Unity项目 Match2023 启动命令已执行
```

## 🛠️ 故障排除

### 虚拟机启动问题

| 问题 | 解决方案 |
|------|----------|
| VBoxManage.exe 不存在 | 检查 VirtualBox 安装路径，确认 `VBoxManagePath` 配置正确 |
| 虚拟机名称找不到 | 在 VirtualBox 中确认虚拟机名称，确保与配置文件中 `Name` 一致 |
| 启动权限问题 | 以管理员身份运行程序 |

### Unity项目启动问题

| 问题 | 解决方案 |
|------|----------|
| Unity编辑器找不到 | 确认 Unity Hub 已安装对应版本，或手动指定 Unity 编辑器路径 |
| 项目路径不存在 | 验证 `ProjectPath` 配置是否正确，路径是否存在 |
| 版本不匹配 | 确认 `UnityVersion` 与实际安装的Unity版本一致 |

### 浏览器启动问题

| 问题 | 解决方案 |
|------|----------|
| Firefox不存在 | 检查 Firefox 安装路径，更新 `FirefoxPath` 配置 |
| URL格式错误 | 确保URL使用完整格式（包含 http:// 或 https://） |

### 配置文件问题

| 问题 | 解决方案 |
|------|----------|
| JSON格式错误 | 使用 JSON 验证工具检查语法，注意逗号和引号 |
| 编码问题 | 确保文件以 UTF-8 编码保存 |
| 权限问题 | 确认程序对配置文件目录有读写权限 |

## 🔧 开发和扩展

### 添加新的启动服务

1. 在 `Models/` 文件夹创建新的配置模型类
2. 在 `Services/` 文件夹创建对应的服务类
3. 在 `Program.cs` 中添加服务注册和调用逻辑
4. 更新 `StartupConfig` 模型以包含新配置

### 代码结构说明

项目采用分层架构设计，遵循单一职责原则：

- **Models层**: 纯数据模型，定义配置结构
- **Managers层**: 管理器类，处理配置和日志
- **Services层**: 服务类，实现具体的启动逻辑
- **Program**: 程序入口，协调各层组件

### 开发环境

- Visual Studio 2022 或 Visual Studio Code
- .NET 10.0 SDK
- 推荐安装 C# 扩展包

## 📊 技术栈

| 技术 | 版本 | 用途 |
|------|------|------|
| .NET | 10.0 | 运行时框架 |
| C# | 12.0 | 编程语言 |
| System.Text.Json | 内置 | JSON 序列化 |
| System.Diagnostics.Process | 内置 | 进程管理 |

## 📄 许可证

本项目采用 MIT 许可证 - 查看 [LICENSE](./LICENSE) 文件了解详情

---

**AutoStartHub** - 让开机启动变得简单高效 ⚡