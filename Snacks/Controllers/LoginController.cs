using Core.Services;
using Infrastructure.Models;
using Snack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snack.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(User pUser)
        {
            try
            {
                User user = null;
                ServicesUser servicesUsers = new ServicesUser();
                user = servicesUsers.GetUser(pUser);
                if (user != null)
                {
                    Session["User"] = user;
                    if (user.Accredited == 1)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    
                }
                TempData["Login"] = "Incorrect user data!";
                return View("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Logout(User usuario)
        {
            try
            {
                Log.Info("Usuario desconectado!");
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
