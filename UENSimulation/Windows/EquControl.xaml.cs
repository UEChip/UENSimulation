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
using System.Windows.Shapes;

namespace UENSimulation.Windows
{
    /// <summary>
    /// EquControl.xaml 的交互逻辑
    /// </summary>
    public partial class EquControl : Window
    {
        public BTClick _btClick3;
        public EquControl()
        {
            InitializeComponent();
            swithuc_sf._btClick += new BTClick(_btClick2);
        }

        public void _btClick2()
        {
            if (_btClick3 != null)
                _btClick3();
        }
    }
}
