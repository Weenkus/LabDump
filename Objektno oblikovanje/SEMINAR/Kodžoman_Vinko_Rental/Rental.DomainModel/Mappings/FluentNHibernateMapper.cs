using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;

namespace Rental
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.LazyLoad();
            Map(x => x.LastName).Not.LazyLoad();
        }
    }

    public class ClientMap : SubclassMap<Client>
    {
        public ClientMap()
        {
            References(x => x.DedicatedAgent).Nullable().Not.LazyLoad();
        }
    }

    public class EmployeeMap : SubclassMap<Employee>
    {
        public EmployeeMap()
        {
            HasMany(x => x.AdvisingClients).Cascade.All().Not.LazyLoad();
        }
    }

    public class RentalMap : ClassMap<Rental>
    {
        public RentalMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            HasOne(x => x.Owner).Cascade.All().Not.LazyLoad();
            Map(x => x.DailyPrice);
        }
    }

    public class ApartmanMap : SubclassMap<Apartment>
    {
        public ApartmanMap()
        {
            Map(x => x.Address);
            Map(x => x.PostalCode);
            HasMany(x => x.IncludedFeatures).Cascade.All().Not.LazyLoad();
            HasMany(x => x.PayedFeatures).Cascade.All().Not.LazyLoad();
        }
    }

    public class RentalIncludeMap : ClassMap<RentalInclude>
    {
        public RentalIncludeMap()
        {
            Id(x => x.Id);
            Map(x => x.Offer);
            Map(x => x.Number);
        }
    }

    public class SpecialFeaturesMap : ClassMap<SpecialFeatures>
    {
        public SpecialFeaturesMap()
        {
            Id(x => x.Id);
            Map(x => x.Price);
            Map(x => x.Description);
        }
    }

    public class RentalInformationMap : ClassMap<RentalInformation>
    {
        public RentalInformationMap()
        {
            Id(x => x.Id);
            //Map(x => x.To).CustomType<NHibernate.Type.TimeType>(); ;
            //Map(x => x.From).CustomType<NHibernate.Type.TimeType>(); ;
            Map(x => x.DailyCost);
            HasOne(x => x.Rented).Cascade.All().Not.LazyLoad();
            HasOne(x => x.Client).Cascade.All().Not.LazyLoad();
        }
    }


}
