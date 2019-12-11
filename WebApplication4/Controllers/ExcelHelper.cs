using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Text;
using System.Data.OleDb;
using System.Collections;

namespace RPTMS.Utility
{
    public class ExcelHelper
    {
        private Excel._Application excelApp;
        private string fileName = string.Empty;
        private Excel.Workbook wbclass;
        public ExcelHelper(string _filename)
        {
            if (excelApp == null)
                excelApp = new Excel.Application();
            object objOpt = System.Reflection.Missing.Value;
            wbclass = (Excel.Workbook)excelApp.Workbooks.Open(_filename, objOpt, false, objOpt, objOpt, objOpt, true, objOpt, objOpt, true, objOpt, objOpt, objOpt, objOpt, objOpt);
            excelApp.DisplayAlerts = false;  //是否需要显示提示
            excelApp.AlertBeforeOverwriting = false;  //是否弹出提示覆盖 
            excelApp.Visible = false;

        }

        /**/
        /// <summary>
        /// 所有sheet的名称列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetSheetNames()
        {
            List<string> list = new List<string>();
            Excel.Sheets sheets = wbclass.Worksheets;
            string sheetNams = string.Empty;
            foreach (Excel.Worksheet sheet in sheets)
            {
                list.Add(sheet.Name);
            }
            return list;
        }
        public Excel.Worksheet GetWorksheetByName(string name)
        {
            Excel.Worksheet sheet = null;
            Excel.Sheets sheets = wbclass.Worksheets;
            foreach (Excel.Worksheet s in sheets)
            {
                if (s.Name.ToUpper() == name.ToUpper())
                {
                    sheet = s;
                    break;
                }
            }
            return sheet;
        }

        /**/
        /// <summary>
        /// 获取某一列的值
        /// </summary>
        /// <param name="sheetName">sheet名称</param>
        /// <returns></returns>
        public Array GetContent(string sheetName, string selectedCol, int startRow)
        {
            Excel.Worksheet sheet = GetWorksheetByName(sheetName);
            //获取 到 范围的单元格
            Excel.Range rang = sheet.get_Range(selectedCol + startRow, selectedCol + sheet.UsedRange.Rows.Count);
            //读一个单元格内容
            //sheet.get_Range("A1", Type.Missing);
            //不为空的区域,列,行数目
            //   int l = sheet.UsedRange.Columns.Count;
            // int w = sheet.UsedRange.Rows.Count;
            //  object[,] dell = sheet.UsedRange.get_Value(Missing.Value) as object[,];
            System.Array values = (Array)rang.Cells.Value2;
            return values;
        }
        /// <summary>
        /// 获取指定区域的值
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="startRow"></param>
        /// <param name="endCol"></param>
        /// <returns></returns>
        public Array GetAllContent(string sheetName, int startRow, string endCol)
        {
            Excel.Worksheet sheet = GetWorksheetByName(sheetName);
            //获取 到 范围的单元格
            Excel.Range rang = sheet.get_Range("A" + startRow, endCol + sheet.UsedRange.Rows.Count);
            //读一个单元格内容
            //sheet.get_Range("A1", Type.Missing);
            //不为空的区域,列,行数目
            //   int l = sheet.UsedRange.Columns.Count;
            // int w = sheet.UsedRange.Rows.Count;
            //  object[,] dell = sheet.UsedRange.get_Value(Missing.Value) as object[,];
            System.Array values = (Array)rang.Cells.Value2;
            return values;
        }

        public Array GetAllContent(int startRow, string endCol)
        {
            Excel.Worksheet ws = (Excel.Worksheet)excelApp.Workbooks[1].Worksheets[1];
            Excel.Worksheet sheet = GetWorksheetByName(ws.Name);
            //获取 到 范围的单元格
            Excel.Range rang = sheet.get_Range("A" + startRow, endCol + sheet.UsedRange.Rows.Count);
            //读一个单元格内容
            //sheet.get_Range("A1", Type.Missing);
            //不为空的区域,列,行数目
            //   int l = sheet.UsedRange.Columns.Count;
            // int w = sheet.UsedRange.Rows.Count;
            //  object[,] dell = sheet.UsedRange.get_Value(Missing.Value) as object[,];
            System.Array values = (Array)rang.Cells.Value2;
            excelApp.Workbooks.Close();
            excelApp = null;
            return values;


        }

        public Array GetAllContentPlanning(string start, string endCol)
        {
            Excel.Worksheet ws = (Excel.Worksheet)excelApp.Workbooks[1].Worksheets[1];
            Excel.Worksheet sheet = GetWorksheetByName(ws.Name);
            //获取 到 范围的单元格
            Excel.Range rang = sheet.get_Range(start, endCol + sheet.UsedRange.Rows.Count);
            //读一个单元格内容
            //sheet.get_Range("A1", Type.Missing);
            //不为空的区域,列,行数目
            //   int l = sheet.UsedRange.Columns.Count;
            // int w = sheet.UsedRange.Rows.Count;
            //  object[,] dell = sheet.UsedRange.get_Value(Missing.Value) as object[,];
            System.Array values = (Array)rang.Cells.Value2;
            excelApp.Workbooks.Close();
            excelApp = null;
            return values;


        }

        public static string OutputExcel(DataView dv, string fileName, List<string> titleList, List<string> unString)
        {   //   // TODO: 在此处添加构造函数逻辑  
            //                          
            //dv为要输出到Excel的数据，str为标题名称  
            GC.Collect();
            Excel.Application excel;// = new Application();  
            int rowIndex = 1;
            int colIndex = 0;
            int colCount = dv.Table.Columns.Count;
            int rowCount = dv.Table.Rows.Count;
            Excel.Workbook xBk;
            Excel.Worksheet xSt;
            excel = new Excel.Application();
            xBk = excel.Workbooks.Add(true);
            xSt = (Excel.Worksheet)xBk.ActiveSheet;
            //   //取得标题   //   

            excel.DisplayAlerts = false;  //是否需要显示提示
            excel.AlertBeforeOverwriting = false;  //是否弹出提示覆盖 
            excel.Visible = false;

            foreach (DataColumn col in dv.Table.Columns)
            {
                colIndex++;
                if (colIndex > titleList.Count)
                    break;
                excel.Cells[rowIndex, colIndex] = titleList[colIndex - 1];

                //xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //设置标题格式为居中对齐  
            }
            object[,] dataArray = new object[rowCount, colCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (unString == null || !unString.Contains(titleList[j]))
                    {
                        dataArray[i, j] = "'" + dv.Table.Rows[i][j];
                    }
                    else
                    {
                        dataArray[i, j] = dv.Table.Rows[i][j];
                    }
                }
            }

            //xSt.get_Range("A2", xSt.Cells[rowCount + 1, colCount]).Value2 = dataArray;  

            xSt.Range[excel.Cells[2, 1], excel.Cells[rowCount + 1, colCount]].Value2 = dataArray;

            //Excel.Range r2 = xSt.Range[excel.Cells[1, 1], excel.Cells[rowCount + 1, colCount]];
            //r2.Select();
            ////r2.Columns.AutoFit();
            //r2.Borders.LineStyle = XlLineStyle.xlDot;
            //r2.Font.Size = 9;
            ////r2.NumberFormat = "[=0]g;G/通用格式";


            excel.Visible = true;

            xBk.SaveCopyAs(fileName);
            //ds = null;
            xBk.Close(false, null, null);
            excel.Quit();
            KillExcel(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            xSt = null;

            //int generation = System.GC.GetGeneration(excel);
            //GC.Collect(generation);
            excel = null;
            //Thread.Sleep(100);
            return fileName; 

        }


        //输出Excel
        public static string OutputExcel_2(DataView dv, string fileName, List<string> titleList, List<string> unString)
        {   //   // TODO: 在此处添加构造函数逻辑  
            //                          
            //dv为要输出到Excel的数据，str为标题名称  
            GC.Collect();
            Excel.Application excel;// = new Application();  
            int rowIndex = 1;
            int colIndex = 0;
            int colCount = dv.Table.Columns.Count;
            int rowCount = dv.Table.Rows.Count;
            Excel.Workbook xBk;
            Excel.Worksheet xSt;
            excel = new Excel.Application();
            xBk = excel.Workbooks.Add(true);
            xSt = (Excel.Worksheet)xBk.ActiveSheet;
            //   //取得标题   //   

            excel.DisplayAlerts = false;  //是否需要显示提示
            excel.AlertBeforeOverwriting = false;  //是否弹出提示覆盖 
            excel.Visible = false;

            foreach (DataColumn col in dv.Table.Columns)
            {
                colIndex++;
                if (colIndex > titleList.Count)
                    break;
                excel.Cells[rowIndex, colIndex] = titleList[colIndex - 1];

                //xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //设置标题格式为居中对齐  
            }
            object[,] dataArray = new object[rowCount, colCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (unString == null || !unString.Contains(titleList[j]))
                    {
                        dataArray[i, j] = "'" + dv.Table.Rows[i][j];
                    }
                    else
                    {
                        dataArray[i, j] = dv.Table.Rows[i][j];
                    }
                }
            }

            //xSt.get_Range("A2", xSt.Cells[rowCount + 1, colCount]).Value2 = dataArray;  

            xSt.Range[excel.Cells[2, 1], excel.Cells[rowCount + 1, colCount]].Value2 = dataArray;

            //Excel.Range r2 = xSt.Range[excel.Cells[1, 1], excel.Cells[rowCount + 1, colCount]];
            //r2.Select();
            ////r2.Columns.AutoFit();
            //r2.Borders.LineStyle = XlLineStyle.xlDot;
            //r2.Font.Size = 9;
            ////r2.NumberFormat = "[=0]g;G/通用格式";


            excel.Visible = true;

            xBk.SaveCopyAs(fileName);
            //ds = null;
            xBk.Close(false, null, null);
            excel.Quit();
            KillExcel(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            xSt = null;

            //int generation = System.GC.GetGeneration(excel);
            //GC.Collect(generation);
            excel = null;
            //Thread.Sleep(100);
            return fileName;

        }


        public void Close()
        {
            try
            {
                excelApp.Quit();
            }
            catch { }
            excelApp = null;
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int nPID);

        public static void KillExcel(Application excel)
        {
            IntPtr hWnd = new IntPtr(excel.Hwnd);
            int nPid = 0;
            GetWindowThreadProcessId(hWnd, out nPid);
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(nPid);
            p.Close();
        }





        //带有事务回滚功能的批量插入数据库操作
        //public static void DataTableToSQLServer(System.Data.DataTable dt, string DBTableName, out string msg, string[] dtStrs, string[] dbdtStrs)
        //{
        //    string connectionString = "Data Source=172.18.10.15;Initial Catalog=htpdm;User ID=sa;Password=abilitysql";
        //    msg = "";
        //    using (SqlConnection destinationConnection = new SqlConnection(connectionString))
        //    {
        //        destinationConnection.Open();
        //        SqlTransaction sqlTran = destinationConnection.BeginTransaction();
        //        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection, SqlBulkCopyOptions.Default, sqlTran))
        //        {
        //            try
        //            {
        //                StringBuilder sb = new StringBuilder();
        //                bulkCopy.DestinationTableName = DBTableName;//要插入的表的表名
        //                bulkCopy.BatchSize = 1000;
        //                //sb.Append("CREATE TABLE myTable(myName [nvarchar](50), myAddress [nvarchar](50), myBalance [nvarchar](50))");//myId INTEGER CONSTRAINT PKeyMyId PRIMARY KEY,
        //                if (dtStrs.Length != dbdtStrs.Length)
        //                {
        //                    msg = "传入的excel数据列和数据库表列应当一一对应";
        //                    return;
        //                }
        //                else
        //                {
        //                    for (int i = 0; i < dtStrs.Length; i++)
        //                    {
        //                        bulkCopy.ColumnMappings.Add(dtStrs[i], dbdtStrs[i]);
        //                    }
        //                }
        //                //bulkCopy.ColumnMappings.Add("星期一", "Monday");//映射字段名 DataTable列名 ,数据库 对应的列名
        //                //bulkCopy.ColumnMappings.Add("星期二", "Tuesday");
        //                //bulkCopy.ColumnMappings.Add("星期三", "Wednesday");
        //                //bulkCopy.ColumnMappings.Add("星期四", "Thursday");
        //                //bulkCopy.ColumnMappings.Add("星期五", "Friday");
        //                //bulkCopy.ColumnMappings.Add("星期六", "Saturday");
        //                bulkCopy.WriteToServer(dt);
        //                sqlTran.Commit();//提交数据事务
        //            }
        //            catch (Exception ex)
        //            {
        //                sqlTran.Rollback();//数据回滚
        //                msg = ex.Message;
        //            }
        //        }
        //    }
        //}







        //预投导出Excel
        public static string OutputExcel_Feedin(DataView dv, string fileName)
        {     //   // TODO: 在此处添加构造函数逻辑  
            //                          
            //dv为要输出到Excel的数据，str为标题名称  
            GC.Collect();
            Excel.Application excel;// = new Application();  
            int rowIndex = 1;
            int colIndex = 0;
            int colCount = dv.Table.Columns.Count - 3;
            int rowCount = dv.Table.Rows.Count;
            Excel.Workbook xBk;
            Excel.Worksheet xSt;
            excel = new Excel.Application();
            xBk = excel.Workbooks.Add(true);
            xSt = (Excel.Worksheet)xBk.ActiveSheet;
            //   //取得标题   //   

            foreach (DataColumn col in dv.Table.Columns)
            {
                colIndex++;
                if (colIndex <= colCount)
                {
                    xSt.Cells[rowIndex, colIndex] = col.ColumnName;
                }
                //xSt.get_Range(xSt.Cells[rowIndex, colIndex], xSt.Cells[rowIndex, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //设置标题格式为居中对齐  
            }

            Excel.Range r = xSt.Range[xSt.Cells[rowIndex, 1], xSt.Cells[rowIndex, colCount]];
            r.Select();
            r.Interior.ColorIndex = 48;



            //取得表格中的数据   //   
            //rowIndex = 2; 

            string[] arr_decim = new[] { "Qty_Lack", "Qty_PreFeed", "Qty_Feeded", "Qty_Purchase", "Qty_System", "Qty_Warehouse", "Qty_Redundant", "PriceInTax", "PriceInTaxBudget", "Amount" };
            ArrayList ar = new ArrayList();//实例化一个ArrayList
            ar.AddRange(arr_decim);//把数组赋到Arraylist对象

            object[,] dataArray = new object[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {

                for (int j = 0; j < colCount; j++)
                {
                    if (dv.Table.Columns[j].ColumnName == "type")
                    {
                        if (dv.Table.Rows[i][j].ToString() == "1")
                        {
                            dataArray[i, j] = "国产";
                        }
                        else if (dv.Table.Rows[i][j].ToString() == "2")
                        {
                            dataArray[i, j] = "进口";
                        }
                    }
                    else if (dv.Table.Columns[j].ColumnName == "IsStorage")
                    {
                        if (dv.Table.Rows[i][j].ToString() == "0")
                        {
                            dataArray[i, j] = "";
                        }
                        else if (dv.Table.Rows[i][j].ToString() == "1")
                        {
                            dataArray[i, j] = "已入库";
                        }
                    }
                    else if (dv.Table.Columns[j].ColumnName == "IsSemiPRD")
                    {
                        if (dv.Table.Rows[i][j].ToString() == "0")
                        {
                            dataArray[i, j] = "";
                        }
                        else if (dv.Table.Rows[i][j].ToString() == "1")
                        {
                            dataArray[i, j] = "是";
                        }
                    }
                    else if (ar.Contains(dv.Table.Columns[j].ColumnName))
                    {
                        dataArray[i, j] = dv.Table.Rows[i][j];
                    }
                    else
                    {
                        dataArray[i, j] = "'" + dv.Table.Rows[i][j];
                    }
                }
            }

            xSt.Range[xSt.Cells[2, 1], xSt.Cells[rowCount + 1, colCount]].Value2 = dataArray;


            Excel.Range r2 = xSt.Range[xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]];
            r2.Select();
            r2.Columns.AutoFit();
            r2.Borders.LineStyle = 1;
            //r2.NumberFormat = "[=0]g;G/通用格式";

            #region Hidden Hints
            Excel.Range A = xSt.get_Range("A1", Type.Missing);
            A.EntireColumn.Hidden = true;

            Excel.Range B = xSt.get_Range("B1", Type.Missing);
            B.EntireColumn.Hidden = true;
            B.Value2 = "pid";

            Excel.Range C = xSt.get_Range("C1", Type.Missing);
            C.EntireColumn.Hidden = true;
            C.Value2 = "pre";

            Excel.Range D = xSt.get_Range("D1", Type.Missing);
            D.Value2 = "款号";

            Excel.Range E = xSt.get_Range("E1", Type.Missing);
            E.Value2 = "物料号";

            Excel.Range F = xSt.get_Range("F1", Type.Missing);
            F.Value2 = "物料名称";

            Excel.Range G = xSt.get_Range("G1", Type.Missing);
            G.EntireColumn.Hidden = true;
            G.Value2 = "sku";

            Excel.Range H = xSt.get_Range("H1", Type.Missing);
            H.Value2 = "颜色";

            Excel.Range I = xSt.get_Range("I1", Type.Missing);
            I.Value2 = "颜色描述";

            Excel.Range J = xSt.get_Range("J1", Type.Missing);
            J.Value2 = "尺码";

            Excel.Range K = xSt.get_Range("K1", Type.Missing);
            K.Value2 = "系统总数量";

            Excel.Range L = xSt.get_Range("L1", Type.Missing);
            L.Value2 = "预投分配数量";

            Excel.Range M = xSt.get_Range("M1", Type.Missing);
            M.Value2 = "缺少数量";

            Excel.Range N = xSt.get_Range("N1", Type.Missing);
            N.Value2 = "投料数量";

            Excel.Range O = xSt.get_Range("O1", Type.Missing);
            O.Value2 = "采购数量";



            Excel.Range P = xSt.get_Range("P1", Type.Missing);
            P.Value2 = "库存数量";
            P.ColumnWidth = 12;

            Excel.Range Q = xSt.get_Range("Q1", Type.Missing);
            Q.Value2 = "剩余数量";

            Excel.Range R = xSt.get_Range("R1", Type.Missing);
            R.Value2 = "含税单价";

            Excel.Range S = xSt.get_Range("S1", Type.Missing);
            S.Value2 = "预算含税价格";

            Excel.Range T = xSt.get_Range("T1", Type.Missing);
            T.Value2 = "总金额";

            Excel.Range U = xSt.get_Range("U1", Type.Missing);
            U.Value2 = "价格单位";

            Excel.Range V = xSt.get_Range("V1", Type.Missing);
            V.Value2 = "是否入库";


            Excel.Range W = xSt.get_Range("W1", Type.Missing);
            W.Value2 = "下单日期";

            Excel.Range X = xSt.get_Range("X1", Type.Missing);
            X.Value2 = "采购反馈交期";

            Excel.Range Y = xSt.get_Range("Y1", Type.Missing);
            Y.Value2 = "发货日期";

            Excel.Range Z = xSt.get_Range("Z1", Type.Missing);
            Z.Value2 = "到厂日期";

            Excel.Range AA = xSt.get_Range("AA1", Type.Missing);
            AA.Value2 = "发料计划";

            Excel.Range AB = xSt.get_Range("AB1", Type.Missing);
            AB.Value2 = "MRP下发日期";

            Excel.Range AC = xSt.get_Range("AC1", Type.Missing);
            AC.Value2 = "采购员";

            Excel.Range AD = xSt.get_Range("AD1", Type.Missing);
            AD.Value2 = "业务员";

            Excel.Range AE = xSt.get_Range("AE1", Type.Missing);
            AE.Value2 = "类型";

            Excel.Range AF = xSt.get_Range("AF1", Type.Missing);
            AF.Value2 = "供应商";

            Excel.Range AG = xSt.get_Range("AG1", Type.Missing);
            AG.Value2 = "半成品";

            Excel.Range AH = xSt.get_Range("AH1", Type.Missing);
            AH.Value2 = "半成品号";


            #endregion

            excel.Visible = true;



            //设定允许操作的单元格
            Microsoft.Office.Interop.Excel.AllowEditRanges ranges = xSt.Protection.AllowEditRanges;
            //ranges.Add("Editable", xSt.get_Range("P:P"), Type.Missing);
            //ranges.Add("Editable2", xSt.get_Range("Q:Q"), Type.Missing);
            //ranges.Add("Editable3", xSt.get_Range("Z:Z"), Type.Missing);
            //保护工作表

            //xSt.Protect("able", Type.Missing, Type.Missing, Type.Missing,
            //  Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            // Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            //  Type.Missing, true, Type.Missing, Type.Missing);



            xBk.SaveCopyAs(fileName);

            //ds = null;
            xBk.Close(false, null, null);
            excel.Quit();
            KillExcel(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
            GC.Collect();

            return fileName;
        }



        public static string OutputExcel_SO(DataView dv, string fileName, List<string> cList)
        {   //   // TODO: 在此处添加构造函数逻辑  
            //                          
            //dv为要输出到Excel的数据，str为标题名称  
            GC.Collect();
            Excel.Application excel;// = new Application();  
            int rowIndex = 1;
            int colIndex = 0;
            int colCount = dv.Table.Columns.Count;
            int rowCount = dv.Table.Rows.Count;
            Excel.Workbook xBk;
            Excel.Worksheet xSt;
            excel = new Excel.Application();
            xBk = excel.Workbooks.Add(true);
            xSt = (Excel.Worksheet)xBk.ActiveSheet;
            //   //取得标题   //   

            foreach (DataColumn col in dv.Table.Columns)
            {
                colIndex++;
                xSt.Cells[rowIndex, colIndex] = col.ColumnName;

                //xSt.get_Range(xSt.Cells[rowIndex, colIndex], xSt.Cells[rowIndex, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //设置标题格式为居中对齐  
            }

            Excel.Range r = xSt.Range[xSt.Cells[rowIndex, 1], xSt.Cells[rowIndex, colCount]];
            r.Select();
            r.Interior.ColorIndex = 48;

            //取得表格中的数据   //   
            //rowIndex = 2; 

            object[,] dataArray = new object[rowCount, colCount];
            var lastDev = string.Empty;
            for (int i = 0; i < rowCount; i++)
            {
                if (i == 0)
                {
                    dv.Table.Rows[i]["订单标识符"] = 0;
                    dv.Table.Rows[i]["行项目标识符"] = 0;
                    lastDev = dv.Table.Rows[i]["交货期"].ToString();
                }
                else
                {

                    dv.Table.Rows[i]["订单标识符"] = 1;
                    if (dv.Table.Rows[i]["物料编号"].Equals(dv.Table.Rows[i - 1]["物料编号"]))
                    {
                        dv.Table.Rows[i]["行项目标识符"] = 1;
                        dv.Table.Rows[i]["交货期"] = lastDev;
                        dv.Table.Rows[i]["交货期2"] = lastDev;
                    }
                    else
                    {
                        dv.Table.Rows[i]["行项目标识符"] = 0;
                        lastDev = dv.Table.Rows[i]["交货期"].ToString();
                    }

                }
                for (int j = 0; j < colCount; j++)
                {
                    if (dv.Table.Columns[j].DataType == System.Type.GetType("System.String") && dv.Table.Columns[j].ColumnName != "价格")
                    {
                        dataArray[i, j] = "'" + dv.Table.Rows[i][j];
                    }
                    else
                    {
                        dataArray[i, j] = dv.Table.Rows[i][j];
                    }
                }
            }

            xSt.get_Range("A2", xSt.Cells[rowCount + 1, colCount]).Value2 = dataArray;



            Excel.Range r2 = xSt.Range[xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]];
            r2.Select();
            r2.Columns.AutoFit();
            r2.Borders.LineStyle = 1;
            r2.NumberFormat = "[=0]g;G/通用格式";


            excel.Visible = true;

            xBk.SaveCopyAs(fileName);
            //ds = null;
            xBk.Close(false, null, null);
            excel.Quit();
            KillExcel(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            excel = null;
            xSt = null;
            GC.Collect();
            return fileName;

        }


        public static string OutputExcel_GS(DataView dv, string fileName, List<string> titleList, List<string> unString)
        {   //   // TODO: 在此处添加构造函数逻辑  
            //                          
            //dv为要输出到Excel的数据，str为标题名称  
            GC.Collect();
            Excel.Application excel;// = new Application();  
            int rowIndex = 1;
            int colIndex = 0;
            int colCount = dv.Table.Columns.Count;
            int rowCount = dv.Table.Rows.Count;
            Excel.Workbook xBk;
            Excel.Worksheet xSt;
            excel = new Excel.Application();
            xBk = excel.Workbooks.Add(true);
            xSt = (Excel.Worksheet)xBk.ActiveSheet;
            //   //取得标题   //   

            excel.DisplayAlerts = false;  //是否需要显示提示
            excel.AlertBeforeOverwriting = false;  //是否弹出提示覆盖 
            excel.Visible = false;

            foreach (DataColumn col in dv.Table.Columns)
            {
                colIndex++;
                if (colIndex > titleList.Count)
                    break;
                excel.Cells[rowIndex, colIndex] = titleList[colIndex - 1];

                //xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //设置标题格式为居中对齐  
            }
            object[,] dataArray = new object[rowCount, colCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (unString == null || !unString.Contains(titleList[j]))
                    {
                        dataArray[i, j] = "'" + dv.Table.Rows[i][j];
                    }
                    else
                    {
                        dataArray[i, j] = dv.Table.Rows[i][j];
                    }
                }
            }

            //xSt.get_Range("A2", xSt.Cells[rowCount + 1, colCount]).Value2 = dataArray;  

            xSt.Range[excel.Cells[2, 1], excel.Cells[rowCount + 1, colCount]].Value2 = dataArray;

            //Excel.Range r2 = xSt.Range[xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]];
            //r2.Select();
            //r2.Columns.AutoFit();
            //r2.Borders.LineStyle = 1;

            Excel.Range r3 = xSt.Range[xSt.Cells[1, 1], xSt.Cells[rowCount + 1, colCount]];
            r3.Select();
            r3.Columns.AutoFit();
            r3.Borders.LineStyle = 1;
            r3.NumberFormat = "[=0]g";


            excel.Visible = true;

            xBk.SaveCopyAs(fileName);
            //ds = null;
            xBk.Close(false, null, null);
            excel.Quit();
            KillExcel(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
            xBk = null;
            xSt = null;

            //int generation = System.GC.GetGeneration(excel);
            //GC.Collect(generation);
            excel = null;
            //Thread.Sleep(100);
            return fileName;

        }


    }
}