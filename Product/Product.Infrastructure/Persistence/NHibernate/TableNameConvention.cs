namespace Product.Infrastructure.Persistence.NHibernate
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;

    public class TableNameConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            instance.Table(Util.GetTableName(instance.EntityType.Name));
        }
    }
}
