using Comment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Comment.Controllers
{
    public class HomeController : Controller
    {
        List<User> Users = new List<User>();
        List<Comment_text> Comments = new List<Comment_text>();

        Database_ database = new Database_();

        public ActionResult Index()
        {
            string sql = "SELECT * FROM [User]";
            DataTable dt_user = database.GetTableBySql(sql);
            List<List<User>> list_user = new List<List<Models.User>>();
            for (int i = 0; i < dt_user.Rows.Count; i+=5)
            {
                List<User> Users_pagelist = new List<User>();
                for (int j = i; j < i + 5; j++)
                {
                    if (j >= dt_user.Rows.Count)
                    {
                        break;
                    }
                    User User_one = new Models.User();
                    User_one.name = dt_user.Rows[j]["UserName"].ToString();
                    User_one.value = int.Parse(dt_user.Rows[j]["UserId"].ToString());

                    Users_pagelist.Add(User_one);
                    Users.Add(User_one);
                }
                list_user.Add(Users_pagelist); 
            }


            ViewBag.Users = list_user;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 初始化用户和评论
        /// </summary>
        public void Set_UserAndComment()
        {
            string sql = "select * from [User]";
            DataTable dt_user = database.GetTableBySql(sql);

            for (int i = 0; i < dt_user.Rows.Count; i ++)
            {
                User User_one = new Models.User();
                User_one.name = dt_user.Rows[i]["UserName"].ToString();
                User_one.value = int.Parse(dt_user.Rows[i]["UserId"].ToString());
                Users.Add(User_one);
            }

            sql = "select * from [CommentText]";
            DataTable dt_comment = database.GetTableBySql(sql);

            for (int i = 0; i < dt_comment.Rows.Count; i += 5)
            {
                Comment_text comment_one = new Comment_text();
                comment_one.value = dt_comment.Rows[i]["CommentText"].ToString();
                Comments.Add(comment_one);
            }
        }

        /// <summary>
        /// 添加评论--未实现
        /// </summary>
        public void Add_Comment()
        {
           
        }

        /// <summary>
        /// 返回一条评论和用户
        /// </summary>
        /// <returns></returns>
        public JsonResult returnUserAndComment()
        {
            if(Users.Count==0){
                Set_UserAndComment();
            }
            Random ran = new Random();
            int index = ran.Next(0, Users.Count-1);
            string UserName = Users[index].name.ToString();
            string UserValue = Users[index].value.ToString();

            index = ran.Next(0, Comments.Count - 1);
            string CommentValue = Comments[index].value.ToString();

            string returnjson = "[{     'UserName': '"+UserName+"'    ,'UserValue': '"+UserValue+"'    ,'CommentValue': '"+CommentValue+"'  }] ";

            return Json(returnjson);
        }

        /// <summary>
        /// 返回一条评论
        /// </summary>
        /// <returns></returns>
        public JsonResult returnOneComment()
        {
            if (Comments.Count == 0)
            {
                Set_UserAndComment();
            }
            Random ran = new Random();

            int index = ran.Next(0, Comments.Count - 1);
            string CommentValue = Comments[index].value.ToString();

            string returnjson = "[{  'CommentValue': '" + CommentValue + "'  }] ";

            return Json(returnjson);
        }

        /// <summary>
        /// 返回一个时间
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public JsonResult returnTime(string url)
        {
            DateTime dt_now = DateTime.Now;
            GetpageTime getpagetime = new GetpageTime();
            DateTime dt_page = getpagetime.returnTime(url);

            DateTime dateTimeMin = Convert.ToDateTime(dt_page);
            DateTime dateTimeMax = Convert.ToDateTime(dt_now);
            dateTimeMax = dateTimeMax.AddDays(3);

            TimeSpan ts = dateTimeMax - dateTimeMin;
            DateTime rTime = DateTime.Now;
            do
            {
                Random r = new Random();
                //int t1 = r.Next(0, (int)ts.TotalDays);
                int t2 = r.Next(0, (int)ts.TotalHours);
                //int t3 = r.Next(0, (int)ts.TotalMinutes);
                //int t4 = r.Next(0, (int)ts.TotalDays);

                DateTime newDT = dateTimeMin.Add(new TimeSpan(0, t2, 0, 0));

                Random random = new Random((int)(DateTime.Now.Ticks));

                int hour = random.Next(7, 22);
                int minute = random.Next(0, 60);
                int second = random.Next(0, 60);
                string tempStr = string.Format("{0} {1}:{2}:{3}", newDT.ToString("yyyy-MM-dd"), hour, minute, second);
                rTime = Convert.ToDateTime(tempStr);
            }
            while (rTime >= dateTimeMax || rTime <= dateTimeMin);
            

            string returnjson = "[{  'Time': '" + rTime + "'  }] ";
            return Json(returnjson);
        }


    }
}