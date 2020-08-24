using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel_ClassLibrary;

namespace Excle操作
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //创建表格
        private void button3_Click(object sender, EventArgs e)
        {
            //Excel地址
            var filePath = "Excel表格.xls";
            //SQL语句
            string sql = "CREATE TABLE 学生信息" +
            "("+
             "[学号] INT,[姓名] VarChar,[班级] VarChar,"+
            " [电话号码] VarChar,[状态] VarChar"+
            ")";

            Excel.Upadate(sql, filePath);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //1.点击查询，全部查询
            if (this.textBox5.Text=="")
            {
                //Excel地址
                var filePath = "Excel表格.xls";
                //SQL语句
                string sql = "select 学号,姓名,班级,电话号码 from [学生信息$] where 状态='正常' ";
                this.dataGridView1.DataSource = Excel.GetDataTable(sql, filePath);
            }
            else
            {
                //Excel地址
                var filePath = "Excel表格.xls";
                //SQL语句
                string sql = "select 学号,姓名,班级,电话号码 from [学生信息$] where 状态='正常' "
                    +" and " + "学号="+this.textBox5.Text;
                //绑定数据库
                this.dataGridView1.DataSource = Excel.GetDataTable(sql, filePath);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.groupBox1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////Excel地址
            //var filePath = "Excel表格.xls";
            ////SQL语句
            //string sql = "insert into [学生信息$](学号,姓名,班级,电话号码,状态)" +
            //                    " values({0},'{1}','{2}','{3}','{4}')";
            //sql = string.Format(sql,
            //                               this.textBox1.Text,
            //                               this.textBox2.Text,
            //                               this.textBox3.Text,
            //                               this.textBox5.Text,
            //                               "正常");
            ////执行SQL语句
            //Excel.Upadate(sql, filePath);
            ////执行查询语句- 查看前面插入的语句是否进入
            //button4_Click(null, null);

            //Excel地址
            var filePath = "Excel表格.xls";
            //SQL语句
            string sql = "update [学生信息$] set 姓名='{0}',班级='{1}',电话号码='{2}',状态='正常' where 学号={3} ";
            sql = string.Format(sql,
                                            this.textBox2.Text,
                                            this.textBox3.Text,
                                            this.textBox4.Text,
                                            this.textBox1.Text
                                               );

            //执行SQL语句
            Excel.Upadate(sql, filePath);

            button4_Click(null, null);



        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Excel地址
            var filePath = "Excel表格.xls";
            //判断学号是否存在
            if (this.textBox5.Text=="")
            {
                MessageBox.Show("请输入要删除的学号");
            }
            //SQL语句
            string sql = "UPDATE [学生信息$] set 状态='删除' where 学号={0} ";
            sql = string.Format(sql, this.textBox5.Text);
            //执行命令
            Excel.Upadate(sql, filePath);
            //执行查询操作
            //SQL语句
            sql = "select 学号,姓名,班级,电话号码 from [学生信息$] where 状态='正常' ";
            this.dataGridView1.DataSource = Excel.GetDataTable(sql, filePath);

        }

        private void button7_Click(object sender, EventArgs e)
        {

            //打开数据编辑
            this.groupBox1.Enabled = true;
        }
    }
}
