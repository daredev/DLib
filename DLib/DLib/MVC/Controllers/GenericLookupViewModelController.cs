using System;
using System.Web.Mvc;
using DLib.Model;

namespace DLib.MVC.Controllers
{
    public abstract class GenericLookupViewModelController<TViewModelType, TModelType> : Controller 
        where TViewModelType : GenericLookupViewModel, new()
        where TModelType : GenericLookupModel, new()
    {
        private GenericLookupModelDataService<TModelType, TViewModelType> dataService;

        private string BrowsingUser;


        public void SetBrowsingUser(string user)
        {
            BrowsingUser = user;
        }

        public string GetBrowsingUser()
        {
            return BrowsingUser;
        }

        protected GenericLookupViewModelController()
        {
            try
            {
                dataService = new GenericLookupModelDataService<TModelType, TViewModelType>();
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Couldn't initialize service. Check if you have ConnectionString in your project named: DefaultConnectionString. Exception Error: {0}", e.Message));
            }
        }

        public GenericLookupViewModelController(string user) : this()
        {
            SetBrowsingUser(user);
        }

        public virtual ActionResult Index()
        {
            var model = dataService.Read();
// ReSharper disable Mvc.ViewNotResolved
            return View("Index",model);
// ReSharper restore Mvc.ViewNotResolved
        }

        public virtual ActionResult Details(int id = 0)
        {
            TViewModelType lookup = dataService.GetByID(id);
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
        public virtual ActionResult Create(TViewModelType lookup)
        {
            if (ModelState.IsValid)
            {
                dataService.Create(lookup, BrowsingUser);
                
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
            TViewModelType lookup = dataService.GetByID(id);
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
        public virtual ActionResult Edit(TViewModelType lookup)
        {
            if (ModelState.IsValid)
            {
                dataService.Update(lookup, BrowsingUser);
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
            TViewModelType lookup = dataService.GetByID(id);
            if (lookup == null)
            {
                return HttpNotFound();
            }
// ReSharper disable Mvc.ViewNotResolved
            return View("Delete",lookup);
// ReSharper restore Mvc.ViewNotResolved
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            TViewModelType lookup = dataService.GetByID(id);

            dataService.Delete(lookup);

// ReSharper disable Mvc.ActionNotResolved
            return RedirectToAction("Index");
// ReSharper restore Mvc.ActionNotResolved
        }

        protected override void Dispose(bool disposing)
        {
            dataService.Dispose();
            base.Dispose(disposing);
        }

    }
}