using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient; //1.引用SQL类库
using System.Data;

namespace ClassLibrary
{
    //2.创建操作数据库的类
    public class SQLServer
    {
        //3.构建连接数据库的语句 Server=;DataBase=;Uid=;Pwd=
        private static string connString = @"Server=DESKTOP-CTV4ATU\SQLSERVER;DataBase=Student_Information;Uid=sa;Pwd=123456";

        //4.创建查询用的功能函数 DataTable返回值：内存中的表格用来接收返回的数据
        /// <summary>
        /// 查询获取数据库中表格数据
        /// </summary>
        /// <param name="sql">查询SQL语句</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql)
        {
            //连接数据库
            SqlConnection sqlcon = new SqlConnection(connString);
            //执行sql语句
            SqlDataAdapter sqlda = new SqlDataAdapter(sql, sqlcon);
            //创建数据集用于接收返回的数据 P263
            DataSet myds = new DataSet();
            //sqlda往dataset填充数据
            sqlda.Fill(myds);
            //返回数据
            return myds.Tables[0];//0表示索引，返回所有表格中的第一个其他忽略

        }

        //5.创建查询语句，查询获取结果
        public static int Upada(string sql)
        {
            //[1] 连接数据库
            using (SqlConnection sqlcon= new SqlConnection(connString))
            {
                //[2] 创建操作数据库对象
                using (SqlCommand sql_cmd= new SqlCommand(sql,sqlcon))
                {
                    //[3] 打开数据库
                    sqlcon.Open();
                    //[4] 执行SQL语句并返回受影响的行
                    return sql_cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
