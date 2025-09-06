namespace EGHeals.Domain.Models
{
    public class SystemUser : SystemAggregate<SystemUserId>
    {
        private readonly List<UserRole> _userRoles = new();
        public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();

        public string Username { get; private set; } = default!;
        public string? Email { get; private set; } = default!;
        public string? Mobile { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public UserType UserType { get; set; } = UserType.ADMIN;
        public SystemUserId? AdminId { get; set; } = default!;

        public static SystemUser Create(string username, string? email, string? mobile, string passwordHash, UserType userType, SystemUserId? adminId)
        {
            //Domain model validation
            Validation(username, email, mobile, passwordHash);           

            var user = new SystemUser
            {
                Id = SystemUserId.Of(Guid.NewGuid()),
                Username = username,
                Email = email,
                Mobile = mobile,
                PasswordHash = passwordHash,
                UserType = userType,
                AdminId = adminId
            };

            return user;
        }
        public void Update(string username, string? email, string? mobile, string passwordHash)
        {
            //Domain model validation
            Validation(username, email, mobile, passwordHash);

            Username = username;
            Email = email;
            Mobile = mobile;
            PasswordHash = passwordHash;
        }

        public UserRole AddUserRole(RoleId roleId)
        {
            var userRole = new UserRole(Id, roleId);
            _userRoles.Add(userRole);
            return userRole;
        }
        public void RemoveUserRole(RoleId roleId)
        {
            var userRole = _userRoles.FirstOrDefault(x => x.RoleId == roleId);
            if (userRole is not null)
            {
                _userRoles.Remove(userRole);
            }
        }

        private static void Validation(string username, string? email, string? mobile, string passwordHash)
        {
            ArgumentException.ThrowIfNullOrEmpty(username);
            ArgumentException.ThrowIfNullOrWhiteSpace(username);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(username.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(username.Length, 3);

            if(email is not null)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(email);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(email.Length, 150);
                ArgumentOutOfRangeException.ThrowIfLessThan(username.Length, 6);
            }

            if (mobile is not null)
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(mobile);
                ArgumentOutOfRangeException.ThrowIfNotEqual(mobile.Length, 11);
            }

            ArgumentException.ThrowIfNullOrEmpty(passwordHash);
            ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);            
        }
    }
}
