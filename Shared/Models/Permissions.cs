namespace PPT.Shared.Models
{
    public enum EPermissions
    {
        // default value = 0
        default_permission_do_not_use,

        // The wildcard-permission (only) for administrators
        wildcard,

        // predefined roles
        // CurrentUser.Role enum preferred
        can_see_attendee_dashboard,
        can_see_trainer_dashboard,
        can_see_staff_dashboard,

        // permissions
        users_create,               // C
        users_list_all,             // R
        users_edit,                 // U
        users_delete                // D
    }

    public enum ERole
    {
        Guest,
        Attendee,
        Trainer,
        Staff
    }
}
