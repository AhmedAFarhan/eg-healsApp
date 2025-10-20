namespace EGHeals.Domain.Models
{
    public class SystemUser : Aggregate<SystemUserId>
    {
        private readonly List<UserRole> _userRoles = new();
        public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();

        public string Username { get; private set; } = default!;
        public string? Email { get; private set; } = default!;
        public string? Mobile { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public UserType UserType { get; set; } = UserType.ADMIN;
        public bool IsActive { get; set; } = true;

        /***************************************** Domain Business *****************************************/

        public static SystemUser Create(SystemUserId id, string username, string? email, string? mobile, string passwordHash, UserType userType, bool isActive = true)
        {
            //Domain model validation
            Validation(username, email, mobile, passwordHash);           

            var user = new SystemUser
            {
                Id = id,
                Username = username,
                Email = email,
                Mobile = mobile,
                PasswordHash = passwordHash,
                UserType = userType,
                IsActive = isActive
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

        public void Activate()
        {
            if (IsActive)
            {
                throw new DomainException("User is already activated");
            }

            IsActive = true;
        }
        public void Deactivate()
        {
            if (!IsActive)
            {
                throw new DomainException("User is already deactivated");
            }

            IsActive = false;
        }

        private static void Validation(string username, string? email, string? mobile, string passwordHash)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null, empty, or whitespace.", nameof(username));
            }

            if (username.Length < 3 || username.Length > 150)
            {
                throw new ArgumentOutOfRangeException(nameof(username), username.Length, "Username should be in range between 3 and 150 characters.");
            }

            if (email is not null)
            {
                if (!email.Contains('@'))
                {
                    throw new ArgumentException("Invalid email address", nameof(email));
                }

                if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("Email cannot be null, empty, or whitespace.", nameof(email));
                }

                if (email.Length < 6 || email.Length > 150)
                {
                    throw new ArgumentOutOfRangeException(nameof(email), email.Length, "Email should be in range between 6 and 150 characters.");
                }
            }

            if (mobile is not null)
            {
                if (string.IsNullOrEmpty(mobile) || string.IsNullOrWhiteSpace(mobile))
                {
                    throw new ArgumentException("Mobile cannot be null, empty, or whitespace.", nameof(mobile));
                }

                if (!mobile.All(char.IsDigit))
                {
                    throw new ArgumentException("Mobile must contain digits only.", nameof(mobile));
                }

                if (mobile.Length != 11)
                {
                    throw new ArgumentOutOfRangeException(nameof(mobile), mobile.Length, "Mobile number must be exactly 11 digits long.");
                }
            }

            if (string.IsNullOrEmpty(passwordHash) || string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Password cannot be null, empty, or whitespace.", nameof(passwordHash));
            }         
        }
    }
}
