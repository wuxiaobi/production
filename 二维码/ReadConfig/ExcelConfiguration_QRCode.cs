using production.二维码.mytool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace production.二维码.ReadConfig
{
    public partial class ExcelConfiguration
    {
        /// <summary>
        ///  创建二维码图片信息、二维码图片文件名、二维码路径上的文件夹名称  <para/>
        ///  <paramref name="filename"/>: <seealso cref="myStringProcess.stringValuesSeparated"/> 二维码图片文件名,根据Target获得<para/>
        ///  <paramref name="qrCodeStr"/>:<seealso cref="myStringProcess.stringQRCodeInfo"/>创建二维码图片信息 <para/>
        ///  
        ///  <paramref name="imageFolder"/>:<seealso cref="string"/>  二维码路径上的文件夹名称<para/>
        /// </summary>
        /// <returns>二维码图片文件路径</returns>
        public static string CreatExcelQRCode()
        {

            return Excel_QRCode.getCreatePathFileName(
                targetRowsByImageName,
                targetRowsValues,
                null,
                "images");

        }



    }
}