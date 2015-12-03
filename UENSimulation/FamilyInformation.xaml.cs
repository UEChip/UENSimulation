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
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data;
namespace UENSimulation

{
    /// <summary>
    /// FamilyInformation.xaml 的交互逻辑
    /// </summary>
    public partial class FamilyInformation : Window
    {
        private string[] cityName = { "北京", "上海", "广州", "沈阳","武汉","重庆" };
        private string[] monthList = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        private string[] hourList = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12","13","14","15","16","17","18","19","20","21","22","23","24" };
        public FamilyInformation()
        {
            InitializeComponent();
            city.Items.Clear();
            foreach (string cName in cityName)
            {
                city.Items.Add(cName);
            }
            month.Items.Clear();
            foreach (string mName in monthList)
            {
                month.Items.Add(mName);
            }
            hour.Items.Clear();
            foreach (string hName in hourList)
            {
                hour.Items.Add(hName);
            }   
            this.Read();
           
            hour.Text = DateTime.Now.Hour.ToString();
            month.Text = DateTime.Now.Month.ToString();
            this.getData();
          
        }
        private void getData()//根据时间与表格获得相应参数
        {
            int mon =Convert.ToInt32(month.Text);
            int hou = Convert.ToInt32(hour.Text);
            string stre;
            string strh;
            string strc;
            DataTable dt;
            dt=ReadXlsx();
            if(mon<8 && mon>4){
                  if(city.Text == "广州")
                {
                    stre = dt.Rows[hou - 1][2].ToString();
                     hot.Content ="0";
                     strc = dt.Rows[hou - 1][1].ToString();
                     if (stre.Length >= 4)
                     {
                         stre = stre.Substring(0, 4);
                     }
                     electric.Content = stre;
                     if (strc.Length >= 4)
                     {
                         strc = strc.Substring(0, 4);
                     }
                     cold.Content = strc;

                }
                  else
                  {
                      stre = dt.Rows[hou - 1][4].ToString();
                hot.Content ="0";
                strc = dt.Rows[hou - 1][3].ToString();
                 if (stre.Length >= 4)
                 {
                     stre = stre.Substring(0, 4);
                 }
                 if (strc.Length >= 4)
                 {
                     strc = strc.Substring(0, 4);
                 }
                 electric.Content = stre;
                 cold.Content = strc;
                  }       
            }
            if(mon>7 && mon<11){
                 electric.Content ="秋季无数据";
                 hot.Content ="秋季无数据";
                 cold.Content ="秋季无数据";
            }
             if(mon>1 && mon<5){
                 electric.Content ="春季无数据";
                hot.Content ="春季无数据";
                 cold.Content ="春季无数据";
            }
             if(mon>10||mon<2){
                    if(city.Text == "广州")
                {
                     electric.Content ="0";
                     hot.Content ="0";
                     cold.Content ="0";
                }
                 else{
                        cold.Content ="0";
                        electric.Content ="0";
                        strh = dt.Rows[hou - 1][1].ToString();
                         if (strh.Length >= 4)
                         {
                             strh = strh.Substring(0, 4);
                         }
                         hot.Content = strh;
                    }
             }
            
        }
        private DataTable ReadXlsx()//将表格放入数据库
        {
            string cityNick;
            switch (city.Text)
            {
                case "北京": cityNick = "BEIJ";
                    break;
                case "上海": cityNick= "SHANGH";
                    break;
                case "广州": cityNick= "GUANGZ";
                    break;
                case "哈尔滨": cityNick= "HAEB";
                    break;
                case "青岛": cityNick= "QINGD";
                    break;
                case "沈阳": cityNick= "SHENY";
                    break;
                case "石家庄": cityNick= "SHIJZ";
                    break;
                case "武汉": cityNick= "WUH";
                    break;
                case "长沙": cityNick= "CHANGS";
                    break;
                case "重庆": cityNick= "CHONGQ";
                    break;
                default: cityNick = "BEIJ";//缺参默认北京
                    break;
            }
            string filePath = @"..\..\Local Storage\database\"+cityNick+".xlsx";
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'"; //固定句不更改
            OleDbConnection OleConn = new OleDbConnection(strConn); 
            OleConn.Open();
            String sql = "select * from [Sheet1$]";
            OleDbCommand select = new OleDbCommand(sql, OleConn);
            OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();
            objAdapter1.SelectCommand = select;
            DataSet OleDsExcle = new DataSet();
            objAdapter1.Fill(OleDsExcle, "Sheet1");
            OleConn.Close();

            return OleDsExcle.Tables["Sheet1"];
        }

        private void Write()//将选项设置存放到本地文件
        {
            string[] dataWrite = new string[29];
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
            dataWrite[28] = city.Text;
            if (!File.Exists(@"..\..\Local Storage\familyInformation.txt"))
            {

                FileStream fs = new FileStream(@"..\..\Local Storage\familyInformation.txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs);
                for (int i = 0; i < 29; i++)
                {
                    sw.WriteLine(dataWrite[i]);
                }
                sw.Close();
                fs.Close();
            }
            else
            {
                System.IO.File.WriteAllText(@"..\..\Local Storage\familyInformation.txt", string.Empty);//清空文档信息方便重新书写
                FileStream fs = new FileStream(@"..\..\Local Storage\familyInformation.txt", FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                for (int i = 0; i < 29; i++)
                {
                    sw.WriteLine(dataWrite[i]);
                }
                sw.Close();
                fs.Close();
            }
            }

        private void Read()//读取配置信息
        {
            try
            {

                string[] dataRead = new string[29];
                bool file = File.Exists(@"..\..\Local Storage\familyInformation.txt");
                if (file == true)
                {
                    StreamReader sr = new StreamReader(@"..\..\Local Storage\familyInformation.txt");
                    int i = 0;
                    for (i = 0; i < 29; i++)
                    {
                        dataRead[i] = sr.ReadLine();
                    }
                    sr.Close();
                    for (i = 0; i < 28; i++)
                    {
                        if (dataRead[i] == null) { 
                            dataRead[i] = "false"; }
                    }
                    if(dataRead[28] == null){
                    dataRead[28] = city.Items[0] as string;
                    }

                }
                else
                {
                    for (int i = 0; i < 28; i++) { dataRead[i] = "false"; }
                    dataRead[28] = city.Items[0] as string;
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
                city.Text =dataRead[28];
            }

            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)//每次关闭界面时自动保存配置
        {
            
            Write();
        }

        private void file_Click(object sender, RoutedEventArgs e)//按钮查看文件
        {
            switch (city.Text)
            {
                case "北京": System.Diagnostics.Process.Start(@"..\..\Local Storage\database\BEIJ.xlsx");
                    break;
                case "上海": System.Diagnostics.Process.Start(@"..\..\Local Storage\database\SHANGH.xlsx");
                    break;
                case "广州": System.Diagnostics.Process.Start(@"..\..\Local Storage\database\GUANGZ.xlsx");
                    break;
                case "哈尔滨": System.Diagnostics.Process.Start(@"..\..\Local Storage\database\HAEB.xlsx");
                    break;
                case "青岛": System.Diagnostics.Process.Start(@"..\..\Local Storage\database\QINGD.xlsx");
                    break;
                case "沈阳": System.Diagnostics.Process.Start(@"..\..\Local Storage\database\SHENY.xlsx");
                    break;
                case "石家庄": System.Diagnostics.Process.Start(@"..\..\Local Storage\database\SHIJZ.xlsx");
                    break;
                case "武汉": System.Diagnostics.Process.Start(@"..\..\Local Storage\database\WUH.xlsx");
                    break;
                case "长沙": System.Diagnostics.Process.Start(@"..\..\Local Storage\database\CHANGS.xlsx");
                    break;
                case "重庆": System.Diagnostics.Process.Start(@"..\..\Local Storage\database\CHONGQ.xlsx");
                    break;
                default: MessageBox.Show("未选择城市", "you can't");
                    break;
            }
           
        }

        private void run_Click(object sender, RoutedEventArgs e)//按钮获取参数可执行相应方法
        {
            getData();
        }
    }
}
