using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaglikOcagi.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace SaglikOcagi.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        //Singleton Patterrn:Uygulamanın tek connection üzerinden işlen yapmasını sağlayan tasarım desenidir.
        private static DB_SaglikMerkeziEntities context;
        public static DB_SaglikMerkeziEntities Context
        {
            get
            {
                context = context ?? new DB_SaglikMerkeziEntities();
                return context;
            }
            set { context = value; }
        }
        public void Delete(T obj)
        {
            Context.Set<T>().Attach(obj);
            Context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
        }

        public void DeleteById(object id, T obj)
        {
            Context.Set<T>();
        }

        public T GetById(int bid)
        {
            return Context.Set<T>().Find(bid);
        }

        public void Insert(T obj)
        {
            Context.Set<T>().Add(obj);
        }

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }

        public IEnumerable<T> SelectAll()
        {
            return Context.Set<T>().ToList();
        }

        public void Update(T obj)
        {          
            Context.Set<T>().AddOrUpdate(obj);
            //Context.Entry(obj).State = EntityState.Modified;        
        }
     
        public tbl_Hekim GetByHekimID(int id)
        {
            tbl_Hekim hekim = Context.tbl_Hekim.SingleOrDefault(t => t.HekimID == id);
            return hekim;
        }

        public tbl_Hemsire GetByHemsireID(int id)
        {
            tbl_Hemsire hemsire = Context.tbl_Hemsire.SingleOrDefault(t => t.HemsireID == id);
            return hemsire;
        }

        public tbl_Kullanici GetByUserID(int id)
        {
            tbl_Kullanici user = Context.tbl_Kullanici.SingleOrDefault(t => t.KullaniciID == id);
            return user;
        }

        public tbl_Adres GetByAddressID(int id)
        {
            tbl_Adres address = Context.tbl_Adres.SingleOrDefault(t => t.AdresID == id);
            return address;
        }
        public tbl_Anasayfa GetByHomepageID(int id)
        {
            tbl_Anasayfa homepage = Context.tbl_Anasayfa.SingleOrDefault(t => t.AnasayfaID == id);
            return homepage;
        }

        public tbl_AsiTakvimi GetByTakvimID(int id)
        {
            tbl_AsiTakvimi asiTakvimi = Context.tbl_AsiTakvimi.SingleOrDefault(t => t.AsiTakvimID == id);
            return asiTakvimi;
        }

        public tbl_MesaiCizelge GetByCizelgeID(int id)
        {
            tbl_MesaiCizelge mesaiCizelge = Context.tbl_MesaiCizelge.SingleOrDefault(t => t.CizelgeID == id);
            return mesaiCizelge;
        }
        public tbl_Galeri GetByGalleryID(int id)
        {
            tbl_Galeri gallery = Context.tbl_Galeri.SingleOrDefault(t => t.GaleriID == id);
            return gallery;
        }

        public tbl_Linkler GetByLinkID(int id)
        {
            tbl_Linkler linkler = Context.tbl_Linkler.SingleOrDefault(t => t.LinkID == id);
            return linkler;
        }
    }
}
