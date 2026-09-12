namespace ProductManagement.Authorization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionBasedAuthorizationAttribute:Attribute
    {
        public PermissionBasedAuthorizationAttribute(Permission permission)
        {
            Permission = permission;
        }
        public Permission Permission { get; }
    }
}
