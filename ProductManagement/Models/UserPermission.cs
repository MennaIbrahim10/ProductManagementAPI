using ProductManagement.Authorization;

namespace ProductManagement.Models
{
    public class UserPermission
    {
        public Permission PermissionId { get; set; }
        public int UserId { get; set; }

        //Navigation
        public User User { get; set; }
    }
}
