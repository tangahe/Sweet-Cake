using Newtonsoft.Json;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projects.Controllers
{
    public class AdminsController : Controller
    {
        public cakeEntities db = new cakeEntities();
        // GET: Admins
        public ActionResult Index()
        {
            return View();
        }
        //获取所有商品 GetAllProducts
        public ActionResult GetAllProducts()
        {
            var data = db.View_Pro.ToList();
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData);

        }
        //商品删除 DeletePro
        [HttpPost]
        public ActionResult DeletePro(int pid)
        {
            var data = db.product.Find(pid);
            db.product.Remove(data);
            db.SaveChanges();
            dynamic sta = new { status = 200, message = "删除成功" };
            return Json(sta);
        }
        //商品查询 Searches
        [HttpPost]
        public ActionResult Searches(string name)
        {
            string Sql = string.Format($"select top 12 * from product where pname like'%{name}%' order by pid desc");
            var data = db.Database.SqlQuery<Models.product>(Sql);
            if (data != null)
            {
                //这是asp.net把字符串Json格式化功能。
                string jsonData = JsonConvert.SerializeObject(data);
                return Content(jsonData);
            }
            else
            {
                return Content("无");
            }

        }
        //商品新增
        public ActionResult AddPro()
        {
            return View();
        }
        //GetCakeClassify
        public ActionResult GetCakeClassify()
        {
            string Sql = string.Format("select top 5 * from classify");
            var data = db.Database.SqlQuery<Models.classify>(Sql);
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData);
        }
        public ActionResult Orders()
        {
            return View();
        }

        //订单
        public ActionResult GetAllOrders()
        {
            var data = db.View_orderss.ToList();
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData);
        }
        //订单查询 SearchesOrder
        [HttpPost]
        public ActionResult SearchesOrder(string name)
        {
            string Sql = string.Format($"select * from View_orderss where pname like'%{name}%' order by pid desc");
            var data = db.Database.SqlQuery<Models.View_orderss>(Sql);
            if (data != null)
            {
                //这是asp.net把字符串Json格式化功能。
                string jsonData = JsonConvert.SerializeObject(data);
                return Content(jsonData);
            }
            else
            {
                return Content("无");
            }

        }
        //新增 Addpro
        [HttpPost]
        public ActionResult Addpro(string name,string pimg, string price,int classify)
        {
            product p = new product()
            {
                pname = name,
                pimg = pimg,
                pprice = decimal.Parse(price),
                pclassify=classify
            };

            db.product.Add(p);
            db.SaveChanges();

            dynamic sta = new { status = 200, message = "成功" };
            return Json(sta);
        }


        public ActionResult Users()
        {
            return View();
        }
        //用户 GetAllUsers
        public ActionResult GetAllUsers()
        {
            string Sql = string.Format("select * from users");
            var data = db.Database.SqlQuery<Models.users>(Sql);
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData);
        }
        //查询 SearchesUsers
        [HttpPost]
        public ActionResult SearchesUsers(string name)
        {
            string Sql = string.Format($"select * from users where uaccount like'%{name}%'");
            var data = db.Database.SqlQuery<Models.users>(Sql);
            if (data != null)
            {
                //这是asp.net把字符串Json格式化功能。
                string jsonData = JsonConvert.SerializeObject(data);
                return Content(jsonData);
            }
            else
            {
                return Content("无");
            }

        }
        public ActionResult Login()
        {
            return View();
        }
        //登录
        [HttpPost]
        public ActionResult Login(string Uaccount, string pwd)
        {

            var data = db.Admin.FirstOrDefault(i => i.adminName == Uaccount && i.pwd == pwd);
            if (data != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Content("用户或密码错误");
            }
        }
    }
}