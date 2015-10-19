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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace UENSimulation
{
    /// <summary>
    /// FamilyInformation.xaml 的交互逻辑
    /// </summary>
    public partial class FamilyInformation : Window
    {
        
        public FamilyInformation()
        {
            InitializeComponent();
            this.Read();
        }

        private void Write()
        {
            string[] dataWrite = new string[28];
            dataWrite[0] = house1.IsChecked.ToString();
            dataWrite[1] = house2.IsChecked.ToString();
            dataWrite[2] = house3.IsChecked.ToString();
            dataWrite[3] = house4.IsChecked.ToString();
            dataWrite[4] = house5.IsChecked.ToString();
            dataWrite[5] = houseType1.IsChecked.ToString();
            dataWrite[6] = houseType2.IsChecked.ToString();
            dataWrite[7] = houseType3.IsChecked.ToString();
            dataWrite[8] = family1.IsChecked.ToString();
            dataWrite[9] = family2.IsChecked.ToString();
            dataWrite[10] = family3.IsChecked.ToString();
            dataWrite[11] = family4.IsChecked.ToString();
            dataWrite[12] = machine1.IsChecked.ToString();
            dataWrite[13] = machine2.IsChecked.ToString();
            dataWrite[14] = machine3.IsChecked.ToString();
            dataWrite[15] = furnace.IsChecked.ToString();
            dataWrite[16] = panels.IsChecked.ToString();
            dataWrite[17] = conditioner.IsChecked.ToString();
            dataWrite[18] = gasStove.IsChecked.ToString();
            dataWrite[19] = buildingParameters1.IsChecked.ToString();
            dataWrite[20] = buildingParameters2.IsChecked.ToString();
            dataWrite[21] = buildingParameters3.IsChecked.ToString();
            dataWrite[22] = material1.IsChecked.ToString();
            dataWrite[23] = material2.IsChecked.ToString();
            dataWrite[24] = material3.IsChecked.ToString();
            dataWrite[25] = direction1.IsChecked.ToString();
            dataWrite[26] = direction2.IsChecked.ToString();
            dataWrite[27] = direction3.IsChecked.ToString();
            if (!File.Exists(@"..\..\Local Storage\familyInformation.txt"))
            {

                FileStream fs = new FileStream(@"..\..\Local Storage\familyInformation.txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs);
                for (int i = 0; i < 28; i++)
                {
                    sw.WriteLine(dataWrite[i]);
                }
                sw.Close();
                fs.Close();
            }
            else
            {
                System.IO.File.WriteAllText(@"..\..\Local Storage\familyInformation.txt", string.Empty);
                FileStream fs = new FileStream(@"..\..\Local Storage\familyInformation.txt", FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                for (int i = 0; i < 28; i++)
                {
                    sw.WriteLine(dataWrite[i]);
                }
                sw.Close();
                fs.Close();
            }
            }

        private void Read()
        {
            try
            {

                string[] dataRead = new string[28];
                bool file = File.Exists(@"..\..\Local Storage\familyInformation.txt");
                if (file == true)
                {
                    StreamReader sr = new StreamReader(@"..\..\Local Storage\familyInformation.txt");
                    int i = 0;
                    for (i = 0; i < 28; i++)
                    {
                        dataRead[i] = sr.ReadLine();
                    }
                    sr.Close();
                    for (i = 0; i < 28; i++)
                    {
                        if (dataRead[i] == null) { dataRead[i] = "false"; }
                    }


                }
                else
                {
                    for (int i = 0; i < 28; i++) { dataRead[i] = "false"; }
                }
                house1.IsChecked = Convert.ToBoolean(dataRead[0]);
                house2.IsChecked = Convert.ToBoolean(dataRead[1]);
                house3.IsChecked = Convert.ToBoolean(dataRead[2]);
                house4.IsChecked = Convert.ToBoolean(dataRead[3]);
                house5.IsChecked = Convert.ToBoolean(dataRead[4]);
                houseType1.IsChecked = Convert.ToBoolean(dataRead[5]);
                houseType2.IsChecked = Convert.ToBoolean(dataRead[6]);
                houseType3.IsChecked = Convert.ToBoolean(dataRead[7]);
                family1.IsChecked = Convert.ToBoolean(dataRead[8]);
                family2.IsChecked = Convert.ToBoolean(dataRead[9]);
                family3.IsChecked = Convert.ToBoolean(dataRead[10]);
                family4.IsChecked = Convert.ToBoolean(dataRead[11]);
                machine1.IsChecked = Convert.ToBoolean(dataRead[12]);
                machine2.IsChecked = Convert.ToBoolean(dataRead[13]);
                machine3.IsChecked = Convert.ToBoolean(dataRead[14]);
                furnace.IsChecked = Convert.ToBoolean(dataRead[15]);
                panels.IsChecked = Convert.ToBoolean(dataRead[16]);
                conditioner.IsChecked = Convert.ToBoolean(dataRead[17]);
                gasStove.IsChecked = Convert.ToBoolean(dataRead[18]);
                buildingParameters1.IsChecked = Convert.ToBoolean(dataRead[19]);
                buildingParameters2.IsChecked = Convert.ToBoolean(dataRead[20]);
                buildingParameters3.IsChecked = Convert.ToBoolean(dataRead[21]);
                material1.IsChecked = Convert.ToBoolean(dataRead[22]);
                material2.IsChecked = Convert.ToBoolean(dataRead[23]);
                material3.IsChecked = Convert.ToBoolean(dataRead[24]);
                direction1.IsChecked = Convert.ToBoolean(dataRead[25]);
                direction2.IsChecked = Convert.ToBoolean(dataRead[26]);
                direction3.IsChecked = Convert.ToBoolean(dataRead[27]);

            }

            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            Write();
        }
    }
}
