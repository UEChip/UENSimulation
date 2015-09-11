using System;
using System.Collections.Generic;
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

namespace FNX_DisplaySimulationPlatform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] staticDate = { "12:00:01.135：客户端指令下发到泛能网关", "12:00:01.136：泛能网关载入家庭环境信息、云服务数据和家庭内传感表计数据", "12:00:01.138：根据载入数据，网关内部能源优化模块计算合适的控制策略", "12:00:01.150：网关将各项优化指令下发到家庭各用能设备", "12:00:01.160：用能设备状态改变，状态信息传递到泛能网关", "12:00:01.167：泛能网关“负荷预测”模块启动，利用IES和优化算法，计算能源负荷", "12:00:01.170：泛能机控制器接收泛能网关的负荷信息，并载入设备参数和优化模拟数据", "12:00:01.190：泛能机控制器优化根据各项参数，调整泛能机各部分工作状态，余电、余热上网", "12:00:01.200：泛能机输出多种能源供给用能设备", "12:00:01.210：用能设备获取能源，正常运行", "12:00:01.220：执行完毕" };
        public void StateGet(){
             state.Items.Clear();
            TextBox tb;
            foreach (string str in staticDate)
            {
                tb = new TextBox();
                tb.Margin = new Thickness(0, 0, 0, 10);
                tb.FontSize = 18;
                tb.Width = 250;
                tb.TextWrapping = System.Windows.TextWrapping.Wrap;
                tb.Text = str;
                state.Items.Add(tb);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            StateGet();
        }
    }
}
