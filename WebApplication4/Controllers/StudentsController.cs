using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.Runtime.InteropServices;
using RPTMS.Utility;
using Microsoft.Office.Interop.Excel;
using System.Collections;
using System.IO;
using MySql.Data.MySqlClient;
using System.Configuration;
using DataTable = System.Data.DataTable;
using System.Text;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
       
        // GET: Students
        public ActionResult Index()
        {
            List<Student> liststudents = new List<Student>();
           
            
            return View(liststudents);
        }

        public string Button_Up_Click()
        {
            DataTable dt= GetVisit();
            string path= Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/";
            string path1 = "E:\\exceldoc" + "/";
            Hashtable ht = new Hashtable();
            string strTitle = "就诊表";
            ExportDataToExcel(dt, path1, ht, strTitle);
            return "成功调用";
        }


        private DataTable GetVisit()
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from survey";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }


        // GET: UserMangers/Create
        public ActionResult Create()
        {
            Student student = new Student();
            student.outputexcel = Button_Up_Click();
            Response.Write("<script>alert('"+ student.outputexcel + "')</script>");
            return View(student);
        }

        // POST: UserMangers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            
            if (ModelState.IsValid)
            {
                string sql = "";
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Index");
            }
            else
            {
                List<string> Keys = ModelState.Keys.ToList();
                //获取每一个key对应的ModelStateDictionary
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述输出到控制台
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
           
            return View(student);
        }

        public void exportexcel(System.Data.DataTable dt)
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("id,MailCount");//标题
            foreach (DataRow dr in dt.Rows)
            {//字段名
                sw.WriteLine(dr["Schemeid"].ToString() + "," + dr["MailCount"].ToString());
            }
            sw.Close();
            string name = DateTime.Now.ToString() + ".csv";//以当前时间为excel表命名
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(name));
            Response.ContentType = "vnd.ms-excel.numberformat:yyyy-MM-dd ";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.Write(sw);
            Response.End();
        }
        /// <summary>
        /// 将DataTable的数据导出到Excel中。
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="xlsFileDir">导出的Excel文件存放目录（绝对路径，最后带“\”）</param>
        /// <param name="nameList">DataTable中列名的中文对应表</param>
        /// <param name="strTitle">Excel表的标题</param>
        /// <returns>Excel文件名</returns>
        public void ExportDataToExcel(System.Data.DataTable dt, string xlsFileDir, Hashtable nameList, string strTitle)
        {
            
           

            Microsoft.Office.Interop.Excel.ApplicationClass excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbooks workBooks = excel.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workBook = workBooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[1];

            int titleRowsCount = 0;
            if (strTitle != null && strTitle.Trim() != "")
            {
                titleRowsCount = 1;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, dt.Columns.Count]).Font.Bold = true;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, dt.Columns.Count]).Font.Size = 16;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[1, dt.Columns.Count]).MergeCells = true;
                workSheet.Cells[1, 1] = strTitle;
            }
            if (!System.IO.Directory.Exists(xlsFileDir))
            {
                System.IO.Directory.CreateDirectory(xlsFileDir);
            }
            string strFileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";

            string tempColumnName = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (i == 0)
                    {
                        tempColumnName = dt.Columns[j].ColumnName.Trim();
                        if (nameList != null)
                        {
                            IDictionaryEnumerator Enum = nameList.GetEnumerator();
                            while (Enum.MoveNext())
                            {
                                if (Enum.Key.ToString().Trim() == tempColumnName)
                                {
                                    tempColumnName = Enum.Value.ToString();
                                }
                            }
                        }
                        workSheet.Cells[titleRowsCount + 1, j + 1] = tempColumnName;
                    }
                    workSheet.Cells[i + titleRowsCount + 2, j + 1] = dt.Rows[i][j].ToString();
                }
            }
            excel.get_Range(excel.Cells[titleRowsCount + 1, 1], excel.Cells[titleRowsCount + 1, dt.Columns.Count]).Font.Bold = true;
            excel.get_Range(excel.Cells[1, 1], excel.Cells[titleRowsCount + 1 + dt.Rows.Count, dt.Columns.Count]).HorizontalAlignment = XlVAlign.xlVAlignCenter;
            excel.get_Range(excel.Cells[1, 1], excel.Cells[titleRowsCount + 1 + dt.Rows.Count, dt.Columns.Count]).EntireColumn.AutoFit();

            workBook.Saved = true;
            workBook.SaveCopyAs(xlsFileDir + strFileName);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            workSheet = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            workBook = null;
            workBooks.Close();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBooks);
            workBooks = null;
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            excel = null;

            FileInfo fi = new FileInfo(xlsFileDir + strFileName);

            Response.Clear();
            Response.Buffer = true; //设置<span style="color: rgb(51, 51, 51); line-height: 24px; white-space: pre-wrap;">输出页面是否被缓冲</span>
            Response.Charset = "GB2312"; //设置了类型为中文防止乱码的出现 
            Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", HttpUtility.UrlEncode(strFileName))); //定义输出文件和文件名 
            Response.AppendHeader("Content-Length", fi.Length.ToString());
            Response.ContentEncoding = Encoding.Default;
            Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
            Response.WriteFile(fi.FullName);
            Response.Flush();
            Response.End();


            //var stream = System.IO.File.OpenRead(xlsFileDir+strFileName);//excel表转换成流
            //return File(stream, "application/vnd.android.package-archive", Path.GetFileName(xlsFileDir+strFileName));//进行浏览器下载
                                                                                                                     //直接获取字节数组
           

        }

        private List<SelectListItem> GetGenderList()
        {
            return new List<SelectListItem>() {
              new SelectListItem
              {
                     Text = "男",
                     Value = "1"
              },new SelectListItem
              {
                     Text = "女",
                     Value = "0"
              }
       };
        }
        private static MySqlConnection getMySqlConnection()
        {
            MySqlConnection mysql = new MySqlConnection(ConfigurationManager.ConnectionStrings["defalutconnection"].ConnectionString);
            return mysql;
        }
        public static MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);

            return mySqlCommand;
        }

    }
}
