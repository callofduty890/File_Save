using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibrary;//引用自己写的动态链接库

namespace SQLServer联合
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //查询按钮
        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = SQLServer.GetDataTable("select * from 学生信息");
        }
        //增加按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //构建SQL插入语句
            string sql = "insert into 学生信息(姓名,年龄,性别,电话号码) values('{0}', {1}, '{2}', '{3}')";
            sql = string.Format(sql, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text,this.textBox5.Text);

            //判断如果返回值大于0说明修改成功
            if (SQLServer.Upada(sql)>0)
            {
                //查询全部数据
                this.dataGridView1.DataSource = SQLServer.GetDataTable("select * from 学生信息");
            }
            else
            {
                MessageBox.Show("查询失败！,请检查输入数据");
            }
            
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //如何验证 1.没有数据内容的时候客户点击删除 2.已经有数据内容但是没有选择
            if (this.dataGridView1.RowCount==0)
            {
                MessageBox.Show("请添加数据进入表格当中", "提示信息");
                return;
            }
            //保证要删除的学生ID是存在，防止脏数据引发错误
            if (this.dataGridView1.CurrentRow.Cells[0].Value.ToString() == "")
            {
                MessageBox.Show("请选择要删除学员的ID", "提示信息");
                return;
            }

            //获取当先选中的行中的学号
            string ID = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Console.WriteLine(ID);

            string sql = "delete from 学生信息 where 学号="+ID;

            //判断如果返回值大于0说明修改成功
            if (SQLServer.Upada(sql) > 0)
            {
                //查询全部数据
                this.dataGridView1.DataSource = SQLServer.GetDataTable("select * from 学生信息");
            }
            else
            {
                MessageBox.Show("查询失败！,请检查输入数据");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //SQL语句
            string sql = "update 学生信息 set 姓名='{0}',年龄={1},性别='{2}',电话号码='{3}' where 学号 = {4}";
            //拼接SQL语句
            sql = string.Format(sql, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text, this.textBox1.Text);

            //判断如果返回值大于0说明修改成功
            if (SQLServer.Upada(sql) > 0)
            {
                //查询全部数据
                this.dataGridView1.DataSource = SQLServer.GetDataTable("select * from 学生信息");
            }
            else
            {
                MessageBox.Show("修改失败！,请检查输入数据");
            }
        }
    }
}
