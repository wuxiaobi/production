using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace production.二维码.mytool
{
    /// <summary>
    /// 本类用于获取工作表列名
    /// </summary>
    public class MyExcel
    {
         public static string selectedCell(Excel.Range cells)
        {
            string str = null;
            foreach (Excel.Range select in cells)
            {
                if (str == null)
                { str = select.Value; }
                else
                { str = str + "," + select.Value; }
            }
            return str;
        }

        /// <summary>
        /// 传入当前运行的application 获得当前激活工作表的列名，以“,”隔开列名。
        /// <para/>
        ///  <paramref name="app"/>:当前运行程序
        /// </summary>  
        /// <param name="app"></param>
        /// <returns><see cref="string"/>: "column1,column2,column3……"</returns>
        public static string 获取当前激活工作表列名(Excel.Application app)
        {
            Dictionary<string, int> titlekeyValuePairs = GetExcelHeaderColum(app.ActiveSheet);

            string str = "";
            foreach (string key in titlekeyValuePairs.Keys)
            {
                str = str == "" ? key : str + "," + key;
            }

            return str;

        }
        /// <summary>
        /// 传入Target，根据Target获取该行的值，如果obj有传入值，则target根据obj传入的列名获取值
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetExcelTargetHeaderRow(Excel.Range Target, object obj = null)
        {

            int row = Target.Row;//获取选中的单元格的行 row
            Excel.Worksheet worksheet = (Excel.Worksheet)Target.Application.ActiveSheet;
            
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            Dictionary<string, int> valuePairs = GetExcelHeaderColum(worksheet, obj);

            foreach (string rngeCol in valuePairs.Keys)
            {
                Excel.Range rng = (Excel.Range)worksheet.Cells[row, valuePairs[rngeCol]];

                keyValues[rngeCol] = rng.Value != null ? rng.Value.ToString() : "";
           
            }
            return keyValues;
        }
        /// <summary>
        /// 将工作表的UsedRange逐列判断，如果一列都是空值则列名为<paramref name="key"/>，值为<paramref name="true"/>
        /// 
        /// </summary>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        public static Dictionary<int, Boolean> NonEmptyColumn(Excel.Worksheet worksheet)
        {
            Excel.Range usedRange = worksheet.UsedRange;
            int colCount = usedRange.Columns.Count;
            Dictionary<int, Boolean> rngEmpty = new Dictionary<int, Boolean>();
            for (int colIndex = colCount; colIndex >= 1; colIndex--)
            {
                Excel.Range colRange = usedRange.Columns[colIndex];//通过下标获取一列
                foreach (Excel.Range cell in colRange.Cells)
                {
                    if (cell.MergeCells) { continue; }//如果这行有合并单元格则跳过
                    if (!string.IsNullOrEmpty(cell.Text?.ToString()))// 单元格的值为空或null,取反
                    {
                        rngEmpty[colIndex] = false;
                        break;
                    }
                    rngEmpty[colIndex] = true;
                }


            }
            return rngEmpty;
        }

        /// <summary>
        /// 获取excel表格的列名，如果obj参数不为null，根据obj为string拆分的数组、或则字典的key，获得列名以及Column
        /// </summary>
        /// <param name="worksheet">传入当前工作表</param>
        /// <param name="obj"></param>
        /// <returns>返回表格的表头，存入字典</returns>
        public static Dictionary<string, int> GetExcelHeaderColum(Excel.Worksheet worksheet, object obj = null)
        {
            int headerRow = -1;
            Excel.Range usedRange = worksheet.UsedRange;
            int rowCount = usedRange.Rows.Count;
            Dictionary<int, Boolean> rngEmpty = NonEmptyColumn(worksheet);
            Dictionary<string, int> keyValues = new Dictionary<string, int>();

            if (obj == null)
            {
                
                for (int rowIndex = 1; rowIndex <= rowCount; rowIndex++)
                {
                    
                    Excel.Range rowRange = usedRange.Rows[rowIndex];//通过下标获取一行                   
                    if (rowRange.MergeCells) { continue; }//如果这行有合并单元格则跳过

                    foreach (Excel.Range cell in rowRange.Cells)
                    {
                        if (rngEmpty[cell.Column]) { continue; }
                        if (string.IsNullOrEmpty(cell.Text?.ToString()))// 单元格的值为空或null
                        {
                            keyValues.Clear();
                            headerRow = -1;
                            break;
                        }
                        keyValues[cell.Value] = cell.Column;
                        headerRow = rowIndex;
                    }
                    if (headerRow != -1) { break; }
                   
                }
            }
            else if (obj.GetType() == typeof(string))//如果传入的obj是string类型
            {
                
                string[] dis = myStringProcess.CommaSlipt((string)obj); ///拆分成string数组
                
                foreach (var di in dis)
                {
                    Excel.Range range = usedRange.Application.Cells.Find(di);
                    if (range != null) { keyValues[di] = range.Column; }
                }
               
            }
            else if (obj.GetType() == typeof(string[]))
            {
                foreach (var di in (string[])obj)
                {
                    Excel.Range range = usedRange.Application.Cells.Find(di);
                    if (range != null) { keyValues[di] = range.Column; }
                }

            }
            else if (obj.GetType() == typeof(Dictionary<string, int>))
            {
                foreach (var key in ((Dictionary<string, int>)obj).Keys)
                {
                    Excel.Range range = usedRange.Application.Cells.Find(key);
                    if (range != null) { keyValues[key] = range.Column; }
                }
            }
            
            return keyValues;
        }
    }
}