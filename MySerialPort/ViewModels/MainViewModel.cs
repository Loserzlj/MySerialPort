using LiveCharts;
using Microsoft.Win32;
using MySerialPort.Converters;
using MySerialPort.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace MySerialPort.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region MyRegion

        public int SerialPortSelectedIndex { get; set; }
        public int BaudRateSelectedIndex { get; set; }
        public int BitsSelectedIndex { get; set; }
        public int ParitySelectedIndex { get; set; }
        public int StopBitsSelectedIndex { get; set; }
        public int CommandSelectedIndex { get; set; }
        public MyLog log { get; set; }
        public ObservableCollection<int> BaudRateList { get; set; }
        public ObservableCollection<int> BitsList { get; set; }
        public ObservableCollection<string> ParityList { get; set; }
        public ObservableCollection<double> StopBitsList { get; set; }

        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand<string> ExecuteCommand { get; set; }
        public DelegateCommand<string> ClearCommand { get; set; }
        public DelegateCommand<string> AutoCommand { get; set; }
        public DelegateCommand Command { get; set; }
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public ObservableCollection<byte> Buffer { get; set; }
        private string _serialPortContent;
        private int _serialWidth;

        public int SerialWidth
        {
            get { return _serialWidth; }
            set { _serialWidth = value; RaisePropertyChanged("SerialWidth"); }
        }

        public bool IsHexShow { get; set; }
        public bool IsHexSend { get; set; }

        private double _receiveCount;
        private double _sendCount;
        private string _serialPortSendContent;
        private double _maxNumber;
        private double _minNumber;

        public double MinNumber
        {
            get { return _minNumber; }
            set { _minNumber = value; RaisePropertyChanged("MinNumber"); }
        }

        public double MaxNumber
        {
            get { return _maxNumber; }
            set { _maxNumber = value; RaisePropertyChanged("MaxNumber"); }
        }

        private ChartValues<double> _serialPortValues;

        public ChartValues<double> SerialPortValues
        {
            get { return _serialPortValues; }
            set
            {
                _serialPortValues = value;
                RaisePropertyChanged("SerialPortValues");
            }
        }

        public string SerialPortSendContent
        {
            get { return _serialPortSendContent; }
            set
            {
                _serialPortSendContent = value;
                RaisePropertyChanged("SerialPortSendContent");
            }
        }

        public double SendCount
        {
            get { return _sendCount; }
            set { _sendCount = value; RaisePropertyChanged("SendCount"); }
        }

        public double ReceiveCount
        {
            get { return _receiveCount; }
            set
            {
                _receiveCount = value;
                RaisePropertyChanged("ReceiveCount");
            }
        }

        public string SerialPortContent
        {
            get { return _serialPortContent; }
            set { _serialPortContent = value; RaisePropertyChanged("SerialPortContent"); }
        }

        private ObservableCollection<string> _serialProtNamesList;

        public ObservableCollection<string> SerialPortNamesList
        {
            get { return _serialProtNamesList; }
            set
            {
                _serialProtNamesList = value;
                RaisePropertyChanged("SerialPortNamesList");
            }
        }

        private SerialPort _mySerialPort;

        public SerialPort MySerialPort
        {
            get { return _mySerialPort; }
            set { _mySerialPort = value; RaisePropertyChanged("MySerialPort"); }
        }

        #endregion MyRegion

        public MainViewModel()
        {
            log = new MyLog();
            SerialWidth = 680;

            SerialPortNamesList = new ObservableCollection<string>(SerialPort.GetPortNames());

            SerialPortSelectedIndex = 0;
            MySerialPort = new SerialPort();

            SerialPortValues = new ChartValues<double>();
            MinNumber = 0;
            MaxNumber = 100;

            BaudRateList = new ObservableCollection<int>();
            BaudRateList.Add(4800);
            BaudRateList.Add(9600);
            BaudRateList.Add(38400);
            BaudRateList.Add(115200);
            BaudRateList.Add(500000);
            BaudRateSelectedIndex = 3;

            BitsList = new ObservableCollection<int>();
            BitsList.Add(8);
            BitsList.Add(9);
            BitsSelectedIndex = 0;

            ParityList = new ObservableCollection<string>();
            ParityList.Add("无奇偶校验");
            ParityList.Add("奇校验");
            ParityList.Add("偶校验");
            ParitySelectedIndex = 0;

            StopBitsList = new ObservableCollection<double>();
            StopBitsList.Add(0);
            StopBitsList.Add(1);
            StopBitsList.Add(1.5);
            StopBitsList.Add(2);
            StopBitsSelectedIndex = 1;

            IsHexShow = IsHexSend = true;
            ReceiveCount = SendCount = 0;

            SearchCommand = new DelegateCommand(Search);
            ExecuteCommand = new DelegateCommand<string>(Execute);
            ClearCommand = new DelegateCommand<string>(Clear);
            SendCommand = new DelegateCommand(SendData);
            SaveCommand = new DelegateCommand(Save);
            Command = new DelegateCommand(command);
            Buffer = new ObservableCollection<byte>();

            log.WriteLog("操作", "用户登录系统");
            Task.Factory.StartNew(Update);
        }

        private void command()
        {
            byte[] buffer = { 0 };
            if (CommandSelectedIndex == 0)
            {
                buffer[0] = 1;
            }
            else if (CommandSelectedIndex == 1)
            {
                buffer[0] = 2;
            }
            else if (CommandSelectedIndex == 2)
            {
                buffer[0] = 3;
            }
            else if (CommandSelectedIndex == 3)
            {
                buffer[0] = 4;
            }
            else
            {
            }
            MySerialPort.Write(buffer, 0, 1);
        }

        private void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SendData();
        }

        public void DrawLineSerial()
        {
            while (true)
            {
                if (Buffer.Count == 0)
                {
                    return;
                }
                else
                {
                    SerialPortValues.Add((double)Buffer[0]);
                    Buffer.RemoveAt(0);
                }

                if (SerialPortValues.Count >= 100)
                {
                    SerialPortValues.RemoveAt(0);
                }

                SerialWidth = 680 + SerialPortValues.Count * 5;
                GetAxis();
            }
        }

        private void Save()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = Environment.CurrentDirectory;
            fileDialog.Filter = "TXT记事本|*.txt";
            string path;
            if (fileDialog.ShowDialog() == true)
            {
                path = fileDialog.FileName;
                try
                {
                    System.IO.File.WriteAllText(path, SerialPortContent);
                }
                catch (Exception)
                {
                    MessageBox.Show("保存失败！");
                }
            }
        }

        private void Clear(string obj)
        {
            if (obj == "ClearShow")
            {
                SerialPortContent = String.Empty;
                SerialPortValues.Clear();
            }
            else if (obj == "ClearSend")
            {
                SerialPortSendContent = String.Empty;
            }
            else
            {
                ReceiveCount = SendCount = 0;
            }
        }

        public void Update()
        {
            while (true)
            {
                Thread.Sleep(1);
                App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(DrawLineSerial));
            }

        }

        private void Execute(string obj)
        {
            if (obj == "Open")
            {
                MySerialPort = new SerialPort(SerialPortNamesList[SerialPortSelectedIndex],
                    BaudRateList[BaudRateSelectedIndex]);
                switch (ParitySelectedIndex)
                {
                    case 0:
                        MySerialPort.Parity = Parity.None;
                        break;

                    case 1:
                        MySerialPort.Parity = Parity.Even;
                        break;

                    default:
                        MySerialPort.Parity = Parity.Odd;
                        break;
                }
                switch (BitsSelectedIndex)
                {
                    case 0:
                        MySerialPort.DataBits = 8;
                        break;

                    default:
                        MySerialPort.DataBits = 9;
                        break;
                }

                switch (StopBitsSelectedIndex)
                {
                    case 0:
                        MySerialPort.StopBits = StopBits.None;
                        break;

                    case 1:
                        MySerialPort.StopBits = StopBits.One;
                        break;

                    case 2:
                        MySerialPort.StopBits = StopBits.OnePointFive;
                        break;

                    default:
                        MySerialPort.StopBits = StopBits.Two;
                        break;
                }

                try
                {
                    MySerialPort.Open();
                }
                catch (Exception e)
                {
                    MessageBox.Show("串口打开失败！", "警告");
                    return;
                }

                MySerialPort.DataReceived += MySerialPort_DataReceived;
            }
            else
            {
                MySerialPort.Close();
            }
        }

        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = (SerialPort)sender;
            int bytesToRead = serialPort.BytesToRead;
            byte[] receiveData = new byte[bytesToRead];
            serialPort.Read(receiveData, 0, bytesToRead);
            String ReceiveData;

            if (IsHexShow)
            {
                ReceiveData = ByteArrayConverter.ByteArrayToHexString(receiveData);
                if (ReceiveData.Contains('\n'))
                {
                    SerialPortContent += "R Hex " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ff") + "  <---  " + ReceiveData;
                }
                else
                {
                    SerialPortContent += "R Hex " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ff") + "  <---  " + ReceiveData + "\r\n";
                }
            }
            else
            {
                ReceiveData = ByteArrayConverter.ByteArrayToDecString(receiveData);
                if (ReceiveData.Contains('\n'))
                {
                    SerialPortContent += "R Char " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ff") + "  <---  " + ReceiveData;
                }
                else
                {
                    SerialPortContent += "R Char " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ff") + "  <---  " + ReceiveData + "\r\n";
                }
            }

            ReceiveCount++;
            string[] str = SerialPortContent.Split('\n');
            int lineCount = str.Length;
            if (lineCount > 5000)
            {
                int index = SerialPortContent.IndexOf('\n');
                SerialPortContent = SerialPortContent.Remove(0, index + 1);
            }

            if (receiveData.Length > 0)
            {
                Buffer.Add(receiveData[0]);
            }
            
        }

    private void SendData()
    {
        if (!String.IsNullOrWhiteSpace(SerialPortSendContent))
        {
            if (IsHexSend)
            {
                byte[] bytes = ByteArrayConverter.HexStringToByteArray(SerialPortSendContent);
                if (bytes[0] == 0)
                {
                    Clear("ClearSend");
                    return;
                }
                MySerialPort.Write(bytes, 0, bytes.Length);
                string str = ByteArrayConverter.ByteArrayToHexString(bytes);
                SerialPortContent += "W " + "Hex " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ff") + "  --->  " + str + "\r\n";
            }
            else
            {
                char[] data = SerialPortSendContent.ToCharArray();
                MySerialPort.Write(data, 0, SerialPortSendContent.Length);
                SerialPortContent += "W " + " Char " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ff") + "  --->  " + SerialPortSendContent + "\r\n";
            }

            SendCount++;
        }
        else
        {
            MessageBox.Show("不能发送空数据！");
        }
    }

    private void Search()
    {
        SerialPortNamesList = new ObservableCollection<string>(SerialPort.GetPortNames());
        SerialPortSelectedIndex = 0;
    }

    public void GetAxis()
    {
        MaxNumber = SerialPortValues.Max() + 10;
        MinNumber = SerialPortValues.Min() - 10;
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return false;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        MySerialPort.Close();
    }
}
}