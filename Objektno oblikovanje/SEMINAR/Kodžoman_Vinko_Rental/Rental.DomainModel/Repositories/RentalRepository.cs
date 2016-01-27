using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class RentalRepository
    {
        // Repo is a singlton, make data consistancy easier and more practical
        private static RentalRepository instance;
        private static List<Rental> _rentableList = new List<Rental>();

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

        public int Count()
        {
            return _rentableList.Count;
        }

        public Rental Get(int id)
        {
            return _rentableList.Where(p => p.Id == id).SingleOrDefault();
        }

        public Rental Get(Rental rental)
        {
            return _rentableList.Where(p => p.Equals(rental)).SingleOrDefault();
        }

        public List<Rental> GetAll()
        {
            return _rentableList;
        }

        public Rental GetByIndex(int index)
        {
            if (index < _rentableList.Count)
                return _rentableList.ElementAt(index);
            else
                return null;
        }

        public void Add(Rental rental)
        {
            _rentableList.Add(rental);
        }

        public void Remove(int id)
        {
            _rentableList.RemoveAll(p => p.Id == id);
        }

        public void Remove(Rental rental)
        {
            _rentableList.RemoveAll(p => p.Equals(rental));
        }

        public void Clear()
        {
            _rentableList.Clear();
        }

        public bool Contains(Rental rental)
        {
            return _rentableList.Any(p => p.Equals(rental));
        }

        public void Update(int id, Rental rental)
        {
            _rentableList.RemoveAll(p => p.Id == id);
            _rentableList.Add(rental);
        }
    }
}
