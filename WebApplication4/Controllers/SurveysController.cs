using MySql.Data.MySqlClient;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class SurveysController : Controller
    {
        private SurveysDbContext db = new SurveysDbContext();

        // GET: Surveys
        public ActionResult Index()
        {
            return View(db.Surveys.ToList());
        }

        // GET: Surveys/Details/5
        public ActionResult Details(int? id,string num,string phone,string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = new Surveys();
            surveys.Id = 0;
            surveys.num = num;
            surveys.phone = phone;
            surveys.name = name;
            surveys.checkid = Checkid(num);
            surveys.dt1 = Getnormalsituation(num);
            surveys.dt2 = Getphysicalconditions(num);
            surveys.dt3 = Getpersonalhistory(num);
            surveys.dt4 = Getfamilyhistory(num);
            surveys.dt5 = Getrelationtumor(num);
            surveys.dt6 = Getmedicalhistory(num);
            surveys.dt7 = Getsymptoms(num);
            surveys.dt8 = Getrecentdrugsituation(num);
            surveys.dt9 = Getsurvey(num);
            return View(surveys);
        }

        // GET: Surveys/Create
        public ActionResult Create(int id, string num, string phone, string name)
        {
            Surveys surveys = new Surveys();
            surveys.Id = id;
            surveys.num = num;
            surveys.phone = phone;
            surveys.name = name;
            DataTable modt = new DataTable();
            surveys.dt1 = modt;
            surveys.dt2 = modt;
            surveys.dt3 = modt;
            surveys.dt4 = modt;
            surveys.dt5 = modt;
            surveys.dt6 = modt;
            surveys.dt7 = modt;
            surveys.dt8 = modt;
            surveys.dt9 = modt;
            return View(surveys);
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                string iden = Request.Form["num"];
                string pho = Request.Form["phone"];
                string namess = Request.Form["name"];
                //A
                {
                    string sex0 = Request.Form["sex0"];
                    string births = Request.Form["births"];
                    if (births == "")
                    {
                        births = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    string Nationalitys = Request.Form["Nationalitys"];
                    string Specificnation = Request.Form["Specificnation"];
                    string educations = Request.Form["educations"];
                    string professions = Request.Form["professions"];
                    string maritalstatus = Request.Form["maritalstatus"];
                    string connect1 = Request.Form["connect1"];
                    string conphones1 = Request.Form["conphones1"];
                    string connect2 = Request.Form["connect2"];
                    string conphones2 = Request.Form["conphones2"];
                    string connect3 = Request.Form["connect3"];
                    string conphones3 = Request.Form["conphones3"];
                    string Currentresidences = Request.Form["Currentresidences"];
                    string Rurals = Request.Form["Rurals"];
                    if (Rurals == "")
                    {
                        Rurals = "0";
                    }
                    string Citys = Request.Form["Citys"];
                    if (Citys == "")
                    {
                        Citys = "0";
                    }
                    string peopletogethers = Request.Form["peopletogethers"];
                    if (peopletogethers == "")
                    {
                        peopletogethers = "0";
                    }
                    string insurances = Request.Form["insurances"];
                    Insertnormalsituation(namess, Convert.ToInt32(sex0), Convert.ToDateTime(births), iden, Convert.ToInt32(Nationalitys), Specificnation, Convert.ToInt32(educations),
                        Convert.ToInt32(professions), Convert.ToInt32(maritalstatus), pho, connect1, conphones1, connect2, conphones2, connect3,
                        conphones3, Convert.ToInt32(Currentresidences), Convert.ToInt32(Rurals), Convert.ToInt32(Citys),
                        Convert.ToInt32(peopletogethers), Convert.ToInt32(insurances));
                }
                //B
                {
                    string heights = Request.Form["heights"];
                    if (heights == "")
                    {
                        heights = "0";
                    }
                    string weights = Request.Form["weights"];
                    if (weights == "")
                    {
                        weights = "0";
                    }
                    string dropweights = Request.Form["dropweights"];

                    string refweights = Request.Form["refweights"];
                    if (refweights == "")
                    {
                        refweights = "0";
                    }
                    string bloods = Request.Form["bloods"];
                    string characters = Request.Form["characters"];
                    string Psychologicalstress = Request.Form["Psychologicalstress"];
                    string anxietys = Request.Form["anxietys"];
                    string Sleepqualitys = Request.Form["Sleepqualitys"];
                    string HPs = Request.Form["HPs"];
                    string isacceptHP = Request.Form["isacceptHP"];
                    string HPtreatment = Request.Form["HPtreatment"];

                    Insertphysicalconditions(iden, Convert.ToInt32(heights), Convert.ToInt32(weights), Convert.ToInt32(dropweights),
                        Convert.ToInt32(refweights), Convert.ToInt32(bloods), Convert.ToInt32(characters),
                        Convert.ToInt32(Psychologicalstress), Convert.ToInt32(anxietys), Convert.ToInt32(Sleepqualitys),
                        Convert.ToInt32(HPs), Convert.ToInt32(isacceptHP), Convert.ToInt32(HPtreatment));
                }
                //C
                {

                    string whetherSmoking = Request.Form["whetherSmoking"];

                    string smokingPerDay = Request.Form["smokingPerDay"];
                    if (smokingPerDay == "")
                    {
                        smokingPerDay = "0";
                    }
                    string regularSmoking = Request.Form["regularSmoking"];
                    if (regularSmoking == "")
                    {
                        regularSmoking = "0";
                    }
                    string regularsmokingbefore = Request.Form["regularsmokingbefore"];
                    if (regularsmokingbefore == "")
                    {
                        regularsmokingbefore = "0";
                    }
                    string rgbefore = Request.Form["rgbefore"];
                    if (rgbefore == "")
                    {
                        rgbefore = "0";
                    }
                    string smokingCessationAge = Request.Form["smokingCessationAge"];
                    if (smokingCessationAge == "")
                    {
                        smokingCessationAge = "0";
                    }
                    string yearsOfSmokingCessation = Request.Form["yearsOfSmokingCessation"];
                    if (yearsOfSmokingCessation == "")
                    {
                        yearsOfSmokingCessation = "0";
                    }
                    string passiveSmoking = Request.Form["passiveSmoking"];
                    string whetherDrinkingAlcohol = Request.Form["whetherDrinkingAlcohol"];
                    string startRegularDrinkingAge = Request.Form["startRegularDrinkingAge"];
                    if (startRegularDrinkingAge == "")
                    {
                        startRegularDrinkingAge = "0";
                    }
                    string regularDrinking = Request.Form["regularDrinking"];
                    if (regularDrinking == "")
                    {
                        regularDrinking = "0";
                    }
                    string drinkingAmountEachTime = Request.Form["drinkingAmountEachTime"];
                    if (drinkingAmountEachTime == "")
                    {
                        drinkingAmountEachTime = "0";
                    }
                    string drinkingReaction = Request.Form["drinkingReaction"];
                    string eatingRegularly = Request.Form["eatingRegularly"];
                    string eatingSpeed = Request.Form["eatingSpeed"];
                    string eatingHabit = Request.Form["eatingHabit"];
                    string whetherDrinkTeaRegularly = Request.Form["whetherDrinkTeaRegularly"];
                    string yearsOfTeaDrinking = Request.Form["yearsOfTeaDrinking"];
                    if (yearsOfTeaDrinking == "")
                    {
                        yearsOfTeaDrinking = "0";
                    }
                    string strongOrLightTeat = Request.Form["strongOrLightTea"];
                    Insertpersonalhistory(iden, Convert.ToInt32(whetherSmoking), Convert.ToInt32(smokingPerDay), Convert.ToInt32(regularSmoking),
                        Convert.ToInt32(regularsmokingbefore), Convert.ToInt32(rgbefore), Convert.ToInt32(smokingCessationAge),
                        Convert.ToInt32(yearsOfSmokingCessation), Convert.ToInt32(passiveSmoking), Convert.ToInt32(whetherDrinkingAlcohol),
                        Convert.ToInt32(startRegularDrinkingAge), Convert.ToInt32(regularDrinking), Convert.ToInt32(drinkingAmountEachTime),
                        Convert.ToInt32(drinkingReaction), Convert.ToInt32(eatingRegularly), Convert.ToInt32(eatingSpeed),
                        Convert.ToInt32(eatingHabit), Convert.ToInt32(whetherDrinkTeaRegularly), Convert.ToInt32(yearsOfTeaDrinking),
                        Convert.ToInt32(strongOrLightTeat));

                }
                //D
                {
                    string numofsons = Request.Form["numofsons"];
                    string numofdaughters = Request.Form["numofdaughters"];
                    string numofbros = Request.Form["numofbros"];
                    string numofsisters = Request.Form["numofsisters"];
                    string hasTumor = Request.Form["hasTumor"];
                    Insertfamilyhistroy(iden, Convert.ToInt32(numofsons), Convert.ToInt32(numofdaughters), Convert.ToInt32(numofbros),
                       Convert.ToInt32(numofsisters), Convert.ToInt32(hasTumor));

                    string Gastrolcancer1 = Request.Form["Gastrolcancer1"];
                    string othercancer1 = Request.Form["othercancer1"];
                    if (Gastrolcancer1 == "0" && othercancer1 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D51", "", 0, Convert.ToInt32(Gastrolcancer1), othercancer1);
                    }

                    string Gastrolcancer2 = Request.Form["Gastrolcancer2"];
                    string othercancer2 = Request.Form["othercancer2"];
                    if (Gastrolcancer2 == "0" && othercancer2 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D52", "", 1, Convert.ToInt32(Gastrolcancer2), othercancer2);
                    }
                    string kinship3 = Request.Form["kinship3"];
                    string Gastrolcancer3 = Request.Form["Gastrolcancer3"];
                    string othercancer3 = Request.Form["othercancer3"];
                    string cansersex3 = Request.Form["cansersex3"];
                    if (Gastrolcancer3 == "0" && othercancer3 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D531", kinship3, Convert.ToInt32(cansersex3), Convert.ToInt32(Gastrolcancer3), othercancer3);
                    }

                    string kinship4 = Request.Form["kinship4"];
                    string Gastrolcancer4 = Request.Form["Gastrolcancer4"];
                    string othercancer4 = Request.Form["othercancer4"];
                    string cansersex4 = Request.Form["cansersex4"];
                    if (Gastrolcancer4 == "0" && othercancer4 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D532", kinship4, Convert.ToInt32(cansersex4), Convert.ToInt32(Gastrolcancer4), othercancer4);
                    }

                    string kinship5 = Request.Form["kinship5"];
                    string Gastrolcancer5 = Request.Form["Gastrolcancer5"];
                    string othercancer5 = Request.Form["othercancer5"];
                    string cansersex5 = Request.Form["cansersex5"];
                    if (Gastrolcancer5 == "0" && othercancer5 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D533", kinship5, Convert.ToInt32(cansersex5), Convert.ToInt32(Gastrolcancer5), othercancer5);
                    }

                    string kinship6 = Request.Form["kinship6"];
                    string Gastrolcancer6 = Request.Form["Gastrolcancer6"];
                    string othercancer6 = Request.Form["othercancer6"];
                    string cansersex6 = Request.Form["cansersex6"];
                    if (Gastrolcancer6 == "0" && othercancer6 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D534", kinship6, Convert.ToInt32(cansersex6), Convert.ToInt32(Gastrolcancer6), othercancer6);
                    }


                    string Gastrolcancer7 = Request.Form["Gastrolcancer7"];
                    string othercancer7 = Request.Form["othercancer7"];
                    string cansersex7 = Request.Form["cansersex7"];
                    if (Gastrolcancer7 == "0" && othercancer7 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D541", "", Convert.ToInt32(cansersex7), Convert.ToInt32(Gastrolcancer7), othercancer7);
                    }

                    string Gastrolcancer8 = Request.Form["Gastrolcancer8"];
                    string othercancer8 = Request.Form["othercancer8"];
                    string cansersex8 = Request.Form["cansersex8"];
                    if (Gastrolcancer8 == "0" && othercancer8 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D542", "", Convert.ToInt32(cansersex8), Convert.ToInt32(Gastrolcancer8), othercancer8);
                    }

                    string Gastrolcancer9 = Request.Form["Gastrolcancer9"];
                    string othercancer9 = Request.Form["othercancer9"];
                    string cansersex9 = Request.Form["cansersex9"];
                    if (Gastrolcancer9 == "0" && othercancer9 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D543", "", Convert.ToInt32(cansersex9), Convert.ToInt32(Gastrolcancer9), othercancer9);
                    }

                    string Gastrolcancer10 = Request.Form["Gastrolcancer10"];
                    string othercancer10 = Request.Form["othercancer10"];
                    string cansersex10 = Request.Form["cansersex10"];
                    if (Gastrolcancer10 == "0" && othercancer10 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D544", "", Convert.ToInt32(cansersex10), Convert.ToInt32(Gastrolcancer10), othercancer10);
                    }


                    string kinship11 = Request.Form["kinship11"];
                    string Gastrolcancer11 = Request.Form["Gastrolcancer11"];
                    string othercancer11 = Request.Form["othercancer11"];
                    string cansersex11 = Request.Form["cansersex11"];
                    if (Gastrolcancer11 == "0" && othercancer11 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D551", kinship11, Convert.ToInt32(cansersex11), Convert.ToInt32(Gastrolcancer11), othercancer11);
                    }

                    string kinship12 = Request.Form["kinship12"];
                    string Gastrolcancer12 = Request.Form["Gastrolcancer12"];
                    string othercancer12 = Request.Form["othercancer12"];
                    string cansersex12 = Request.Form["cansersex12"];
                    if (Gastrolcancer12 == "0" && othercancer12 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D552", kinship12, Convert.ToInt32(cansersex12), Convert.ToInt32(Gastrolcancer12), othercancer12);
                    }

                    string kinship13 = Request.Form["kinship13"];
                    string Gastrolcancer13 = Request.Form["Gastrolcancer13"];
                    string othercancer13 = Request.Form["othercancer13"];
                    string cansersex13 = Request.Form["cansersex13"];
                    if (Gastrolcancer13 == "0" && othercancer13 == "")
                    {

                    }
                    else
                    {
                        Insertrelationtumor(iden, "D553", kinship13, Convert.ToInt32(cansersex13), Convert.ToInt32(Gastrolcancer13), othercancer13);
                    }

                }
                //E
                {

                    string Numendoscopy = Request.Form["Numendoscopy"];
                    string numofhps = Request.Form["numofhps"];
                    string hyperts = Request.Form["hyperts"];
                    string time1s = Request.Form["time1s"];
                    if (time1s == "")
                    {
                        time1s = DateTime.Now.ToString();
                    }
                    string Hyperlipidemia = Request.Form["Hyperlipidemia"];
                    string time2s = Request.Form["time2s"];
                    if (time2s == "")
                    {
                        time2s = DateTime.Now.ToString();
                    }
                    string codisease = Request.Form["codisease"];
                    string time3s = Request.Form["time3s"];
                    if (time3s == "")
                    {
                        time3s = DateTime.Now.ToString();
                    }
                    string apoplexy = Request.Form["apoplexy"];
                    string time4s = Request.Form["time4s"];
                    if (time4s == "")
                    {
                        time4s = DateTime.Now.ToString();
                    }
                    string Diabetes = Request.Form["Diabetes"];
                    string time5s = Request.Form["time5s"];
                    if (time5s == "")
                    {
                        time5s = DateTime.Now.ToString();
                    }
                    string chronichepatitis = Request.Form["chronichepatitis"];
                    string time6s = Request.Form["time6s"];
                    if (time6s == "")
                    {
                        time6s = DateTime.Now.ToString();
                    }
                    string Pulmonarytuberculosis = Request.Form["Pulmonarytuberculosis"];
                    string time7s = Request.Form["time7s"];
                    if (time7s == "")
                    {
                        time7s = DateTime.Now.ToString();
                    }
                    string othersbing = Request.Form["othersbing"];
                    string time8s = Request.Form["time8s"];
                    if (time8s == "")
                    {
                        time8s = DateTime.Now.ToString();
                    }
                    Insertmedicalhistory(iden, Convert.ToInt32(Numendoscopy), Convert.ToInt32(numofhps), Convert.ToInt32(hyperts),
                        Convert.ToString(time1s), Convert.ToInt32(Hyperlipidemia), Convert.ToString(time2s),
                        Convert.ToInt32(codisease), Convert.ToString(time3s),
                        Convert.ToInt32(apoplexy), Convert.ToString(time4s),
                        Convert.ToInt32(Diabetes), Convert.ToString(time5s),
                        Convert.ToInt32(chronichepatitis), Convert.ToString(time6s),
                        Convert.ToInt32(Pulmonarytuberculosis), Convert.ToString(time7s),
                        Convert.ToInt32(othersbing), Convert.ToString(time8s));

                    string tengtong = Request.Form["tengtong"];
                    Insertsymptoms(iden, 1, Convert.ToInt32(tengtong));
                    string zhuoshao = Request.Form["zhuoshao"];
                    Insertsymptoms(iden, 2, Convert.ToInt32(zhuoshao));
                    string fansuan = Request.Form["fansuan"];
                    Insertsymptoms(iden, 3, Convert.ToInt32(fansuan));
                    string exinotu = Request.Form["exinotu"];
                    Insertsymptoms(iden, 4, Convert.ToInt32(exinotu));
                    string aiqi = Request.Form["aiqi"];
                    Insertsymptoms(iden, 5, Convert.ToInt32(aiqi));
                    string fuzhang = Request.Form["fuzhang"];
                    Insertsymptoms(iden, 6, Convert.ToInt32(fuzhang));
                }
                //F
                {
                    string Aspirins = Request.Form["Aspirins"];
                    string AspirinCRs = Request.Form["AspirinCRs"];
                    string NonSteroidalAntiInflammatory = Request.Form["NonSteroidalAntiInflammatory"];
                    string Glucocorticoids = Request.Form["Glucocorticoids"];
                    string GlucocorticoidsCR = Request.Form["GlucocorticoidsCR"];
                    string Antibiotics = Request.Form["Antibiotics"];
                    string Tincture = Request.Form["Tincture"];
                    string PPIs = Request.Form["PPIs"];
                    Insertrecentdrugsituation(iden,Convert.ToInt32(Aspirins), Convert.ToInt32(AspirinCRs),
                        Convert.ToInt32(NonSteroidalAntiInflammatory), Convert.ToInt32(Glucocorticoids), 
                        Convert.ToInt32(GlucocorticoidsCR),
                        Convert.ToInt32(Antibiotics), Convert.ToInt32(Tincture), Convert.ToInt32(PPIs));
                }
                //G
                {
                    string surveyobject = Request.Form["surveyobject"];
                    string surveybelieve = Request.Form["surveybelieve"];
                    string surveyend = Request.Form["surveyend"];
                    string surveyspendtime = Request.Form["surveyspendtime"];
                    string surveyevaluate = Request.Form["surveyevaluate"];
                    Insertsurvey(iden,Convert.ToInt32(surveyobject), Convert.ToInt32(surveybelieve),
                        surveyend, Convert.ToInt32(surveyspendtime), Convert.ToInt32(surveyevaluate));
                }
                return RedirectToAction("Index", "UserMangers");
            }

            return View(surveys);
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = db.Surveys.Find(id);
            if (surveys == null)
            {
                return HttpNotFound();
            }
            return View(surveys);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveys).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(surveys);
        }

        //A基本情况编辑
        public ActionResult normalsituationEdit(int? id,string num,string phone,string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = new Surveys();
            surveys.num = num;
            surveys.phone = phone;
            surveys.name = name;
            surveys.checkid = Checkid(num);
            surveys.dt1 = Getnormalsituation(num);
            return View(surveys);
        }
        // POST: Surveys/normalsituationEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult normalsituationEdit( Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                string iden = Request.Form["num"];
                string pho = Request.Form["phone"];
                
                string b = Request.Form["sex0"];
                string c = Request.Form["births"];
                if (c == "")
                {
                    c = DateTime.Now.ToString("yyyy-MM-dd");
                }
                string d = Request.Form["Nationalitys"];
                string e = Request.Form["Specificnation"];
                string f = Request.Form["educations"];
                string g = Request.Form["professions"];
                string h = Request.Form["maritalstatus"];
                string j = Request.Form["connect1"];
                string k = Request.Form["conphones1"];
                string l = Request.Form["connect2"];
                string m = Request.Form["conphones2"];
                string n = Request.Form["connect3"];
                string o = Request.Form["conphones3"];
                string p = Request.Form["Currentresidences"];
                string q = Request.Form["Rurals"];
                string r = Request.Form["Citys"];
                string s = Request.Form["peopletogethers"];
                string t = Request.Form["insurances"];
                Updatenormalsituation( Convert.ToInt32(b), Convert.ToDateTime(c), iden, Convert.ToInt32(d), e, Convert.ToInt32(f),
                    Convert.ToInt32(g), Convert.ToInt32(h), pho, j, k, l, m, n, o, Convert.ToInt32(p), Convert.ToInt32(q), Convert.ToInt32(r),
                    Convert.ToInt32(s), Convert.ToInt32(t));
                return RedirectToAction("Index", "UserMangers");
            }
            return View(surveys);
        }

        //B身体状况编辑
        public ActionResult physicalconditionsEdit(int? id, string num, string phone, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = new Surveys();
            surveys.num = num;
            surveys.phone = phone;
            surveys.name = name;
            surveys.checkid = Checkid(num);
            surveys.dt2 = Getphysicalconditions(num);
            return View(surveys);
        }
        // POST: Surveys/physicalconditions/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult physicalconditionsEdit(Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                string iden = Request.Form["num"];
                string pho = Request.Form["phone"];

                string b = Request.Form["heights"];
                if (b == "")
                {
                    b = "0";
                }
                string c = Request.Form["weights"];
                if (c == "")
                {
                    c = "0";
                }
                string d = Request.Form["dropweights"];
                
                string e = Request.Form["refweights"];
                if (e == "")
                {
                    e = "0";
                }
                string f = Request.Form["bloods"];
                string g = Request.Form["characters"];
                string h = Request.Form["Psychologicalstress"];
                string j = Request.Form["anxietys"];
                string k = Request.Form["Sleepqualitys"];
                string l = Request.Form["HPs"];
                string m = Request.Form["isacceptHP"];
                string n = Request.Form["HPtreatment"];
                Updatephysicalconditions(iden,Convert.ToInt32(b), Convert.ToInt32(c), Convert.ToInt32(d), Convert.ToInt32(e), Convert.ToInt32(f), Convert.ToInt32(g),
                    Convert.ToInt32(h), Convert.ToInt32(j), Convert.ToInt32(k), Convert.ToInt32(l), Convert.ToInt32(m), Convert.ToInt32(n));

                return RedirectToAction("Index", "UserMangers");
            }
            return View(surveys);
        }


        //C
        public ActionResult personalhistoryEdit(int? id, string num, string phone, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = new Surveys();
            surveys.num = num;
            surveys.phone = phone;
            surveys.name = name;
            surveys.checkid = Checkid(num);
            surveys.dt3 = Getpersonalhistory(num);
            return View(surveys);
        }
        // POST: Surveys/physicalconditions/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult personalhistoryEdit(Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                string iden = Request.Form["num"];

                string a = Request.Form["whetherSmoking"];

                string b = Request.Form["smokingPerDay"];
                if (b == "")
                {
                    b = "0";
                }
                string c = Request.Form["regularSmoking"];
                if (c == "")
                {
                    c = "0";
                }
                string d = Request.Form["regularsmokingbefore"];
                if (d == "")
                {
                    d = "0";
                }
                string e = Request.Form["rgbefore"];
                if (e == "")
                {
                    e = "0";
                }
                string f = Request.Form["smokingCessationAge"];
                if (f == "")
                {
                    f = "0";
                }
                string g = Request.Form["yearsOfSmokingCessation"];
                if (g == "")
                {
                    g = "0";
                }
                string h = Request.Form["passiveSmoking"];
                string j = Request.Form["whetherDrinkingAlcohol"];
                string k = Request.Form["startRegularDrinkingAge"];
                if (k == "")
                {
                    k = "0";
                }
                string l = Request.Form["regularDrinking"];
                if (l == "")
                {
                    l = "0";
                }
                string m = Request.Form["drinkingAmountEachTime"];
                if (m == "")
                {
                    m = "0";
                }
                string n = Request.Form["drinkingReaction"];
                string o = Request.Form["eatingRegularly"];
                string p = Request.Form["eatingSpeed"];
                string q = Request.Form["eatingHabit"];
                string r = Request.Form["whetherDrinkTeaRegularly"];
                string s = Request.Form["yearsOfTeaDrinking"];
                if (s == "")
                {
                    s = "0";
                }
                string t = Request.Form["strongOrLightTea"];
                Updatepersonalhistory(iden, Convert.ToInt32(a), Convert.ToInt32(b), Convert.ToInt32(c), Convert.ToInt32(d), Convert.ToInt32(e), Convert.ToInt32(f),
                    Convert.ToInt32(g), Convert.ToInt32(h), Convert.ToInt32(j), Convert.ToInt32(k), Convert.ToInt32(l), Convert.ToInt32(m),
                    Convert.ToInt32(n), Convert.ToInt32(o), Convert.ToInt32(p), Convert.ToInt32(q), Convert.ToInt32(r), Convert.ToInt32(s), Convert.ToInt32(t));

                return RedirectToAction("Index", "UserMangers");
            }
            return View(surveys);
        }

        //D
        public ActionResult familyhistoryEdit(int? id, string num, string phone, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = new Surveys();
            surveys.num = num;
            surveys.phone = phone;
            surveys.name = name;
            surveys.checkid = Checkid(num);
            surveys.dt4 = Getfamilyhistory(num);
            surveys.dt5 = Getrelationtumor(num);
            surveys.D51 = Getrelationtumors(num, "D51");
            surveys.D52 = Getrelationtumors(num, "D52");
            surveys.D531 = Getrelationtumors(num, "D531");
            surveys.D532 = Getrelationtumors(num, "D532");
            surveys.D533 = Getrelationtumors(num, "D533");
            surveys.D534 = Getrelationtumors(num, "D534");
            surveys.D541 = Getrelationtumors(num, "D541");
            surveys.D542 = Getrelationtumors(num, "D542");
            surveys.D543 = Getrelationtumors(num, "D543");
            surveys.D544 = Getrelationtumors(num, "D544");
            surveys.D551 = Getrelationtumors(num, "D551");
            surveys.D552 = Getrelationtumors(num, "D552");
            surveys.D553 = Getrelationtumors(num, "D553");
            return View(surveys);
        }
        // POST: Surveys/physicalconditions/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult familyhistoryEdit(Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                string iden = Request.Form["num"];
                string pho = Request.Form["phone"];
                string namess = Request.Form["name"];
                string numofsons = Request.Form["numofsons"];
                string numofdaughters = Request.Form["numofdaughters"];
                string numofbros = Request.Form["numofbros"];
                string numofsisters = Request.Form["numofsisters"];
                string hasTumor = Request.Form["hasTumor"];
                Updatefamilyhistroy(iden, Convert.ToInt32(numofsons), Convert.ToInt32(numofdaughters), Convert.ToInt32(numofbros),
                   Convert.ToInt32(numofsisters), Convert.ToInt32(hasTumor));

                string Gastrolcancer1 = Request.Form["Gastrolcancer1"];
                string othercancer1 = Request.Form["othercancer1"];
                if (Gastrolcancer1 == "0" && othercancer1 == "")
                {

                }
                else
                {
                    int check1 = Ckeckrelationtumor(iden, "D51");
                    if (check1==0)
                    {
                        Insertrelationtumor(iden, "D51", "", 0, Convert.ToInt32(Gastrolcancer1), othercancer1);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D51", "", 0, Convert.ToInt32(Gastrolcancer1), othercancer1);
                    }
                    
                }

                string Gastrolcancer2 = Request.Form["Gastrolcancer2"];
                string othercancer2 = Request.Form["othercancer2"];
                if (Gastrolcancer2 == "0" && othercancer2 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D52");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D52", "", 1, Convert.ToInt32(Gastrolcancer2), othercancer2);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D52", "", 1, Convert.ToInt32(Gastrolcancer2), othercancer2);
                    }
                    
                }
                string kinship3 = Request.Form["kinship3"];
                string Gastrolcancer3 = Request.Form["Gastrolcancer3"];
                string othercancer3 = Request.Form["othercancer3"];
                string cansersex3 = Request.Form["cansersex3"];
                if (Gastrolcancer3 == "0" && othercancer3 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D531");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D531", kinship3, Convert.ToInt32(cansersex3), Convert.ToInt32(Gastrolcancer3), othercancer3);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D531", kinship3, Convert.ToInt32(cansersex3), Convert.ToInt32(Gastrolcancer3), othercancer3);
                    }
                    
                }

                string kinship4 = Request.Form["kinship4"];
                string Gastrolcancer4 = Request.Form["Gastrolcancer4"];
                string othercancer4 = Request.Form["othercancer4"];
                string cansersex4 = Request.Form["cansersex4"];
                if (Gastrolcancer4 == "0" && othercancer4 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D532");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D532", kinship4, Convert.ToInt32(cansersex4), Convert.ToInt32(Gastrolcancer4), othercancer4);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D532", kinship4, Convert.ToInt32(cansersex4), Convert.ToInt32(Gastrolcancer4), othercancer4);
                    }
                    
                }

                string kinship5 = Request.Form["kinship5"];
                string Gastrolcancer5 = Request.Form["Gastrolcancer5"];
                string othercancer5 = Request.Form["othercancer5"];
                string cansersex5 = Request.Form["cansersex5"];
                if (Gastrolcancer5 == "0" && othercancer5 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D533");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D533", kinship5, Convert.ToInt32(cansersex5), Convert.ToInt32(Gastrolcancer5), othercancer5);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D533", kinship5, Convert.ToInt32(cansersex5), Convert.ToInt32(Gastrolcancer5), othercancer5);
                    }
                    
                }

                string kinship6 = Request.Form["kinship6"];
                string Gastrolcancer6 = Request.Form["Gastrolcancer6"];
                string othercancer6 = Request.Form["othercancer6"];
                string cansersex6 = Request.Form["cansersex6"];
                if (Gastrolcancer6 == "0" && othercancer6 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D534");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D534", kinship6, Convert.ToInt32(cansersex6), Convert.ToInt32(Gastrolcancer6), othercancer6);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D534", kinship6, Convert.ToInt32(cansersex6), Convert.ToInt32(Gastrolcancer6), othercancer6);
                    }
                    
                }


                string Gastrolcancer7 = Request.Form["Gastrolcancer7"];
                string othercancer7 = Request.Form["othercancer7"];
                string cansersex7 = Request.Form["cansersex7"];
                if (Gastrolcancer7 == "0" && othercancer7 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D541");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D541", "", Convert.ToInt32(cansersex7), Convert.ToInt32(Gastrolcancer7), othercancer7);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D541", "", Convert.ToInt32(cansersex7), Convert.ToInt32(Gastrolcancer7), othercancer7);
                    }
                    
                }

                string Gastrolcancer8 = Request.Form["Gastrolcancer8"];
                string othercancer8 = Request.Form["othercancer8"];
                string cansersex8 = Request.Form["cansersex8"];
                if (Gastrolcancer8 == "0" && othercancer8 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D542");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D542", "", Convert.ToInt32(cansersex8), Convert.ToInt32(Gastrolcancer8), othercancer8);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D542", "", Convert.ToInt32(cansersex8), Convert.ToInt32(Gastrolcancer8), othercancer8);
                    }
                    
                }

                string Gastrolcancer9 = Request.Form["Gastrolcancer9"];
                string othercancer9 = Request.Form["othercancer9"];
                string cansersex9 = Request.Form["cansersex9"];
                if (Gastrolcancer9 == "0" && othercancer9 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D543");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D543", "", Convert.ToInt32(cansersex9), Convert.ToInt32(Gastrolcancer9), othercancer9);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D543", "", Convert.ToInt32(cansersex9), Convert.ToInt32(Gastrolcancer9), othercancer9);
                    }
                    
                }

                string Gastrolcancer10 = Request.Form["Gastrolcancer10"];
                string othercancer10 = Request.Form["othercancer10"];
                string cansersex10 = Request.Form["cansersex10"];
                if (Gastrolcancer10 == "0" && othercancer10 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D544");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D544", "", Convert.ToInt32(cansersex10), Convert.ToInt32(Gastrolcancer10), othercancer10);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D544", "", Convert.ToInt32(cansersex10), Convert.ToInt32(Gastrolcancer10), othercancer10);
                    }
                    
                }


                string kinship11 = Request.Form["kinship11"];
                string Gastrolcancer11 = Request.Form["Gastrolcancer11"];
                string othercancer11 = Request.Form["othercancer11"];
                string cansersex11 = Request.Form["cansersex11"];
                if (Gastrolcancer11 == "0" && othercancer11 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D551");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D551", kinship11, Convert.ToInt32(cansersex11), Convert.ToInt32(Gastrolcancer11), othercancer11);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D551", kinship11, Convert.ToInt32(cansersex11), Convert.ToInt32(Gastrolcancer11), othercancer11);
                    }
                    
                }

                string kinship12 = Request.Form["kinship12"];
                string Gastrolcancer12 = Request.Form["Gastrolcancer12"];
                string othercancer12 = Request.Form["othercancer12"];
                string cansersex12 = Request.Form["cansersex12"];
                if (Gastrolcancer12 == "0" && othercancer12 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D552");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D552", kinship12, Convert.ToInt32(cansersex12), Convert.ToInt32(Gastrolcancer12), othercancer12);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D552", kinship12, Convert.ToInt32(cansersex12), Convert.ToInt32(Gastrolcancer12), othercancer12);
                    }
                    
                }

                string kinship13 = Request.Form["kinship13"];
                string Gastrolcancer13 = Request.Form["Gastrolcancer13"];
                string othercancer13 = Request.Form["othercancer13"];
                string cansersex13 = Request.Form["cansersex13"];
                if (Gastrolcancer13 == "0" && othercancer13 == "")
                {

                }
                else
                {
                    int check2 = Ckeckrelationtumor(iden, "D553");
                    if (check2 == 0)
                    {
                        Insertrelationtumor(iden, "D553", kinship13, Convert.ToInt32(cansersex13), Convert.ToInt32(Gastrolcancer13), othercancer13);
                    }
                    else
                    {
                        Updaterelationtumor(iden, "D553", kinship13, Convert.ToInt32(cansersex13), Convert.ToInt32(Gastrolcancer13), othercancer13);
                    }
                    
                }

                return RedirectToAction("Index", "UserMangers");
            }
            return View(surveys);
        }

        //E
        public ActionResult medicalhistoryEdit(int? id, string num, string phone, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = new Surveys();
            surveys.num = num;
            surveys.phone = phone;
            surveys.name = name;
            surveys.checkid = Checkid(num);
            surveys.dt6 = Getmedicalhistory(num);
            surveys.dt7 = Getsymptoms(num);
            return View(surveys);
        }
        // POST: Surveys/physicalconditions/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult medicalhistoryEdit(Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                string iden = Request.Form["num"];
                string pho = Request.Form["phone"];
                string namess = Request.Form["name"];
                string tengtong = Request.Form["tengtong"];
                Updatesymptoms(iden, 1, Convert.ToInt32(tengtong));
                string zhuoshao = Request.Form["zhuoshao"];
                Updatesymptoms(iden, 2, Convert.ToInt32(zhuoshao));
                string fansuan = Request.Form["fansuan"];
                Updatesymptoms(iden, 3, Convert.ToInt32(fansuan));
                string exinotu = Request.Form["exinotu"];
                Updatesymptoms(iden, 4, Convert.ToInt32(exinotu));
                string aiqi = Request.Form["aiqi"];
                Updatesymptoms(iden, 5, Convert.ToInt32(aiqi));
                string fuzhang = Request.Form["fuzhang"];
                Updatesymptoms(iden, 6, Convert.ToInt32(fuzhang));

                string Numendoscopy = Request.Form["Numendoscopy"];
                string numofhps = Request.Form["numofhps"];
                string hyperts = Request.Form["hyperts"];
                string time1s = Request.Form["time1s"];
                if (time1s == "")
                {
                    time1s = DateTime.Now.ToString();
                }
                string Hyperlipidemia = Request.Form["Hyperlipidemia"];
                string time2s = Request.Form["time2s"];
                if (time2s == "")
                {
                    time2s = DateTime.Now.ToString();
                }
                string codisease = Request.Form["codisease"];
                string time3s = Request.Form["time3s"];
                if (time3s == "")
                {
                    time3s = DateTime.Now.ToString();
                }
                string apoplexy = Request.Form["apoplexy"];
                string time4s = Request.Form["time4s"];
                if (time4s == "")
                {
                    time4s = DateTime.Now.ToString();
                }
                string Diabetes = Request.Form["Diabetes"];
                string time5s = Request.Form["time5s"];
                if (time5s == "")
                {
                    time5s = DateTime.Now.ToString();
                }
                string chronichepatitis = Request.Form["chronichepatitis"];
                string time6s = Request.Form["time6s"];
                if (time6s == "")
                {
                    time6s = DateTime.Now.ToString();
                }
                string Pulmonarytuberculosis = Request.Form["Pulmonarytuberculosis"];
                string time7s = Request.Form["time7s"];
                if (time7s == "")
                {
                    time7s = DateTime.Now.ToString();
                }
                string othersbing = Request.Form["othersbing"];
                string time8s = Request.Form["time8s"];
                if (time8s == "")
                {
                    time8s = DateTime.Now.ToString();
                }
                Updatemedicalhistory(iden, Convert.ToInt32(Numendoscopy), Convert.ToInt32(numofhps), Convert.ToInt32(hyperts),
                    Convert.ToString(time1s), Convert.ToInt32(Hyperlipidemia), Convert.ToString(time2s),
                    Convert.ToInt32(codisease), Convert.ToString(time3s),
                    Convert.ToInt32(apoplexy), Convert.ToString(time4s),
                    Convert.ToInt32(Diabetes), Convert.ToString(time5s),
                    Convert.ToInt32(chronichepatitis), Convert.ToString(time6s),
                    Convert.ToInt32(Pulmonarytuberculosis), Convert.ToString(time7s),
                    Convert.ToInt32(othersbing), Convert.ToString(time8s));

                

                return RedirectToAction("Index", "UserMangers");
            }
            return View(surveys);
        }

        //F
        public ActionResult recentdrugsituationEdit(int? id, string num, string phone, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = new Surveys();
            surveys.num = num;
            surveys.phone = phone;
            surveys.name = name;
            surveys.checkid = Checkid(num);
            surveys.dt8 = Getrecentdrugsituation(num);
            
            return View(surveys);
        }
        // POST: Surveys/physicalconditions/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult recentdrugsituationEdit(Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                string iden = Request.Form["num"];
                string pho = Request.Form["phone"];
                string namess = Request.Form["name"];
                string Aspirins = Request.Form["Aspirins"];
                string AspirinCRs = Request.Form["AspirinCRs"];
                string NonSteroidalAntiInflammatory = Request.Form["NonSteroidalAntiInflammatory"];
                string Glucocorticoids = Request.Form["Glucocorticoids"];
                string GlucocorticoidsCR = Request.Form["GlucocorticoidsCR"];
                string Antibiotics = Request.Form["Antibiotics"];
                string Tincture = Request.Form["Tincture"];
                string PPIs = Request.Form["PPIs"];
                Updaterecentdrugsituation(iden, Convert.ToInt32(Aspirins), Convert.ToInt32(AspirinCRs),
                    Convert.ToInt32(NonSteroidalAntiInflammatory), Convert.ToInt32(Glucocorticoids),
                    Convert.ToInt32(GlucocorticoidsCR),
                    Convert.ToInt32(Antibiotics), Convert.ToInt32(Tincture), Convert.ToInt32(PPIs));

                return RedirectToAction("Index", "UserMangers");
            }
            return View(surveys);
        }

        //G
        public ActionResult surveyEdit(int? id, string num, string phone, string name)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = new Surveys();
            surveys.num = num;
            surveys.phone = phone;
            surveys.name = name;
            surveys.checkid = Checkid(num);
            surveys.dt9 = Getsurvey(num);

            return View(surveys);
        }
        // POST: Surveys/physicalconditions/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult surveyEdit(Surveys surveys)
        {
            if (ModelState.IsValid)
            {
                string iden = Request.Form["num"];
                string pho = Request.Form["phone"];
                string namess = Request.Form["name"];
                string surveyobject = Request.Form["surveyobject"];
                string surveybelieve = Request.Form["surveybelieve"];
                string surveyend = Request.Form["surveyend"];
                string surveyspendtime = Request.Form["surveyspendtime"];
                string surveyevaluate = Request.Form["surveyevaluate"];
                Updatesurvey(iden, Convert.ToInt32(surveyobject), Convert.ToInt32(surveybelieve),
                    surveyend, Convert.ToInt32(surveyspendtime), Convert.ToInt32(surveyevaluate));

                return RedirectToAction("Index", "UserMangers");
            }
            return View(surveys);
        }

        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Surveys surveys = db.Surveys.Find(id);
            if (surveys == null)
            {
                return HttpNotFound();
            }
            return View(surveys);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Surveys surveys = db.Surveys.Find(id);
            db.Surveys.Remove(surveys);
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

        //身体状况增改查
        private void Insertphysicalconditions(string identinum, int height, int weight, int dropweight,
        int refweight, int blood, int character, int Psychologicalstress, int anxiety, int Sleepquality, int HP, int isacceptHP, int HPtreatment)
        {
            
            MySqlConnection mysql = getMySqlConnection();
            string sql = "INSERT INTO physicalconditions (`identinum`, `height`, `weight`, `dropweight`, `refweight`, `blood`, `character`, `Psychologicalstress`, `anxiety`, `Sleepquality`, `HP`, `isacceptHP`, `HPtreatment`) " +
                "VALUES ('" + identinum + "', " + height + ", " + weight + ", " + dropweight + ", " + refweight + ", " + blood + ", " + character + ", " + Psychologicalstress + "," +
            + anxiety + ", " + Sleepquality + ", " + HP + ", " + isacceptHP + ", " + HPtreatment + ");";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            
        }

        private DataTable Getphysicalconditions(string identinum)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from physicalconditions where identinum='" + identinum + "';";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }



        private void Updatephysicalconditions(string identinum, int height, int weight, int dropweight,
        int refweight, int blood, int character, int Psychologicalstress, int anxiety, int Sleepquality, int HP, int isacceptHP, int HPtreatment)
        {
            DataTable dt = new DataTable();
            MySqlConnection mysql = getMySqlConnection();
            string sql = "UPDATE physicalconditions  " +
                "SET `height`=" + height + ", `weight`=" + weight + ", `dropweight`=" + dropweight + ", `refweight`=" + refweight + ", `blood`=" + blood + ", " +
       "`character`=" + character + ", `Psychologicalstress`=" + Psychologicalstress + ", `anxiety`=" + anxiety + ", `Sleepquality`=" + Sleepquality + ", " +
       "`HP`=" + HP + ", `isacceptHP`=" + isacceptHP + ", `HPtreatment`=" + HPtreatment + " " +
                "where identinum='" + identinum + "'";


            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
        }

        //基本情况增改查
        private void Insertnormalsituation(string name,int sex,DateTime birth,string identinum,int Nationality,string Specificnation,
            int education,int profession,int maritalstatus,string phone,string relationship1,string rphone1, string relationship2, string rphone2,
            string relationship3, string rphone3,int Currentresidence,int Rural,int City,int peopletogether,int insurance)
        {
            
            MySqlConnection mysql = getMySqlConnection();
            string sql = "INSERT INTO normalsituation(identinum,name,sex,birth,Nationality,Specificnation,education,profession,maritalstatus,phone," +
                "relationship1,rphone1,relationship2,rphone2,relationship3,rphone3,Currentresidence,Rural,City,peopletogether,insurance)VALUES" + "(" +
                "'" + identinum + "'" + "," +"'" + name + "'" + ","+ sex + "," + "'" + birth + "'" + "," + Nationality +  "," + "'" + Specificnation + "'" + ","+
                 +education + "," + profession + "," + maritalstatus + "," + "'" + phone + "'," + "'" + relationship1 + "'," + "'" + rphone1 + "'," + "'" + relationship2 + "'," +
                 "'" + rphone2 + "'," + "'" + relationship3 + "'," + "'" + rphone3 + "',"+ Currentresidence + "," + Rural + "," + City + ","  + peopletogether + ","  + insurance 
                 + " )";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
        }

        private DataTable Getnormalsituation(string identinum)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from normalsituation where identinum='" + identinum + "'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }

        private void Updatenormalsituation( int sex, DateTime birth, string identinum, int Nationality, string Specificnation,
            int education, int profession, int maritalstatus, string phone, string relationship1, string rphone1, string relationship2, string rphone2,
            string relationship3, string rphone3, int Currentresidence, int Rural, int City, int peopletogether, int insurance)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = " UPDATE normalsituation set sex=" + sex + ",birth=" + "'" + birth + "'" + ",Nationality=" +
                 Nationality + ",Specificnation=" + "'" + Specificnation + "'" + ",education=" + education +  ",profession=" + profession +
                 ",maritalstatus=" + maritalstatus + ",phone=" + "'" + phone + "',relationship1=" + "'" + relationship1 + "',rphone1=" + "'" + rphone1 +
                 "',relationship2=" + "'" + relationship2 + "',rphone2=" + "'" + rphone2 + "',relationship3=" + "'" + relationship3 + "',rphone3=" + 
                 "'" + rphone3 + "',Currentresidence="  + Currentresidence + ",Rural="  + Rural + ",City="  + City + ",peopletogether="  + peopletogether +
                 ",insurance=" + insurance + " where identinum='" + identinum+"'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
          
        }

        //个人史增改查

        private void Insertpersonalhistory(string identinum, int whetherSmoking, int smokingPerDay, int regularSmoking, int regularsmokingbefore, int rgbefore,
          int smokingCessationAge, int yearsOfSmokingCessation, int passiveSmoking, int whetherDrinkingAlcohol, int startRegularDrinkingAge, int regularDrinking, 
          int drinkingAmountEachTime, int drinkingReaction, int eatingRegularly, int eatingSpeed, int eatingHabit, int whetherDrinkTeaRegularly,
          int yearsOfTeaDrinking, int strongOrLightTea)
        {

            MySqlConnection mysql = getMySqlConnection();
            string sql = "INSERT INTO personalhistory (`identinum`, `whetherSmoking`, `smokingPerDay`, `regularSmoking`, `regularsmokingbefore`, `rgbefore`, " +
                "`smokingCessationAge`, `yearsOfSmokingCessation`, `passiveSmoking`, `whetherDrinkingAlcohol`, `startRegularDrinkingAge`, `regularDrinking`," +
                " `drinkingAmountEachTime`, `drinkingReaction`, `eatingRegularly`, `eatingSpeed`, `eatingHabit`, `whetherDrinkTeaRegularly`," +
                " `yearsOfTeaDrinking`,`strongOrLightTea`) " +
                "VALUES ('" + identinum + "', " + whetherSmoking + ", " + smokingPerDay + ", " + regularSmoking + ", " + regularsmokingbefore + ", " + rgbefore + ", " +
                smokingCessationAge + ", " + yearsOfSmokingCessation + ","  + passiveSmoking + "," +  +whetherDrinkingAlcohol + ","  + startRegularDrinkingAge + "," + regularDrinking + "," 
            + drinkingAmountEachTime + ", " + drinkingReaction + ", " + eatingRegularly + ", " + eatingSpeed + ", " + eatingHabit + ", " + whetherDrinkTeaRegularly + ", " 
            + yearsOfTeaDrinking + ", " + strongOrLightTea + ");";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();

        }
        private DataTable Getpersonalhistory(string identinum)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from personalhistory where identinum='" + identinum + "'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }

        private void Updatepersonalhistory(string identinum, int whetherSmoking, int smokingPerDay, int regularSmoking, int regularsmokingbefore, int rgbefore,
         int smokingCessationAge, int yearsOfSmokingCessation, int passiveSmoking, int whetherDrinkingAlcohol, int startRegularDrinkingAge, int regularDrinking,
         int drinkingAmountEachTime, int drinkingReaction, int eatingRegularly, int eatingSpeed, int eatingHabit, int whetherDrinkTeaRegularly,
         int yearsOfTeaDrinking, int strongOrLightTea)
        {

            MySqlConnection mysql = getMySqlConnection();
           
            string sql = "UPDATE personalhistory  " +
                "SET  `whetherSmoking`=" + whetherSmoking + ", `smokingPerDay`=" + smokingPerDay + ", `regularSmoking`=" + regularSmoking +
                ", `regularsmokingbefore`=" + regularsmokingbefore + ", " + "`rgbefore`=" + rgbefore + ", `smokingCessationAge`=" + smokingCessationAge +
                ", `yearsOfSmokingCessation`=" + yearsOfSmokingCessation + ", `passiveSmoking`=" + passiveSmoking  + ", `whetherDrinkingAlcohol`=" + whetherDrinkingAlcohol +
                ", `startRegularDrinkingAge`=" + startRegularDrinkingAge  + ", `regularDrinking`=" + regularDrinking + ", `drinkingAmountEachTime`=" + drinkingAmountEachTime  +
                ", `drinkingReaction`=" + drinkingReaction + ", " + "`eatingRegularly`=" + eatingRegularly + ", `eatingHabit`=" + eatingHabit + 
                ", `whetherDrinkTeaRegularly`=" + whetherDrinkTeaRegularly + ", `yearsOfTeaDrinking`=" + yearsOfTeaDrinking  + ", `strongOrLightTea`=" + strongOrLightTea  +
                " where identinum='" + identinum + "'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();

        }

        //家庭史
        private DataTable Getfamilyhistory(string identinum)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from familyhistory where identinum='" + identinum + "'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        private DataTable Getrelationtumor(string identinum)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from relationtumor where identinum='" + identinum +"';";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }

        private DataTable Getrelationtumors(string identinum, string kinshipnum)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from relationtumor where identinum='" + identinum +
                "' and kinshipnum='" + kinshipnum + "';";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }


        private int Ckeckrelationtumor(string identinum,string kinshipnum)
        {
            int a=0;
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select id from relationtumor where identinum='" + identinum + "' and"+
                " kinshipnum='"+kinshipnum+"'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        a = reader.GetInt32("id");

                    }
                }

            }
            catch
            {
               
            }
            finally
            {
                mysql.Close();
            }
            mysql.Close();
            
            return a;
        }
        private void Insertfamilyhistroy(string identinum, int numofson, int numofdaughter, int numofbro, int numofsister, int hasTumor)
        {
            
            MySqlConnection mysql = getMySqlConnection();
            string sql = "INSERT INTO familyhistory (`identinum`, `numofson`, `numofdaughter`, `numofbro`, `numofsister`, `hasTumor`) " +
                "VALUES ('" + identinum + "', " + numofson + ", " + numofdaughter + ", " + numofbro + ", " + numofsister + ", " + hasTumor + ");";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
           
        }
        private void Insertrelationtumor(string identinum, string kinshipnum, string kindship, int sex, int Gastrolcancer, string othercancer)
        {
            
            MySqlConnection mysql = getMySqlConnection();
            string sql = "INSERT INTO relationtumor(identinum,kinshipnum,kindship,sex,Gastrolcancer,othercancer)VALUES" + "(" +
                "'" + identinum + "'" + "," + "'" + kinshipnum + "'" + "," + "'" + kindship + "'" + "," + sex + "," + Gastrolcancer + "," + "'" + othercancer + "')";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
          
        }
        private void Updatefamilyhistroy(string identinum, int numofson, int numofdaughter, int numofbro, int numofsister, int hasTumor)
        {
           
            MySqlConnection mysql = getMySqlConnection();

            string sql = "UPDATE familyhistory " +
                          "SET `numofson`=" + numofson + ", `numofdaughter`=" + numofdaughter + ", `numofbro`=" + numofbro + ", `numofsister`=" + numofsister + ", `hasTumor`=" + hasTumor + " " +
                          " where identinum='" + identinum + "'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            
        }
        private void Updaterelationtumor(string identinum, string kinshipnum, string kindship, int sex, int Gastrolcancer, string othercancer)
        {
            
            MySqlConnection mysql = getMySqlConnection();
            string sql = "UPDATE relationtumor set kinshipnum=" + "'" + kinshipnum + "',kindship='" + kindship + "',sex=" + sex + ",Gastrolcancer=" + Gastrolcancer + ",othercancer='" + othercancer + "'"+
                " where identinum='" + identinum + "' and kinshipnum='"+ kinshipnum+"'"; 
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            
            
        }

        //疾病史
        private DataTable Getsymptoms(string identinum)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from symptoms where identinum='" + identinum + "'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        private DataTable Getmedicalhistory(string identinum)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from medicalhistory where identinum='" + identinum + "'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        private void Insertmedicalhistory(string identinum, int Numendoscopy, int numofhp,
        int hypert, string time1, int Hyperlipidemia, string time2, int codisease, string time3, int apoplexy, string time4,
        int Diabetes, string time5, int chronichepatitis, string time6, int Pulmonarytuberculosis, string time7, int other, string time8)
        {
            
            MySqlConnection mysql = getMySqlConnection();
            string sql = "INSERT INTO medicalhistory " +
                "(`identinum`, `Numendoscopy`, `numofhp`, `hypert`, `time1`, `Hyperlipidemia`, `time2`, `codisease`, `time3`, `apoplexy`, `time4`, `Diabetes`, `time5`, `chronichepatitis`, `time6`, `Pulmonarytuberculosis`, `time7`, `other`, `time8`) " +
                "VALUES ('" + identinum + "', " + Numendoscopy + ", " + numofhp + ", " + hypert + ", '" + time1 + "', " + Hyperlipidemia + ", '" + time2 + "', " +
            codisease + ",' " + time3 + "', " + apoplexy + ", '" + time4 + "', " + Diabetes + ", '" + time5 + "', " + chronichepatitis + ", '" + time6 + "'," +
            Pulmonarytuberculosis + ", '" + time7 + "', " + other + ", '" + time8 + "');";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            
        }

        private void Updatemedicalhistory(string identinum, int Numendoscopy, int numofhp,
        int hypert, string time1, int Hyperlipidemia, string time2, int codisease, string time3, int apoplexy, string time4,
        int Diabetes, string time5, int chronichepatitis, string time6, int Pulmonarytuberculosis, string time7, int other, string time8)
        {
            
            MySqlConnection mysql = getMySqlConnection();

            string sql = "UPDATE medicalhistory " +
                "SET `Numendoscopy`=" + Numendoscopy + ", `numofhp`=" + numofhp + ", `hypert`=" + hypert + ", `time1`='" + time1 + "', `Hyperlipidemia`=" + Hyperlipidemia + ", " +
       "`time2`='" + time2 + "', `codisease`=" + codisease + ", `time3`='" + time3 + "', `apoplexy`=" + apoplexy + ", `time4`='" + time4 + "', `Diabetes`=" + Diabetes + ", `time5`='" + time5 + "', `chronichepatitis`=" + chronichepatitis + ", " +
       "`time6`='" + time6 + "', `Pulmonarytuberculosis`=" + Pulmonarytuberculosis + ", `time7`='" + time7 + "', `other`=" + other + ", `time8`='" + time8 + "' " +
       "  where identinum='" + identinum + "'";

            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
           
        }

        private void Insertsymptoms(string identinum, int symptomstype, int symptomscore)
        {
            
            MySqlConnection mysql = getMySqlConnection();
            string sql = "INSERT INTO symptoms(identinum,symptomstype,symptomscore)VALUES" + "(" +
                "'" + identinum + "'" + "," + symptomstype + "," + symptomscore + ")";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            
        }
        private void Updatesymptoms(string identinum, int symptomstype, int symptomscore)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql = "UPDATE symptoms set symptomscore=" + symptomscore+
                " where identinum='"+identinum+ "'and symptomstype="+ symptomstype;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
           
        }
        //F
        private DataTable Getrecentdrugsituation(string identinum)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from recentdrugsituation where identinum='" + identinum + "'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        private void Insertrecentdrugsituation(string identinum, int Aspirin, int AspirinCR, int NonSteroidalAntiInflammatory, int Glucocorticoids, int GlucocorticoidsCR, int Antibiotics, int Tincture, int PPI)
        {
            
            MySqlConnection mysql = getMySqlConnection();
            string sql = "INSERT INTO recentdrugsituation(identinum,Aspirin,AspirinCR,NonSteroidalAntiInflammatory,Glucocorticoids,GlucocorticoidsCR,Antibiotics,Tincture,PPI)VALUES" + "(" +
                "'" + identinum + "'" + "," + Aspirin + "," + AspirinCR + "," + NonSteroidalAntiInflammatory + "," + Glucocorticoids + "," + GlucocorticoidsCR + "," + Antibiotics + "," + Tincture + "," + PPI + " )";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            
        }
        private void Updaterecentdrugsituation(string identinum, int Aspirin, int AspirinCR, int NonSteroidalAntiInflammatory, int Glucocorticoids, int GlucocorticoidsCR, int Antibiotics, int Tincture, int PPI)
        {
            
            MySqlConnection mysql = getMySqlConnection();
            string sql = "UPDATE recentdrugsituation set Aspirin=" + Aspirin + ",AspirinCR=" + AspirinCR + 
                ",NonSteroidalAntiInflammatory=" + NonSteroidalAntiInflammatory + 
                ",Glucocorticoids=" + Glucocorticoids + ",GlucocorticoidsCR=" + GlucocorticoidsCR + 
                ",Antibiotics=" + Antibiotics + ",Tincture=" + Tincture + ",PPI=" + PPI+
                " where identinum='"+identinum+"'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
           
        }
        //G
        private DataTable Getsurvey(string identinum)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from survey where identinum='"+ identinum+"'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        private void Insertsurvey(string identinum, int surveyobject, int surveybelieve, string surveyend, int surveyspendtime, int surveyevaluate)
        {
           
            MySqlConnection mysql = getMySqlConnection();
            string sql = "INSERT INTO survey(identinum,surveyobject,surveybelieve,surveyend,surveyspendtime,surveyevaluate)VALUES" + "(" +
                "'" + identinum + "'," + surveyobject + "," + surveybelieve + "," + "'" + surveyend + "'" + "," + surveyspendtime + "," + surveyevaluate + " )";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            
        }
        private void Updatesurvey(string identinum, int surveyobject, int surveybelieve, string surveyend, int surveyspendtime, int surveyevaluate)
        {
            
            MySqlConnection mysql = getMySqlConnection();
            string sql = "UPDATE survey set surveyobject=" + surveyobject + ",surveybelieve=" + surveybelieve + ",surveyend="
                + "'" + surveyend + "'" + ",surveyspendtime=" + surveyspendtime + ",surveyevaluate=" + surveyevaluate +
                " where identinum='"+identinum+"'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
           
        }



        private int Checkid(string num)
        {
            int rawId = 0;
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select id from normalsituation where identinum=" + "'" + num + "'", mysql);
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
    }
}
