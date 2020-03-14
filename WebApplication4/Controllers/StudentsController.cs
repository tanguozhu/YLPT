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
using System.Reflection;

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
            DataTable dt= GetUser();
            DataTable dt1 = GetVisit();
            DataTable dt2 = Getweijing();
            DataTable dt3 = Getbingli();
            DataTable dt4 = Getweinianmo();
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt1);
            ds.Tables.Add(dt2);
            ds.Tables.Add(dt3);
            ds.Tables.Add(dt4);

            string path= Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/";
            string path1 = "E:\\exceldoc" + "/";
            string xlsName = "medicalsheets";


            Hashtable ht = new Hashtable();
            ht.Add("id", "id");
            ht.Add("name", "姓名");
            ht.Add("password", "密码");
            ht.Add("phone", "手机号");
            ht.Add("sex", "性别");
            ht.Add("identinum", "身份证号");
            ht.Add("scannum", "筛查号");
          
            Hashtable ht1 = new Hashtable();
            ht1.Add("id", "id");
            ht1.Add("visitId", "就诊");
            ht1.Add("date", "日期");
            ht1.Add("scannum", "筛查号");
            ht1.Add("identinum", "身份证号");

            Hashtable ht2 = new Hashtable();
            ht2.Add("id", "id");
            ht2.Add("visit", "就诊");
            ht2.Add("scannum", "筛查号");
            ht2.Add("checkname", "checkname");
            ht2.Add("checktime", "检查时间");
            ht2.Add("checknum", "检查号");
            ht2.Add("conclusion", "胃镜结论");
            ht2.Add("pathologynum", "病理号");
            ht2.Add("other", "其他");
            ht2.Add("isfollowup", "是否需要复查");
            ht2.Add("followuptime", "复查时间");

            string[] titlelist = new string[10];
            titlelist[0] = "用户表";
            titlelist[1] = "就诊表";
            titlelist[2] = "胃镜表";
            titlelist[3] = "病理表";
            titlelist[4] = "胃粘膜表";

            //string sheetName1 = "sheet1";
            ExportDataToExcel(ds,path1,ht,ht1,ht2,titlelist,xlsName);
           // ExportDataToExcel(dt, path1, ht, strTitle, sheetName1, xlsName);
            return "成功调用";
        }

        private DataTable Getbingli()
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select id,pathologynum,part,chronic,acute,atrophy,intestinal,dysplasia,lymphoid," +
                "pitepithelial,Mucosal from pathology ";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        private DataTable Getweinianmo()
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select gastroscopy.id,gastroscopy.visit,gastroscopy.scannum,gastroscopy.checkname," +
                "gastroscopy.checknum,gastroscopy.isfollowup,gastroscopy.followuptime," +
                "gasweinianmo.checks,gasweinianmo.checktime,gasweinianmo.result from gastroscopy,gasweinianmo " +
                "where gastroscopy.id=gasweinianmo.gasid";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        private DataTable Getweijing()
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select id,visit,scannum,checkname,checknum,conclusion,pathologynum,pathologyconclusion," +
                "other,isfollowup,followuptime from gastroscopy where checkname='" + "胃镜检查"+"'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        private DataTable GetUser()
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select id,name,password,phone,identinum,sex,scannum from user where kind=0";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }

        private DataTable GetVisit()
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from visits";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        private DataTable GetSurvey()
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
        public void ExportDataToExcel(DataSet ds,string xlsFileDir, Hashtable nameList1, Hashtable nameList2, 
            Hashtable nameList3,string[] titlelist,  string xlsName)
        {

            
            Microsoft.Office.Interop.Excel.ApplicationClass excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excel.Visible = true;
            Workbooks workbooks = excel.Workbooks;
            Workbook workbook = workbooks.Add(true);
            workbook.Sheets.Add(Missing.Value, workbook.Sheets[1], ds.Tables.Count - 1, Missing.Value);

            for (int d = 0; d < ds.Tables.Count; d++)
            {
                Worksheet ws = workbook.Worksheets[d + 1] as Worksheet;
                ws.Name = titlelist[d];
                Hashtable nameList = new Hashtable();
                if (d==0)
                {
                    nameList = nameList1;
                }
                if (d==1)
                {
                    nameList = nameList2;
                }
                if (d == 2)
                {
                    nameList = nameList3;
                }
                int titleRowsCount = 0;
                if (titlelist[d] != null && titlelist[d].Trim() != "")
                {
                    titleRowsCount = 1;
                    excel.get_Range(excel.Cells[1, 1], excel.Cells[1, ds.Tables[d].Columns.Count]).Font.Bold = true;
                    excel.get_Range(excel.Cells[1, 1], excel.Cells[1, ds.Tables[d].Columns.Count]).Font.Size = 16;
                    excel.get_Range(excel.Cells[1, 1], excel.Cells[1, ds.Tables[d].Columns.Count]).MergeCells = true;
                    ws.Cells[1, 1] = titlelist[d];
                }
                if (!System.IO.Directory.Exists(xlsFileDir))
                {
                    System.IO.Directory.CreateDirectory(xlsFileDir);
                }
                string strFileName = xlsName + ".xls";

                string tempColumnName = "";

                for (int i = 0; i < ds.Tables[d].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[d].Columns.Count; j++)
                    {
                        if (i == 0)
                        {
                            tempColumnName = ds.Tables[d].Columns[j].ColumnName.Trim();
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
                            ws.Cells[titleRowsCount + 1, j + 1] = tempColumnName;
                        }
                        ws.Cells[i + titleRowsCount + 2, j + 1] = ds.Tables[d].Rows[i][j].ToString();
                    }
                }
                excel.get_Range(excel.Cells[titleRowsCount + 1, 1], excel.Cells[titleRowsCount + 1, ds.Tables[d].Columns.Count]).Font.Bold = true;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[titleRowsCount + 1 + ds.Tables[d].Rows.Count, ds.Tables[d].Columns.Count]).HorizontalAlignment = XlVAlign.xlVAlignCenter;
                excel.get_Range(excel.Cells[1, 1], excel.Cells[titleRowsCount + 1 + ds.Tables[d].Rows.Count, ds.Tables[d].Columns.Count]).EntireColumn.AutoFit();
            }







            //Microsoft.Office.Interop.Excel.Workbooks workBooks = excel.Workbooks;
            //Microsoft.Office.Interop.Excel.Workbook workBook = workBooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

            //Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[1];






            //int titleRowsCount = 0;
            //if (strTitle != null && strTitle.Trim() != "")
            //{
            //    titleRowsCount = 1;
            //    excel.get_Range(excel.Cells[1, 1], excel.Cells[1, dt.Columns.Count]).Font.Bold = true;
            //    excel.get_Range(excel.Cells[1, 1], excel.Cells[1, dt.Columns.Count]).Font.Size = 16;
            //    excel.get_Range(excel.Cells[1, 1], excel.Cells[1, dt.Columns.Count]).MergeCells = true;
            //    workSheet.Cells[1, 1] = strTitle;
            //}
            //if (!System.IO.Directory.Exists(xlsFileDir))
            //{
            //    System.IO.Directory.CreateDirectory(xlsFileDir);
            //}
            //string strFileName = xlsName + ".xls";

            //string tempColumnName = "";

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dt.Columns.Count; j++)
            //    {
            //        if (i == 0)
            //        {
            //            tempColumnName = dt.Columns[j].ColumnName.Trim();
            //            if (nameList != null)
            //            {
            //                IDictionaryEnumerator Enum = nameList.GetEnumerator();
            //                while (Enum.MoveNext())
            //                {
            //                    if (Enum.Key.ToString().Trim() == tempColumnName)
            //                    {
            //                        tempColumnName = Enum.Value.ToString();
            //                    }
            //                }
            //            }
            //            workSheet.Cells[titleRowsCount + 1, j + 1] = tempColumnName;
            //        }
            //        workSheet.Cells[i + titleRowsCount + 2, j + 1] = dt.Rows[i][j].ToString();
            //    }
            //}
            //excel.get_Range(excel.Cells[titleRowsCount + 1, 1], excel.Cells[titleRowsCount + 1, dt.Columns.Count]).Font.Bold = true;
            //excel.get_Range(excel.Cells[1, 1], excel.Cells[titleRowsCount + 1 + dt.Rows.Count, dt.Columns.Count]).HorizontalAlignment = XlVAlign.xlVAlignCenter;
            //excel.get_Range(excel.Cells[1, 1], excel.Cells[titleRowsCount + 1 + dt.Rows.Count, dt.Columns.Count]).EntireColumn.AutoFit();

            workbook.Saved = true;
            workbook.SaveCopyAs(xlsFileDir + xlsName + ".xls");
            for (int d = 0; d < ds.Tables.Count; d++)
            {
                Worksheet ws = workbook.Worksheets[d + 1] as Worksheet;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                ws = null;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            workbook = null;
            workbooks.Close();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
            workbooks = null;
            excel.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            excel = null;

            //((Worksheet)workbook.Sheets[1]).Select(true);//选中第一个worksheet
            //workbook.Close(true, Missing.Value, Missing.Value);//关闭workbook, 并保存对workbook的所有修改

            //System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            //workbook = null;
            //excel.Quit();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            //excel = null;
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
      


            FileInfo fi = new FileInfo(xlsFileDir + xlsName + ".xls");
           
            Response.Clear();
            Response.Buffer = true; //设置<span style="color: rgb(51, 51, 51); line-height: 24px; white-space: pre-wrap;">输出页面是否被缓冲</span>
            Response.Charset = "GB2312"; //设置了类型为中文防止乱码的出现 
            Response.AppendHeader("Content-Disposition", String.Format("attachment;filename={0}", HttpUtility.UrlEncode(xlsName + ".xls"))); //定义输出文件和文件名 
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
//if (ds.Tables.Count == 2)
//{
//    int a = d + 1;
//    ws.Name = "mysheet"+a;
//}

//     int totalCellsCount = ds.Tables[d].Columns.Count;
//    Range range = ws.Range[ws.Cells[1, 1], ws.Cells[1, ds.Tables[d].Columns.Count]];
//    //生成列
//    for (int i = 0; i < ds.Tables[d].Columns.Count; i++)
//    {


//        range.Cells[1, i + 1] = ds.Tables[d].Columns[i].ColumnName;
//        range.Interior.ColorIndex = 15;
//        range.Font.Bold = true;
//        range.Font.Size = 10;
//        range.EntireColumn.AutoFit();
//        range.EntireRow.AutoFit();
//    }
////插入数据
//for (int r = 0; r < ds.Tables[d].Rows.Count; r++)
//{
//    for (int i = 0; i < ds.Tables[d].Columns.Count; i++)
//    {
//        ws.Cells[r + 2, i + 1] = "'" + ds.Tables[d].Rows[r][i].ToString();
//    }
//}
//System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
//range = null;
//System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
//ws = null;