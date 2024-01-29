using System.ComponentModel.DataAnnotations;

namespace Bai2.Request
{
    public class CreateCategoryRequest
    {

        [Required, StringLength(50)]

        public string Name { get; set; }
    }
}
