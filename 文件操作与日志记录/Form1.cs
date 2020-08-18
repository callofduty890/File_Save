using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 文件操作与日志记录
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //写文本
        private void button1_Click(object sender, EventArgs e)
        {
            //1.创建文件流，形成二进制流
            FileStream fs = new FileStream("D:\\myfile.txt", FileMode.Create);
            //2.创建写入器，为在内存条中的二进制文件流转化成硬盘数据做准备
            StreamWriter sw = new StreamWriter(fs);
            //3.利用写入器，往硬盘中写入数据
            sw.Write(this.textBox1.Text.Trim());
            //4.关闭写入器
            sw.Close();
            //5.关闭文件流
            fs.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //1.创建文件流，形成二进制流
            FileStream fs = new FileStream("D:\\myfile.txt", FileMode.Open);
            //2.创建读取器，将硬盘中的数据转化成内存条中的数据
            StreamReader sr = new StreamReader(fs);
            //3.以流的方式读取显示
            this.textBox1.Text = sr.ReadToEnd();
            //4.关闭读取器
            sr.Close();
            //5.关闭文件流
            fs.Close();
        }
        //模拟日志写入-用来记录软件运行过程，如BUG检查日志排
        //适合多线或while/For循环使用,长时间运行监控软件
        private void button3_Click(object sender, EventArgs e)
        {
            //1.创建文件流，形成二进制流
            FileStream fs = new FileStream("D:\\日志记录.txt", FileMode.Append);
            //2.创建写入器，为在内存条中的二进制文件流转化成硬盘数据做准备
            StreamWriter sw = new StreamWriter(fs);
            //3.利用写入器，往硬盘中写入数据
            //2020-8-17 9:11:20 [Form1] [button3_Click] 往硬盘写入日志
            //string str = DateTime.Now.ToString() + "[类的名称] [函数名称] 调试信息";
            string str = DateTime.Now.ToString() + " [Form1] [button3_Click] 往硬盘写入日志，内容:XXXX";
            sw.WriteLine(str);
            //4.关闭写入器
            sw.Close();
            //5.关闭文件流
            fs.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //删除txt文件
            File.Delete(this.textBox2.Text.Trim());
        }
        //复制
        private void button5_Click(object sender, EventArgs e)
        {
            //判断目的文件，如果存在删除防止重复
            if (File.Exists(this.textBox3.Text.Trim()))
            {
                File.Delete(this.textBox3.Text.Trim());
            }
            //复制文件
            File.Copy(this.textBox2.Text.Trim(), this.textBox3.Text.Trim());
        }
        //移动文件
        private void button6_Click(object sender, EventArgs e)
        {
            //判断目的文件，如果存在删除防止重复
            if (File.Exists(this.textBox3.Text.Trim()))
            {
                File.Delete(this.textBox3.Text.Trim());
            }

            //移动文件-移动之前判断要移动的文件是否存在
            if (File.Exists(this.textBox2.Text.Trim()))
            {
                File.Move(this.textBox2.Text.Trim(), this.textBox3.Text.Trim());
            }
            else
            {
                MessageBox.Show("文件不存在");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //遍历 C盘 下的文件
            string[] files = Directory.GetFiles("C:\\");
            //清空文本框方便显示
            this.textBox1.Clear();
            //遍历显示所有的内容
            foreach (string item in files)
            {
                this.textBox1.Text += item + "\r\n";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //遍历 C盘 子目录
            string[] files = Directory.GetDirectories("C:\\");
            //清空文本框方便显示
            this.textBox1.Clear();
            //遍历显示所有的内容
            foreach (string item in files)
            {
                this.textBox1.Text += item + "\r\n";
            }
        }
        //创建文件夹
        private void button9_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory("D:\\TEST"); //删除
        }
        //删除
        private void button10_Click(object sender, EventArgs e)
        {
            //获取文件夹的信息
            DirectoryInfo dir = new DirectoryInfo("D:\\TEST");
            //删除
            dir.Delete(true);
        }
    }
}
