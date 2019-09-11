using System.Collections.Generic;

namespace NCode.QuerySpecifications.TestModels
{
	public class Tag
	{
		public string TagId { get; set; }

		public List<PostTag> Posts { get; set; }
	}
}