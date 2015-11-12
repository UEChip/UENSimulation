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
    /// SetValueUC.xaml 的交互逻辑
    /// </summary>
    public partial class SetValueUC : UserControl
    {
        public SetValueUC()
        {
            InitializeComponent();
        }

        #region 显示效果
        //按钮效果，鼠标移入改变显示
        private void image_xs_MouseEnter(object sender, MouseEventArgs e)
        {
            this.image_xs.Opacity = 0.8;
        }

        private void image_xs_MouseLeave(object sender, MouseEventArgs e)
        {
            this.image_xs.Opacity = 1;
        }

        private void border_value_MouseEnter(object sender, MouseEventArgs e)
        {
            this.border_value.Opacity = 0.8;
        }

        private void border_value_MouseLeave(object sender, MouseEventArgs e)
        {
            this.border_value.Opacity = 1;
        }
        private void image_xx_MouseEnter(object sender, MouseEventArgs e)
        {
            this.image_xx.Opacity = 0.8;
        }

        private void image_xx_MouseLeave(object sender, MouseEventArgs e)
        {
            this.image_xx.Opacity = 1;
        }

        private string protitle = "温度";
        public string ProTitle
        {
            get { return protitle; }
            set
            {
                protitle = value;
                this.label_tit.Content = protitle;
            }
        }
        #endregion

        public static valueChanged _valueChanged;
        #region 事件
        //点击向上
        private void image_xs_MouseLeftMouseDown(object sender, MouseEventArgs e)
        {
            String str = this.textblock_value.Text;
            if (str.Length > 0)
            {
                int i = Int32.Parse(str);
                if (i > 0)
                {
                    this.textblock_value.Text = i - 1 + "";
                }
                else
                {
                    this.textblock_value.Text = "0";
                }
            }
            _valueChanged();
        }
        //点击向下
        private void image_xx_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            String str = this.textblock_value.Text;
            if (str.Length > 0)
            {
                int i = Int32.Parse(str);
                this.textblock_value.Text = i + 1 + "";
            }
            _valueChanged();
        }
        #endregion

        #region 返回值
        public string GetValue()
        {
            return this.textblock_value.Text;
        }
        #endregion
    }
}
