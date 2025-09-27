namespace EGHeals.Domain.Models
{
    public class Role : SystemAggregate<RoleId>
    {
        private readonly List<RolePermission> _permissions = new();
        public IReadOnlyList<RolePermission> Permissions => _permissions.AsReadOnly();

        public string Name { get; private set; } = default!;
        public static Role Create(RoleId id, string name)
        {
            //Domain model validation
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, 3);

            var role = new Role
            {
                Id = id,
                Name = name,
            };

            return role;
        }

        public void AddPermission(RolePermissionType rolePermissionType)
        {
            if (!Enum.IsDefined<RolePermissionType>(rolePermissionType))
            {
                throw new DomainException("rolePermissionType value is out of range");
            }

            var permission = new RolePermission(Id, rolePermissionType);

            _permissions.Add(permission);
        }
        public void RemovePermission(RolePermissionType rolePermissionType)
        {
            if (!Enum.IsDefined<RolePermissionType>(rolePermissionType))
            {
                throw new DomainException("rolePermissionType value is out of range");
            }

            var permission = _permissions.FirstOrDefault(x => x.RolePermissionType == rolePermissionType);

            if (permission is not null)
            {
                _permissions.Remove(permission);
            }
        }
    }
}
