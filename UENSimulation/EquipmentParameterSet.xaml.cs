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
using UENSimulation.Model;
using UENSimulation.Utility;

namespace UENSimulation
{
    /// <summary>
    /// EquipmentParameterSet.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentParameterSet : Window
    {
        EquipmentParameter equipmentParameter = new EquipmentParameter();
        Txt_Handle txt_Handle = new Txt_Handle();

        string path = @"..\..\Local Storage\EquipmentParameter.txt";

        public EquipmentParameterSet()
        {
            InitializeComponent();

            string[] s = txt_Handle.dataRead(path);
        }
    }
}
