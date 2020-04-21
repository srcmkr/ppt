using PPT.Shared.Models;
using System;

namespace FakeAPI.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public GroupModel Group { get; set; }

        // just a helper function
        public bool HasPermission(EPermissions perm, bool explizit = false)
        {
            return Group.HasPermission(perm, explizit);
        }

        public ERole Role
        {
            get => Group.Role;
        }

        // nur ein Getter - der Set erfolgt über die Zuweisung der Gruppe
        
    }
}
