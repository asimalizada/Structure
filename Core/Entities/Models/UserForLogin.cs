using Core.Entities.Abstract;

namespace Core.Entities.Models
{
    public class UserForLogin : IModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
