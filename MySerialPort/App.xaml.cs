using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MySerialPort.ViewModels;
using MySerialPort.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace MySerialPort
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView,MainViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }
    }
}
