using FluentNHibernate.Mapping;

namespace dotnetcoreNHibernateSqlite.DbModel
{
    public class Person
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }

        public override string ToString()
        {
            return Name + " : " + Age;
        }
    }

    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Not.LazyLoad();
            Id(e => e.Id);
            Map(e => e.Name).Not.Nullable().Length(64);
            Map(e => e.Age).Not.Nullable();

            Table("Persons");
        }
    }
}
