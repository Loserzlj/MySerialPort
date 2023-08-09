using MySerialPortV2.Models;
using System;
using System.IO.Ports;  // 引用串口命名空间
using System.Text;
using System.Threading; // 引用线程命名空间
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace MySerialPortV2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 属性

        public SerialPort MySerialPort { get; set; }
        public Timer Timer { get; set; }

        #endregion 属性

        public MainWindow()
        {
            InitializeComponent();
            // 初始化串口以及定时器
            MySerialPort = new SerialPort();
            Timer = new Timer(new TimerCallback(Timer_Tick), null, 0, 1000);//1秒刷新一次
            // 寻找当前电脑所有串口
            string[] serials = SerialPortFindTool.GetSerialPort();
            // 给Combox添加串口号
            foreach (var item in serials)
            {
                comboBox.Items.Add(item);
            }
        }

        private void Timer_Tick(object state)
        {
        }

        private void button_Checked(object sender, RoutedEventArgs e)
        {
            string serialPortItem = comboBox.Items.GetItemAt(comboBox.SelectedIndex).ToString();
            string serialPortName = serialPortItem.Substring(serialPortItem.IndexOf('(') + 1, serialPortItem.IndexOf(')') - serialPortItem.IndexOf('(') - 1);
            try
            {
                MySerialPort = new SerialPort(serialPortName, 115200, Parity.None, 8, StopBits.One);
                MySerialPort.Open();
                MySerialPort.DataReceived += MySerialPort_DataReceived;
            }
            catch (System.Exception)
            {
                MessageBox.Show("串口打开失败");
                button.IsChecked = false;
            }
            
        }

        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = (SerialPort)sender;
            int bytesToRead = serialPort.BytesToRead;
            byte[] receiveData = new byte[bytesToRead];
            serialPort.Read(receiveData, 0, bytesToRead);
            this.Dispatcher.Invoke(() =>
            {
                string str = Encoding.ASCII.GetString(receiveData);
                textBox.AppendText(str);
                textBox.ScrollToEnd();
            });

            
        }

        private void button_Unchecked(object sender, RoutedEventArgs e)
        {
            if (MySerialPort.IsOpen)
            {
                MySerialPort.Close();
            }
            
        }
    }
}