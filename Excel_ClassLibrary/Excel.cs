using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Excel_ClassLibrary
{
    public class Excel
    {
        //[1] 查询数据
        public static DataTable GetDataTable(string sql, string Excel_Path)
        {
            //1.构建连接数据库的字符串
            string SConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + //Access数据库版本
               "Data Source=" + Excel_Path + ";" + //Excle路径
               "Extended Properties='Excel 8.0;HDR=Yes;IMEX=0' ";

            //Extended Properties Excel版本主要时候区分office2007以下:7.0
            //HDR:是否创建Excel的表头
            //IMEX:工作模式 [0] 代表可读可写

            //2.构建连接
            using (OleDbConnection ole_cnn = new OleDbConnection(SConnectionString))
            {
                ole_cnn.Open();//3.连接并打开数据库
                //4.创建操作对象
                using (OleDbCommand ole_cmd = ole_cnn.CreateCommand())
                {
                    //5.传递SQL语句
                    ole_cmd.CommandText = sql;
                    //6.执行SQL语句
                    using (OleDbDataAdapter dapter= new OleDbDataAdapter(ole_cmd))
                    {
                        //创建dataset用以填充
                        DataSet dr = new DataSet();
                        //填充数据
                        dapter.Fill(dr);
                        //返回表格数据
                        return dr.Tables[0];
                    }
                }
            }

        }
        //[2] 修改数据
        public static int Upadate(string sql,string Excel_Path)
        {
            //1.构建连接数据库的字符串
            string SConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + //Access数据库版本
               "Data Source=" + Excel_Path + ";" + //Excle路径
               "Extended Properties='Excel 8.0;HDR=Yes;IMEX=0' ";

            //Extended Properties Excel版本主要时候区分office2007以下:7.0
            //HDR:是否创建Excel的表头
            //IMEX:工作模式 [0] 代表可读可写

            //2.构建连接
            using (OleDbConnection ole_cnn= new OleDbConnection(SConnectionString))
            {
                ole_cnn.Open();//3.连接并打开数据库
                //4.创建操作对象
                using (OleDbCommand ole_cmd= ole_cnn.CreateCommand())
                {
                    //5.传递SQL语句
                    ole_cmd.CommandText = sql;
                    //6.返回受影响的行
                    return ole_cmd.ExecuteNonQuery(); 
                }
            }

        }
    }
}
