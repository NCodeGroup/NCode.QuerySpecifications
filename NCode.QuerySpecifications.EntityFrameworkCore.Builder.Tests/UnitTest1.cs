using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NCode.QuerySpecifications.Builder;
using Xunit;

namespace NCode.QuerySpecifications.EntityFrameworkCore.Builder.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var services = new ServiceCollection();

            services.AddQueryBuilder()
                .AddEntityFrameworkCore();

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

    public class Person
    {
        public int PersonId { get; set; }

        public string Name { get; set; }

        public List<Post> AuthoredPosts { get; set; }

        public List<Blog> OwnedBlogs { get; set; }
    }

    public class Blog
    {
        public int BlogId { get; set; }

        public string Url { get; set; }

        public int? Rating { get; set; }

        public List<Post> Posts { get; set; }

        public int OwnerId { get; set; }

        public Person Owner { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }

        public int AuthorId { get; set; }

        public Person Author { get; set; }

        public List<PostTag> Tags { get; set; }
    }

    public class PostTag
    {
        public int PostTagId { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public string TagId { get; set; }

        public Tag Tag { get; set; }
    }

    public class Tag
    {
        public string TagId { get; set; }

        public List<PostTag> Posts { get; set; }
    }
}