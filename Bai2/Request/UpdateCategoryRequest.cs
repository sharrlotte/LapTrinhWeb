using System.ComponentModel.DataAnnotations;

namespace Bai2.Request
{
	public class UpdateCategoryRequest
	{
		public int Id { get; set; }

		[Required, StringLength(50)]

		public string Name { get; set; }
	}
}
