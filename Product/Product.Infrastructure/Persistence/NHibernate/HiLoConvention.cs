namespace Product.Infrastructure.Persistence.NHibernate
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;
    public class HiLoConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            var column = Util.GetTableName(instance.EntityType.Name) + "_id";
            instance.Column(column);
            instance.GeneratedBy.Native();
            //instance.GeneratedBy.HiLo("ids", "next_high", "9", "entity_name = '" + instance.EntityType.Name + "'");
        }
    }
}
