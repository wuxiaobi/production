using production.二维码.ReadConfig;
using System;
using System.Windows.Forms;

namespace production.二维码.FormTools.myPictureBox
{
    public partial class MyPictureBox
    {
        private ToolTip toolTip = new ToolTip();

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            // 当鼠标进入pictureBox区域时，设置新的提示信息
            toolTip.SetToolTip(pictureBox, string.IsNullOrEmpty(ExcelConfiguration.targetRowsValues) ? "这是要显示的文字信息" : ExcelConfiguration.targetRowsValues);
        }

        /// <summary>
        /// 用于设置 ToolTip 实例的属性 <para/>
        /// <paramref name="autoPopDelay"/>:指定当鼠标悬浮在目标控件上时，控件自动隐藏的等待毫秒数。例如，当 AutoPopDelay 属性值为 5000 时，鼠标离开控件后会在 5 秒后自动隐藏提示信息。<para/>
        /// <paramref name="initialDelay"/> :指定当鼠标第一次悬浮在目标控件上时，控件显示的等待毫秒数。例如，当 InitialDelay 属性值为 1000 时，在鼠标第一次悬浮在控件上时会等待 1 秒后才显示提示信息。  <para/>
        /// <paramref name="reshowDelay"/>:指定当鼠标连续悬浮在目标控件上时，控件重新显示的等待毫秒数。例如，当 ReshowDelay 属性值为 500 时，鼠标连续悬浮在控件上时，控件会在 0.5 秒内重新显示提示信息。 <para/>
        /// <paramref name="ShowAlways"/> :指定当鼠标悬浮在目标控件上时，控件是否始终显示。例如，当 ShowAlways 属性值为 true 时，无论鼠标是否移动，控件都会一直显示提示信息。默认true<para/>
        /// </summary>
        /// <param name="autoPopDelay">:指定当鼠标悬浮在目标控件上时，控件自动隐藏的等待毫秒数。例如，当 AutoPopDelay 属性值为 5000 时，鼠标离开控件后会在 5 秒后自动隐藏提示信息。<para/></param>
        /// <param name="initialDelay">:指定当鼠标第一次悬浮在目标控件上时，控件显示的等待毫秒数。例如，当 InitialDelay 属性值为 1000 时，在鼠标第一次悬浮在控件上时会等待 1 秒后才显示提示信息。   <para/></param>
        /// <param name="reshowDelay">:指定当鼠标连续悬浮在目标控件上时，控件重新显示的等待毫秒数。例如，当 ReshowDelay 属性值为 500 时，鼠标连续悬浮在控件上时，控件会在 0.5 秒内重新显示提示信息。   <para/></param>
        /// <param name="ShowAlways">:指定当鼠标悬浮在目标控件上时，控件是否始终显示。例如，当 ShowAlways 属性值为 true 时，无论鼠标是否移动，控件都会一直显示提示信息。默认true</param>
        /// <returns></returns>
        public MyPictureBox ToolTipDelay(int autoPopDelay = 5000, int initialDelay = 1000, int reshowDelay = 500, Boolean ShowAlways = true)
        {
            this.toolTip.AutoPopDelay = autoPopDelay;
            this.toolTip.InitialDelay = initialDelay;
            this.toolTip.ReshowDelay = reshowDelay;
            this.toolTip.ShowAlways = ShowAlways;
            return this;
        }

    }


}