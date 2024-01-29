using Bai2.Models;
using System.ComponentModel.DataAnnotations;

namespace Bai2.Request
{
	public class UpdateProductRequest
	{
		public int Id { get; set; }

		[Required, StringLength(100)]

		public string Name { get; set; }

		[Range(0.01, 10000.00)]

		public decimal Price { get; set; }

		public string Description { get; set; }

		public int CategoryId { get; set; }
	}
}
