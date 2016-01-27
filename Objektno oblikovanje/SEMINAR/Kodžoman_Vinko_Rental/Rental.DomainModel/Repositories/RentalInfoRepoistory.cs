using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class RentalInfoRepository
    {
        private static RentalInfoRepository instance;
        private List<RentalInformation> _rentalInfoList = new List<RentalInformation>();

        private RentalInfoRepository() { }

        public static RentalInfoRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RentalInfoRepository();
                }
                return instance;
            }
        }

        public int Count()
        {
            return _rentalInfoList.Count;
        }

        public RentalInformation Get(int id)
        {
            return _rentalInfoList.Where(p => p.Id == id).SingleOrDefault();
        }

        public RentalInformation Get(RentalInformation rentalInfo)
        {
            return _rentalInfoList.Where(p => p.Equals(rentalInfo)).SingleOrDefault();
        }

        public List<RentalInformation> GetAll()
        {
            return _rentalInfoList;
        }

        public RentalInformation GetByIndex(int index)
        {
            if (index < _rentalInfoList.Count)
                return _rentalInfoList.ElementAt(index);
            else
                return null;
        }

        public void Add(RentalInformation rentalInfo)
        {
            _rentalInfoList.Add(rentalInfo);
        }

        public void Remove(int id)
        {
            _rentalInfoList.RemoveAll(p => p.Id == id);
        }

        public void Remove(RentalInformation rentalInfo)
        {
            _rentalInfoList.RemoveAll(p => p.Equals(rentalInfo));
        }

        public void Clear()
        {
            _rentalInfoList.Clear();
        }

        public bool Contains(RentalInformation rentalInfo)
        {
            return _rentalInfoList.Any(p => p.Equals(rentalInfo));
        }

        public void Update(int id, RentalInformation rentalInfo)
        {
            _rentalInfoList.RemoveAll(p => p.Id == id);
            _rentalInfoList.Add(rentalInfo);
        }
    }
}
