using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ClassLibrary
{
    public class INI
    {
        #region INI 配置文件加载 - DLL加载(非委托类型动态链接库)
        [DllImport("kernel32.dll")] //DllImport 引用C++后接着声明函数
        public static extern bool WritePrivateProfileString(string section, string key, string val, string filepath);

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern char GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);


        //封装读取INI文件功能函数
        public static string ContentValue(string Section, string key, string strFilePath)
        {
            //新建可变字符串空间
            StringBuilder temp = new StringBuilder(1024);
            //调用DLL函数
            GetPrivateProfileString(Section, key, "NULL", temp, 1024, strFilePath);
            //返回内容
            return temp.ToString();
        }
        #endregion

    }
}
