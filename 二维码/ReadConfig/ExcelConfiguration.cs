using production.二维码.mytool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
namespace production.二维码.ReadConfig
{
    /// <summary>
    /// 用于处理配置文件的类
    /// </summary>
    public partial class ExcelConfiguration
    {
        /// <summary> 项目路径 </summary>
        public static string filePath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>用户自定义路径 通过读取filePath里的userFilePath字段生效</summary>
        public static string userFilePath;
        public static Excel.Application app;

        /// <summary>初始配置 </summary>
        public static Dictionary<string, string> configuration = new Dictionary<string, string>();//用于存储获取项目下的\prop\red.txt文件内容

        /// <summary>
        /// 用于存储用户点击单元个，获取这一行的数据
        /// </summary>
        public static string targetRowsValues = "";
        /// <summary>
        /// 用于图片名称使用
        /// </summary>
        public static string targetRowsByImageName = "";

        /// <summary>用户初始配置，当读取到configuration里的userFilePath有值时，就会根据这个路径生成用户配置
        /// </summary>
        public static Dictionary<string, string> userDictionary = new Dictionary<string, string>();

        /// <summary>
        /// 列名column的信息作为 imageName，用于图片名称
        /// </summary>
        public static string imageName;
        /// <summary> 配置文件有userFilePath路径 ，则为true</summary>
        public static Boolean userboolean;

        /// <summary>数组接收title的值</summary>
        public static string[] title;

        /// <summary> 数组title，拼接成字符串 </summary>
        public static string stringTitle;

        /// <summary>
        /// 自动加载，获取当前运行application,并进行初始配置设置
        /// </summary>
        public ExcelConfiguration()
        {
            sExcelConfiguration();
        }
        /// <summary>
        /// 主方法：
        /// 设置：<para/>
        ///  <seealso cref="targetRowsValues"/> 根据<paramref name="title"/>的列名取得Target该行的值  <![CDATA[Dictionary<string, string>();]]>title作为键<para/>
        ///  <seealso cref="targetRowsByImageName"/> 根据<paramref name="imageName"/>的列名取得Target该行的值  <![CDATA[Dictionary<string, string>();]]>imageName作为键
        /// </summary>
        /// <param name="Target"></param>
        public static void TargetRow(Excel.Range Target)
        {
            
            targetRowsValues = myStringProcess.stringQRCodeInfo(TargetRowsValues(Target, stringTitle));
            targetRowsByImageName = myStringProcess.stringValuesSeparated(TargetRowsValues(Target, imageName));
        }
        /// <summary>
        /// 用于初始化的类
        /// </summary>
        public void sExcelConfiguration()
        {
            app = (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");

            configuration = getTextDirectoryConfiguration(
                getConfigPath(filePath, app, "adminConfig.txt")
                );
            userboolean = setUserFilePath();

            userDictionary = getTextDirectoryConfiguration(
                getConfigPath(userFilePath, app, "userConfig.txt")
                );

            imageName = userDictionary["imageName"];
            title = myStringProcess.CommaSlipt(userDictionary["title"]);
            stringTitle = myStringProcess.getStringCommaSeparated(title);
        }

        /// <summary>
        /// 用户选中获取一行到的信息
        /// <para/>
        /// <paramref name="Target"/> :是单击选中单元格 
        /// <para/>
        /// <paramref name="obj"/> :
        /// <code>
        /// <paramref name="示例"/>:  string[]
        /// string[] str={"列名1, "列名2","列名3"};
        /// <para/>
        /// <paramref name="示例"/>:  string
        /// string str= "列名1,列名2,列名3";
        /// <para/>
        /// <paramref name="字典示例"/>:<para/>
        /// <![CDATA[Dictionary<string, string> str=new Dictionary<string, string>();]]><para/>
        /// str["列名1"]="";str["列名2"]="";str["列名3"]=""; <para/>
        /// <![CDATA[Dictionary<string, int>]]> <para/>
        /// 如果是字典只循环读取key作为列名
        ///  </code>
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="obj"></param>
        /// <returns><![CDATA[类型:Dictionary<string, string> ]]><para/>
        /// 赋值给静态变量，例如:  <see cref="targetRowsValue"/>
        /// </returns>
        public static Dictionary<string, string> TargetRowsValues(Excel.Range Target, object obj)
        {
            return MyExcel.GetExcelTargetHeaderRow(Target, obj);
        }


        /// <summary>
        /// 设置userFilePath属性，读取的配置文件：  userFilePath   
        /// </summary>
        /// <returns>userFilePath : 有 true 并设置属性; 没有 false</returns>
        public Boolean setUserFilePath()
        {
            Boolean b = MyAttribute.ContainsDictionary("userFilePath", configuration);
            userFilePath = b ? configuration["userFilePath"] : filePath;
            return b;
        }
        /// <summary>
        ///  返回包含指定字符数组中某一个<para/>
        ///  示例：<para/>
        ///   <![CDATA[Dictionary<string, int>]]> dict =new <![CDATA[Dictionary<string, int>()]]>;<para/>
        ///   dict.Add("abcd",0);<para/>
        ///   dict.Add("bcd",0); <para/>
        ///   string[] str={"a","f"}<para/>
        ///   string s= stringContains(dict,str);<para/>
        ///   返回：  abcd
        /// </summary>
        /// <param name="dict">dict[key] 是否包含str[]中某一个，如果有就返回</param>
        /// <param name="str">用作检查字符，检查dict[]是否包含str[]</param>
        /// <returns></returns>
        public static string stringContains(Dictionary<string, int> dict, string[] str)
        {

            foreach (string key in dict.Keys)
            {
                if (str.Any(s => key.Contains(s)))
                {
                    return key;
                }
            }

            // 若未找到匹配的key，则返回dict的第一个
            return dict.First().Key;
        }
        public static string adminConfig_path;
        public static string userConfig_path;
        /// <summary>
        /// 生成一个完整文件路径
        /// <para>示例:</para> 
        /// <code>
        /// string path= @"C:\example"
        /// ExcelConfiguration.getConfigPath(path,app,"red.txt")   
        /// //会在目录下生成文件C:\example\workbookName\worksheetName\red.txt
        /// </code>
        /// </summary>
        /// <param name="fPath"></param>
        /// <param name="application"></param>
        /// <param name="fileNme"></param>
        /// <returns></returns>

        public static string getConfigPath(string fPath, Excel.Application application, string fileNme)
        {
            fullFolder_config =
                FilePath_WorkName_SheetNameAsFolder(
                    fPath,
                    application.ActiveWorkbook.Name,
                    application.ActiveWorkbook.ActiveSheet.Name,
                    fileNme
                   );

            if (fileNme == "adminConfig.txt") { adminConfig_path = fullFolder_config; }
            else
            if (fileNme == "userConfig.txt") { userConfig_path = fullFolder_config; }
            return fullFolder_config;
        }

        /// <summary>
        /// 默认ImageName 包含"id", "ID", "编号", "号" ,"名" 这些字样选取最先循环到的一个作为列名
        /// </summary>
        static string[] defaultImageName = { "id", "ID", "编号", "号" ,"名"};
        /// <summary>
        /// 主方法：<para/>
        /// 读取路径里的配置文件，以键值对返回字典，传入路径。
        /// <example> 
        /// <para>示例:</para> 
        ///    <paramref name="fPath"/>:传入一个文件完整路径
        ///  </example>   
        /// </summary>
        ///   
        /// <param name="fPath">文件夹路径</param>
        /// <returns>根据传入的路径，读取配置</returns>
        public static Dictionary<string, string> getTextDirectoryConfiguration(string fPath)
        {
            if (!File.Exists(fPath))
            {//取反，如果文件不存在就执行下面这段
                
                Dictionary<string, int> keys = MyExcel.GetExcelHeaderColum(app.ActiveSheet);
                
                string[] text = ConfigText(fPath, MyExcel.获取当前激活工作表列名(app), stringContains(keys, defaultImageName));

                MyFile.ExistPathBeWriteConfiguration(fPath, text);
            }

            return MyFile.ReadingTextStringDictionary(fPath);
        }



        /// <summary>
        /// 根据传入路径 <paramref name="filepath"/> 判断文件名 为 config<para/>
        /// 写入 userFilePath≅<para/>
        /// 如果为 userConfig <para/>
        /// 写入 title≅激活表的列名 和  imageName≅某一个列名
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="title"></param>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public static string[] ConfigText(string filepath, string title = null, string imageName = null)
        {
            string[] text = null;
            if (Path.GetFileNameWithoutExtension(filepath) == "adminConfig")
            {
                text = new string[2];
                text[0] = "#userFilePath用户自定义路径";
                text[1] = "userFilePath≅";
            }
            else if (Path.GetFileNameWithoutExtension(filepath) == "userConfig")
            {
                text = new string[4];
                text[0] = "#title是获取列名";
                text[1] = "title≅" + title;
                text[2] = "#imageName是生成图片的名字";
                text[3] = "imageName≅" + imageName;
            }

            return text;
        }

    }

}
