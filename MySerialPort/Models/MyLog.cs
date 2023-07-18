using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySerialPort.Models
{
    public class MyLog
    {
        #region MyRegion

        public string fileName { get; set; }
        public string filePlace { get; set; }
        public bool writeNewLine { get; set; }

        #endregion

        public MyLog()
        {
            writeNewLine = true;
            fileName = "MyLog";
            filePlace = System.AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="log"></param>
        public void WriteLog(string kind, string log)
        {
            string path = filePlace + fileName + ".txt";
            Boolean Result = IsOccupy(path);
            if (File.Exists(path))
            {
                if (writeNewLine)
                {
                    string oldContents = System.IO.File.ReadAllText(path);
                    try
                    {
                        string content = oldContents + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff") + "  " +
                                         kind + "  " + log + "\r\n";
                        System.IO.File.WriteAllText(path, content);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("日志写入失败！", "错误");
                    }
                }
                else
                {
                    File.Delete(path);
                    try
                    {
                        string content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff") + "  " +
                                         kind + "  " + log + "\r\n";
                        System.IO.File.WriteAllText(path, content);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("日志写入失败！", "错误");
                    }
                }
                
            }
            else
            {
                try
                {
                    string content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff") + "  " +
                                     kind + "  " + log + "\r\n";
                    System.IO.File.WriteAllText(path, content);
                }
                catch (Exception e)
                {
                    MessageBox.Show("日志生成失败！", "错误");
                }
            }
        }

        public bool IsOccupy(string file)
        {
            Boolean result = false;
            //判断文件是否存在，如果不存在，直接返回 false
            if (!System.IO.File.Exists(file))
            {
                result = false;
            }//end: 如果文件不存在的处理逻辑
            else
            {//如果文件存在，则继续判断文件是否已被其它程序使用
                //逻辑：尝试执行打开文件的操作，如果文件已经被其它程序使用，则打开失败，抛出异常，根据此类异常可以判断文件是否已被其它程序使用。
                System.IO.FileStream fileStream = null;
                try
                {
                    fileStream = System.IO.File.Open(file, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None);
                    result = false;
                }
                catch (System.IO.IOException ioEx)
                {
                    result = true;
                }
                catch (System.Exception ex)
                {
                    result = true;
                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Close();
                    }
                }
            }//end: 如果文件存在的处理逻辑
            //返回指示文件是否已被其它程序使用的值
            return result;
        }
    }
}
