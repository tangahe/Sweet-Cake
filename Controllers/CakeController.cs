using Newtonsoft.Json;
using Projects.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Projects.Controllers
{
    public class CakeController : Controller
    {

        public cakeEntities db = new cakeEntities();
        public string userid = "";

        // GET: Cake
        //首页
        public ActionResult Index()
        {
            return View();
        }
        //获取头像
        public ActionResult Index1()
        {

            int id = int.Parse(Session["id"].ToString());
            var data = db.users.FirstOrDefault(i => i.uid == id);
            Session["img"] = data.uimage.ToString();
            return Content(data.uimage.ToString());


        }
        //热销商品ShowHotCake
        public ActionResult ShowHotCake()
        {

            var data = db.View_hot.ToList();
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData);
        }
        //所有产品
        public ActionResult Products()
        {
            return View();
        }
        //所有蛋糕
        [HttpGet]
        public ActionResult GetProducts()
        {
            string Sql = string.Format("select top 12 * from product order by pid desc");
            var data = db.Database.SqlQuery<Models.product>(Sql);
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData);
        }
        //加入购物车 AddCart1
        [HttpPost]
        public ActionResult AddCart1(int pid)
        {
            //添加
            cart cart = new cart();
            cart.pid = pid;
            cart.size = 1;
            cart.quantity = 1;
            cart.uid = int.Parse(Session["id"].ToString());

            db.cart.Add(cart);
            db.SaveChanges();
            dynamic sta = new { status = 200, message = "新增成功" };
            return Json(sta);
        }
        //通过分类查询
        [HttpPost]
        public ActionResult GetClassify(int classify)
        {
            string Sql = string.Format($"select top 12 * from product where pclassify={classify} order by pid desc");
            var data = db.Database.SqlQuery<Models.product>(Sql);
            //var list = db.classify.ToList();
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData);
        }
        //查询
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
        //下一页NextPage
        [HttpPost]
        public ActionResult NextPage(string pnum)
        {
            int num = int.Parse(pnum);
            string Sql = string.Format($"select top 12 * from product where pid <= {num} order by pid desc");
            var data = db.Database.SqlQuery<Models.product>(Sql);
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);

            return Content(jsonData);
        }
        //上一页PrevPage
        [HttpPost]
        public ActionResult PrevPage(string pnum)
        {
            int num = int.Parse(pnum);
            string Sql = string.Format($"select top 12 * from product where pid <= {num} order by pid desc");
            var data = db.Database.SqlQuery<Models.product>(Sql);
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);

            return Content(jsonData);
        }
        //关于我们
        public ActionResult About()
        {
            return View();
        }

        //详情
        public ActionResult Details()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Details1(int id)
        {
            string Sql = string.Format($"select * from product where pid={id}");
            var data = db.Database.SqlQuery<Models.product>(Sql);
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData);
        }
        //尺寸选择（GetCakeSize）
        public ActionResult GetCakeSize()
        {
            string Sql = string.Format("select * from size");
            var data = db.Database.SqlQuery<Models.size>(Sql);
            string jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData);
        }
        //详情界面的相似蛋糕
        [HttpPost]
        public ActionResult SimilarCake(int cid)
        {
            string Sql = string.Format($"select top 4 * from product where pclassify={cid}");
            var data = db.Database.SqlQuery<Models.product>(Sql);
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData);
        }
        [HttpPost]
        //查询购物车有没有该商品 SelectCart
        public ActionResult SelectCart(int pid)
        {
            var uid = int.Parse(Session["id"].ToString());
            string Sql = string.Format($"select * from cart where pid={pid} and uid={uid}");
            var data = db.Database.SqlQuery<Models.cart>(Sql);

            if (data.Count() > 0)
            {
                return Content("有");
            }
            else
            {
                return Content("无");
            }
        }
        //查询有没有登录 SelectUserInfo
        public ActionResult SelectUserInfo()
        {
            if (Session["id"]==null)
            {
                return Content("无");
            }
            else
            {
                return Content("有");
            }
        }

        //加入购物车AddCart
        [HttpPost]
        public ActionResult AddCart(int pid, int size, int q)
        {
            //添加
            cart cart = new cart();
            cart.pid = pid;
            cart.size = size;
            cart.quantity = q;
            cart.uid = int.Parse(Session["id"].ToString());

            db.cart.Add(cart);
            db.SaveChanges();
            dynamic sta = new { status = 200, message = "新增成功" };
            return Json(sta);
        }

        //购物车
        public ActionResult Cart()
        {
            return View();
        }
        //购物车显示
        public ActionResult Cart1()
        {
            int id = int.Parse(Session["id"].ToString());
            //string Sql = string.Format($"select * from product p,cart c where c.uid={id} and c.pid=p.pid");
            //var data = db.Database.SqlQuery<Models.product>(Sql);
            var data = db.CartView.Where(c => c.uid == id).ToList();
            //这是asp.net把字符串Json格式化功能。
            string jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData);
        }
        //结算
        [HttpPost]
        public ActionResult OnBalance(orders order, int? cid)
        {
            if (order != null && cid != null)
            {
                //添加到订单表
                order.o_picktime = DateTime.Now;
                db.orders.Add(order);
                //删除购物车的商品
                var cart = db.cart.Find(cid);
                db.cart.Remove(cart);
                db.SaveChanges();
                return Content("200");
            }
            return Content("400");
        }
        //购物车删除DeleteCart
        [HttpPost]
        public ActionResult DeleteCart(int cid)
        {
            var data = db.cart.FirstOrDefault(c => c.cid == cid);
            if (data != null)
            {
                db.cart.Remove(data);
                db.SaveChanges();
                dynamic sta = new { status = 200, message = "成功" };
                return Json(sta);
            }
            else
            {
                return Content("失败");
            }
        }
        //获取头像 Showimg
        public ActionResult Showimg()
        {
            
            if (Session["id"]==null)
            {
                return Content("无");
            }var uid = int.Parse(Session["id"].ToString());
            var data = db.users.FirstOrDefault(u => u.uid == uid);
            if (data != null)
            {
                return Content(data.uimage);
            }
            else
            {
                return Content("无");
            }

        }

        //修改头像
        [HttpPost]
        public ActionResult ChangeImg(string img)
        {
            if (!string.IsNullOrEmpty(img) && Session["id"] != null)
            {
                var uid = int.Parse(Session["id"].ToString());
                var data = db.users.Find(uid);
                if (data!=null)
                {
                    data.uimage = img;
                    db.SaveChanges();
                    return Content("200");
                }
            }
            return Content("400");
        }
        //个人中心
        public ActionResult Personal()
        {
            return View();
        }
        // 修改用户头像


        //获取个人信息 GetUsersInfo
        public ActionResult GetUsersInfo()
        {
            var uid = int.Parse(Session["id"].ToString());
            string Sql = string.Format($"select * from users where uid={uid}");
            var data = db.Database.SqlQuery<Models.users>(Sql);
            if (data.Count() > 0)
            {
                string jsonData = JsonConvert.SerializeObject(data);
                return Content(jsonData);
            }
            else
            {
                return Content("无");
            }
        }
        //修改个人信息 EditUsers
        [HttpPost]
        public ActionResult EditUsers(string name, string pwd, string phone, string email)
        {
            var uid = int.Parse(Session["id"].ToString());
            var data = db.users.FirstOrDefault(u => u.uid == uid);

            if (data != null)
            {
                data.uaccount = name;
                data.upwd = pwd;
                data.uphone = phone;
                data.uemail = email;
                db.SaveChanges();

                return Content("成功");
            }
            else
            {
                return Content("失败");
            }
        }
        //修改邮箱 EideEmail
        [HttpPost]
        public ActionResult EideEmail(string email)
        {
            var uid = int.Parse(Session["id"].ToString());
            var data = db.users.FirstOrDefault(u => u.uid == uid);

            if (data != null)
            {
                data.uemail = email;
                db.SaveChanges();

                return Content("成功");
            }
            else
            {
                return Content("失败");
            }
        }
        //订单管理GetOrderInfo
        public ActionResult GetOrderInfo()
        {
            var uid = int.Parse(Session["id"].ToString());
            string Sql = string.Format($"select * from View_orders where uid={uid} order by oid");
            var data = db.Database.SqlQuery<Models.View_orders>(Sql);
            if (data.Count() > 0)
            {
                string jsonData = JsonConvert.SerializeObject(data);
                return Content(jsonData);
            }
            else
            {
                return Content("无");
            }
        }
        //订单管理的删除Deleteorders
        [HttpPost]
        public ActionResult Deleteorders(int id)
        {
            var data = db.orders.FirstOrDefault(c => c.oid == id);
            if (data != null)
            {
                db.orders.Remove(data);
                db.SaveChanges();
                dynamic sta = new { status = 200, message = "成功" };
                return Json(sta);
            }
            else
            {
                return Content("失败");
            }
        }
        //地址 GetAddressInfo
        public ActionResult GetAddressInfo()
        {
            var uid = int.Parse(Session["id"].ToString());
            string Sql = string.Format($"select * from View_address where uid={uid}");
            var data = db.Database.SqlQuery<Models.View_address>(Sql);
            if (data.Count() > 0)
            {
                string jsonData = JsonConvert.SerializeObject(data);
                return Content(jsonData);
            }
            else
            {
                return Content("无");
            }
        }
        //地址删除Deleteaddress
        [HttpPost]
        public ActionResult Deleteaddress(int id)
        {
            var data = db.address.FirstOrDefault(c => c.aid == id);
            if (data != null)
            {
                db.address.Remove(data);
                db.SaveChanges();
                dynamic sta = new { status = 200, message = "成功" };
                return Json(sta);
            }
            else
            {
                return Content("失败");
            }
        }
        //地址新增 Addaddress
        [HttpPost]
        public ActionResult Addaddress(string name, string phone, string datail)
        {
            address a = new address()
            {
                name = name,
                phone = phone,
                addressName = datail,
                uid = int.Parse(Session["id"].ToString()),
            };
           
            db.address.Add(a);
            db.SaveChanges();

            dynamic sta = new { status = 200, message = "成功" };
            return Json(sta);

        }
        //邮箱验证
        public ActionResult Register1(string uemail)
        {
            var data = db.users.Where(p => p.uemail == uemail);
            if (data.Count() > 0)
            {
                return Content("失败");
            }
            else
            {
                return Content("成功");

            }
        }
        //注册register
        public ActionResult Register(string upwd, string uemail, string Uaccount)
        {
            var data = db.users.Where(p => p.uemail == uemail);
            if (data.Count() > 0)
            {
                return View();
            }
            else
            {
                //添加
                users user = new users();
                user.uaccount = Uaccount;
                user.upwd = upwd;
                user.uemail = uemail;

                db.users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login");

            }
        }
        //登录
        public ActionResult Login()
        {
            return View();
        }

        //账号登录
        [HttpPost]
        public ActionResult Login(string Uaccount, string pwd)
        {

            var data = db.users.FirstOrDefault(i => i.uaccount == Uaccount && i.upwd == pwd);
            if (data != null)
            {
                Session["id"] = data.uid.ToString();
                Session["img"] = data.uimage.ToString();
                return RedirectToAction("Index");
            }
            else
            {
                return Content("用户或密码错误");
            }
        }
        //账号验证
        public ActionResult Register_Account(string account)
        {
            var data = db.users.FirstOrDefault(i => i.uaccount == account);
            if (data != null)
            {
                return Content("成功");
            }
            else
            {
                return Content("失败");
            }
        }
        //邮箱登录
        [HttpPost]
        public ActionResult loginPass(string uemail)
        {
            var data = db.users.FirstOrDefault(u => u.uemail == uemail);
            if (data != null)
            {
                Session["id"] = data.uid.ToString();
                Session["img"] = data.uimage.ToString();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }
        
        //邮箱验证码
        [HttpPost]
        public ActionResult Login_Emali(string uemail, string email_text)
        {
            string TextBox1 = uemail;
            MailMessage msg = new MailMessage();  //生成一个关于邮箱的类

            msg.To.Add(TextBox1.Trim());   //文本框里的QQ邮箱  （即注册人的邮箱）
            msg.CC.Add(TextBox1.Trim());   //上同

            msg.From = new MailAddress("2776276474@qq.com", "登录验证");   //第一个是发送验证码的QQ  第二个参数是发送的标题

            msg.Subject = "登录验证";
            //标题格式为UTF8格式
            msg.SubjectEncoding = Encoding.UTF8;

            //随机生成6位验证码

            ArrayList MyArray = new ArrayList();
            Random random = new Random();
            string str = null;
            int Nums = 6;
            while (Nums > 0)
            {
                int i = random.Next(1, 9);
                if (MyArray.Count < 6)
                {
                    MyArray.Add(i);
                }
                Nums -= 1;
            }
            for (int j = 0; j <= MyArray.Count - 1; j++)
            {
                str += MyArray[j].ToString();
            }
            msg.Body = str;  //str为6位随机数
                             //内容格式为UTF8格式
            msg.BodyEncoding = Encoding.UTF8;

            SmtpClient client = new SmtpClient();
            //SMTP服务器地址
            client.Host = "smtp.qq.com";
            //SMTP端口，QQ邮箱填写587
            client.Port = 587;
            //启用SSL加密
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential("2776276474@qq.com", "aklddptwnrqwdcdj");    //第一个参数为发送验证码的QQ邮箱     第二个参数为  发送验证码QQ邮箱的授权码  
                                                                                                    //发送邮件
            client.Send(msg);
            return Content(msg.Body.ToString());


        }


    }
}