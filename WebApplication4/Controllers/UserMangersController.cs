using MySql.Data.MySqlClient;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;


namespace WebApplication4.Controllers
{
    public class UserMangersController : Controller
    {
        private UserMangerDbContext db = new UserMangerDbContext();
        
        // GET: UserMangers
        public ActionResult Index(int page = 1)
        {
            List<UserManger> listUser = new List<UserManger>();
            MySqlConnection mysql = getMySqlConnection();
            
            MySqlCommand mySqlCommand = getSqlCommand("select * from user where kind=0", mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            var i = 0;
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        UserManger user = new UserManger();
                        
                        user.Id = reader.GetInt32("id");
                        user.Name = reader.GetString("name");

                        user.scannum = reader.GetString("scannum");
                        user.Password = reader.GetString("password");
                        user.Phone = reader.GetString("phone");
                        user.Identinum = reader.GetString("identinum");
                        user.pnickname = reader.GetString("pnickname");
                        user.Image = reader.GetString("Image");

                        user.Isnew = reader.GetString("isnew");
                        listUser.Add(user);
                        i = i + 1;
                    }
                }
            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                mysql.Close();
            }
            const int pagesize = 8;
            var data = listUser
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);
            
            return View(data);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Identinum, string scannum, int page = 1)
        {
            List<UserManger> listUser = new List<UserManger>();
            MySqlConnection mysql = getMySqlConnection();
            string con;
            if (Identinum == "" && scannum == "")
            {
                con = "select * from user where kind=0";
            }
            else if (Identinum != "" && scannum != "")
            {
                con = "select * from user where kind=0 and (identinum " +
                    "like" + "'%" + Identinum + "%'" + "and scannum like " + "'%" + scannum + "%')";
            }
            else if (Identinum != "" && scannum == "")
            {
                con = "select * from user where kind=0 and identinum " +
                   "like" + "'%" + Identinum + "%'" ;
            }
            else 
            {
                con = "select * from user where kind=0 and " +
                     " scannum like " + "'%" + scannum + "%'";
            }
            MySqlCommand mySqlCommand = getSqlCommand(con, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            var i = 0;
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        UserManger user = new UserManger();

                        user.Id = reader.GetInt32("id");
                        user.Name = reader.GetString("name");
                        user.pnickname = reader.GetString("pnickname");
                        user.scannum = reader.GetString("scannum");
                        user.Password = reader.GetString("password");
                        user.Phone = reader.GetString("phone");
                        user.Identinum = reader.GetString("identinum");
                        
                        user.Image = reader.GetString("Image");

                        user.Isnew = reader.GetString("isnew");
                        listUser.Add(user);
                        i = i + 1;
                    }
                }
            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                mysql.Close();
            }
            const int pagesize = 8;
            var data = listUser
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);

            return View(data);


        }

        // GET: UserMangers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserManger userManger = new UserManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from user where kind=0 and id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {

                        userManger.Id = reader.GetInt32("id");
                        userManger.Name = reader.GetString("name");
                        userManger.pnickname = reader.GetString("pnickname");
                        userManger.scannum = reader.GetString("scannum");
                        userManger.Password = reader.GetString("password");
                        userManger.Phone = reader.GetString("phone");
                        userManger.Identinum = reader.GetString("identinum");
                       
                        userManger.Image = reader.GetString("Image");

                        userManger.Isnew = reader.GetString("isnew");
                        


                       

                        userManger.isverifyId = GetDiseaseId(userManger.Phone);
                    }
                }
                reader.Close();
            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                reader.Close();
            }
            if (userManger == null)
            {
                return HttpNotFound();
            }
            return View(userManger);
        }

        // GET: UserMangers/Create
        public ActionResult Create()
        {
            ViewBag.SexList = GetSexList();
            UserManger userManger = new UserManger();
            return View(userManger);
        }

        // POST: UserMangers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( UserManger userManger)
        {
            string result = "";
            HttpPostedFileBase files = Request.Files["filename"];
            //docManger.File = Request.Files["filename"];
            string fileName = files.FileName;

            string fileFormat = fileName.Split('.')[fileName.Split('.').Length - 1]; // 以“.”截取，获取“.”后面的文件后缀
            Regex imageFormat = new Regex(@"^(bmp)|(png)|(gif)|(jpg)|(jpeg)"); // 验证文件后缀的表达式（这段可以限制上传文件类型）
            Console.WriteLine(Server.MapPath("~/"));

            if (string.IsNullOrEmpty(fileName) || !imageFormat.IsMatch(fileFormat)) // 验证后缀，判断文件是否是所要上传的格式
            {
                result = "error";
            }
            else
            {
                string timeStamp = DateTime.Now.Ticks.ToString(); // 获取当前时间的string类型
                string firstFileName = timeStamp.Substring(0, timeStamp.Length - 4); // 通过截取获得文件名
                string imageStr = "pic/"; // 获取保存附件的项目文件夹
                string uploadPath = Server.MapPath("~/" + imageStr); // 将项目路径与文件夹合并
                string pictureFormat = fileName.Split('.')[fileName.Split('.').Length - 1];// 设置文件格式
                string fileNames = firstFileName + "." + fileFormat;// 设置完整（文件名+文件格式） 
                string saveFile = uploadPath + fileNames;//文件路径
                files.SaveAs(saveFile);// 保存文件
                                       // 如果单单是上传，不用保存路径的话，下面这行代码就不需要写了！
                result = "http://localhost:53028/" + imageStr + fileNames;// 设置数据库保存的路径

                userManger.Image = result;
            }
            
            userManger.Kind = "0";
            userManger.Isnew = "0";
            if (ModelState.IsValid)
            {
                string aa = Request.Form["checkidentinum"];
                userManger.checkidentinum = Convert.ToInt32(aa);
               

                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand("INSERT INTO user(name,pnickname,identinum,scannum,phone,password,Image,kind,isnew)VALUES" + "(" +
                "\"" + userManger.Name + "\"" + "," + "\"" + userManger.pnickname + "\"" + "," + "\"" + userManger.Identinum + "\"" + "," + "\"" + userManger.scannum + "\"" + ","+ "\"" + userManger.Phone + "\"" + "," +
                "\"" + userManger.Password + "\"" + ","  + "\"" + userManger.Image + "\"" + "," + "\"" + userManger.Kind + "\"" + "," + "\"" + userManger.Isnew + "\"" + ")", mysql);
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
            ViewBag.SexList = GetSexList();
            return View(userManger);
        }

        // GET: UserMangers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserManger userManger = new UserManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from user where kind=0 and id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {

                        userManger.Id = reader.GetInt32("id");
                        userManger.Name = reader.GetString("name");
                        userManger.scannum = reader.GetString("scannum");
                        userManger.Password = reader.GetString("password");
                        userManger.pnickname = reader.GetString("pnickname");
                        userManger.Phone = reader.GetString("phone");
                        userManger.Identinum = reader.GetString("identinum");
                        userManger.Image = reader.GetString("Image");
                        userManger.Kind = reader.GetString("kind");
                        userManger.Isnew = reader.GetString("isnew");

                    }
                }
               
            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                reader.Close();
            }
            if (userManger == null)
            {
                return HttpNotFound();
            }
            ViewBag.SexList = GetSexList();
            return View(userManger);
        }

        // POST: UserMangers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( UserManger userManger)
        {
            string result = "";
            string getimage = userManger.Image;
            HttpPostedFileBase files = Request.Files["filename"];
            string fileName = files.FileName;
            //Console.WriteLine(fileName);
            if (fileName == "")
            {
                userManger.Image = getimage;
            }
            else
            {
                string fileFormat = fileName.Split('.')[fileName.Split('.').Length - 1]; // 以“.”截取，获取“.”后面的文件后缀
                Regex imageFormat = new Regex(@"^(bmp)|(png)|(gif)|(jpg)|(jpeg)"); // 验证文件后缀的表达式（这段可以限制上传文件类型）
                Console.WriteLine(Server.MapPath("~/"));

                if (string.IsNullOrEmpty(fileName) || !imageFormat.IsMatch(fileFormat)) // 验证后缀，判断文件是否是所要上传的格式
                {
                    result = "error";
                }
                else
                {
                    string timeStamp = DateTime.Now.Ticks.ToString(); // 获取当前时间的string类型
                    string firstFileName = timeStamp.Substring(0, timeStamp.Length - 4); // 通过截取获得文件名
                    string imageStr = "pic/"; // 获取保存附件的项目文件夹
                    string uploadPath = Server.MapPath("~/" + imageStr); // 将项目路径与文件夹合并
                    string pictureFormat = fileName.Split('.')[fileName.Split('.').Length - 1];// 设置文件格式
                    string fileNames = firstFileName + "." + fileFormat;// 设置完整（文件名+文件格式） 
                    string saveFile = uploadPath + fileNames;//文件路径
                    files.SaveAs(saveFile);// 保存文件
                                           // 如果单单是上传，不用保存路径的话，下面这行代码就不需要写了！
                    result = "http://localhost:53028/" + imageStr + fileNames;// 设置数据库保存的路径

                    userManger.Image = result;
                }
            }
            
            if (ModelState.IsValid)
            {
                
               
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand(" UPDATE user set name=" + 
                 "'" + userManger.Name + "'" + ",identinum=" + "\"" + userManger.Identinum + "\"" + ",phone=" + "\"" + userManger.Phone + "\"" + ",password=" +
                "\"" + userManger.Password + "\"" + ",pnickname=" + "\"" + userManger.pnickname + "\""+
                 ",Image=" + "\"" + userManger.Image + "\""  +"where id="+userManger.Id, mysql);
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
            ViewBag.SexList = GetSexList();
            return View(userManger);
        }

        // GET: UserMangers/DiseaseEdit/5
        public ActionResult DiseaseEdit(int? diseaseId)
        {
            UserManger userManger = new UserManger();
            if (diseaseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            try
            {
                
                MySqlConnection mysql = getMySqlConnection();
                string sql;
                sql = "select * from disease where id=" + diseaseId;
                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                DataSet ds = new DataSet();
                command.Fill(ds);
                
            }
            catch
            {
                return HttpNotFound();
            }
            return View(userManger);
        }


        // POST: UserMangers/DiseaseEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DiseaseEdit(UserManger userManger)
        //{
        //    int diseaseID = Convert.ToInt32(userManger.Disease.Rows[0][0]);
        //    int disease = Convert.ToInt32(userManger.Disease.Rows[0][2]);
        //    int cardiovascular= Convert.ToInt32(userManger.Disease.Rows[0][3]);
        //    int breathing = Convert.ToInt32(userManger.Disease.Rows[0][4]);
        //    int URM = Convert.ToInt32(userManger.Disease.Rows[0][5]);
        //    int SportsOthers = Convert.ToInt32(userManger.Disease.Rows[0][6]);
        //    string specific = Convert.ToString(userManger.Disease.Rows[0][7]);
        //    int gastric = Convert.ToInt32(userManger.Disease.Rows[0][8]);
        //    string specificgastric = Convert.ToString(userManger.Disease.Rows[0][9]);
        //    int allergy = Convert.ToInt32(userManger.Disease.Rows[0][10]);
        //    string specificallergy = Convert.ToString(userManger.Disease.Rows[0][11]);
        //    int Hptherapy = Convert.ToInt32(userManger.Disease.Rows[0][12]);
        //    string specificHptherapy = Convert.ToString(userManger.Disease.Rows[0][13]);
        //    if (ModelState.IsValid)
        //    {

        //        MySqlConnection mysql = getMySqlConnection();
        //        MySqlCommand mySqlCommand = getSqlCommand(" UPDATE disease set disease=" +
        //          + disease + ",cardiovascular=" + cardiovascular + ",breathing=" + breathing + ",URM=" +
        //        URM + ",SportsOthers=" + SportsOthers + ",specific=" +"'"+ specific + "'"+ ",gastric=" +
        //        gastric + ",specificgastric=" + "'" + specificgastric + "'" + ",allergy=" + allergy + ",specificallergy=" + "'" + specificallergy + "'" + ",Hptherapy=" +
        //        Hptherapy + ",specificHptherapy=" + "'" + specificHptherapy + "where id=" + diseaseID, mysql);
        //        mysql.Open();
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
        //        mySqlCommand.ExecuteNonQuery();
        //        mysql.Close();
        //        return RedirectToAction("Index");
        //    }
            
        //    return View(userManger.Disease);
        //}



        // GET: UserMangers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserManger userManger = new UserManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from user where kind=0 and id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {

                        userManger.Id = reader.GetInt32("id");
                        userManger.Name = reader.GetString("name");
                        userManger.pnickname = reader.GetString("pnickname");
                        userManger.Password = reader.GetString("password");
                        userManger.Phone = reader.GetString("phone");
                        userManger.Identinum = reader.GetString("identinum");
                        userManger.scannum = reader.GetString("scannum");
                        userManger.Image = reader.GetString("Image");

                        userManger.Isnew = reader.GetString("isnew");

                    }
                }
                
            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                reader.Close();
            }
            if (userManger == null)
            {
                return HttpNotFound();
            }
            return View(userManger);
        }

        // POST: UserMangers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocManger docManger = new DocManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("DELETE FROM user where id=" + id, mysql);
            mysql.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public static DataSet GetDisease(string phone)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from disease where phone=" + phone;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataSet ds = new DataSet();
            command.Fill(ds);
            return ds;
        }

        public static DataSet GetPreviouseradication(string phone)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from previouseradication where phone=" + phone;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataSet ds = new DataSet();
            command.Fill(ds);
            return ds;
        }

        private int GetDiseaseId (string phone)
        {
            int rawId=0;
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select id from disease where phone=" +"'"+ phone+"'", mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        rawId = reader.GetInt32("id");
                       
                    }
                }
                reader.Close();
            }
            catch
            {
                
            }
            return rawId;
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

        private List<SelectListItem> GetSexList()
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
    }
}
