namespace Product.Infrastructure.Persistence.NHibernate.Mapping
{
    using FluentNHibernate.Mapping;
    using Product.Domain.Entity;

    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id).Column("category_id");
            Map(x => x.Name).Column("name");
            Map(x => x.Description).Column("description");
            
        }
    }
}
