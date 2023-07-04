using System.Windows.Forms;
using System.Drawing;

namespace production.二维码.FormTools.myPictureBox
{
    public partial class MyPictureBox
    {
        private PictureBox pictureBox = new PictureBox();


        public MyPictureBox(Form form)
        {

            // 初始化PictureBox和ToolTip
            form.Controls.Clear();
            setPictureBox(form);
            ToolTipDelay();
            // 启用ToolTip功能并将其绑定到PictureBox上
            toolTip.AutomaticDelay = 0;
            toolTip.SetToolTip(pictureBox, "默认提示信息");

            // 注册事件处理程序
            pictureBox.MouseEnter += PictureBox_MouseEnter;

        }



        public MyPictureBox BackColor(Color color)
        {
            pictureBox.BackColor = color;
            return this;
        }

        public MyPictureBox Size(int width, int height)
        {
            pictureBox.Size = new Size(width, height);
            return this;
        }

        public MyPictureBox Location(int x, int y)
        {
            pictureBox.Location = new Point(x, y);
            return this;
        }

        public MyPictureBox Name(string name)
        {
            pictureBox.Name = name;
            return this;
        }

        public MyPictureBox SizeMode(PictureBoxSizeMode sizeMode)
        {
            pictureBox.SizeMode = sizeMode;
            return this;
        }

        public MyPictureBox Cursor(Cursor cursor)
        {
            pictureBox.Cursor = cursor;
            return this;
        }

        public void ImageDispose()
        {
            pictureBox.Image.Dispose();

        }

        public void setPicture(string path)
        {

            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
            }

            // 从指定的文件路径中创建一个新的Image对象
            Image image = Image.FromFile(path);
            // 将图像添加到PictureBox1中
            pictureBox.Image = image;

        }


        public void setPictureBox(Form form)
        {

            BackColor(Color.LightGreen)
            .Size(250, 250)
            .Location((form.ClientSize.Width - 250) / 2, (form.ClientSize.Height - 250) / 2)
            .Name("pictureBox")
            .SizeMode(PictureBoxSizeMode.StretchImage)
            .Cursor(Cursors.Hand);

            form.Controls.Add(pictureBox);
        }
    }
}