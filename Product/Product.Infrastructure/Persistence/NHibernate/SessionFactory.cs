using NHibernate;

namespace Product.Infrastructure.Persistence.NHibernate
{
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using System.Reflection;
    using FluentNHibernate.Conventions.Helpers;
    using FluentNHibernate.Conventions.AcceptanceCriteria;

    public class SessionFactory
    {
        private readonly ISessionFactory _sessionFactory;

        public SessionFactory(string connectionString)
        {
            _sessionFactory = BuildSessionFactory(connectionString);
        }

        private static ISessionFactory BuildSessionFactory(string connectionString)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(
                        ForeignKey.EndsWith("_id"),
                        ConventionBuilder.Property.When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set),
                            x => x.Not.Nullable()))
                    .Conventions.Add<OtherConversions>()
                    .Conventions.Add<TableNameConvention>()
                    .Conventions.Add<HiLoConvention>()
                );

            return configuration.BuildSessionFactory();
        }

        internal ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
