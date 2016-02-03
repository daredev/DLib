using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DLib.Model
{
    public class GenericLookupModelDataService<TModel, TViewModel> : object, IDisposable 
        where TModel : GenericLookupModel, new()
        where TViewModel : GenericLookupViewModel, new()
    {
        private readonly DbContext _db;

        private DbContext entities
        {
            get { return _db; }
        }

        public GenericLookupModelDataService()
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
                throw new Exception(string.Format("Couldn't initialize service. Check if you have ConnectionString in your project named: DefaultConnectionString. Exception Error: {0}", e.Message));
            }
        }

        public virtual IEnumerable<TViewModel> ReadAllActive()
        {
            return Read().Where(x => x.Active).AsEnumerable();
        }

        public virtual IEnumerable<TViewModel> Read()
        {
            return entities.Set<TModel>().Select(product => new TViewModel
            {
                ID = product.id,
                Active = product.is_active,
                Name = product.value,
                CreatedBy = product.created_by,
                CreatedOn = product.created_on,
                ModifiedBy = product.modified_by,
                ModifiedOn = product.modified_on
            }).AsEnumerable();
        }

        public virtual void Create(TViewModel product, string username = "SYSTEM")
        {
            TModel entity = new TModel
            {
                is_active = product.Active,
                value = product.Name,
                created_on = product.CreatedOn,
                created_by = product.CreatedBy,
                modified_on = product.ModifiedOn,
                modified_by = product.ModifiedBy
            };

            if (string.IsNullOrEmpty(entity.created_by))
                entity.SetCreated(username);
            if (string.IsNullOrEmpty(entity.modified_by))
                entity.SetModified(username);

            _db.Set<TModel>().Add(entity);
            _db.SaveChanges();
        }

        public virtual void Update(TViewModel product, string username = "SYSTEM")
        {
            var entity = new TModel
            {
                id = product.ID,
                is_active = product.Active,
                value = product.Name,
                created_by = product.CreatedBy,
                created_on = product.CreatedOn,
                modified_by = product.ModifiedBy,
                modified_on = product.ModifiedOn
            };

            if (string.IsNullOrEmpty(entity.created_by))
                entity.SetCreated(username);
            if (string.IsNullOrEmpty(entity.modified_by))
                entity.SetModified(username);


            _db.Set<TModel>().Attach(entity);
            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public virtual void Delete(TViewModel product)
        {
            var entity = new TModel {id = product.ID};

            entities.Set<TModel>().Attach(entity);
            entities.Set<TModel>().Remove(entity);
            entities.SaveChanges();
        }

        public virtual void Deactivate(TViewModel product, string username = "SYSTEM")
        {
            TViewModel deactivated = product;
            deactivated.Active = false;
            Update(deactivated, username);
        }

        public virtual void Activate(TViewModel product, string username = "SYSTEM")
        {
            TViewModel deactivated = product;
            deactivated.Active = true;
            Update(deactivated, username);
        }

        public virtual TViewModel GetByID(int ID)
        {
            var item = entities.Set<TModel>().Find(ID);
            var result = new TViewModel
            {
                ID = item.id,
                Active = item.is_active,
                CreatedBy = item.created_by,
                CreatedOn = item.created_on,
                ModifiedBy = item.modified_by,
                ModifiedOn = item.modified_on,
                Name = item.value
            };
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool really)
        {
            if (really)
                entities.Dispose();
        }
        
    }
}
