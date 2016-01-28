using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using System.Text.RegularExpressions;

namespace Rental
{
    public class PersonRepository : IPersonRepository
    {
        // Repo is a singlton, make data consistancy easier and more practical
        private static PersonRepository instance;
        private IList<Person> _personList = new List<Person>();

        private PersonRepository() { }

        public static PersonRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PersonRepository();
                }
                return instance;
            }
        }

        private void LoadData()
        {
            using (ISession nhibernateSession = NHibernateService.OpenSession())
            {
                IQuery query = nhibernateSession.CreateQuery(
                "from Employee as ws order by ws.Surname asc");
                _personList = query.List<Person>();
            }
        }

        public int Count()
        {
            return _personList.Count;
        }

        public Person Get(int id)
        {
            return _personList.Where(p => p.Id == id).SingleOrDefault();
        }

        public Person Get(Person person)
        {
            return _personList.Where(p => p.Equals(person)).SingleOrDefault();
        }

        public Person GetByIndex(int index)
        {
            if (index < _personList.Count)
                return _personList.ElementAt(index);
            else
                return null;
        }

        public IList<Person> GetAll()
        {
            return _personList;
        }

        public void Add(Person person)
        {
            _personList.Add(person);
        }

        public void Remove(int id)
        {
           // _personList.Remove(p => p.Id == id);
        }

        public void Remove(Person person)
        {
            //_personList.RemoveAll(p => p.Equals(person));
        }

        public void Clear()
        {
            _personList.Clear();
        }

        public bool Contains(Person person)
        {
            return _personList.Any(p => p.Equals(person));
        }

        public void Update(int id, Person person)
        {
            //_personList.RemoveAll(p => p.Id == id);
            _personList.Add(person);
        }
    }
}
