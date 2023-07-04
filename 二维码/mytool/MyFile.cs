using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace production.二维码.mytool
{
    public class MyFile
    {
        /// <summary>
        /// 根据路径判断文件是否存在，如果不存在则创建文件，并写入初始配置title(为当前激活sheet的列名)，imageName
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>写入成功返回true，已有文件则不写入，直接返回false</returns>
        public static Boolean ExistPathBeWriteConfiguration(string filePath, string[] text)
        {

            Boolean b = FileExistsPathCreate(filePath);
            if (!b)//如果不存在，取反进入创建文件，写入基础配置列名，以及生成的图片的名字列名
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string txt in text)
                    {
                        writer.WriteLine(txt);
                    }
                }
                return !b;
            }
            return !b;

        }

        /// <summary>
        /// 用于修改txt内的配置，如果没有则追加到最后
        /// </summary>
        /// <param name="path"></param> txt路径
        /// <param name="keyValue"></param> 
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static Boolean WriteTextStoringDictionary(string path, string keyValue, string newValue)
        {
            try
            {
                string line;
                using (StreamReader reader = new StreamReader(path))
                {
                    using (StreamWriter writer = new StreamWriter(path + ".tmp", false, System.Text.Encoding.UTF8))
                    {
                        Boolean witered = false;
                        while ((line = reader.ReadLine()) != null || !reader.EndOfStream)
                        {
                            while (String.IsNullOrEmpty(line))
                            {
                                writer.WriteLine(line);
                                line = reader.ReadLine(); //如果是空行就直接下一行
                            }
                            line = line.Trim();
                            if (line.Substring(0, 1)[0].ToString() == "#") { writer.WriteLine(line); continue; }//如果第一个字符是#则跳过                                                                            // 检查是否到达文件末尾

                            if (line.Contains(keyValue + "\u2245"))//根据key值替换
                            {
                                witered = true;//如果有key键值，做个标记
                                               // 找到要修改的行，替换里面的文本
                                line = keyValue + "\u2245" + newValue;

                            }
                            // 写回到文件中
                            writer.WriteLine(line);
                            //判断是否读到末尾，如果是则判断是否替换过key，如果没替换过，表示配置里面没有该 键值对。那么直接添加到末尾
                            if (reader.Peek() == -1 || reader.EndOfStream)
                            {
                                if (!witered) { line = keyValue + "≅" + newValue; writer.Write(line); }
                            }

                        }
                    }
                }
                // 用修改后的文件替换原始文件
                File.Delete(path);
                File.Move(path + ".tmp", path);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 读取配置文件   根据传入的文件路径，逐行读取， "#"开头作为注释跳过
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ReadingTextStringDictionary(string path)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            // 打开文件并读取内容
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line == "") { continue; }//如果是空行就直接下一行
                    line = line.Trim();

                    if (line.Substring(0, 1)[0].ToString() == "#") { continue; }//如果第一个字符是#则跳过

                    string[] dis = myStringProcess.CommaSlipt(line, "≅");//字符串 拆分成数组
                    string dis1 = null;
                    if (dis.Length == 2) { dis1 = dis[1]; } //如果数组长度等于2就给dis1赋值数组的第二个元素
                    keyValuePairs[dis[0]] = dis1;
                }
                sr.Dispose();
                sr.Close();
            }

            return keyValuePairs;
        }
        /// <summary>
        /// 本方法传入文件路径 判断文件是否存在:不存在返回false 并创建文件，存在则返回true;
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Boolean FileExistsPathCreate(string filePath)
        {
            CreateDirectoryExists(filePath);
            Boolean flg = File.Exists(filePath);//如果指定的文件存在，则为true；否则为false
            if (!flg) { File.Create(filePath).Dispose(); }//创建文件 如果指定的文件存在，则为false；否则为true 取反
            return flg;
        }
        /// <summary>
        /// 给个文件路径，创建目录:
        /// <code>
        /// MyFile.CreateDirectoryExists("C:\example\prop\red.txt");
        ///
        /// </code>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>C:\example\prop\</returns>

        public static string CreateDirectoryExists(string filePath)
        {
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
            {
                try
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));  // 创建目录（如果不存在）
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"创建目录时发生错误: {ex.Message},\\n 你没有权限创建文件，请用管理员运行安装程序安装！");
                }
            }
            return filePath;
        }

    }

}