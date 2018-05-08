using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using scrc.Models;
using System.Data.Entity;

namespace scrc.Controllers
{
    public class BooksController : Controller
    {
        private BooksDBEntities bookEntity = new BooksDBEntities();

        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View(bookEntity.Books.ToList());
            }
            else
            {
                return RedirectToAction("LogIn", "Users");
            }
        }

        public ActionResult Create()
        {

            return View();
        } 

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult Create([Bind(Exclude = "Id")] Book newBook)

        {

            if (!ModelState.IsValid)

                return View();

            bookEntity.Books.Add(newBook);

            bookEntity.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var bookToEdit = (from b in bookEntity.Books
                              where b.Id == id
                              select b).First();
            return View(bookToEdit);
        }

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult Edit(Book bookToEdit)

        {

            var originalBook = (from b in bookEntity.Books

                                where b.Id == bookToEdit.Id

                                select b).First();

            if (!ModelState.IsValid)
            {
                return View(originalBook);
            }

            bookEntity.Entry(originalBook).State = EntityState.Modified;
            originalBook.Title = bookToEdit.Title;
            originalBook.Author = bookToEdit.Author;
            originalBook.DatePublished = originalBook.DatePublished;

            bookEntity.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            var bookToDelete = (from b in bookEntity.Books
                              where b.Id == id
                              select b).First();
            return View(bookToDelete);
        }

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult Delete(Book bookToDelete)
        {
            //bookEntity.Books.Remove(bookToDelete);
            bookEntity.Entry(bookToDelete).State = EntityState.Deleted;
            bookEntity.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var book = (from b in bookEntity.Books
                               where b.Id == id
                               select b).First();
           
            return View(book);
        }

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult Details(Book bookToShow)
        {
            return RedirectToAction("Index");
        }
    }
}