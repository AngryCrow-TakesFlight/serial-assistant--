# WPF-Tool
WPF Related projects
# 串口通信助手

一个功能完善的串口通信工具，支持文本和HEX模式的双向通信，适用于硬件调试、设备测试和串口通信开发。

## 功能特性

- **多协议支持**：
  - 文本模式（UTF-8编码）
  - HEX模式（十六进制格式）
- **完整的串口参数设置**：
  - 串口号、波特率（9600-115200）
  - 数据位（5-8）、校验位（None/Odd/Even）、停止位（One/Two/None）
- **实时通信**：
  - 自动接收并显示数据
  - 发送历史记录
  - 接收数据累积显示
- **用户友好界面**：
  - 发送/接收区分离
  - 状态反馈（串口打开/关闭）
  - 一键清除功能
- **多实例支持**：
  - 可同时运行多个程序实例进行通信测试

## 使用说明

### 基本操作

1. **连接串口**：
   - 选择串口号（如COM1）
   - 设置波特率（如9600）
   - 配置数据位、校验位和停止位
   - 点击"打开串口"按钮

2. **发送数据**：
   - 在发送区输入内容
   - 选择发送模式：
     - 文本模式：直接发送可读文本
     - HEX模式：发送十六进制值（如`13 21 3A`）
   - 点击"发送"按钮

3. **接收数据**：
   - 接收区自动显示数据
   - 可选择HEX显示模式查看原始字节

### 多实例测试（双串口通信验证）

1. **创建虚拟串口对**：
   - 使用虚拟串口工具（如com0com）创建COM1↔COM2端口对
  
   - 虚拟串口软件汉化：https://blog.csdn.net/liu3332456755/article/details/142796085

2. **启动两个程序实例**：
   ```bash
   # 实例1（COM1）
   # 实例2（COM2）

   - ctrl+f5
   
3. **配置串口参数**：

   - 实例1：选择COM1，波特率9600

   - 实例2：选择COM2，波特率9600

   - 确保其他参数一致

3. **测试通信：**：

   - 在实例1发送消息 → 实例2接收显示

   - 在实例2回复消息 → 实例1接收显示
  

  
