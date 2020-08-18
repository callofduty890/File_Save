using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class TXT
    {

        /// <summary>
        /// txt写入操作
        /// </summary>
        /// <param name="Path">txt的路径</param>
        /// <param name="Content">要保存的字符串内容</param>
        public static void txtWrite(string Path,string Content)
        {
            //1.创建文件流，形成二进制流
            FileStream fs = new FileStream(Path, FileMode.Create);
            //2.创建写入器，为在内存条中的二进制文件流转化成硬盘数据做准备
            StreamWriter sw = new StreamWriter(fs);
            //3.利用写入器，往硬盘中写入数据
            sw.Write(Content);
            //4.关闭写入器
            sw.Close();
            //5.关闭文件流
            fs.Close();
        }

        /// <summary>
        /// 用于日志的写入/追加
        /// </summary>
        /// <param name="Path">日志的路径</param>
        /// <param name="Content">日志的内容</param>
        public static void LogWrite(string Path,string Content)
        {
            //1.创建文件流，形成二进制流
            FileStream fs = new FileStream(Path, FileMode.Append);
            //2.创建写入器，为在内存条中的二进制文件流转化成硬盘数据做准备
            StreamWriter sw = new StreamWriter(fs);
            //3.利用写入器，往硬盘中写入数据
            sw.WriteLine(DateTime.Now.ToString()+" "+Content);
            //4.关闭写入器
            sw.Close();
            //5.关闭文件流
            fs.Close();
        }

        /// <summary>
        /// txt读取操作
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string txtRead(string Path)
        {
            //1.创建文件流，形成二进制流
            FileStream fs = new FileStream(Path, FileMode.Open);
            //2.创建读取器，将硬盘中的数据转化成内存条中的数据
            StreamReader sr = new StreamReader(fs);
            //3.以流的方式读取显示
            string str = sr.ReadToEnd();
            //4.关闭读取器
            sr.Close();
            //5.关闭文件流
            fs.Close();
            //6.返回读取到的内容
            return str;
        }

    }
}
