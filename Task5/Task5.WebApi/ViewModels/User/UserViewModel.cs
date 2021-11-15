using System.Collections.Generic;

namespace Task5.WebApi.ViewModels.User
{
    public class UserViewModel : BaseUserViewModel
    {
        public IEnumerable<string> Role { get; set; }
    }
}
