namespace Product.Infrastructure.Persistence.NHibernate
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;
    using FluentNHibernate.Mapping;

    public class OtherConversions : IHasManyConvention, IReferenceConvention
    {
        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.LazyLoad();
            instance.AsBag();
            instance.Cascade.SaveUpdate();
            instance.Inverse();
        }

        public void Apply(IManyToOneInstance instance)
        {
            instance.LazyLoad(Laziness.Proxy);
            instance.Cascade.None();
            instance.Not.Nullable();
        }
    }
}
