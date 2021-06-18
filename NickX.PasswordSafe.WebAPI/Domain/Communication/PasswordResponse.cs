using NickX.PasswordSafe.Models.Main;

namespace NickX.PasswordSafe.WebAPI.Domain.Communication
{
    public class PasswordResponse : BaseResponse
    {
        public Password Password { get; private set; }

        private PasswordResponse(bool isSuccess, string message, Password password) : base(isSuccess, message)
        {
            Password = password;
        }

        public PasswordResponse(Password password) : this(true, string.Empty, password)
        { }

        public PasswordResponse(string message) : this(false, message, null)
        { }
    }
}
