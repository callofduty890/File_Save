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
    }
}
