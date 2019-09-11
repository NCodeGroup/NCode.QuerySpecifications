using System.Collections.Generic;

namespace NCode.QuerySpecifications.TestModels
{
	public class Person
	{
		public int PersonId { get; set; }

		public string Name { get; set; }

		public List<Post> AuthoredPosts { get; set; }

		public List<Blog> OwnedBlogs { get; set; }
	}
}