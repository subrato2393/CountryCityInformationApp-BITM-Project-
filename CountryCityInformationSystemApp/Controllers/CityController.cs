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
    public class CityController : Controller
    {
        private CountryCityDbContext db = new CountryCityDbContext();

        // GET: /City/
        //public ActionResult Index(string searchByCountry)
        //{
        //    var countrys = from m in db.Countries
        //                   select m.Name;
        //    //if(!String.IsNullOrEmpty(searchByCountry))
        //    //{
        //    //    citys=citys.Where(m=>m.
        //    //}
        //    var cities = db.Cities.Include(c => c.Country);
        //    return View(countrys);
        //}
        //[HttpPost]
        public ActionResult Index(string searchString, string searchByCountry)
        {
           //string searchValue;
            var countryList = new List<string>();
            var citys = from m in db.Cities
                        select m;
            var countrys = from m in db.Countries
                           orderby m.Name
                           select m.Name;
            countryList.AddRange(countrys.Distinct());
            
                          
                    ViewBag.SearchByCountry = countryList;

                    if (!String.IsNullOrEmpty(searchByCountry))
                    {
                        citys = citys.Where(m=>m.Country.Name.Contains(searchByCountry));
                    }
            if (!String.IsNullOrEmpty(searchString))
            {
                citys = citys.Where(m => m.Name.Contains(searchString));
            }
            citys = db.Cities.Include(x => x.Country);
            return View(citys.ToList());
        }

        // GET: /City/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: /City/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        // POST: /City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        //[ActionName("Create")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CityID,Name,About,NoOfDwellers,Location,Weather,CountryId")] City city)
        {
            var citys = from m in db.Cities
                        select m.Name;
            var countrys = from m in db.Countries
                           select m.Cities;
            int count = countrys.Count();
            if (citys.Contains(city.Name))
            {
                ViewBag.Message = "City Name Already Exist";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Cities.Add(city);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", city.CountryId);
            return View(city);
        }

        // GET: /City/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", city.CountryId);
            return View(city);
        }

        // POST: /City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CityID,Name,About,NoOfDwellers,Location,Weather,CountryId")] City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", city.CountryId);
            return View(city);
        }

        // GET: /City/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: /City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            City city = db.Cities.Find(id);
            db.Cities.Remove(city);
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
