using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace production.二维码.FormTools
{
    public class MyTool
    {
        /// <summary>
        ///     获取指定名称窗体
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        public static Form getFormByName(string formName)
        {
            // 遍历应用程序的所有打开的窗口
            foreach (Form form in Application.OpenForms)
            {
                // 如果找到了指定名称的窗口，返回它
                if (form.Name == formName)
                {
                    return form;
                }
            }
            return null;
        }
    }
}
