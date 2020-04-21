using FakeAPI.Lib;
using FakeAPI.Models;
using PPT.Shared.Models;
using System;
using System.Collections.Generic;

namespace FakeAPI.Controllers
{
    public class UsersController
    {
        #region Helper zur Erstellung zweier User
        // vollständige Definition der Administratorengruppe mit der Möglichkeit "alles" zu tun
        public static UserModel CreateSomeAdmin()
        {
            var admingroup = new GroupModel
            {
                Id = Guid.Parse("deadbeef-dead-beef-dead-beef00000075"),
                Name = "Administratoren",
                Permissions = new List<EPermissions>
                {
                    EPermissions.can_see_staff_dashboard,
                    EPermissions.wildcard
                }
            };

            return new UserModel
            {
                Id = Guid.NewGuid(),
                Email = "admin@etraining.app",
                Group = admingroup,
                Password = "test123"
            };
        }

        // Verwaltungsgruppe mit differenzierten Berechtigungen (exemplarisch für User CRUD)
        public static UserModel CreateSomeStaff()
        {
            var dispositionGroup = new GroupModel
            {
                Id = Guid.Parse("deadbeef-dead-beef-dead-beefcafed00d"),
                Name = "Lehrgangsverwaltung",
                Permissions = new List<EPermissions>
                {
                    EPermissions.can_see_staff_dashboard,       // Permissionbasierter Rollen-Switcher, für Abfrage einfach CurrentUser.Role nutzen
                    EPermissions.users_create,                  // kann Benutzer erstellen
                    EPermissions.users_delete,                  // kann Benutzer löschen
                    EPermissions.users_list_all,                // kann alle Benutzer auflisten
                    EPermissions.users_edit,                    // kann Benutzer bearbeiten
                }
            };

            return new UserModel
            {
                Id = Guid.NewGuid(),
                Email = "verwaltung@etraining.app",
                Group = dispositionGroup,
                Password = "test123"
            };
        }

        public static UserModel CreateSomeTrainer()
        {
            var trainerGroup = new GroupModel
            {
                Id = Guid.Parse("deadbeef-dead-beef-dead-beefcafed00d"),
                Name = "Trainer Maximus",
                Permissions = new List<EPermissions>
                {
                    EPermissions.can_see_trainer_dashboard,     // Permissionbasierter Rollen-Switcher, für Abfrage einfach CurrentUser.Role nutzen
                    EPermissions.users_list_all,                // kann alle Benutzer auflisten
                }
            };

            return new UserModel
            {
                Id = Guid.NewGuid(),
                Email = "robert@trainingszentrum.de",
                Group = trainerGroup,
                Password = "test123"
            };
        }

        // Verwaltungsgruppe mit differenzierten Berechtigungen (exemplarisch für User CRUD)
        public static UserModel CreateSomeAttendee()
        {
            var attendeeGroup = new GroupModel
            {
                Id = Guid.Parse("deadbeef-dead-beef-dead-beefcafed00d"),
                Name = "Max Mustermann",
                Permissions = new List<EPermissions>
                {
                    EPermissions.can_see_attendee_dashboard,    // Permissionbasierter Rollen-Switcher, für Abfrage einfach CurrentUser.Role nutzen
                    // sonst darf der nutzer nichts
                }
            };

            return new UserModel
            {
                Id = Guid.NewGuid(),
                Email = "max@gmail.com",
                Group = attendeeGroup,
                Password = "test123"
            };
        }

        #endregion

        // Hardcoded list of people and groups
        // normalerweise wird ein action filter genutzt um den Bearer-Token auszulesen und den Benutzer zu laden
        // dieser Actionfilter ist implementiert, da dies aber nur eine FakeAPI ist, wird die Validierung im Methodenbody durchgeführt
        // und der angemeldete Benutzer vom "Frontend" via Parameter übergeben
        [RequirePermission(EPermissions.users_list_all)]
        public List<UserModel> GetCurrentUsers(UserModel currentUser = null)
        {
            if (currentUser.HasPermission(EPermissions.users_list_all))
            {
                var userList = new List<UserModel>
                {
                    CreateSomeAdmin(),
                    CreateSomeStaff()
                };
                return userList;
            } else
            {
                // Berechtigungen sind nicht ausreichend
                return new List<UserModel>();
            }
        }

        public UserModel Login(string username)
        {
            if (username.Equals("admin")) return CreateSomeAdmin();
            if (username.Equals("disposition")) return CreateSomeStaff();

            // wenn login failed wird normalerweise ApiResponse genutzt, hier stattdessen ein leeres Objekt
            return new UserModel();
        }
    }
}
