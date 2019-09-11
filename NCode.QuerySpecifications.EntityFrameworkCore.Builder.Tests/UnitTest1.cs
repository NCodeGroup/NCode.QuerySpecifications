using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NCode.QuerySpecifications.Builder;
using NCode.QuerySpecifications.TestModels;
using Xunit;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var services = new ServiceCollection();

            services.AddQueryBuilder(config =>
            {
	            config.AddEntityFrameworkCore();
            });

            services.AddDbContext<BloggingContext>(config =>
            {
                config.EnableDetailedErrors();
                config.UseInMemoryDatabase("test");
            });

            using (var serviceProvider = services.BuildServiceProvider())
            {
            }
        }

    }

    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
}