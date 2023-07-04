using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace production.二维码.ReadConfig
{
    public partial class ExcelConfiguration
    {
        private static string sheetNameAsFolder;
        private static string workNameAsFolder;
        /// <summary>
        /// 完整配置路径
        /// </summary>
        public static string fullFolder_config;
        public static string fullFolder;
        public static string folder;

        /// <summary>
        /// 路径拼接 代码示例:
        /// <code>
        ///string path1 = @"C:\test1";
        ///string path2 = @"test2";
        ///string path3 = @"test3";
        ///string s= MyFile.PathCombineFullFolde(path1, path2, path3……) 
        /// 得到//C:\test1\test2\test3……
        /// </code>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public string PathCombineFullFolde(string path, params string[] strings)
            => strings.Select(s
                => Path.GetFileNameWithoutExtension(s)).Aggregate(path, Path.Combine);

        /// <summary>
        ///  拼接项目路径下的文件夹路径
        /// </summary>
        /// <param name="path">[可选]<paramref name="path"/>为空则默认:<seealso cref="userFilePath"/> <para/>
        /// <seealso cref="userFilePath"/>也为空则:<seealso cref="filePath"/></param>
        /// <returns>
        /// <paramref name="path"/>不为<paramref name="null"/>则返回:<paramref name="path"/>\workName\sheetName\  <para/>
        /// <paramref name="path"/>=<paramref name="null"/>则返回:<paramref name="userFilePath"/>\workName\sheetName\  <para/>
        /// <paramref name="userFilePath"/>也为空，则返回：<paramref name="filePath"/>\workName\sheetName\ 
        /// </returns>
        public static string PathFolder(string path = null)
        {
            return fullFolder = Path.Combine(
                    string.IsNullOrEmpty(path)
                    ? (string.IsNullOrEmpty(userFilePath) ? filePath : userFilePath)
                    : path,
                WorkName_SheetNameAsFolder());
        }

        /// <summary>
        /// 用于将连个名字拼接成部分路径
        /// <code>
        /// <paramref name="ExcelConfiguration"/>.<seealso cref="WorkName_SheetNameAsFolder("/>"path1","path2" <see cref=")"/> <para/>
        /// <paramref name="或者"/>: <para/>
        /// <seealso cref="WorkNameAsFolder"/> = "path1"  <para/>
        /// <seealso cref="SheetNameAsFolder"/> ="path2"  <para/>
        /// <paramref name="ExcelConfiguration"/>.<seealso cref="WorkName_SheetNameAsFolder("/><see cref=")"/> 
        /// </code>
        /// </summary>
        /// <param name="workName"></param>
        /// <param name="sheetName"></param>
        /// <returns>"path1\path2"</returns>
        private static string WorkName_SheetNameAsFolder(string workName = null, string sheetName = null)
        {
            WorkNameAsFolder = (!string.IsNullOrEmpty(workName)) ? workName : WorkNameAsFolder;
            SheetNameAsFolder = (!string.IsNullOrEmpty(sheetName)) ? sheetName : SheetNameAsFolder;
            folder = Path.Combine(WorkNameAsFolder, SheetNameAsFolder);
            return folder;
        }


        /// <summary>项目路径下的文件夹名称，以工作表的名称</summary>
        public static string SheetNameAsFolder
        {
            get => sheetNameAsFolder;
            set
            {
                value = value ?? "";
                sheetNameAsFolder = Path.GetFileNameWithoutExtension(value);
            }
        }

        /// <summary>项目路径下的文件夹名称，以工作薄的名称</summary>
        public static string WorkNameAsFolder
        {
            get => workNameAsFolder;
            set
            {
                value = value ?? "";
                workNameAsFolder = Path.GetFileNameWithoutExtension(value);
            }

        }
        /// <summary>
        /// 项目路径下的文件夹名称，以工作薄、工作表的名称<para/>
        /// <see cref="SheetNameAsFolder"/>="path1"  <para/>
        /// <see cref="WorkNameAsFolder"/> ="path2"  <para/>
        /// <seealso cref="FilePath_WorkName_SheetNameAsFolder("/>"项目路径\<paramref name="path1"></paramref>\<paramref name="path2"></paramref>\prop\red.txt")
        /// </summary>
        /// <param name="path"></param>
        /// <param name="workName"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static string FilePath_WorkName_SheetNameAsFolder(string path, string workName, string sheetName, string fileName)
        {
            return Path.Combine(path, WorkName_SheetNameAsFolder(workName, sheetName), fileName);
        }



        /// <summary>
        /// 拼接项目路径下 
        /// </summary>
        /// <param name="imageFolder"></param>
        /// <param name="fileName"></param>
        /// <returns><see cref="string"/>:一个字符串地址  项目路径……workName\sheetName\<paramref name="imageFolder"/>\<paramref name="fileName"/>.png</returns>
        public static string imagePath(string imageFolder, string fileName)
            => Path.Combine(PathFolder(), imageFolder, fileName + ".png");




    }

}