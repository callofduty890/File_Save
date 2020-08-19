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
using HalconDotNet;//引用halcon的动态链接库

namespace 文件操作与日志记录
{
    public partial class Form2 : Form
    {
        //全局变量，提供给dataGridView2 使用 .将其与datetable绑定
        DataTable dt = new DataTable();

        //Halcon类型的变量与对象-全局 变量与对象
        HObject ho_Image;
        HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
        HTuple hv_WindowHandle = new HTuple();


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
            //DataTable与控件dataGridView绑定
            this.dataGridView2.DataSource = dt;

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

        private void ucBtnExt9_BtnClick(object sender, EventArgs e)
        {
            //学号	 姓名	班级	电话号码
            string[] a = new string[] { "学号","姓名", "班级", "电话号码" };
            string[] b = new string[] { "1", "A", "视觉班", "123-456" };
            //构建表格内容
            List<string[]> result = new List<string[]>();
            result.Add(a);
            result.Add(b);
            //往表格中写入 最后的参数位是否追加 true为追加/false不追加
            CSV.WriteCSV("学生信息.csv", result, false);
        }
        //读取CSV
        private void ucBtnExt10_BtnClick(object sender, EventArgs e)
        {
            List<string[]> results = CSV.ReadCSV("学生信息.csv");
            //遍历显示所有的内容
            foreach (var item in results)
            {
                foreach (var va in item)
                {
                    Console.Write(va + " ");
                }
                Console.WriteLine();
            }
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
        //创建表头
        private void ucBtnExt11_BtnClick(object sender, EventArgs e)
        {
            dt.Columns.Add("学号", typeof(int));
            dt.Columns.Add("姓名", typeof(string));
            dt.Columns.Add("班级", typeof(string));
            dt.Columns.Add("电话号码", typeof(string));
        }

        private void ucBtnExt12_BtnClick(object sender, EventArgs e)
        {
            dt.Rows.Add(1, "助教A", "语言班", "123-456");
            dt.Rows.Add(2, "助教B", "语言班", "123-456");
        }

        private void ucBtnExt13_BtnClick(object sender, EventArgs e)
        {
            //拿到索引  获取最后一行
            int index = dt.Rows.Count - 1;
            if (index>-1)
            {
                //删除最后一行
                dt.Rows.Remove(dt.Rows[index]);
            }
            else
            {
                MessageBox.Show("全部删除");
            }

        }
        //修改一行
        private void ucBtnExt14_BtnClick(object sender, EventArgs e)
        {
            //获取最后一行：修改
            int index = dt.Rows.Count - 1;
            //修改
            dt.Rows[index][1] = "嘉嘉";
            dt.Rows[index][2] = "三年二班";
            dt.Rows[index][3] = "089-123";

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //删除
        private void ucBtnExt15_BtnClick(object sender, EventArgs e)
        {
            //获取当前活跃状态的单元格索引
            int index = this.dataGridView2.CurrentCell.RowIndex;
            //将活动单元格删除
            dt.Rows.Remove(dt.Rows[index]);
        }

        private void ucBtnExt18_BtnClick(object sender, EventArgs e)
        {
            //显示图像
            HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
            //显示提示信息
            disp_message(hv_WindowHandle, "读取图片成功", "window", 12, 12, "black", "true");
        }

        private void ucBtnExt19_BtnClick(object sender, EventArgs e)
        {
            //读取图片
            HOperatorSet.ReadImage(out ho_Image, "./street_01.jpg");
            //获取图片宽高
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            //打开新窗口
            HOperatorSet.OpenWindow(0, 0, this.pictureBox1.Width, this.pictureBox1.Height, this.pictureBox1.Handle, "visible", "", out hv_WindowHandle);
            //设置显示的宽高
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_Height, hv_Width);
            //推送->设置活跃窗口
            HDevWindowStack.Push(hv_WindowHandle);
        }

        private void ucBtnExt16_BtnClick(object sender, EventArgs e)
        {
            //读取图片
            HOperatorSet.ReadImage(out ho_Image, "./street_01.jpg");
            //获取图片宽高
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            //获取窗口句柄
            hv_WindowHandle = this.hWindowControl1.HalconWindow;
            //设置显示的宽高
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_Height, hv_Width);
        }

        private void ucBtnExt17_BtnClick(object sender, EventArgs e)
        {
            //显示图像
            HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
            //显示提示信息
            disp_message(hv_WindowHandle, "读取图片成功", "window", 12, 12, "black", "true");
        }

        //Halcon显示消息功能函数-直接复制粘贴
        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
        HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_GenParamName = new HTuple(), hv_GenParamValue = new HTuple();
            HTuple hv_Color_COPY_INP_TMP = new HTuple(hv_Color);
            HTuple hv_Column_COPY_INP_TMP = new HTuple(hv_Column);
            HTuple hv_CoordSystem_COPY_INP_TMP = new HTuple(hv_CoordSystem);
            HTuple hv_Row_COPY_INP_TMP = new HTuple(hv_Row);

            // Initialize local and output iconic variables 
            //This procedure displays text in a graphics window.
            //
            //Input parameters:
            //WindowHandle: The WindowHandle of the graphics window, where
            //   the message should be displayed
            //String: A tuple of strings containing the text message to be displayed
            //CoordSystem: If set to 'window', the text position is given
            //   with respect to the window coordinate system.
            //   If set to 'image', image coordinates are used.
            //   (This may be useful in zoomed images.)
            //Row: The row coordinate of the desired text position
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Column: The column coordinate of the desired text position
            //   A tuple of values is allowed to display text at different
            //   positions.
            //Color: defines the color of the text as string.
            //   If set to [], '' or 'auto' the currently set color is used.
            //   If a tuple of strings is passed, the colors are used cyclically...
            //   - if |Row| == |Column| == 1: for each new textline
            //   = else for each text position.
            //Box: If Box[0] is set to 'true', the text is written within an orange box.
            //     If set to' false', no box is displayed.
            //     If set to a color string (e.g. 'white', '#FF00CC', etc.),
            //       the text is written in a box of that color.
            //     An optional second value for Box (Box[1]) controls if a shadow is displayed:
            //       'true' -> display a shadow in a default color
            //       'false' -> display no shadow
            //       otherwise -> use given string as color string for the shadow color
            //
            //It is possible to display multiple text strings in a single call.
            //In this case, some restrictions apply:
            //- Multiple text positions can be defined by specifying a tuple
            //  with multiple Row and/or Column coordinates, i.e.:
            //  - |Row| == n, |Column| == n
            //  - |Row| == n, |Column| == 1
            //  - |Row| == 1, |Column| == n
            //- If |Row| == |Column| == 1,
            //  each element of String is display in a new textline.
            //- If multiple positions or specified, the number of Strings
            //  must match the number of positions, i.e.:
            //  - Either |String| == n (each string is displayed at the
            //                          corresponding position),
            //  - or     |String| == 1 (The string is displayed n times).
            //
            //
            //Convert the parameters for disp_text.
            if ((int)((new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(new HTuple())))) != 0)
            {

                hv_Color_COPY_INP_TMP.Dispose();
                hv_Column_COPY_INP_TMP.Dispose();
                hv_CoordSystem_COPY_INP_TMP.Dispose();
                hv_Row_COPY_INP_TMP.Dispose();
                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();

                return;
            }
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP.Dispose();
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP.Dispose();
                hv_Column_COPY_INP_TMP = 12;
            }
            //
            //Convert the parameter Box to generic parameters.
            hv_GenParamName.Dispose();
            hv_GenParamName = new HTuple();
            hv_GenParamValue.Dispose();
            hv_GenParamValue = new HTuple();
            if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(0))) != 0)
            {
                if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleEqual("false"))) != 0)
                {
                    //Display no box
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "box");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                "false");
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
                else if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleNotEqual("true"))) != 0)
                {
                    //Set a color other than the default.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "box_color");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                hv_Box.TupleSelect(0));
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
            }
            if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleEqual("false"))) != 0)
                {
                    //Display no shadow.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "shadow");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                "false");
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
                else if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleNotEqual("true"))) != 0)
                {
                    //Set a shadow color other than the default.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "shadow_color");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                hv_Box.TupleSelect(1));
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
            }
            //Restore default CoordSystem behavior.
            if ((int)(new HTuple(hv_CoordSystem_COPY_INP_TMP.TupleNotEqual("window"))) != 0)
            {
                hv_CoordSystem_COPY_INP_TMP.Dispose();
                hv_CoordSystem_COPY_INP_TMP = "image";
            }
            //
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(""))) != 0)
            {
                //disp_text does not accept an empty string for Color.
                hv_Color_COPY_INP_TMP.Dispose();
                hv_Color_COPY_INP_TMP = new HTuple();
            }
            //
            HOperatorSet.DispText(hv_WindowHandle, hv_String, hv_CoordSystem_COPY_INP_TMP,
                hv_Row_COPY_INP_TMP, hv_Column_COPY_INP_TMP, hv_Color_COPY_INP_TMP, hv_GenParamName,
                hv_GenParamValue);

            hv_Color_COPY_INP_TMP.Dispose();
            hv_Column_COPY_INP_TMP.Dispose();
            hv_CoordSystem_COPY_INP_TMP.Dispose();
            hv_Row_COPY_INP_TMP.Dispose();
            hv_GenParamName.Dispose();
            hv_GenParamValue.Dispose();

            return;
        }

    }
}
