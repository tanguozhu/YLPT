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
    public class PathologiesController : Controller
    {
        private PathologyDbContext db = new PathologyDbContext();

        // GET: Pathologies
        public ActionResult Index()
        {
            return View(db.Pathologies.ToList());
        }

        // GET: Pathologies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pathology pathology = db.Pathologies.Find(id);
            if (pathology == null)
            {
                return HttpNotFound();
            }
            return View(pathology);
        }

        // GET: Pathologies/Create
        public ActionResult Create(int?id,string num,int refid,string checkname,int detailid)
        {
            Pathology pathology = new Pathology();
            pathology.rawid = refid;
            pathology.detailid = detailid;
            pathology.rawname = checkname;
            pathology.pathologynum = num;
            return View(pathology);
        }

        // POST: Pathologies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Pathology pathology)
        {
            
            if (ModelState.IsValid)
            {
                pathology.pathologynum= Request.Form["pathologynum"];
                string aa = Request.Form["detailid"];
                int bb = Convert.ToInt32(aa);
                if (pathology.parts == null)
                {

                }
                else if (pathology.parts != null)
                {
                    InsertPathlogy(pathology.pathologynum, pathology.parts, pathology.chronic, pathology.acute,
                       pathology.atrophy, pathology.intestinal, pathology.dysplasia, pathology.lymphoid,
                       pathology.pitepithelial, pathology.Mucosal);
                }
               

                if (pathology.parts1 == null)
                {

                }
                else if(pathology.parts1 != null)
                {
                    InsertPathlogy(pathology.pathologynum, pathology.parts1, pathology.chronic1, pathology.acute1,
                        pathology.atrophy1, pathology.intestinal1, pathology.dysplasia1, pathology.lymphoid1,
                        pathology.pitepithelial1, pathology.Mucosal1);
                }

                if (pathology.parts2 == null)
                {

                }
                else if (pathology.parts2 != null)
                {
                    InsertPathlogy(pathology.pathologynum, pathology.parts2, pathology.chronic2, pathology.acute2,
                        pathology.atrophy2, pathology.intestinal2, pathology.dysplasia2, pathology.lymphoid2,
                        pathology.pitepithelial2, pathology.Mucosal2);
                }

                if (pathology.parts3 == null)
                {

                }
                else if (pathology.parts3 != null)
                {
                    InsertPathlogy(pathology.pathologynum, pathology.parts3, pathology.chronic3, pathology.acute3,
                        pathology.atrophy3, pathology.intestinal3, pathology.dysplasia3, pathology.lymphoid3,
                        pathology.pitepithelial3, pathology.Mucosal3);
                }

                if (pathology.parts4 == null)
                {

                }
                else if(pathology.parts4 != null)
                {
                    InsertPathlogy(pathology.pathologynum, pathology.parts4, pathology.chronic4, pathology.acute4,
                        pathology.atrophy4, pathology.intestinal4, pathology.dysplasia4, pathology.lymphoid4,
                        pathology.pitepithelial4, pathology.Mucosal4);
                }
                return RedirectToAction("Details/"+bb, "Visits");
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

            return View(pathology);
        }

        // GET: Pathologies/Edit/5
        public ActionResult Edit(int? id,string num,int detailid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataTable dt = GetId(num);
            Pathology pathology = new Pathology();
            pathology.pathologynum = num;
            pathology.detailid = detailid;
            int flag = dt.Rows.Count;
            if (flag == 1)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    pathology.idd = Convert.ToInt32(dt.Rows[0][0]);
                    DataTable dtContent = GetContent(pathology.idd);
                    pathology.parts = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal = Convert.ToInt32(dtContent.Rows[0][10]);
                }
                
                    
                    pathology.id1 = 0;
                    pathology.id2 = 0;
                    pathology.id3 = 0;
                    pathology.id4 = 0;
              
            }
            if (flag==2)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    pathology.idd = Convert.ToInt32(dt.Rows[0][0]);
                    DataTable dtContent = GetContent(pathology.idd);
                    pathology.parts = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal = Convert.ToInt32(dtContent.Rows[0][10]);
                }
                if (dt.Rows[1][0] != DBNull.Value)
                {
                    pathology.id1 = Convert.ToInt32(dt.Rows[1][0]);
                    DataTable dtContent = GetContent(pathology.id1);
                    pathology.parts1 = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic1 = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute1 = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy1 = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal1 = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia1 = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid1 = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial1 = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal1 = Convert.ToInt32(dtContent.Rows[0][10]);
                }
                pathology.id2 = 0;
                pathology.id3 = 0;
                pathology.id4 = 0;
            }
            if (flag == 3)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    pathology.idd = Convert.ToInt32(dt.Rows[0][0]);
                    DataTable dtContent = GetContent(pathology.idd);
                    pathology.parts = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal = Convert.ToInt32(dtContent.Rows[0][10]);
                }
                if (dt.Rows[1][0] != DBNull.Value)
                {
                    pathology.id1 = Convert.ToInt32(dt.Rows[1][0]);
                    DataTable dtContent = GetContent(pathology.id1);
                    pathology.parts1 = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic1 = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute1 = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy1 = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal1 = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia1 = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid1 = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial1 = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal1 = Convert.ToInt32(dtContent.Rows[0][10]);
                }
                if (dt.Rows[2][0] != DBNull.Value)
                {
                    pathology.id2 = Convert.ToInt32(dt.Rows[2][0]);
                    DataTable dtContent2 = GetContent(pathology.id2);
                    pathology.parts2 = Convert.ToString(dtContent2.Rows[0][2]);
                    pathology.chronic2 = Convert.ToInt32(dtContent2.Rows[0][3]);
                    pathology.acute2 = Convert.ToInt32(dtContent2.Rows[0][4]);
                    pathology.atrophy2 = Convert.ToInt32(dtContent2.Rows[0][5]);
                    pathology.intestinal2 = Convert.ToInt32(dtContent2.Rows[0][6]);
                    pathology.dysplasia2 = Convert.ToInt32(dtContent2.Rows[0][7]);
                    pathology.lymphoid2 = Convert.ToInt32(dtContent2.Rows[0][8]);
                    pathology.pitepithelial2 = Convert.ToInt32(dtContent2.Rows[0][9]);
                    pathology.Mucosal2 = Convert.ToInt32(dtContent2.Rows[0][10]);
                }
                pathology.id3 = 0;
                pathology.id4 = 0;
            }
            if (flag==4)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    pathology.idd = Convert.ToInt32(dt.Rows[0][0]);
                    DataTable dtContent = GetContent(pathology.idd);
                    pathology.parts = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal = Convert.ToInt32(dtContent.Rows[0][10]);
                }
                if (dt.Rows[1][0] != DBNull.Value)
                {
                    pathology.id1 = Convert.ToInt32(dt.Rows[1][0]);
                    DataTable dtContent = GetContent(pathology.id1);
                    pathology.parts1 = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic1 = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute1 = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy1 = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal1 = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia1 = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid1 = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial1 = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal1 = Convert.ToInt32(dtContent.Rows[0][10]);
                }
                if (dt.Rows[2][0] != DBNull.Value)
                {
                    pathology.id2 = Convert.ToInt32(dt.Rows[2][0]);
                    DataTable dtContent2 = GetContent(pathology.id2);
                    pathology.parts2 = Convert.ToString(dtContent2.Rows[0][2]);
                    pathology.chronic2 = Convert.ToInt32(dtContent2.Rows[0][3]);
                    pathology.acute2 = Convert.ToInt32(dtContent2.Rows[0][4]);
                    pathology.atrophy2 = Convert.ToInt32(dtContent2.Rows[0][5]);
                    pathology.intestinal2 = Convert.ToInt32(dtContent2.Rows[0][6]);
                    pathology.dysplasia2 = Convert.ToInt32(dtContent2.Rows[0][7]);
                    pathology.lymphoid2 = Convert.ToInt32(dtContent2.Rows[0][8]);
                    pathology.pitepithelial2 = Convert.ToInt32(dtContent2.Rows[0][9]);
                    pathology.Mucosal2 = Convert.ToInt32(dtContent2.Rows[0][10]);
                }
                if (dt.Rows[3][0] != DBNull.Value)
                {
                    pathology.id3 = Convert.ToInt32(dt.Rows[3][0]);
                    DataTable dtContent = GetContent(pathology.id3);
                    pathology.parts3 = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic3 = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute3 = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy3 = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal3 = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia3 = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid3 = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial3 = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal3 = Convert.ToInt32(dtContent.Rows[0][10]);
                }
                pathology.id4 = 0;
            }
            if (flag == 5)
            {
                if (dt.Rows[0][0] != DBNull.Value)
                {
                    pathology.idd = Convert.ToInt32(dt.Rows[0][0]);
                    DataTable dtContent = GetContent(pathology.idd);
                    pathology.parts = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal = Convert.ToInt32(dtContent.Rows[0][10]);
                }
                if (dt.Rows[1][0] != DBNull.Value)
                {
                    pathology.id1 = Convert.ToInt32(dt.Rows[1][0]);
                    DataTable dtContent = GetContent(pathology.id1);
                    pathology.parts1 = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic1 = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute1 = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy1 = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal1 = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia1 = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid1 = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial1 = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal1 = Convert.ToInt32(dtContent.Rows[0][10]);
                }
                if (dt.Rows[2][0] != DBNull.Value)
                {
                    pathology.id2 = Convert.ToInt32(dt.Rows[2][0]);
                    DataTable dtContent2 = GetContent(pathology.id2);
                    pathology.parts2 = Convert.ToString(dtContent2.Rows[0][2]);
                    pathology.chronic2 = Convert.ToInt32(dtContent2.Rows[0][3]);
                    pathology.acute2 = Convert.ToInt32(dtContent2.Rows[0][4]);
                    pathology.atrophy2 = Convert.ToInt32(dtContent2.Rows[0][5]);
                    pathology.intestinal2 = Convert.ToInt32(dtContent2.Rows[0][6]);
                    pathology.dysplasia2 = Convert.ToInt32(dtContent2.Rows[0][7]);
                    pathology.lymphoid2 = Convert.ToInt32(dtContent2.Rows[0][8]);
                    pathology.pitepithelial2 = Convert.ToInt32(dtContent2.Rows[0][9]);
                    pathology.Mucosal2 = Convert.ToInt32(dtContent2.Rows[0][10]);
                }
                if (dt.Rows[3][0] != DBNull.Value)
                {
                    pathology.id3 = Convert.ToInt32(dt.Rows[3][0]);
                    DataTable dtContent = GetContent(pathology.id3);
                    pathology.parts3 = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic3 = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute3 = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy3 = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal3 = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia3 = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid3 = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial3 = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal3 = Convert.ToInt32(dtContent.Rows[0][10]);
                }
                if (dt.Rows[4][0] != DBNull.Value)
                {
                    pathology.id4 = Convert.ToInt32(dt.Rows[4][0]);
                    DataTable dtContent = GetContent(pathology.id4);
                    pathology.parts4 = Convert.ToString(dtContent.Rows[0][2]);
                    pathology.chronic4 = Convert.ToInt32(dtContent.Rows[0][3]);
                    pathology.acute4 = Convert.ToInt32(dtContent.Rows[0][4]);
                    pathology.atrophy4 = Convert.ToInt32(dtContent.Rows[0][5]);
                    pathology.intestinal4 = Convert.ToInt32(dtContent.Rows[0][6]);
                    pathology.dysplasia4 = Convert.ToInt32(dtContent.Rows[0][7]);
                    pathology.lymphoid4 = Convert.ToInt32(dtContent.Rows[0][8]);
                    pathology.pitepithelial4 = Convert.ToInt32(dtContent.Rows[0][9]);
                    pathology.Mucosal4 = Convert.ToInt32(dtContent.Rows[0][10]);
                }
            }

            //if (dt.Rows[0][0]!=DBNull.Value)
            //{
            //    pathology.idd = Convert.ToInt32(dt.Rows[0][0]);
            //    DataTable dtContent = GetContent(pathology.idd);
            //    pathology.parts = Convert.ToString(dtContent.Rows[0][2]);
            //    pathology.chronic = Convert.ToInt32(dtContent.Rows[0][3]);
            //    pathology.acute = Convert.ToInt32(dtContent.Rows[0][4]);
            //    pathology.atrophy = Convert.ToInt32(dtContent.Rows[0][5]);
            //    pathology.intestinal = Convert.ToInt32(dtContent.Rows[0][6]);
            //    pathology.dysplasia = Convert.ToInt32(dtContent.Rows[0][7]);
            //    pathology.lymphoid = Convert.ToInt32(dtContent.Rows[0][8]);
            //    pathology.pitepithelial = Convert.ToInt32(dtContent.Rows[0][9]);
            //    pathology.Mucosal = Convert.ToInt32(dtContent.Rows[0][10]);
            //}
            //else
            //{
            //    pathology.idd = 0;
            //}

            //if (dt.Rows[1][0] != DBNull.Value)
            //{
            //    pathology.id1 = Convert.ToInt32(dt.Rows[1][0]);
            //    DataTable dtContent = GetContent(pathology.id1);
            //    pathology.parts1 = Convert.ToString(dtContent.Rows[0][2]);
            //    pathology.chronic1 = Convert.ToInt32(dtContent.Rows[0][3]);
            //    pathology.acute1 = Convert.ToInt32(dtContent.Rows[0][4]);
            //    pathology.atrophy1 = Convert.ToInt32(dtContent.Rows[0][5]);
            //    pathology.intestinal1 = Convert.ToInt32(dtContent.Rows[0][6]);
            //    pathology.dysplasia1 = Convert.ToInt32(dtContent.Rows[0][7]);
            //    pathology.lymphoid1 = Convert.ToInt32(dtContent.Rows[0][8]);
            //    pathology.pitepithelial1 = Convert.ToInt32(dtContent.Rows[0][9]);
            //    pathology.Mucosal1 = Convert.ToInt32(dtContent.Rows[0][10]);
            //}
            //else
            //{
            //    pathology.id1 = 0;
            //}

            //if (dt.Rows[2][0] != DBNull.Value)
            //{
            //    pathology.id2 = Convert.ToInt32(dt.Rows[2][0]);
            //    DataTable dtContent2 = GetContent(pathology.id2);
            //    pathology.parts2 = Convert.ToString(dtContent2.Rows[0][2]);
            //    pathology.chronic2 = Convert.ToInt32(dtContent2.Rows[0][3]);
            //    pathology.acute2 = Convert.ToInt32(dtContent2.Rows[0][4]);
            //    pathology.atrophy2 = Convert.ToInt32(dtContent2.Rows[0][5]);
            //    pathology.intestinal2 = Convert.ToInt32(dtContent2.Rows[0][6]);
            //    pathology.dysplasia2 = Convert.ToInt32(dtContent2.Rows[0][7]);
            //    pathology.lymphoid2 = Convert.ToInt32(dtContent2.Rows[0][8]);
            //    pathology.pitepithelial2 = Convert.ToInt32(dtContent2.Rows[0][9]);
            //    pathology.Mucosal2 = Convert.ToInt32(dtContent2.Rows[0][10]);
            //}
            //else
            //{
            //    pathology.id2 = 0;
            //}

            //if (dt.Rows[3][0] != DBNull.Value)
            //{
            //    pathology.id3 = Convert.ToInt32(dt.Rows[3][0]);
            //    DataTable dtContent = GetContent(pathology.id3);
            //    pathology.parts3 = Convert.ToString(dtContent.Rows[0][2]);
            //    pathology.chronic3 = Convert.ToInt32(dtContent.Rows[0][3]);
            //    pathology.acute3 = Convert.ToInt32(dtContent.Rows[0][4]);
            //    pathology.atrophy3 = Convert.ToInt32(dtContent.Rows[0][5]);
            //    pathology.intestinal3 = Convert.ToInt32(dtContent.Rows[0][6]);
            //    pathology.dysplasia3 = Convert.ToInt32(dtContent.Rows[0][7]);
            //    pathology.lymphoid3 = Convert.ToInt32(dtContent.Rows[0][8]);
            //    pathology.pitepithelial3 = Convert.ToInt32(dtContent.Rows[0][9]);
            //    pathology.Mucosal3 = Convert.ToInt32(dtContent.Rows[0][10]);
            //}
            //else
            //{
            //    pathology.id3 = 0;
            //}
            //if (dt.Rows[4][0] != DBNull.Value)
            //{
            //    pathology.id4 = Convert.ToInt32(dt.Rows[4][0]);
            //    DataTable dtContent = GetContent(pathology.id4);
            //    pathology.parts4 = Convert.ToString(dtContent.Rows[0][2]);
            //    pathology.chronic4 = Convert.ToInt32(dtContent.Rows[0][3]);
            //    pathology.acute4 = Convert.ToInt32(dtContent.Rows[0][4]);
            //    pathology.atrophy4 = Convert.ToInt32(dtContent.Rows[0][5]);
            //    pathology.intestinal4 = Convert.ToInt32(dtContent.Rows[0][6]);
            //    pathology.dysplasia4 = Convert.ToInt32(dtContent.Rows[0][7]);
            //    pathology.lymphoid4 = Convert.ToInt32(dtContent.Rows[0][8]);
            //    pathology.pitepithelial4 = Convert.ToInt32(dtContent.Rows[0][9]);
            //    pathology.Mucosal4 = Convert.ToInt32(dtContent.Rows[0][10]);
            //}
            //else
            //{
            //    pathology.id4 = 0;
            //}



           

            return View(pathology);
        }

        // POST: Pathologies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Pathology pathology)
        {
            if (ModelState.IsValid)
            {
                pathology.pathologynum = Request.Form["pathologynum"];
                string aa = Request.Form["detailid"];
                int bb = Convert.ToInt32(aa);
                string a= Request.Form["Id2"];
                int b= Convert.ToInt32(a);
                if (pathology.idd!=0)
                {
                    UpdatePathlogy(pathology.idd, pathology.pathologynum, pathology.parts, pathology.chronic, pathology.acute, pathology.atrophy,
                   pathology.intestinal, pathology.dysplasia, pathology.lymphoid, pathology.pitepithelial, pathology.Mucosal);
                }
                else if(pathology.idd == 0&&pathology.parts!=null)
                {
                    InsertPathlogy(pathology.pathologynum, pathology.parts, pathology.chronic, pathology.acute,
                        pathology.atrophy, pathology.intestinal, pathology.dysplasia, pathology.lymphoid,
                        pathology.pitepithelial, pathology.Mucosal);
                }

                if (pathology.id1!=0)
                {
                    UpdatePathlogy(pathology.id1, pathology.pathologynum, pathology.parts1, pathology.chronic1, pathology.acute1, pathology.atrophy1,
                  pathology.intestinal1, pathology.dysplasia1, pathology.lymphoid1, pathology.pitepithelial1, pathology.Mucosal1);
                }
                else if (pathology.id1 == 0 && pathology.parts1 != null)
                {
                    InsertPathlogy(pathology.pathologynum, pathology.parts1, pathology.chronic1, pathology.acute1,
                       pathology.atrophy1, pathology.intestinal1, pathology.dysplasia1, pathology.lymphoid1,
                       pathology.pitepithelial1, pathology.Mucosal1);
                }


                if (pathology.id2 != 0)
                {
                    UpdatePathlogy(pathology.id2, pathology.pathologynum, pathology.parts2, pathology.chronic2, pathology.acute2, pathology.atrophy2,
                   pathology.intestinal2, pathology.dysplasia2, pathology.lymphoid2, pathology.pitepithelial2, pathology.Mucosal2);
                }
                else if (pathology.id2 == 0 && pathology.parts2 != null)
                {
                    InsertPathlogy(pathology.pathologynum, pathology.parts2, pathology.chronic2, pathology.acute2,
                       pathology.atrophy2, pathology.intestinal2, pathology.dysplasia2, pathology.lymphoid2,
                       pathology.pitepithelial2, pathology.Mucosal2);
                }

                if (pathology.id3 != 0)
                {
                    UpdatePathlogy(pathology.id3, pathology.pathologynum, pathology.parts3, pathology.chronic3, pathology.acute3, pathology.atrophy3,
                   pathology.intestinal3, pathology.dysplasia3, pathology.lymphoid3, pathology.pitepithelial3, pathology.Mucosal3);
                }
                else if (pathology.id3 == 0 && pathology.parts3 != null)
                {
                    InsertPathlogy(pathology.pathologynum, pathology.parts3, pathology.chronic3, pathology.acute3,
                        pathology.atrophy3, pathology.intestinal3, pathology.dysplasia3, pathology.lymphoid3,
                        pathology.pitepithelial3, pathology.Mucosal3);
                }

                if (pathology.id4 != 0)
                {
                    UpdatePathlogy(pathology.id4, pathology.pathologynum, pathology.parts4, pathology.chronic4, pathology.acute4, pathology.atrophy4,
                    pathology.intestinal4, pathology.dysplasia4, pathology.lymphoid4, pathology.pitepithelial4, pathology.Mucosal4);
                }
                else if (pathology.id4 == 0 && pathology.parts4 != null)
                {
                    InsertPathlogy(pathology.pathologynum, pathology.parts4, pathology.chronic4, pathology.acute4,
                        pathology.atrophy4, pathology.intestinal4, pathology.dysplasia4, pathology.lymphoid4,
                        pathology.pitepithelial4, pathology.Mucosal4);
                }


                return RedirectToAction("Details/" + bb, "Visits");
            }
            return View(pathology);
        }

        // GET: Pathologies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pathology pathology = db.Pathologies.Find(id);
            if (pathology == null)
            {
                return HttpNotFound();
            }
            return View(pathology);
        }

        // POST: Pathologies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pathology pathology = db.Pathologies.Find(id);
            db.Pathologies.Remove(pathology);
            db.SaveChanges();
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

        public static DataTable GetId(string num)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select id from pathology where pathologynum='" + num+"'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        public static DataTable GetContent(int id)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from pathology where id=" +id;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }

        public static void InsertPathlogy( string pathologynum, string part, int chronic, int acute,
            int atrophy, int intestinal, int dysplasia, int lymphoid, int pitepithelial, int Mucosal)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "INSERT INTO pathology(pathologynum,part,chronic,acute,atrophy,intestinal,dysplasia,lymphoid,pitepithelial,Mucosal)VALUES" + "('" +
                    pathologynum + "','" + part + "'," + chronic + "," + acute + "," + atrophy + "," + intestinal + "," + dysplasia +
                    "," + lymphoid + "," + pitepithelial + "," + Mucosal + ")" ;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();


        }

        public static void UpdatePathlogy(int id,string pathologynum,string part,int chronic,int acute,
            int atrophy,int intestinal,int dysplasia,int lymphoid,int pitepithelial,int Mucosal)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = " UPDATE pathology set pathologynum='" + pathologynum + "',part='" + part + "',chronic=" + chronic + ",acute=" +
                acute + ",atrophy=" + atrophy + ",intestinal=" + intestinal + ",dysplasia=" + dysplasia + ",lymphoid=" +
                lymphoid + ",pitepithelial=" + pitepithelial + ",Mucosal=" + Mucosal+" where id="+id;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
           
            
        }
    }
}
