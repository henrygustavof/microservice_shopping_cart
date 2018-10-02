namespace Product.Infrastructure.Persistence.NHibernate.Mapping
{
    using FluentNHibernate.Mapping;
    using Domain.Entity;
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id).Column("product_id");
            Map(x => x.Name).Column("name");
            Map(x => x.Unit).Column("unit");
            Map(x => x.PictureUrl).Column("picture_url");
            Map(x => x.Description).Column("description");
            Component(x => x.Balance, m =>
            {
                m.Map(x => x.Amount, "price");
                m.Map(x => x.Currency, "currency");
            });
            References(x => x.Category);

        }
    }
}
