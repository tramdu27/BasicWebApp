using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
       DataContext db = new DataContext();

      
        // GET: User
        public ActionResult Index()
        {
            return View(db.GetUsers());
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            return View(db.GetUsersByID(id));
        }

        
        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(Users users)
        {
            int i = db.SaveUser(users);
            if(i> 0)
            {
                return RedirectToAction("Index");
            }
            else 
            {
                
                return View(); 
            }
           
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {
            Users user = db.GetUsersByID(id);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(Users users)
        {
            int i = db.UpdateUser(users);
            if(i> 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View() ;
            }

        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            Users us = db.GetUsersByID(id);
            return View(us);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            int i = db.DeleteUser(id);
           if(i> 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
      
    }
}
