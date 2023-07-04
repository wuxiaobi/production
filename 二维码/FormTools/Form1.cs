using production.二维码.FormTools.myPictureBox;
using production.二维码.mytool;
using production.二维码.ReadConfig;
using System;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace production.二维码.FormTools
{
    public partial class Form1 : Form
    {
        MyPictureBox myPictureBox;
        static ToolTip toolTip1 = new ToolTip();
        PathMonitor monitor;
        private bool _isFormVisible = false;

        static Excel.Workbook oWB;
        static Excel.Worksheet sheet;
        static Excel.Range rng;

        public Form1()
        {
            InitializeComponent();

            
            this.Size = new Size(300, 330);
            this.Text = "二维码";
            this.Name = "Form1";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;//设置窗体大小不可改变
            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;
            this.VisibleChanged += Form1_VisibleChanged;
        }




        /// <summary>
        /// 用于监听配置文件是否有变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyCustomHandler(object sender, PathChangedEventArgs e)
        {
            Ribbon1.excelConfiguration.sExcelConfiguration();
        }


        private void Form1_Load(object sender, System.EventArgs e)
        {
            _isFormVisible = true;
            
            // 获取激活工作簿
            oWB = (Excel.Workbook)ExcelConfiguration.app.ActiveWorkbook;
            

        }


        /// <summary>
        /// 这是一个重写方法，执行点击关闭按钮，则隐藏窗体，不释放窗体。用于下次调用.
        /// 不需要引用，就可以执行
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && _isFormVisible)
            {
                Hide();
                e.Cancel = true;
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isFormVisible = false;
        }

        /// <summary>
        /// 工作表激活事件
        /// </summary>
        /// <param name="Sh"></param>
        public void OnSheetActivate(object Sh)
        {
            sheet = ExcelConfiguration.app.ActiveSheet;

            foreach (Excel.Worksheet worksheet in oWB.Worksheets)
            {
                worksheet.SelectionChange -= new Excel.DocEvents_SelectionChangeEventHandler(MyEvent);

                if (sheet.Name == worksheet.Name)//判断工作表是否激活，激活就注册：单元格选中事件。 不激活就取消注册
                {
                    sheet.SelectionChange += new Excel.DocEvents_SelectionChangeEventHandler(MyEvent);
                }
            }

            Ribbon1.excelConfiguration.sExcelConfiguration();
        }

        //单元格选中的具体操作
        private void MyEvent(Excel.Range Target)
        {
            
            ExcelConfiguration.TargetRow(Target);
           
            string path = ExcelConfiguration.CreatExcelQRCode();

            myPictureBox.setPicture(path);

            Excel_QRCode.imageDrawingTxt(path, ExcelConfiguration.targetRowsByImageName);

        }



        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            
            if (this.Visible)
            {
                myPictureBox = new MyPictureBox(this);
                //添加工作表激活事件
                oWB.SheetActivate += new Excel.WorkbookEvents_SheetActivateEventHandler(OnSheetActivate); ;
                OnSheetActivate(ExcelConfiguration.app.ActiveSheet);
                //监听事件
                monitor = new PathMonitor(System.IO.Path.GetDirectoryName(ExcelConfiguration.fullFolder_config));
                monitor.PathChanged += MyCustomHandler;
            }
            else
            {
                oWB.SheetActivate -= new Excel.WorkbookEvents_SheetActivateEventHandler(OnSheetActivate);
                sheet.SelectionChange -= new Excel.DocEvents_SelectionChangeEventHandler(MyEvent);
                monitor.PathChanged -= MyCustomHandler;
                myPictureBox.ImageDispose();
            }

        }



    }
}
