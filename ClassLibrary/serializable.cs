using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// 序列化与反序列化的类
    /// </summary>
    public class serializable
    {
        /// <summary>
        /// 序列化保存对象
        /// </summary>
        /// <param name="objectPath">对象保存的路径</param>
        /// <param name="obj">对象</param>
        public static void SaveObj(string objectPath,object obj)
        {
            //[1] 创建文件流
            FileStream fs = new FileStream(objectPath, FileMode.Create);
            //[2]创建二进制格式转换器
            BinaryFormatter formatter = new BinaryFormatter();
            //[3] 调用 序列化的方法
            formatter.Serialize(fs, obj);
            //[4] 关闭文件流
            fs.Close();
        }

        /// <summary>
        /// 读取序列化对象
        /// </summary>
        /// <param name="objectPath">序列化路径</param>
        /// <returns></returns>
        public static object ReadObj(string objectPath)
        {
            //创建文件流
            FileStream fs = new FileStream(objectPath, FileMode.Open);
            //创建二进制格式转换器
            BinaryFormatter formatter = new BinaryFormatter();
            //调用反序列化
            object obj = (object)formatter.Deserialize(fs);
            //关闭文件流
            fs.Close();
            //返回对象
            return obj;
        }

    }
}
