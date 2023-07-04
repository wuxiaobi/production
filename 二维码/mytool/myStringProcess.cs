using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace production.二维码.mytool
{
    public static class myStringProcess
    {

        /// <summary>
        /// 用逗号分割字符串，成为数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static string[] CommaSlipt(string str, string symbol = "")
        {
            string[] condition = { (symbol == "") ? "," : symbol };
            string[] result = str.Split(condition, StringSplitOptions.RemoveEmptyEntries);
            return result;
        }
        /// <summary>
        /// 传入字典将key作为字符串,或者string数组,拼接用“,”
        /// </summary>
        /// <param name="obj">这个参数可以是字典或者string数组</param>
        /// <returns></returns>
        public static string getStringCommaSeparated(object obj)
        {
            string str = "";
            if (obj is Dictionary<string, string>)
            {
                var obj2 = (obj as Dictionary<string, string>).Keys;
                foreach (var key in obj2)
                {
                    str = str == "" ? key : str + "," + key;
                }
                return str;
            }
            else if (obj is string[])
            {
                var obj2 = obj as string[];
                if (obj2 != null)
                {
                    foreach (var key in obj2)
                    {
                        str = str == "" ? key : str + "," + key;
                    }
                }
                return str;
            }
            return null;
        }
        /// <summary>
        /// 将字典的key，value拼接成要展示的信息
        /// </summary>
        /// <param name="dic"></param>
        /// <returns>key1 : value \n key2 :value \n……
        /// </returns>
        public static string stringQRCodeInfo(Dictionary<string, string> dic)
        {
            string str = "";
            foreach (var key in dic.Keys)
            {
                str = str == "" ? key + ": " + dic[key] + "\n"
                                : str + key + ": " + dic[key] + "\n";
            }
            str = str.Trim();
            return str;

        }
        /// <summary>
        /// 将字典的value拼接成字符串
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string stringValuesSeparated(Dictionary<string, string> dic)
        {
            string str = "";
            foreach (var vul in dic.Values)
            {
                str = str == "" ? vul
                                : str + "_" + vul;
            }
            str = str.Trim();
            return str;
        }


    }
}