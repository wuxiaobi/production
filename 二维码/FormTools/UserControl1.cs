using production.二维码.mytool;
using production.二维码.ReadConfig;
using System;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace production.二维码.FormTools
{
    public partial class UserControl1 : UserControl
    {

        public UserControl1()
        {
            InitializeComponent();

        }
        //移除UserControl所有控件
        public void removeAllControls()
        {
            this.Controls.Clear();
        }
        private void UserControl1_Load(object sender, System.EventArgs e)
        {

        }


        public Boolean addflowLayoutPanel(string btnString, string textBoxString)
        {
            MyFlowLayoutPanel flowLayoutPanel1 = new MyFlowLayoutPanel();
            Button button1 = new Button();
            TextBox textBox1 = new TextBox();
            button1.Text = btnString;
            button1.Width = 110;
            button1.Height = 30;
            // 创建 TextBox 控件
            textBox1.Width = 300;
            textBox1.Height = 27;
            textBox1.Text = textBoxString;
            textBox1.Name = btnString;
            textBox1.Font = new System.Drawing.Font("微软雅黑", 12);
            button1.Click += Button1_Click;
            try
            {
                flowLayoutPanel1.Controls.Add(button1);
                flowLayoutPanel1.Controls.Add(textBox1);
                Controls_Add(flowLayoutPanel1);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "userFilePath") { btn_userFilePath(button); }
            else { btn_ReviseText(button); }

            Ribbon1.excelConfiguration.sExcelConfiguration();
        }

        public void btn_userFilePath(Button button)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = folderBrowserDialog.SelectedPath;
                TextBox textBox = GetTextBoxByName(button);
                textBox.Text = selectedPath;
                MyFile.WriteTextStoringDictionary(ExcelConfiguration.adminConfig_path, textBox.Name, selectedPath);
            }

            
        }
        public void btn_ReviseText(Button button)
        {
            TextBox textBox = GetTextBoxByName(button);
            Excel.Range selectedCell = ExcelConfiguration.app.ActiveWindow.RangeSelection;
            textBox.Text= MyExcel.selectedCell(selectedCell);

            MyFile.WriteTextStoringDictionary(ExcelConfiguration.userConfig_path, button.Text, textBox.Text);
        }
        /// <summary>
        /// 根据指定名称获取button的同级TextBox
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private TextBox GetTextBoxByName(Button button)
        {
            TextBox textBox = button.Parent.Controls.Find(button.Text, true).FirstOrDefault() as TextBox; // 获取指定名称的子窗体
            return textBox;
        }

        public void Controls_Add(FlowLayoutPanel flowLayoutPanel1)
        {
            this.Controls.Add(flowLayoutPanel1);
        }
        public void Controls_Remove(FlowLayoutPanel flowLayoutPanel1)
        {
            this.Controls.Remove(flowLayoutPanel1);
        }

    }
}