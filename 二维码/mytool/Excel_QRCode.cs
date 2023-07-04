using production.二维码.ReadConfig;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace production.二维码.mytool
{
    public class Excel_QRCode
    {
        /// <summary>
        ///    <paramref name="filename"/>:要生产二维码的图片名字[文件名] <para/>
        ///    <paramref name="qrCodeStr"/>: 二维码信息    <para/>
        ///    <paramref name="filepath"/>: 二维码中间的图片完整路径<para/> 
        ///    <paramref name="imageFolder"/>:存放图片的文件夹
        /// </summary>
        /// <returns>string: 图片储存路径，该路径是图片文件</returns>
        public static string getCreatePathFileName(string filename, string qrCodeStr, string filepath, string imageFolder = null)
        {

            // 取反判断 filepath 是否为空，不为空则 new Bitmap(filepath) 二维码中间的图片
            var res = !string.IsNullOrEmpty(filepath) ? new Bitmap(filepath) : null;

            QRCodeGenerator qRCoderGenerator = new QRCoder.QRCodeGenerator();
            QRCodeData qrCodeData = qRCoderGenerator.CreateQrCode(qrCodeStr, QRCodeGenerator.ECCLevel.Q);
            QRCode qRcode = new QRCode(qrCodeData);
            //通过第三方组件画出一个二维码
            Bitmap qrCodeImage = qRcode.GetGraphic(5, Color.Black, Color.White, res, 15, 6, true);
            //生成二维码需要找个路径存起来,返回一个图片路径地址，用于后面保存图片用
            string pathfileName = getCreateFilePathFull(imageFolder, filename);
            //保存图片
            qrCodeImage.Save(pathfileName);
            qrCodeImage.Dispose();
            return pathfileName;
        }
        /// <summary>
        /// 生成二维码需要找个路径存起来,返回一个图片路径地址，用于后面保存图片用 <para/>
        /// 内部调用了 <seealso cref="ExcelConfiguration.imagePath"/>   生成路径    <para/>
        /// 调用<seealso cref="MyFile.CreateDirectoryExists"/> 将路径放进去用于创建路径上的目录
        /// </summary>
        /// <param name="imageFolder">图片存放的目录，传入一个名字，即可生产该名字文件夹</param>
        /// <param name="fileName"></param>
        /// <returns><see cref="string"/>:一个字符串地址  项目路径……workName\sheetName\<paramref name="imageFolder"/>\<paramref name="fileName"/>.png</returns>
        public static string getCreateFilePathFull(string imageFolder, string fileName)
        {
            return MyFile.CreateDirectoryExists(
                ExcelConfiguration.imagePath(
                    string.IsNullOrEmpty(imageFolder) ? "images" : imageFolder, fileName)
                );
        }






        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFilePath"></param>二维码图片地址，加载进来
        /// <param name="text"></param>  在图片上添加的文本内容，也做为图片文件
        /// <returns></returns>
        public static Boolean imageDrawingTxt(string imageFilePath, string text)
        {
            Image image = Image.FromFile(imageFilePath);
            // 加长图片下方 100 像素
            Bitmap bitmap = new Bitmap(image.Width, image.Height + 100);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // 设置背景颜色为白色
                graphics.Clear(Color.White);

                // 在新的图片上绘制原图
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);

                // 设置文本样式
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Font font = new Font("楷体", 15, FontStyle.Bold);
                Brush brush = new SolidBrush(Color.Black);

                // 在图片下方添加文本
                RectangleF rectangle = new RectangleF(0, image.Height, bitmap.Width, 100);
                graphics.DrawString(text, font, brush, rectangle, format);
            }
            imageFilePath = getCreateFilePathFull("Pimages", text);
            // 保存新的图片
            bitmap.Save(imageFilePath, ImageFormat.Png);

            // 释放图像资源
            image.Dispose();
            bitmap.Dispose();
            return true;
        }


    }
}
