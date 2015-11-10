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

namespace UENSimulation.UserControls
{
    /// <summary>
    /// SwitchUC.xaml 的交互逻辑
    /// </summary>
    public partial class SwitchUC : UserControl
    {
        public SwitchUC()
        {
            InitializeComponent();
        }

        #region 基础显示功能
        //总名称
        private string bname = "灯光";

        public string Bname
        {
            get { return bname; }
            set
            {
                bname = value;
                this.label_0.Content = bname;
            }
        }

        //右上名字
        private string funame = "照明";

        public string Funame
        {
            get { return funame; }
            set
            {
                funame = value;
                this.label_1.Content = funame;
            }
        }

        //设置照明、装饰是否选中效果
        private bool bfu = true;
        public bool Bfu
        {
            get { return bfu; }
            set
            {
                bfu = value;
                if (bfu)
                {
                    this.label_1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fccolour));
                    this.label_1.Tag = "1";
                }
                else
                {
                    this.label_1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fucolour));
                    this.label_1.Tag = "0";
                }
                SetBorder();
            }
        }

        private bool bfd = true;
        public bool Bfd
        {
            get { return bfd; }
            set
            {
                bfd = value;
                if (bfu)
                {
                    this.label_2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fccolour));
                    this.label_2.Tag = "1";
                }
                else
                {
                    this.label_2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fucolour));
                    this.label_2.Tag = "0";
                }

                SetBorder();
            }
        }

        //右下名字
        private string fdname = "装饰";

        public string Fdame
        {
            get { return fdname; }
            set
            {
                fdname = value;
                this.label_2.Content = fdname;
            }
        }

        private void label_1_MouseEnter(object sender, MouseEventArgs e)
        {
            this.label_1.Opacity = 0.8;
        }

        private void label_1_MouseLeave(object sender, MouseEventArgs e)
        {
            this.label_1.Opacity = 1;
        }

        private void label_2_MouseEnter(object sender, MouseEventArgs e)
        {
            this.label_2.Opacity = 0.8;
        }

        private void label_2_MouseLeave(object sender, MouseEventArgs e)
        {
            this.label_2.Opacity = 1;
        }
        #endregion

        #region 点击关联对应房间区域显示

        //分部选中颜色
        string fccolour = "#FF5B9BD5";
        //分部取消颜色
        string fucolour = "#FFADB9CA";
        //整体选中颜色
        string zccolour = "#FFBDD7EE";
        //整体取消颜色
        string zucolour = "#FFD6DCE5";
        private void label_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.label_1.Tag.Equals("0"))
            {
                this.label_1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fccolour));
                this.label_1.Tag = "1";
            }
            else
            {
                this.label_1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fucolour));
                this.label_1.Tag = "0";
            }
            SetBorder();
        }
        private void label_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.label_2.Tag.Equals("0"))
            {
                this.label_2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fccolour));
                this.label_2.Tag = "1";
            }
            else
            {
                this.label_2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fucolour));
                this.label_2.Tag = "0";
            }
            SetBorder();
        }

        private void SetBorder()
        {
            if (this.label_1.Tag.Equals("1") && this.label_2.Tag.Equals("1"))
            {
                this.border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(zccolour));
            }
            else
            {
                this.border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(zucolour));
            }
        }
        #endregion

        #region 返回值
        public bool GetUpState()
        {
            if (this.label_1.Tag.Equals("0"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool GetDownState()
        {
            if (this.label_2.Tag.Equals("0"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
