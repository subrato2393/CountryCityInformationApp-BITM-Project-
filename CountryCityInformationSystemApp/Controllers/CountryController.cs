using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CountryCityInformationSystemApp.Models;

namespace CountryCityInformationSystemApp.Controllers
{
    public class CountryController : Controller
    {
        private CountryCityDbContext db = new CountryCityDbContext();

        // GET: /Country/
        public ActionResult Index(string searchstring)
        {
            var countrys = from m in db.Countries
                           select m;
            if(!String.IsNullOrEmpty(searchstring))
            {
                countrys = countrys.Where(s=>s.Name.Contains(searchstring));
            }
            
           //List<int> dwellers=new List<int>();
           // var citys = from m in db.Cities
                        
           //             select m.NoOfDwellers;
           // dwellers.AddRange(citys);
           // ViewBag.Total = dwellers.ToList();
            return View(countrys);
        }

        // GET: /Country/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: /Country/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Country/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,About")] Country country)
        {
            var countrys = from m in db.Countries
                           select m.Name;
            //var cityCount = from m in db.Cities
            //                select m;

            if (countrys.Contains(country.Name))
            {
                ViewBag.Message = "Country Name already Exist";
               
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Countries.Add(country);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
              
            }
            return View(countrys);
           
        }

        // GET: /Country/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: /Country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,About")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: /Country/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: /Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = db.Countries.Find(id);
            db.Countries.Remove(country);
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
    }
}
