﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace 文件操作与日志记录
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }
    }
}
