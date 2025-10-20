namespace EGHeals.Domain.Models
{
    public class Permission : SystemEntity<PermissionId>
    {
        public string Name { get; set; } = default!;

        /***************************************** Domain Business *****************************************/

        public static Permission Create(PermissionId id, string name)
        {
            //Domain model validation
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Permission name cannot be null, empty, or whitespace.", nameof(name));
            }

            if (name.Length < 3 || name.Length > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(name), name.Length, "Permission name should be in range between 3 and 150 characters.");
            }

            var permission = new Permission
            {
                Id = id,
                Name = name,
            };

            return permission;
        }
    }
}
