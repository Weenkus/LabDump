using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class RentalRepository : IRentalRepository
    {
        // Repo is a singlton, make data consistancy easier and more practical
        private static RentalRepository instance;
        private static IList<Rental> _rentableList = new List<Rental>();

        private RentalRepository() { }

        public static RentalRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RentalRepository();
                }
                return instance;
            }
        }

        private void LoadData()
        {
            using (var session = NHibernateService.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    _rentableList = session.CreateCriteria(typeof(Rental)).List<Rental>();
                }
            }
        }

        public int Count()
        {
            LoadData();
            return _rentableList.Count;
        }

        public Rental Get(int id)
        {
            LoadData();
            return _rentableList.Where(p => p.Id == id).SingleOrDefault();
        }

        public Rental Get(Rental rental)
        {
            LoadData();
            return _rentableList.Where(p => p.Equals(rental)).SingleOrDefault();
        }

        public IList<Rental> GetAll()
        {
            LoadData();
            return _rentableList;
        }

        public Rental GetByIndex(int index)
        {
            LoadData();
            if (index < _rentableList.Count)
                return _rentableList.ElementAt(index);
            else
                return null;
        }

        public void Add(Rental rental)
        {
            using (var session = NHibernateService.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(rental);
                    transaction.Commit();
                }
            }
        }

        public void Remove(int id)
        {
            if (_rentableList.Any(x => x.Id == id))
            {
                using (var session = NHibernateService.SessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {

                        session.Delete(_rentableList.Where(x => x.Id == id).SingleOrDefault());
                        transaction.Commit();
                    }
                }
            }
        }

        public void Remove(Rental rental)
        {
            if (_rentableList.Any(x => x.Id == rental.Id))
            {
                using (var session = NHibernateService.SessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {

                        session.Delete(_rentableList.Where(x => x.Id == rental.Id).SingleOrDefault());
                        transaction.Commit();
                    }
                }
            }
        }

        public void Clear()
        {
            LoadData();
            _rentableList.Clear();
        }

        public bool Contains(Rental rental)
        {
            LoadData();
            return _rentableList.Any(p => p.Equals(rental));
        }

        public void Update(Rental rental)
        {
            using (var session = NHibernateService.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {

                    session.SaveOrUpdate(rental);
                    transaction.Commit();
                }
            }
        }
    }
}
