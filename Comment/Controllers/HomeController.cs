using Comment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Comment.Controllers
{
    public class HomeController : Controller
    {
        List<User> Users = new List<User>();
        List<Comment_text> Comments = new List<Comment_text>();

        public ActionResult Index()
        {
            Set_UserAndComment();

            ViewBag.Users = Users;

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

        #region 初始化用户和评论
        /// <summary>
        /// 初始化用户 && 评论
        /// </summary>
        public void Set_UserAndComment()
        {
            User user = new User();
            user.name = "用户1";
            user.value = "1";
            Users.Add(user);
            user = new User();
            user.name = "用户2";
            user.value = "2";
            Users.Add(user);
            user = new User();
            user.name = "用户3";
            user.value = "3";
            Users.Add(user);
            user = new User();
            user.name = "用户4";
            user.value = "4";
            Users.Add(user);
            user = new User();
            user.name = "用户5";
            user.value = "5";
            Users.Add(user);

            Comment_text comment = new Comment_text();
            comment.value = "我说的第一句话1";
            Comments.Add(comment);
            comment = new Comment_text();
            comment.value = "我说的第一句话2";
            Comments.Add(comment);
            comment = new Comment_text();
            comment.value = "我说的第一句话3";
            Comments.Add(comment);
            comment = new Comment_text();
            comment.value = "我说的第一句话4";
            Comments.Add(comment);
            comment = new Comment_text();
            comment.value = "我说的第一句话5";
            Comments.Add(comment);
            comment = new Comment_text();
            comment.value = "我说的第一句话6";
            Comments.Add(comment);
        }
        
        #endregion

        public void Add_Comment()
        {
           
        }
        



    }
}