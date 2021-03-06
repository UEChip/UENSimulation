﻿using System;
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
    /// LeftRightUC.xaml 的交互逻辑
    /// </summary>
    public partial class LeftRightUC : UserControl
    {
        public LeftRightUC()
        {
            InitializeComponent();
        }

        #region 显示效果
        //左名字
        private string lname = "电视";

        public string Lname
        {
            get { return lname; }
            set
            {
                lname = value;
                this.label_1.Content = lname;
            }
        }

        //右名字
        private string rname = "音响";

        public string Rname
        {
            get { return rname; }
            set
            {
                rname = value;
                this.label_2.Content = rname;
            }
        }

        //设置电视、音响是否选中效果  11.10 Jessie
        private bool lstate;

        public bool Lstate
        {
            get { return lstate; }
            set
            {
                lstate = value;
                if (lstate)
                {
                    this.border_1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fccolour));
                    this.border_1.Tag = "1";
                }
                else
                {
                    this.border_1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fucolour));
                    this.border_1.Tag = "0";
                }
                SetBorder();
            }
        }

        private bool rstate;

        public bool Rstate
        {
            get { return rstate; }
            set
            {
                rstate = value;
                if (rstate)
                {
                    this.border_2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fccolour));
                    this.border_2.Tag = "1";
                }
                else
                {
                    this.border_2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fucolour));
                    this.border_2.Tag = "0";
                }
                SetBorder();
            }
        }

        //分部选中颜色
        string fccolour = "#FF5B9BD5";
        //分部取消颜色
        string fucolour = "#FFADB9CA";
        //整体选中颜色
        string zccolour = "#FFBDD7EE";
        //整体取消颜色
        string zucolour = "#FFD6DCE5";

        public static valueChanged _valueChanged;

        private void border_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.border_2.Tag.Equals("0"))
            {
                this.border_2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fccolour));
                this.border_2.Tag = "1";
                Rstate = true;
            }
            else
            {
                this.border_2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fucolour));
                this.border_2.Tag = "0";
                Rstate = false;
            }
            SetBorder();

            _valueChanged();
        }

        private void border_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.border_1.Tag.Equals("0"))
            {
                this.border_1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fccolour));
                this.border_1.Tag = "1";
                Lstate = true;
            }
            else
            {
                this.border_1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fucolour));
                this.border_1.Tag = "0";
                Lstate = false;
            }
            SetBorder();

            _valueChanged();
        }

        private void SetBorder()
        {
            if (this.border_1.Tag.Equals("1") && this.border_2.Tag.Equals("1"))
            {
                this.border_0.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(zccolour));
            }
            else
            {
                this.border_0.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(zucolour));
            }
        }

        private void border_2_MouseEnter(object sender, MouseEventArgs e)
        {
            this.border_2.Opacity = 0.8;
        }

        private void border_2_MouseLeave(object sender, MouseEventArgs e)
        {
            this.border_2.Opacity = 1;
        }

        private void border_1_MouseEnter(object sender, MouseEventArgs e)
        {
            this.border_1.Opacity = 0.8;
        }

        private void border_1_MouseLeave(object sender, MouseEventArgs e)
        {
            this.border_1.Opacity = 1;
        }
        #endregion

        #region 返回值

        public bool GetLeftState()
        {
            if (this.border_1.Tag.Equals("1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool GetRightState()
        {
            if (this.border_2.Tag.Equals("1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
