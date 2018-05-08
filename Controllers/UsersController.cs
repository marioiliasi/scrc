using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using scrc.Models;
using System.Data.Entity;

namespace scrc.Controllers
{
    public class UsersController : Controller
    {
        private BooksDBEntities db = new BooksDBEntities();

        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View(db.Users.ToList());
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult Create()
        {

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult Create([Bind(Exclude = "Id")] User user)
        {
            if (!ModelState.IsValid)

                return View();

            db.Users.Add(user);
            db.SaveChanges();
            ModelState.Clear();

            //ViewBag.Message = user.username + " successfully registered";

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var user = (from b in db.Users
                              where b.Id == id
                              select b).First();
            return View(user);
        }

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult Edit(User user)

        {

            var lastUser = (from b in db.Users

                                where b.Id == user.Id

                                select b).First();

            if (!ModelState.IsValid)
            {
                return View(lastUser);
            }

            db.Entry(lastUser).State = EntityState.Modified;
            lastUser.username = user.username;
            lastUser.password = user.password;

            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            var userToDelete = (from b in db.Users
                                where b.Id == id
                                select b).First();
            return View(userToDelete);
        }

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult Delete(Book userToDelete)
        {
            foreach (User user in db.Users.ToList())
            {
                if (user.Id == userToDelete.Id)
                {
                    db.Entry(user).State = EntityState.Deleted;
                    db.SaveChanges();
                    break;
                }
            }
            /*db.SaveChanges();
            //bookEntity.Books.Remove(bookToDelete);
            db.Entry(userToDelete).State = EntityState.Deleted;
            db.SaveChanges();*/
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var book = (from b in db.Users
                        where b.Id == id
                        select b).First();

            return View(book);
        }

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult Details(Book bookToShow)
        {
            return RedirectToAction("Index");
        }

        public ActionResult LogIn(User user)
        {
            var usr = db.Users.Where(u => u.username == user.username & u.password == user.password).FirstOrDefault();
            if(usr != null)
            {
                Session["UserID"] = usr.Id.ToString();
                Session["Username"] = usr.username.ToString();
                return RedirectToAction("Index", "Books");
            }
            ModelState.AddModelError("", "Wrong username or password");
            return View();
        }
    }
}