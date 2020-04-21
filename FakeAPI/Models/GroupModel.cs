using PPT.Shared.Models;
using System;
using System.Collections.Generic;

namespace FakeAPI.Models
{
    public class GroupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<EPermissions> Permissions { get; set; }

        // just a helper function
        public bool HasPermission(EPermissions perm, bool explizit = false)
        {
            if (explizit)
            {
                return Permissions.Contains(perm);
            } else
            {
                return Permissions.Contains(EPermissions.wildcard) || Permissions.Contains(perm);
            }   
        }

        public ERole Role
        {
            get
            {
                if (HasPermission(EPermissions.can_see_attendee_dashboard, true)) return ERole.Attendee;
                if (HasPermission(EPermissions.can_see_trainer_dashboard, true)) return ERole.Trainer;
                if (HasPermission(EPermissions.can_see_staff_dashboard, true)) return ERole.Staff;
                return ERole.Guest;
            }
        }

        // Constructor, so Permissions is never null
        public GroupModel()
        {
            Permissions = new List<EPermissions>();
        }
    }
}
