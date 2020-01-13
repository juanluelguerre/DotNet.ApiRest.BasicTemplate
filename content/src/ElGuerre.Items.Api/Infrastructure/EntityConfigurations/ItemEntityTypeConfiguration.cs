using ElGuerre.Items.Api.Application.Models;
using ElGuerre.Items.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElGuerre.Items.Api.Infrastructure.EntityConfigurations
{
	internal class ItemEntityTypeConfiguration
		: IEntityTypeConfiguration<ItemEntity>
	{
		public void Configure(EntityTypeBuilder<ItemEntity> builder)
		{
			builder.ToTable("Items");

			builder.HasKey(ci => ci.Id);

			builder.Property(ci => ci.Id)			   
			   .ForSqlServerUseSequenceHiLo("catalog_brand_hilo")
			   .IsRequired();

			builder.Property(cb => cb.Name)
				.IsRequired()
				.HasMaxLength(100);
		}
	}
}
