using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ClassLibrary;//引用自定义的函数-动态链接库

namespace 文件操作与日志记录
{
    public partial class Form2 : Form
    {
        //全局变量，提供给dataGridView2 使用 .将其与datetable绑定
        DataTable dt = new DataTable();


        public Form2()
        {
            InitializeComponent();
        }

        private void ucBtnExt1_BtnClick(object sender, EventArgs e)
        {
            //string str = DateTime.Now.ToString() + " [Form2] [ucBtnExt1_BtnClick]  调试信息: 写入日志";
            //TXT.txtWrite("运行日志.txt", str);
            TXT.LogWrite("运行日志.txt", "new information");
        }

        private void ucBtnExt2_BtnClick(object sender, EventArgs e)
        {
            string str = TXT.txtRead("运行日志.txt");
            MessageBox.Show(str);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //保存INI文件
        private void ucBtnExt3_BtnClick(object sender, EventArgs e)
        {
            //路径写入
            string Save_File_Path = System.AppDomain.CurrentDomain.BaseDirectory + "Save_File.ini";



            //调用C++的功能函数
            Console.WriteLine(INI.WritePrivateProfileString("information", "加速度", this.textBoxEx1.Text, Save_File_Path));
            Console.WriteLine(INI.WritePrivateProfileString("information", "减速度", this.textBoxEx2.Text, Save_File_Path));
            Console.WriteLine(INI.WritePrivateProfileString("information", "最大速度", this.textBoxEx4.Text, Save_File_Path));

            //MessageBox.Show("写入成功");
        }
        //读取
        private void ucBtnExt4_BtnClick(object sender, EventArgs e)
        {
            //读取路径
            string Save_File_Path = System.AppDomain.CurrentDomain.BaseDirectory + "Save_File.ini";
            //读取赋值
            this.textBoxEx3.Text = INI.ContentValue("information", "初始速度", Save_File_Path);
            this.textBoxEx1.Text = INI.ContentValue("information", "加速度", Save_File_Path);
            this.textBoxEx2.Text = INI.ContentValue("information", "减速度", Save_File_Path);
            this.textBoxEx4.Text = INI.ContentValue("information", "最大速度", Save_File_Path);

            //实例化一个对象
            information objinformaiton = new information();
            //对象赋值
            objinformaiton.Speed = Convert.ToInt32(INI.ContentValue("information", "初始速度", Save_File_Path));
            objinformaiton.Acceleration = Convert.ToInt32(INI.ContentValue("information", "加速度", Save_File_Path));
            objinformaiton.Deceleration = Convert.ToInt32(INI.ContentValue("information", "减速度", Save_File_Path));
            objinformaiton.Maxspeed = Convert.ToInt32(INI.ContentValue("information", "最大速度", Save_File_Path));


        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //DataTable与控件dataGridView绑定
            this.dataGridView2.DataSource = dt;

            //读取路径
            string Save_File_Path = System.AppDomain.CurrentDomain.BaseDirectory + "Save_File.ini";
            //读取赋值
            this.textBoxEx3.Text = INI.ContentValue("information", "初始速度", Save_File_Path);
            this.textBoxEx1.Text = INI.ContentValue("information", "加速度", Save_File_Path);
            this.textBoxEx2.Text = INI.ContentValue("information", "减速度", Save_File_Path);
            this.textBoxEx4.Text = INI.ContentValue("information", "最大速度", Save_File_Path);



        }
       
        [Serializable]
        class information
        {
            //字段
            private int _speed;//初始速度
            private int _maxspeed;//最大速度
            private int _acceleration;//加速度
            private int _deceleration;//减速度
            //属性
            public int Speed { get => _speed; set => _speed = value; }
            public int Maxspeed { get => _maxspeed; set => _maxspeed = value; }
            public int Acceleration { get => _acceleration; set => _acceleration = value; }
            public int Deceleration { get => _deceleration; set => _deceleration = value; }
            
            //1.初始化板卡
            //2.上下左右
            public void UP()
            {
                Console.WriteLine("Speed:{0}", Speed);
            }
        }

        //序列化-保存
        private void ucBtnExt6_BtnClick(object sender, EventArgs e)
        {
            //实例化对象
            information objinformation = new information()
            {
                Speed= Convert.ToInt32(this.textBoxEx7.Text),
                Acceleration= Convert.ToInt32(this.textBoxEx8.Text),
                Deceleration= Convert.ToInt32(this.textBoxEx6.Text),
                Maxspeed= Convert.ToInt32(this.textBoxEx5.Text)
            };

            //序列化保存对象
            serializable.SaveObj("objInformation.stu", objinformation);
        }
        //反序列化
        private void ucBtnExt5_BtnClick(object sender, EventArgs e)
        {
            //反序列化
            information objInformation = (information)serializable.ReadObj("objInformation.stu");

            //对控件进行赋值
            this.textBoxEx7.Text = objInformation.Speed.ToString();
            this.textBoxEx8.Text = objInformation.Acceleration.ToString();
            this.textBoxEx6.Text = objInformation.Deceleration.ToString();
            this.textBoxEx5.Text = objInformation.Maxspeed.ToString();
        }

        private void ucBtnExt8_BtnClick(object sender, EventArgs e)
        {
            //实例化对象
            XmlConfigUtil configxml = new XmlConfigUtil("Save.xml");
            //==========================保存界面数据
            //初始速度
            configxml.Write(this.textBoxEx11.Text, "information", "speed");
            //加速度
            configxml.Write(this.textBoxEx12.Text, "information", "Acceleration");
            //减速度
            configxml.Write(this.textBoxEx10.Text, "information", "Deceleration");
            //最大速度
            configxml.Write(this.textBoxEx9.Text, "information", "Maxspeed");
        }
        //读取xml配置文件
        private void ucBtnExt7_BtnClick(object sender, EventArgs e)
        {
            //实例化对象
            XmlConfigUtil configxml = new XmlConfigUtil("Save.xml");
            //读取xml文件
            this.textBoxEx11.Text = configxml.Read("information", "speed");
            this.textBoxEx12.Text = configxml.Read("information", "Acceleration");
            this.textBoxEx10.Text = configxml.Read("information", "Deceleration");
            this.textBoxEx9.Text = configxml.Read("information", "Maxspeed");
        }

        private void ucBtnExt9_BtnClick(object sender, EventArgs e)
        {
            //学号	 姓名	班级	电话号码
            string[] a = new string[] { "学号","姓名", "班级", "电话号码" };
            string[] b = new string[] { "1", "A", "视觉班", "123-456" };
            //构建表格内容
            List<string[]> result = new List<string[]>();
            result.Add(a);
            result.Add(b);
            //往表格中写入 最后的参数位是否追加 true为追加/false不追加
            CSV.WriteCSV("学生信息.csv", result, false);
        }
        //读取CSV
        private void ucBtnExt10_BtnClick(object sender, EventArgs e)
        {
            List<string[]> results = CSV.ReadCSV("学生信息.csv");
            //遍历显示所有的内容
            foreach (var item in results)
            {
                foreach (var va in item)
                {
                    Console.Write(va + " ");
                }
                Console.WriteLine();
            }
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
        //创建表头
        private void ucBtnExt11_BtnClick(object sender, EventArgs e)
        {
            dt.Columns.Add("学号", typeof(int));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("班级", typeof(string));
            dt.Columns.Add("电话号码", typeof(string));
        }

        private void ucBtnExt12_BtnClick(object sender, EventArgs e)
        {
            dt.Rows.Add(1, "助教A", "语言班", "123-456");
            dt.Rows.Add(2, "助教B", "语言班", "123-456");
        }

        private void ucBtnExt13_BtnClick(object sender, EventArgs e)
        {
            //拿到索引  获取最后一行
            int index = dt.Rows.Count - 1;
            if (index>-1)
            {
                //删除最后一行
                dt.Rows.Remove(dt.Rows[index]);
            }
            else
            {
                MessageBox.Show("全部删除");
            }

        }
        //修改一行
        private void ucBtnExt14_BtnClick(object sender, EventArgs e)
        {
            //获取最后一行：修改
            int index = dt.Rows.Count - 1;
            //修改
            dt.Rows[index][1] = "嘉嘉";
            dt.Rows[index][2] = "三年二班";
            dt.Rows[index][3] = "089-123";

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //删除
        private void ucBtnExt15_BtnClick(object sender, EventArgs e)
        {
            //获取当前活跃状态的单元格索引
            int index = this.dataGridView2.CurrentCell.RowIndex;
            //将活动单元格删除
            dt.Rows.Remove(dt.Rows[index]);
        }
    }
}
