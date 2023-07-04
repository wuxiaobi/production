using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace production.二维码.FormTools
{
    internal class MyFlowLayoutPanel : FlowLayoutPanel
    {
        private System.Drawing.Color originalBackColor;
        private RadialGradientBrush gradientBrush;

        public MyFlowLayoutPanel() : base()
        {
            // 记录初始的背景色
            originalBackColor = this.BackColor;

            // 添加鼠标悬停事件处理程序
            this.MouseEnter += new EventHandler(OnMouseEnter);

            // 添加鼠标离开事件处理程序
            this.MouseLeave += new EventHandler(OnMouseLeave);

            this.Location = new Point(0, 0);
            this.Dock = DockStyle.Top;
            this.FlowDirection = FlowDirection.LeftToRight;//s设置子控件排列从左到右
            this.WrapContents = false;
            this.Name = "this";
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new System.Drawing.Size(321, 36);
            this.TabIndex = 0;

        }
        // 鼠标悬停时，背景色为从中心扩散渐变的颜色
        private void OnMouseEnter(object sender, EventArgs e)
        {
            if (gradientBrush != null)
            {
                gradientBrush.Dispose();
            }

            // 创建渐变画刷
            gradientBrush = new RadialGradientBrush(new PointF(this.Width / 2f, this.Height / 2f),
                                                    this.Width / 2f,
                                                    Color.FromArgb(50, Color.White),
                                                    Color.FromArgb(240, Color.PowderBlue));

            // 更新背景色为渐变画刷的中心颜色，并设置透明度为100
            this.BackColor = Color.FromArgb(255, gradientBrush.CenterColor);
        }
        // 鼠标离开时，恢复原本的背景色
        private void OnMouseLeave(object sender, EventArgs e)
        {
            // 释放渐变画刷资源
            if (gradientBrush != null)
            {
                gradientBrush.Dispose();
                gradientBrush = null;
            }

            // 恢复原本的背景色
            this.BackColor = originalBackColor;
        }
    }
    // 自定义画刷类，实现了从中心扩散的渐变效果
    public class RadialGradientBrush : System.Drawing.Brush
    {
        private PointF centerPoint;
        private float radius;
        private System.Drawing.Color startColor;
        private System.Drawing.Color endColor;

        public RadialGradientBrush(PointF centerPoint, float radius, Color startColor, Color endColor)
        {
            this.centerPoint = centerPoint;
            this.radius = radius;
            this.startColor = startColor;
            this.endColor = endColor;
        }

        public Color CenterColor
        {
            get
            {
                // 开始和结束颜色的RGB值差
                var rDiff = endColor.R - startColor.R;
                var gDiff = endColor.G - startColor.G;
                var bDiff = endColor.B - startColor.B;

                // 开始和结束颜色的Alpha值差
                var aDiff = endColor.A - startColor.A;

                // 计算画刷中心颜色的RGB值和Alpha值
                var r = (int)(startColor.R + rDiff * (radius / Math.Sqrt(Math.Pow(centerPoint.X - 0, 2) + Math.Pow(centerPoint.Y - 0, 2))));
                var g = (int)(startColor.G + gDiff * (radius / Math.Sqrt(Math.Pow(centerPoint.X - 0, 2) + Math.Pow(centerPoint.Y - 0, 2))));
                var b = (int)(startColor.B + bDiff * (radius / Math.Sqrt(Math.Pow(centerPoint.X - 0, 2) + Math.Pow(centerPoint.Y - 0, 2))));
                var a = (int)(startColor.A + aDiff * (radius / Math.Sqrt(Math.Pow(centerPoint.X - 0, 2) + Math.Pow(centerPoint.Y - 0, 2))));

                // 返回中心颜色
                return Color.FromArgb(a, r, g, b);
            }
        }


        public override object Clone()
        {
            // 拷贝对象
            return new RadialGradientBrush(centerPoint, radius, startColor, endColor);
        }


    }
}
