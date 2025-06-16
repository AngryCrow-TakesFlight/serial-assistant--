using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace serial_assistant
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPort serialPort;
        public MainWindow()
        {
            InitializeComponent();
            //初始化可用的串口列表
            InitializeSerialPortList();
        }

        /// <summary>
        /// 初始化当前机器上可用的串口列表并添加到串口的下拉列表框中
        /// </summary>
        private void InitializeSerialPortList()
        {
            
            // 获取本地串口数组
            var porstlist= SerialPort.GetPortNames();
            //绑定串口到下拉列表框中
           
            cbPort.ItemsSource = porstlist;
            //选择第一个选项
            cbPort.SelectedIndex = 0;
        }
        /// <summary>
        /// 定义打开串口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenSerialPort_Click(object sender, RoutedEventArgs e)
        {
            if (serialPort!=null && serialPort.IsOpen)
            {
                //关闭串口
                if (CloseSerialPort())
                {
                    BtnOpenSerialPort.Content = "打开串口";
                    BtnOpenSerialPort.Background=Brushes.LightBlue;
                }
            }
            else
            {
                if (OpenSerialPort())
                {
                    //打开串口
                    BtnOpenSerialPort.Content = "关闭串口";
                    BtnOpenSerialPort.Background = Brushes.Green;
                }
            }
        }
        /// <summary>
        /// 打开串口方法
        /// </summary>
        /// <returns></returns>
        private bool OpenSerialPort()
        {
            bool flag = false;
            try
            {
                //创建串口对象
                serialPort = new SerialPort();
                //设置串口号
                serialPort.PortName = cbPort.Text;
                //设置波特率
                serialPort.BaudRate = int.Parse(cbBaudRate.Text);
                //设置数据位
                serialPort.DataBits = int.Parse(cbDataBits.Text);
                //设置校验位
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), cbParity.Text);
                //设置停止位
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text);
                flag = true;
                serialPort.Open();
                serialPort.DataReceived += SerialPort_DataReceived;
            }
            catch (Exception)
            {

                throw;
            }
            return flag;
        }
        /// <summary>
        /// 串口数据自动接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //获取串口缓冲区的字节数
            int count = serialPort.BytesToRead;
            //实例化接收串口中数据的缓冲区
            byte[] buffer = new byte[count];
            //把串口中的数据读取到缓冲区
            serialPort.Read(buffer,0,count);
            //需要把读取的结果转换为一个字符串
            string receivedData = Encoding.UTF8.GetString(buffer);
            //切换到UI线程给对应接收数据的文本框赋值
            Dispatcher.Invoke(()=> 
            {
                if (chkRecHex.IsChecked==true) { };
                txtReceiveData.Text += receivedData;
            });
            
        }
        /// <summary>
        /// 关闭串口方法
        /// </summary>
        /// <returns></returns>
        private bool CloseSerialPort()
        {
            bool flag = false;
            try
            {
                serialPort.Close();
                flag = true;
            }
            catch (Exception)
            {

                throw;
            }
            return flag;
        }
        /// <summary>
        /// 消息发送事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //获取文本框数据
                var datas = Encoding.Default.GetBytes(txtSendData.Text);
                //发送数据到指定串口
                serialPort.Write(datas, 0, datas.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
