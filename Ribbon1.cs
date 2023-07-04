using Microsoft.Office.Tools.Ribbon;
using production.二维码.FormTools;
using production.二维码.ReadConfig;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace production
{
    public partial class Ribbon1 
    {
        public static Form1 fm;
        public static ExcelConfiguration excelConfiguration;
        
        MyCustomTaskPane taskPane;
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            this.button1.Click += Button1_Click;
            this.group1.DialogLauncherClick += Group1_DialogLauncherClick;
            this.tab1.Label = "生产模块";
            
            taskPane = new MyCustomTaskPane("配置");
           
        }


        public void Button1_Click(object sender, RibbonControlEventArgs e)
        {
            OtainFm();
            fm.Show();
        }

        /// <summary>
        /// 初始化配置，并创建Form1，如果已有则获取Form1
        /// </summary>
        public static void OtainFm()
        {
            excelConfiguration = new ExcelConfiguration();

            fm = Application.OpenForms.Cast<Form1>().FirstOrDefault(f => f.Name == "Form1");

            if (Object.ReferenceEquals(fm, null))
            {
                fm = new Form1();
            }
            fm.TopMost = true;

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Group1_DialogLauncherClick(object sender, RibbonControlEventArgs e)
        {

            taskPane.CustomTaskPane_UserControl();

        }
    }
}