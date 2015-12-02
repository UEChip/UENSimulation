using System;
using System.Collections;
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
using UENSimulation.Utility;
using UENSimulation.UserControls;
using Visifire.Charts;

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
            XzLabelHidden();
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
                tb.Width = 270;
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

        #region 公用方法、设置数据
        //设置图片闪烁次数，path流动时常，两组动画间隔时间
        private int blingtimes = 4;
        private double flowtime = 4;
        private double mstime = 8000;//毫秒

        //全局变量，控制循环方法中的数据获取
        int num = 0;

        //所有的path name（不包括箭头，箭头name由此name得出
        string[] pstr = { "path_1", "path_2", "path_3", "path_4", "path_5", "path_6", "path_7", "path_9", "path_10",
                            "path_11", "path_12", "path_13", "path_14", "path_15", "path_16", "path_18"};

        //将path颜色恢复灰色
        private void RecoverPath()
        {
            foreach (string str in pstr)
            {
                Path p = FindName(str) as Path;
                p.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2E75B6"));
                p.StrokeDashOffset -= 10;
                Path pj = FindName("pathj_" + str.Split('_')[1]) as Path;
                pj.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2E75B6"));
            }
        }

        //将path颜色设为已选色
        private void SelectedPath()
        {
            foreach (string str in pstr)
            {
                Path p = FindName(str) as Path;
                p.Stroke = new SolidColorBrush(Colors.Blue);
                p.StrokeDashOffset -= 10;
                Path pj = FindName("pathj_" + str.Split('_')[1]) as Path;
                pj.Fill = new SolidColorBrush(Colors.Blue);
            }
        }

        System.Timers.Timer timer;
        //设定动画方案，以一组为单位
        private void StartMove()
        {
            num = 0;
            timer = null;
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

                    if (P_th != null)
                    {
                        P_th.Abort();
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
                if (timer != null && timer.Enabled != true)
                {
                    //此处编写动画运行完毕后程序代码
                    RecoverPath();
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
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(flowtime / (blingtimes * 2)))));
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(flowtime / blingtimes))));
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
                                 Path pj = FindName("pathj_" + str.Split('_')[1]) as Path;
                                 pj.Fill = new SolidColorBrush(Colors.Blue);
                             }
                         }));
                     Thread.Sleep(500);//线程挂起
                     i += 1;
                 }
             });
            P_th.IsBackground = true;//设置线程为后台线程
            P_th.Start();//线程开始
        }

        //收起展示文字
        private void TextCollapsed()
        {
            for (int i = 1; i < 51; i++)
            {
                if (FindName("textblock_" + i) != null)
                {
                    ((UIElement)FindName("textblock_" + i)).Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        //耗能、供能量统计数据归零，赋初值
        private void UtiliValue()
        {
            this.Consume_E.Content = "0 kWh";
            this.Consume_H.Content = "0 kWh";
            this.Consume_G.Content = "0 m³";
            this.Provide_E.Content = "0 kWh";
            this.Provide_H.Content = "0 kWh";
        }
        #endregion

        #region 整体调用流程展示
        #region 设置动画数据
        //设置动画组别和展示顺序:文本框名、图片名、path名
        private String[,] configstr;

        private String[,] _configstr = new String[,]{
                                   {"textblock_1","image_1,image_2", "path_3,path_4"},
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
                                     {"textblock_9","", "path_15,path_16"},
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

        //整体调用
        private void ztdy_Click(object sender, RoutedEventArgs e)
        {
            XzLabelHidden();
            bflag = false;
            UtiliValue();
            TextCollapsed();
            //算法线程
            Thread dataCalculation = new Thread(new ThreadStart(dataFromEnergyCalculation));
            dataCalculation.Start();

            SetConfigstr(_configstr);
            StartMove();
        }
        #endregion

        #region 区域调用流程展示

        //设置动画组别和展示顺序:文本框名、图片名、path名
        private String[,] _configstr2 = new String[,] { { "textblock_12", "image_7", "path_18" }, { "textblock_13", "image_4", "path_7" }, { "textblock_14", "image_5,image_11,image_12", "path_1" }, { "textblock_15", "image_6", "path_12,path_13,path_14,path_11,path_10" }, { "textblock_16", "", "path_15,path_16" }, { "textblock_16", "image_7", "" }, { "textblock_17", "", "" } };

        //区域调用
        private void qydy_Click(object sender, RoutedEventArgs e)
        {
            XzLabelHidden();
            bflag = false;
            UtiliValue();
            //算法线程
            Thread dataCalculation = new Thread(new ThreadStart(dataFromEnergyCalculation));
            dataCalculation.Start();

            TextCollapsed();
            SetConfigstr(_configstr2);
            StartMove();
        }
        #endregion

        #region 典型日24时调用展示
        #region 设置动画数据
        //设置动画组别和展示顺序:文本框名、图片名、path名
        private String[,] _configstr3 = new String[,] { 
        { "textblock_18", "image_4,image_5,image_6", "" }, 
        { "textblock_19", "image_4,image_5,image_6", "" },
        { "textblock_20", "image_4,image_5,image_6", "" },
        { "textblock_21", "image_4,image_5,image_6", "" },
        { "textblock_22", "image_4,image_5,image_6", "" },
        { "textblock_23", "image_4,image_5,image_6", "" },
        { "textblock_24", "image_4,image_5,image_6", "" },
        { "textblock_25", "image_4,image_5,image_6", "" },
        { "textblock_26", "image_4,image_5,image_6", "" },
        { "textblock_27", "image_4,image_5,image_6", "" },
        { "textblock_28", "image_4,image_5,image_6", "" },
        { "textblock_29", "image_4,image_5,image_6", "" },
        { "textblock_30", "image_4,image_5,image_6", "" },
        { "textblock_31", "image_4,image_5,image_6", "" },
        { "textblock_32", "image_4,image_5,image_6", "" },
        { "textblock_33", "image_4,image_5,image_6", "" },
        { "textblock_34", "image_4,image_5,image_6", "" },
        { "textblock_35", "image_4,image_5,image_6", "" },
        { "textblock_36",  "image_4,image_5,image_6", "" },
        { "textblock_37", "image_4,image_5,image_6", "" },
        { "textblock_38", "image_4,image_5,image_6", "" },
        { "textblock_39", "image_4,image_5,image_6", "" },
        { "textblock_40", "image_4,image_5,image_6", "" },
        { "textblock_41","image_4,image_5,image_6", "" },
         };
        #endregion
        private void dxrdy_Click(object sender, RoutedEventArgs e)
        {
            XzLabelShow();
            bflag = true;
            UtiliValue();
            SelectedPath();
            TextCollapsed();
            SetConfigstr(_configstr3);
            StartMoveDX();
            IntialChart();
        }

        //显示调用类型
        private void XzLabelShow()
        {
            this.xzlabel.Visibility = System.Windows.Visibility.Visible;
        }
        //隐藏调用类型
        private void XzLabelHidden()
        {
            this.xzlabel.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion
        #region 循环调用算法的方法实现
        string[] zstre;
        string[] zstrh;

        //设定动画方案，以一组为单位，与通用方法不同的是，这里要循环屡次调用算法模块
        private void StartMoveDX()
        {
            string strstate = txthandle.dataRead(@"..\..\Local Storage\mode_Season.txt")[0];
            SetTxtPath(strstate);
            zstre = txthandle.dataRead(strpath_e);
            zstrh = txthandle.dataRead(strpath_h);

            num = 0;
            timer = null;
            MoveTeam(null, null);
            timer = new System.Timers.Timer(mstime);
            timer.Elapsed += new ElapsedEventHandler(MoveTeam);
            timer.Elapsed += new ElapsedEventHandler(InvokeAl);
            timer.Start();
        }

        //操作txt类
        Txt_Handle txthandle = new Txt_Handle();
        Thread dataCalculationdx = null;
        //电需求存储文件路径
        string strpath_e = "";
        //热需求存储文件路径
        string strpath_h = "";
        //电、热需求文件，更改其中数据可调整电热需求
        string filePath_EnergyNeed = @"..\..\Local Storage\EnergyNeed.txt";
        //调用算法
        private void InvokeAl(object sender, ElapsedEventArgs e)
        {
            String axisLabel = num + "时";
            DataPoints_DE.Add(new DataPoint() { AxisXLabel = axisLabel, YValue = double.Parse(zstre[num]) });
            DataPoints_DH.Add(new DataPoint() { AxisXLabel = axisLabel, YValue = double.Parse(zstrh[num]) });
            string[] zstr = { zstre[num], zstrh[num] };
            txthandle.dataWrite(filePath_EnergyNeed, zstr);
            dataCalculationdx = new Thread(new ThreadStart(dataFromEnergyCalculation));
            dataCalculationdx.Start();
        }

        //根据“优化模拟数据”页面设置，获取当前选择（冬季W/夏季S/自定义C/春秋典型日A），定位相应保存文件
        private void SetTxtPath(string strstate)
        {
            switch (strstate)
            {
                case "W":
                    //冬季典型日电需求保存文件路径
                    strpath_e = @"..\..\Local Storage\needE_W.txt";
                    //冬季典型日热需求保存文件路径
                    strpath_h = @"..\..\Local Storage\needH_W.txt";
                    this.xzlabel.Content = "当前选择展示类型：冬季典型日";
                    break;
                case "S":
                    //夏季典型日电需求保存文件路径
                    strpath_e = @"..\..\Local Storage\needE_S.txt";
                    //夏季典型日热需求保存文件路径
                    strpath_h = @"..\..\Local Storage\needH_S.txt";
                    this.xzlabel.Content = "当前选择展示类型：夏季典型日";
                    break;
                case "SA"://春秋
                    strpath_e = @"..\..\Local Storage\needE_SA.txt";
                    strpath_h = @"..\..\Local Storage\needH_SA.txt";
                    this.xzlabel.Content = "当前选择展示类型：春秋季典型日";
                    break;
                case "M"://手动
                    strpath_e = @"..\..\Local Storage\needE_Manual.txt";
                    strpath_h = @"..\..\Local Storage\needH_Manual.txt";
                    this.xzlabel.Content = "当前选择展示类型：自定义内容";
                    break;
            }
        }
        #endregion

        #region 当前场景调用展示
        #region 设置动画数据
        //设置动画组别和展示顺序:文本框名、图片名、path名
        private String[,] _configstr4 = new String[,] { 
        { "textblock_42", "image_4,image_5,image_6", "" }, 
        { "textblock_43", "image_4,image_5,image_6", "" },
        { "textblock_44", "image_4,image_5,image_6", "" },
        { "textblock_45", "image_4,image_5,image_6", "" },
        { "textblock_46", "image_4,image_5,image_6", "" },
        { "textblock_47", "image_4,image_5,image_6", "" },
        { "textblock_48","image_4,image_5,image_6", "" },
        { "textblock_49","image_4,image_5,image_6", "" },
        { "textblock_50","image_4,image_5,image_6", "" },
         };
        #endregion
        private void cjdy_Click(object sender, RoutedEventArgs e)
        {
            XzLabelHidden();
            bflag = true;
            UtiliValue();
            SelectedPath();
            TextCollapsed();
            SetConfigstr(_configstr4);
            StartMoveDX();
        }
        #endregion
        #endregion

        #region 点击手动控制、场景选择按钮
        //手动控制
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EquControl ec = new EquControl();
            ec._btClick += new BTClick(btClick);
            ec._manualControl += new ManualControl(dataFromEnergyCalculation);
            Thread dataCalculation = new Thread(new ThreadStart(ec._manualControl));
            dataCalculation.Start();
            //Thread dataCalculation = new Thread(new ThreadStart(dataFromEnergyCalculation));
            //dataCalculation.Start();
            ec.Show();
        }

        private void btClick(List<string> lsequ, List<string> lsstate)
        {
            if (lsequ != null && lsstate != null && lsequ.Count == lsstate.Count)
            {
                for (int i = 0; i < lsequ.Count; i++)
                {
                    if (FindName(lsequ[i]) != null)
                    {
                        if (!lsequ[i].Split('_')[1].Contains("tem"))
                        {
                            Image im = FindName(lsequ[i]) as Image;
                            if (lsstate[i].Equals("on"))
                            {
                                im.Opacity = 1;
                            }
                            else if (lsstate[i].Equals("off"))
                            {
                                im.Opacity = 0;
                            }
                        }
                        else
                        {
                            if (FindName(lsequ[i] + "_text") != null)
                            {
                                Label l = FindName(lsequ[i] + "_text") as Label;
                                if (FindName(lsequ[i]) != null && !lsstate[i].Equals(l.Content.ToString().Trim()))
                                {
                                    Image im2 = FindName(lsequ[i]) as Image;
                                    im2.Opacity = 1;
                                }
                                if (FindName(lsequ[i] + "_tem") != null && !lsstate[i].Equals(l.Content.ToString().Trim()))
                                {
                                    Image im3 = FindName(lsequ[i] + "_tem") as Image;
                                    im3.Opacity = 1;
                                }
                                l.Content = lsstate[i] + "℃";
                            }
                        }
                    }
                }
            }
        }

        //场景选择
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SceneSet ss = new SceneSet();
            ss.Show();
        }
        #endregion

        #region 可点击图片鼠标移入移出效果
        private void image_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Image)sender).Opacity = 0.8;
        }

        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Image)sender).Opacity = 1;
        }
        #endregion

        #region 鼠标移入，显示chart曲线图效果

        //控制是否鼠标移入图片时显示图表控件---只有点击“家庭24时调用展示”后才显示
        bool bflag = false;
        #region 泛能管家移入移出实现
        #region 数据
        ChartLineUC cl_fnwg = null;
        //绑定到图表控件的数据点List
        public List<DataPointCollection> ListDataPoints
        {
            get;
            set;
        }
        //24h需求数据——耗电
        public DataPointCollection DataPoints_DE
        {
            get;
            set;
        }
        //24h需求数据——耗热
        public DataPointCollection DataPoints_DH
        {
            get;
            set;
        }
        #endregion
        private void Chart_ImageMouseEnter(object sender, MouseEventArgs e)
        {
            if (bflag)
            {
                ((UIElement)sender).Opacity = 0.8;
                Point point = ((UIElement)sender).TranslatePoint(new Point(0, 0), (UIElement)zgrid);
                if (cl_fnwg != null)
                {
                    cl_fnwg.Margin = new Thickness(point.X + 100, point.Y, 0, 0);
                    cl_fnwg.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    //数据：数值+x轴时间+标题+单位（？）+曲线颜色
                    ListDataPoints = new List<DataPointCollection>();
                    DataPoints_DE = new DataPointCollection();
                    DataPoints_DH = new DataPointCollection();
                    ListDataPoints.Add(DataPoints_DE);
                    ListDataPoints.Add(DataPoints_DH);
                    string[] zstr = { "电需求(kW)", "热需求(kW)" };
                    cl_fnwg = new ChartLineUC(ListDataPoints, zstr);
                    //_stackPanel为子元素，_grid为父元素
                    cl_fnwg.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    cl_fnwg.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    cl_fnwg.Margin = new Thickness(point.X + 100, point.Y, 0, 0);
                    this.zgrid.Children.Add(cl_fnwg);
                }
            }
        }
        private void Chart_ImageMouseLeaveChart(object sender, MouseEventArgs e)
        {
            if (bflag)
            {
                ((UIElement)sender).Opacity = 0.8;
                if (cl_fnwg != null)
                {
                    cl_fnwg.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }
        #endregion

        #region 泛能机输入输出Label移入移出实现
        #region 相关数据
        ChartLineUC cl_ye = null;
        //余电
        public DataPointCollection DataPoints_YE
        {
            get;
            set;
        }
        ChartLineUC cl_yh = null;
        //余热
        public DataPointCollection DataPoints_YH
        {
            get;
            set;
        }
        ChartLineUC cl_se = null;
        //输入电（市电）
        public DataPointCollection DataPoints_SE
        {
            get;
            set;
        }
        ChartLineUC cl_sh = null;
        //输入热
        public DataPointCollection DataPoints_SH
        {
            get;
            set;
        }
        ChartLineUC cl_sg = null;
        //输入燃气
        public DataPointCollection DataPoints_SG
        {
            get;
            set;
        }
        ChartLineUC cl_pe = null;
        //输出电
        public DataPointCollection DataPoints_PE
        {
            get;
            set;
        }
        ChartLineUC cl_ph = null;
        //输出热
        public DataPointCollection DataPoints_PH
        {
            get;
            set;
        }
        #endregion
        ChartLineUC cuc;
        private void Chart_LabelMouseEnter(object sender, MouseEventArgs e)
        {
            if (bflag)
            {
                ((UIElement)sender).Opacity = 0.8;
                Label lb = sender as Label;
                switch (lb.Name.ToString().Trim())
                {
                    //余电
                    case "outE":
                        cuc = cl_ye;
                        break;
                    //余热
                    case "outH":
                        cuc = cl_yh;
                        break;
                    case "outsideE":
                        cuc = cl_se;
                        break;
                    case "outsideH":
                        cuc = cl_sh;
                        break;
                    case "gas":
                        cuc = cl_sg;
                        break;
                    case "needE":
                        cuc = cl_pe;
                        break;
                    case "needH":
                        cuc = cl_ph;
                        break;
                    default:
                        cuc = new ChartLineUC();
                        break;
                }

                Point point = ((UIElement)sender).TranslatePoint(new Point(0, 0), (UIElement)zgrid);
                if (cuc != null)
                {
                    cuc.Margin = new Thickness(point.X + 100, point.Y, 0, 0);
                    cuc.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    //_stackPanel为子元素，_grid为父元素
                    cuc.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    cuc.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    cuc.Margin = new Thickness(point.X + 100, point.Y, 0, 0);
                    this.zgrid.Children.Add(cuc);
                }
            }

        }
        private void Chart_LabelMouseLeaveChart(object sender, MouseEventArgs e)
        {
            if (bflag)
            {
                ((UIElement)sender).Opacity = 0.8;
                if (cuc != null)
                {
                    cuc.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        //初始化图表控件和数据点，将其互相赋值绑定
        private void IntialChart()
        {
            //余电
            DataPoints_YE = new DataPointCollection();
            cl_ye = new ChartLineUC(DataPoints_YE, "余电(kW)");
            //余热
            DataPoints_YH = new DataPointCollection();
            cl_yh = new ChartLineUC(DataPoints_YH, "余热(kW)");
            //输入电
            DataPoints_SE = new DataPointCollection();
            cl_ph = new ChartLineUC(DataPoints_SE, "市电(kW)");
            //输入热
            DataPoints_SH = new DataPointCollection();
            cl_sh = new ChartLineUC(DataPoints_SH, "输入热(kW)");
            //输入气
            DataPoints_SG = new DataPointCollection();
            cl_sg = new ChartLineUC(DataPoints_SG, "输入气(m³/h)");
            //输出电
            DataPoints_PE = new DataPointCollection();
            cl_pe = new ChartLineUC(DataPoints_PE, "输出电(kW)");
            //输出热
            DataPoints_DH = new DataPointCollection();
            cl_ph = new ChartLineUC(DataPoints_DH, "输出热(kW)");
        }
        #endregion
        #endregion

        #region 点击家庭全年调用展示
        private void qndy_Click(object sender, RoutedEventArgs e)
        {
            YearDayShow yds = new YearDayShow();
            yds.Show();
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

        //主页面计算泛能机相关的输入输出数据
        private void dataFromEnergyCalculation()
        {
            string filePath_EquipmentParameter = @"..\..\Local Storage\EquipmentParameter.txt";
            string filePath_SimulatedData = @"..\..\Local Storage\SimulatedData.txt";
            string filePath_EnergyNeed = @"..\..\Local Storage\EnergyNeed.txt";
            string filePath_Mode = @"..\..\Local Storage\Mode.txt";

            EnergyCalculation energyCalculation = new EnergyCalculation(filePath_EquipmentParameter, filePath_SimulatedData, filePath_EnergyNeed, filePath_Mode);

            //光伏
            double output_PT;
            output_PT = energyCalculation.pt();

            //光热
            double output_PV;
            output_PV = energyCalculation.pv();

            //储热
            double[] output_SaveH = new double[2];
            output_SaveH = energyCalculation.saveH();

            //储电
            double output_SaveE;
            output_SaveE = energyCalculation.saveE();

            //泛能机
            double[] output_UEMachine = new double[3];
            output_UEMachine = energyCalculation.ueMachine();

            //补燃锅炉
            double[] output_GasBoiler = new double[2];
            output_GasBoiler = energyCalculation.gasBoiler();


            //优化
            ArrayList output_Ctrlopt = new ArrayList();
            output_Ctrlopt = energyCalculation.ctrlopt_Array();

            //设备效率
            double[] outPut_Etacal = new double[3];
            outPut_Etacal = energyCalculation.etacal();

            this.Dispatcher.Invoke(new Action(() =>
            {
                String axisLabel = num + "时";
                //额外的电和热
                outsideE.Content = "电： " + output_Ctrlopt[10].ToString() + " kW";
                DataPoints_SE.Add(new DataPoint() { AxisXLabel = axisLabel, YValue = double.Parse(output_Ctrlopt[10].ToString()) });
                outsideH.Content = "热： " + output_Ctrlopt[11].ToString() + " kW";
                DataPoints_SH.Add(new DataPoint() { AxisXLabel = axisLabel, YValue = double.Parse(output_Ctrlopt[11].ToString()) });

                //燃气
                gas.Content = "天然气： " + (output_UEMachine[2] + output_GasBoiler[1]).ToString() + " m³/h";
                DataPoints_SG.Add(new DataPoint() { AxisXLabel = axisLabel, YValue = double.Parse((output_UEMachine[2] + output_GasBoiler[1]).ToString()) });

                //泛能机系统输出的电和热
                needE.Content = "电： " + energyCalculation.EnergyNeed.Electricity_Need + " kW";
                DataPoints_PE.Add(new DataPoint() { AxisXLabel = axisLabel, YValue = energyCalculation.EnergyNeed.Electricity_Need });
                needH.Content = "热： " + energyCalculation.EnergyNeed.Heat_Need + "kW";
                DataPoints_PH.Add(new DataPoint() { AxisXLabel = axisLabel, YValue = energyCalculation.EnergyNeed.Heat_Need });


                outE.Content = "余电： 0 kW";
                DataPoints_YE.Add(new DataPoint() { AxisXLabel = axisLabel, YValue = 0 });
                outH.Content = "余热： 0 kW";
                DataPoints_YH.Add(new DataPoint() { AxisXLabel = axisLabel, YValue = 0 });

                efficiency_E_uemachine.Content = (outPut_Etacal[0] * 100).ToString("0.00") + "%";
                efficiency_H_uemachine.Content = (outPut_Etacal[1] * 100).ToString("0.00") + "%";
                efficiency_E_gasboiler.Content = (outPut_Etacal[2] * 100).ToString("0.00") + "%";

                string str1 = outsideE.Content.ToString().TrimStart('电').TrimStart('：').TrimEnd('W').TrimEnd('k').Trim();
                string str2 = Consume_E.Content.ToString().TrimEnd('h').TrimEnd('W').TrimEnd('k').Trim();
                Consume_E.Content = double.Parse(str1) + double.Parse(str2) + " kWh";
                Consume_H.Content = double.Parse(outsideH.Content.ToString().TrimStart('热').TrimStart('：').TrimEnd('W').TrimEnd('k').Trim()) + double.Parse(Consume_H.Content.ToString().TrimEnd('h').TrimEnd('W').TrimEnd('k').Trim()) + " kWh";
                string str = gas.Content.ToString().TrimStart('天').TrimStart('然').TrimStart('气').TrimStart('：').TrimEnd('h').TrimEnd('/').Trim();
                string str3 = Consume_G.Content.ToString().Trim();
                Consume_G.Content = double.Parse(str.Substring(0, str.Length - 2).Trim()) + double.Parse(str3.Substring(0, str3.Length - 2).Trim()) + " m³";
                Provide_E.Content = double.Parse(needE.Content.ToString().TrimStart('电').TrimStart('：').TrimEnd('W').TrimEnd('k').Trim()) + double.Parse(Provide_E.Content.ToString().TrimEnd('h').TrimEnd('W').TrimEnd('k').Trim()) + " kWh";
                Provide_H.Content = double.Parse(needH.Content.ToString().TrimStart('热').TrimStart('：').TrimEnd('W').TrimEnd('k').Trim()) + double.Parse(Provide_H.Content.ToString().TrimEnd('h').TrimEnd('W').TrimEnd('k').Trim()) + " kWh";
            }));
        }
        #endregion
    }
}