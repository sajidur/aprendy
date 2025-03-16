using Apprendi.Application.Features.Users;

namespace Apprendi.Web.Client.Extensions
{
    public static class UserExtensions
    {
        private const int TutorRoleId = 2;
        private const int StudentRoleId = 3;

        public static bool IsTutor(this UserDto role)
        {
            return role.RoleIds.Contains(TutorRoleId);
        }

        public static bool IsStudent(this UserDto role)
        {
            return role.RoleIds.Contains(StudentRoleId);
        }
    }
}
