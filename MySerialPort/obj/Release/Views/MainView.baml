   M S B A M L   `   `   `  ���� G  CMySerialPort, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MySerialPort.ViewsMySerialPort.Views  A%clr-namespace:MySerialPort.ConvertersMySerialPort.Converters  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf !    MySerialPort.Views.MainView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:MySerialPort.Views     T PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59[ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  _	 [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes 	 6
converters%clr-namespace:MySerialPort.Converters 	 >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf 	    MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � Title$ MySerialPort��$
��1080��$	��650�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�#  Prism.Services.Dialogs.Dialog   WindowStyle ��  �� 
TargetType" �.���  �� Property � �� �� Value$	 600����  � ��$	 480����  �@�SizeToContent$ WidthAndHeight=���  �@�
ResizeMode$ NoResize=���  �@ WindowStartupLocation$ CenterScreen=�	 � 	Resources	 1   +MySerialPort.Converters.ToggleBoolConverter  ToggleBoolConverter��%   &	         .��� 
 � RowDefinitions
 �� $
��Auto ��� $
��Auto ��� �� $
��Auto �.+��� $��0��$��串口助手��$��Center=�$��Center=�$��20(�$��10q��� $��0��$��Designed By 陈皮皮��$��Right=�$��Bottom=�$��10(�$��12q��� $��0�����#DDDDDD$��	0 0 0 0.5q�� $��2�� � ColumnDefinitions �� �� $
��0.5* �.+�� $��0��.+��� ���#DDDDDD$��0 0 1 0q��� -    ��Name$ scrollViewer��$
��Auto=�$��Disabled=�.��#  LiveCharts.Wpf.CartesianChart  &   LiveCharts.Wpf.Charts.Base.Chart   DisableAnimations .    	Hoverable .     ScrollHorizontalFrom$ 0Q�   ScrollHorizontalTo$
 1000Q�$��10q�$��Visible=�   LegendLocation$	 Top=����� *SerialWidth+   Series   LiveCharts.Wpf.LineSeries    LiveCharts.Wpf.Series   Title$ 转速��   LineSmoothness$ 1Q�$��Visible=�   StrokeThickness$ 3Q�   Values �� *SerialPortValues+   AxisX   LiveCharts.Wpf.Axis  ��. $��Visible=�   	Separator 	  LiveCharts.Wpf.Separator	   	  Step$ 1Q�$��Visible=�   AxisY      MaxValue �� *	MaxNumber+   MinValue �� *	MinNumber+� $��1��
 �� �� $
��Auto �.+��� -    ��Name$ textBox��$��0��$��10q�" �� VerticalScrollBarVisibility$
 Auto=�$ �� HorizontalScrollBarVisibility$
 Auto=�  �� 
IsReadOnly  . ���� *SerialPortContent+�� $��1��$��Center=�! �� Orientation$! 
Horizontal=�.+��� $	��100��$��10q�$��Center=�$��Center=�$��Center=�" �� SelectedIndex" �� *CommandSelectedIndex+.@��� .��开始�� .��关闭�� .��加速�� .��减速�� $��执行��$��10q����� *	Command+� $��1��.+��� $! 
Horizontal=�$��5q�{��� # �� ElementName$# toggleButton��$ �� Path$$ 	IsChecked�% �� 	Converter#% [ .+��� $��搜索串口��$��10q����� *SearchCommand+�� $��Center=�$��Center=�$	��100��$��30��$��10q�$��Center=�$��Center=�" �� *SerialPortSelectedIndex+& �� Mode$& TwoWay=�' �� UpdateSourceTrigger$' PropertyChanged=����� *SerialPortNamesList+�� $��波特率：��$��Center=�$��15(�$��
10 10 5 10q��� $
��Left=�$��Center=�$	��100��$��30��$��5 10 10 10 q�$��Center=�$��Center=�" �� *BaudRateSelectedIndex+$& TwoWay=�$' PropertyChanged=����� *BaudRateList+�� $��数据位：��$��Center=�$��15(�$��
10 10 5 10q��� $
��Left=�$��Center=�$��50��$��30��$��5 10 10 10 q�$��Center=�$��Center=�" �� *BitsSelectedIndex+$& TwoWay=�$' PropertyChanged=����� *
BitsList+�� $��奇偶校验：��$��Center=�$��15(�$��
10 10 5 10q��� $
��Left=�$��Center=�$	��100��$��30��$��5 10 10 10 q�$��Center=�$��Center=�" �� *ParitySelectedIndex+$& TwoWay=�$' PropertyChanged=����� *
ParityList+�� $��停止位：��$��Center=�$��15(�$��
10 10 5 10q��� $
��Left=�$��Center=�$��50��$��30��$��5 10 10 10 q�$��Center=�$��Center=�" �� *StopBitsSelectedIndex+$& TwoWay=�$' PropertyChanged=����� *StopBitsList+�� $! 
Horizontal=�$��Right=�$��10q�.+��� $��打开串口：��$��Center=�$��15(�$��
10 10 5 10q�d� -   ( d�Name$( toggleButton�� $  MaterialDesignSwitchToggleButton#��[ $��Center=�*
  $Microsoft.Xaml.Behaviors.Interaction) 
  Triggers) +  %Microsoft.Xaml.Behaviors.EventTrigger  *   	EventName$* Checked��*  $Microsoft.Xaml.Behaviors.TriggerBase+   Actions.+ 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  ,   CommandParameter$
, Open��-   Command- �� *ExecuteCommand+  $* 	Unchecked��.+   $, Close��- �� *ExecuteCommand+� $��3��$	��100��.+��� $! 
Horizontal=�$��Center=�$��5q�.+��� . �� AcceptsReturn. . $��15(�$��Center=�$��10q�$��70��$	��350��$��1q����� *SerialPortSendContent+$& TwoWay=�$' PropertyChanged=��� $��发送��$��10q�{��� $# toggleButton��$$ 	IsChecked����� *SendCommand+�� $! Vertical=�$��Center=�.+��� $��发送选项��$��15(�$��5q�	� $��	HEX发送��$��15(�$��5q�/ d� 	IsChecked/ �� *	IsHexSend+$& TwoWay=�$' PropertyChanged=�	� $��
CHAR发送��$��15(�$��5q��� $! Vertical=�$��Center=�.+��� $��显示选项��$��15(�$��5q�	� $��	HEX显示��$��15(�$��5q�/ �� *	IsHexShow+$& TwoWay=�$' PropertyChanged=�	� $��
CHAR显示��$��15(�$��5q��� $! Vertical=�$��Center=�.+��� $��计数��$
��Bold"�$��15(�$��5q��� $��15(�$	��100��$��5q����� *ReceiveCount+0 �� StringFormat$0 接收：{0}���� $��15(�$	��100��$��5q����� *	SendCount+$0 发送：{0}���� $! Vertical=�$��Center=�.+��� $��清除接收��$��5q�$��	ClearShow������ *ClearCommand+�� $��清除发送��$��5q�$��	ClearSend������ *ClearCommand+�� $! Vertical=�$��Center=�.+��� $��清除计数��$��5q�$��
ClearCount������ *ClearCommand+�� $��保存数据��$��5q����� *SaveCommand+