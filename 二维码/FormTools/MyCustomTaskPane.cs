using Microsoft.Office.Core;
using production.二维码.ReadConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomTaskPane = Microsoft.Office.Tools.CustomTaskPane;
namespace production.二维码.FormTools
{
    public class MyCustomTaskPane
    {
        static UserControl1 uc1;
        static CustomTaskPane ctp;
        public MyCustomTaskPane(string title)
        {
           
            uc1 = new UserControl1();

            ctp = Globals.ThisAddIn.CustomTaskPanes.Add(uc1, title);
           

        }
        public void CustomTaskPane_UserControl()
        {
            if (HasCustomTaskPane(uc1))
            {
                ctp = Globals.ThisAddIn.CustomTaskPanes.FirstOrDefault(p => p.Title == "配置");
            }
            ctp.Width = 300;
            ctp.DockPosition = MsoCTPDockPosition.msoCTPDockPositionRight;
            ctp.Visible = ctp.Visible ? !ctp.Visible : !ctp.Visible;
            if (ctp.Visible)
            {
                Ribbon1.OtainFm();
            };
            addUserConfig();
        }

        /// <summary>
        /// 向UserControl1添加配置数据，用于展示给用户看
        /// </summary>
        /// <returns></returns>
        public Boolean addUserConfig()
        {
            uc1.removeAllControls();
            uc1.addflowLayoutPanel("userFilePath", ExcelConfiguration.userFilePath);
            Dictionary<string, string> config = ExcelConfiguration.userDictionary;
            foreach (string key in config.Keys)
            {
                uc1.addflowLayoutPanel(key, config[key]);
            }
            return true;
        }



        /// <summary>
        /// 判断是否存在指定的 UserControl
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public bool HasCustomTaskPane(UserControl1 ctrl)
        {
            return Globals.ThisAddIn.CustomTaskPanes.Cast<CustomTaskPane>().Any(ctp => ctp != null && ctp.Window != null && ctp.Control == ctrl);
        }

    }
}