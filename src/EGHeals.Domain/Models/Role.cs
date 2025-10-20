namespace EGHeals.Domain.Models
{
    public class Role : SystemAggregate<RoleId>
    {
        private readonly List<RolePermission> _permissions = new();
        public IReadOnlyList<RolePermission> Permissions => _permissions.AsReadOnly();

        public string Name { get; private set; } = default!;
        public RoleType RoleType { get; private set; } = RoleType.NONE;
        public bool IsActive { get; set; } = true;

        /***************************************** Domain Business *****************************************/

        public static Role Create(RoleId id, string name, RoleType roleType, bool isActive = true)
        {
            //Domain model validation
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Role name cannot be null, empty, or whitespace.", nameof(name));
            }

            if (name.Length < 3 || name.Length > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(name), name.Length, "Role name should be in range between 3 and 150 characters.");
            }

            if (!Enum.IsDefined<RoleType>(roleType))
            {
                throw new ArgumentException("Role type value is out of range.", nameof(roleType));
            }

            var role = new Role
            {
                Id = id,
                Name = name,
                RoleType = roleType,
                IsActive = isActive
            };

            return role;
        }

        public void AddPermission(PermissionId permissionId)
        {
            var permission = new RolePermission(Id, permissionId);

            _permissions.Add(permission);
        }
        public void RemovePermission(PermissionId permissionId)
        {
            var permission = _permissions.FirstOrDefault(x => x.PermissionId == permissionId);

            if (permission is not null)
            {
                _permissions.Remove(permission);
            }
        }

        public void Activate()
        {
            if (IsActive)
            {
                throw new DomainException("Role is already activated");
            }

            IsActive = true;
        }
        public void Deactivate()
        {
            if (!IsActive)
            {
                throw new DomainException("Role is already deactivated");
            }

            IsActive = false;
        }
    }
}
