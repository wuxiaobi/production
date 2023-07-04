using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace production.二维码.mytool
{
    public class MyAttribute
    {
        /// <summary>
        /// 判断字典key是否存在，value是否为空
        /// </summary>
        /// <returns>key存在 且 value不为空，返回true</returns>
        public static Boolean ContainsDictionary(string attribute, Dictionary<string, string> config)
        {
            return ContainsKey(attribute, config) &&
                ContainsValue(attribute, config);
        }

        /// <summary>
        /// 传入key判断字典key的值是否为空或null，若传入了字典变量，则判断字典变量key的值是否为空或null
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValuePairs"></param>
        /// <returns>值不为空或者null，返回true，否则false</returns>
        public static Boolean ContainsValue(string key, Dictionary<string, string> keyValuePairs)
        {
            return (keyValuePairs[key] == null || keyValuePairs[key] == "") ? false : true;
        }
        /// <summary>
        /// 传入key判断key是否存在，判断字典变量是否存在key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyValuePairs"></param>
        /// <returns>如果key存在就返回true</returns>
        public static Boolean ContainsKey(string key, Dictionary<string, string> keyValuePairs)
        {
            return keyValuePairs.ContainsKey(key);
        }
    }
}