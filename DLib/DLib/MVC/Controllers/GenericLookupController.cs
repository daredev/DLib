using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DLib.Model;

namespace DLib.MVC.Controllers
{
    public abstract class GenericLookupController<TModelType> : Controller where TModelType : GenericLookupModel
    {
        private readonly DbContext _db;

        private string UserName;

        public DbContext DB
        {
            get { return _db; }
        }


        public DbContext GetModel()
        {
            return _db;
        }

        public void SetUser(string user)
        {
            UserName = user;
        }

        protected GenericLookupController()
        {
            try
            {
                _db =
                    new DbContext(
                        System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionString"]
                            .ConnectionString);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Couldn't initialize service. Check if you have ConnectionString in your project named: \"DefaultConnectionString\". Exception Error: {0}", e.Message));
            }
        }

        public virtual ActionResult Toggle(int id)
        {
            TModelType instance = _db.Set<TModelType>().Find(id);
            instance.ToggleActive();
            instance.SetModified();
            _db.Entry(instance).State = EntityState.Modified;
            _db.SaveChanges();
// ReSharper disable Mvc.ActionNotResolved
            return RedirectToAction("Index");
// ReSharper restore Mvc.ActionNotResolved
        }

        public virtual ActionResult Index()
        {
            var model = _db.Set<TModelType>().ToList();
// ReSharper disable Mvc.ViewNotResolved
            return View("Index",model);
// ReSharper restore Mvc.ViewNotResolved
        }

        public virtual ActionResult Details(int id = 0)
        {
            TModelType lookup = _db.Set<TModelType>().Find(id);
            if (lookup == null)
            {
                return HttpNotFound();
            }
// ReSharper disable Mvc.ViewNotResolved
            return View("Details", lookup);
// ReSharper restore Mvc.ViewNotResolved
        }

        public virtual ActionResult Create()
        {
// ReSharper disable Mvc.ViewNotResolved
            return View("Create");
// ReSharper restore Mvc.ViewNotResolved
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(TModelType lookup)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(UserName))
                    lookup.SetCreated(UserName);
                else
                    lookup.SetCreated();
                
                if (!string.IsNullOrEmpty(UserName))
                    lookup.SetModified(UserName);
                else
                    lookup.SetModified();

                lookup.is_active = true;
                _db.Set<TModelType>().Add(lookup);
                _db.SaveChanges();
// ReSharper disable Mvc.ActionNotResolved
                return RedirectToAction("Index");
// ReSharper restore Mvc.ActionNotResolved
            }

// ReSharper disable Mvc.ViewNotResolved
            return View("Create",lookup);
// ReSharper restore Mvc.ViewNotResolved
        }


        public virtual ActionResult Edit(int id = 0)
        {
            TModelType lookup = _db.Set<TModelType>().Find(id);
            if (lookup == null)
            {
                return HttpNotFound();
            }
// ReSharper disable Mvc.ViewNotResolved
            return View("Edit",lookup);
// ReSharper restore Mvc.ViewNotResolved
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TModelType lookup)
        {
            if (ModelState.IsValid)
            {
                lookup.SetModified();
                _db.Entry(lookup).State = EntityState.Modified;
                _db.SaveChanges();
// ReSharper disable Mvc.ActionNotResolved
                return RedirectToAction("Index");
// ReSharper restore Mvc.ActionNotResolved
            }
// ReSharper disable Mvc.ViewNotResolved
            return View(lookup);
// ReSharper restore Mvc.ViewNotResolved
        }

        public virtual ActionResult Delete(int id = 0)
        {
            TModelType lookup = _db.Set<TModelType>().Find(id);
            if (lookup == null)
            {
                return HttpNotFound();
            }
// ReSharper disable Mvc.ViewNotResolved
            return View("Delete",lookup);
// ReSharper restore Mvc.ViewNotResolved
        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public virtual ActionResult DeleteConfirmed(int id)
//        {
//            TModelType lookup = _db.Set<TModelType>().Find(id);
//            _db.Set<TModelType>().Remove(lookup);
//            _db.SaveChanges();
//// ReSharper disable Mvc.ActionNotResolved
//            return RedirectToAction("Index");
//// ReSharper restore Mvc.ActionNotResolved
//        }

        [HttpGet]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            TModelType lookup = _db.Set<TModelType>().Find(id);
            _db.Set<TModelType>().Remove(lookup);
            _db.SaveChanges();
            // ReSharper disable Mvc.ActionNotResolved
            return RedirectToAction("Index");
            // ReSharper restore Mvc.ActionNotResolved
        }


        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

    }
}