using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UENSimulation.Windows;

namespace UENSimulation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region tjd
        private string[] staticCloudDetail = { "当前共有五个家庭参与调度与交易，其中：", "1.家庭A从家庭D买入热，并将余热卖给家庭B；", "2.家庭B从家庭A买入热，并将余热和余电分别卖给家庭C和家庭E；", "3.家庭C从家庭B买入热，从家庭D买入电；", "4.家庭D将余热卖给家庭A，将余电卖给家庭C和家庭E；", "5.家庭E从家庭B和家庭D买入电。" };

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("是否确定关闭", "选择", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            e.Cancel = (mbr == MessageBoxResult.No);
        }
        private void cloud_MouseDown(object sender, MouseButtonEventArgs e)
        {

            UENSimulation.CloudService cloudService = new UENSimulation.CloudService();
            cloudService.cloudDetail.Items.Clear();
            TextBox tb;
            foreach (string str in staticCloudDetail)
            {
                tb = new TextBox();
                tb.Margin = new Thickness(0, 0, 0, 10);
                tb.FontSize = 18;
                tb.Width = 150;
                tb.TextWrapping = System.Windows.TextWrapping.Wrap;
                tb.Text = str;
                cloudService.cloudDetail.Items.Add(tb);
            }


            cloudService.ShowDialog();

        }
        private void gateway_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Gateway gateway = new Gateway();
            gateway.ShowDialog();
        }

        private void familyButton_Click(object sender, RoutedEventArgs e)
        {
            FamilyInformation family = new FamilyInformation();
            family.ShowDialog();
        }
        #endregion

        #region sjl
        #region 箭头流动，动画效果实现

        #region 设置动画数据
        //设置动画组别和展示顺序:图片名、path名、文本框名
        private String[,] configstr = new String[,]{
                                   {"textblock_1","image_1,image_2", "path_3,path_4,path_5"},
                                    //显示文字，app直接发给网关或者app连接泛能云，通过云端发给网关
                                   {"textblock_2","image_1,image_4,image_3,image_10", "path_2,path_5,path_6"},
                                    //泛能网关载入家庭环境信息、云服务数据和家庭内传感表计数据
                                    {"textblock_3","image_4,image_9", ""},
                                    //根据载入数据，网关内部能源优化模块计算合适的控制策略
                                    {"textblock_4","image_7", "path_9"},
                                    //网关将各项优化指令下发到家庭各用能设备
                                    {"textblock_5","image_7", "path_18"},
                                    //用能设备状态改变，状态信息传递到泛能网关
                                    {"textblock_6","image_8", ""},
                                    //泛能网关'负荷预测'模块启动，利用IES和优化算法，计算能源负荷
                                     {"textblock_7","image_5,image_11,image_12", "path_7,path_1"},
                                    //泛能机控制器接收泛能网关的负荷信息，并载入设备参数和优化模拟数据
                                    {"textblock_8","image_6", "path_12,path_13,path_14,path_11,path_10"},
                                    //泛能机控制器优化根据各项参数，调整泛能机各部分工作状态，余电、余热上网
                                     {"textblock_9","", "path_15,path_16,path_17"},
                                    //泛能机输出多种能源供给用能设备
                                    {"textblock_10","image_7", ""},
                                    //用能设备获取能源，正常运行
                                     {"textblock_11","", ""}
                                    //执行完毕
                               };
        public String[,] GetConfigstr()
        {
            return configstr;
        }

        public void SetConfigstr(String[,] s)
        {
            configstr = s;
        }
        #endregion

        //设置图片闪烁次数，path流动时常，两组动画间隔时间
        private int blingtimes = 3;
        private double flowtime = 3;
        private double mstime = 10000;//毫秒
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartMove();
        }

        int num = 0;
        System.Timers.Timer timer = null;
        //设定动画方案，以一组为单位
        private void StartMove()
        {
            MoveTeam(null, null);
            timer = new System.Timers.Timer(mstime);
            timer.Elapsed += new ElapsedEventHandler(MoveTeam);
            timer.Start();
        }

        //将image、path和对应的文本框组成一组动画效果，image先闪烁，后path流动，后文本出现
        private void MoveTeam(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
                   {
                       String[,] zstr = GetConfigstr();
                       if (num < zstr.GetLength(0))
                       {
                           String textname = zstr[num, 0];
                           String imagename = zstr[num, 1];
                           String pathname = zstr[num, 2];
                           if (textname.Trim().Length > 0)
                           {
                               String[] tstr = textname.Split(',');
                               foreach (String str in tstr)
                               {
                                   TextBlock t = FindName(str) as TextBlock;
                                   ShowData(t);
                               }
                           }

                           if (imagename.Trim().Length > 0)
                           {
                               String[] istr = imagename.Split(',');
                               foreach (String str in istr)
                               {
                                   Image i = FindName(str) as Image;
                                   Bling(i);
                               }
                           }

                           if (pathname.Trim().Length > 0)
                           {
                               Flow(pathname); 
                           }
                           num += 1;
                       }
                       else if (timer != null)
                       {
                           if (P_th != null)
                           {
                               P_th.Abort();
                           }
                           timer.Stop();
                       }
                   }));
        }

        //控制文本框显示
        private void ShowData(TextBlock t)
        {
            t.Visibility = System.Windows.Visibility.Visible;
        }

        //控制某image完成闪烁效果
        private void Bling(Image image)
        {
            //Border长度关键帧动画
            DoubleAnimationUsingKeyFrames dak = new DoubleAnimationUsingKeyFrames();
            //关键帧定义
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(flowtime / 2))));
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(flowtime))));
            dak.RepeatBehavior = new RepeatBehavior(blingtimes);//动画重复次数
            //开始动画
            image.BeginAnimation(Border.OpacityProperty, dak);
        }

        //控制某path完成流动效果
        Thread P_th = null;
        private void Flow(String pathname)
        {
            String[] pstr = pathname.Split(',');
            //如果线程已存在则删除/取消该线程
            if (P_th != null)
            {
                P_th.Abort();
            }

            int i = 0;
            P_th = new Thread(//建立线程

             () =>//使用lambda表达式
             {
                 while (i <= flowtime * 2)//开始
                 {
                     this.Dispatcher.BeginInvoke(//在窗体线程中执行相关代码

                         System.Windows.Threading.DispatcherPriority.Normal,//设置线程优先级

                         (ThreadStart)(() =>//使用lambda表达式
                         {
                             foreach (string str in pstr)
                             {
                                 Path p = FindName(str) as Path;
                                 p.Stroke = new SolidColorBrush(Colors.Blue);
                                 p.StrokeDashOffset -= 10;
                             }
                         }));
                     Thread.Sleep(500);//线程挂起
                     //i += 1;
                 }
             });
            P_th.IsBackground = true;//设置线程为后台线程
            P_th.Start();//线程开始
        }
        #endregion

        #region 点击手动控制、场景选择按钮
        //手动控制
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EquControl ec = new EquControl();
            ec._btClick2 += new BTClick(BTClickFunc);
            ec.Show();
        }

        private void BTClickFunc()
        {

        }

        //场景选择
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SceneSet ss = new SceneSet();
            ss.Show();
        }
        #endregion
        #endregion 


        #region fcc
        //设备参数
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EquipmentParameterSet eps = new EquipmentParameterSet();
            eps.Show();
        }

        //优化模拟数据
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SimulatedDataSet sds = new SimulatedDataSet();
            sds.Show();
        }

        //泛能机
        private void image_6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UEMSystem us = new UEMSystem();
            us.Show();
        }
        #endregion
    }
}
