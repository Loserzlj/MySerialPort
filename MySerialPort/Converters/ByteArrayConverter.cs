using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySerialPort.Converters
{
    public class ByteArrayConverter
    {
        /// <summary>
        /// byte数组转string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] data)
        {
            return Encoding.Default.GetString(data);
        }



        /// <summary>
        /// string转 byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string data)
        {

            return Encoding.Default.GetBytes(data);
        }





        /// <summary>
        /// byte数组转16进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(string.Format("{0:X2} ", data[i]));
            }
            return builder.ToString().Trim();
        }

        /// <summary>
        /// 16进制字符串转byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string data)
        {
            string[] chars = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                try
                {
                    returnBytes[i] = Convert.ToByte(chars[i], 16);
                }
                catch (Exception e)
                {
                    MessageBox.Show("数据格式错误！", "警告");
                    return new byte[chars.Length];
                }

            }
            return returnBytes;
        }



        /// <summary>
        /// byte数组转10进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToDecString(byte[] data)
        {

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i] + " ");
            }
            return builder.ToString().Trim();
        }

        /// <summary>
        /// 10进制字符串转byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] DecStringToByteArray(string data)
        {

            string[] chars = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];
            //逐个字符变为10进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(chars[i], 10);
            }
            return returnBytes;
        }




        /// <summary>
        /// byte数组转八进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToOtcString(byte[] data)
        {

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(Convert.ToString(data[i], 8) + " ");
            }
            return builder.ToString().Trim();
        }

        /// <summary>
        /// 八进制字符串转byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] OtcStringToByteArray(string data)
        {

            string[] chars = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];
            //逐个字符变为8进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(chars[i], 8);
            }
            return returnBytes;
        }





        /// <summary>
        /// 二进制字符串转byte数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] BinStringToByteArray(string data)
        {
            string[] chars = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];
            //逐个字符变为2进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(chars[i], 2);
            }
            return returnBytes;
        }



        /// <summary>
        /// byte数组转二进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToBinString(byte[] data)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(Convert.ToString(data[i], 2) + " ");
            }
            return builder.ToString().Trim();
        }

        // public static CharArrayToByteArray(Char[] data)
        // {
        //     Byte[] bytes = new byte[data.Length];
        // }
    }
}
