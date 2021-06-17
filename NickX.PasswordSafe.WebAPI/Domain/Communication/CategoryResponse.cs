using NickX.PasswordSafe.WebAPI.Domain.Models;

namespace NickX.PasswordSafe.WebAPI.Domain.Communication
{
    public class CategoryResponse : BaseResponse
    {
        public Category Category { get; private set; }

        private CategoryResponse(bool isSuccess, string message, Category category) : base(isSuccess, message)
        {
            Category = category;
        }

        public CategoryResponse(Category category) : this(true, string.Empty, category)
        { }

        public CategoryResponse(string message) : this(false, message, null)
        { }
    }
}
