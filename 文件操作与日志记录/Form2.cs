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
    }
}
