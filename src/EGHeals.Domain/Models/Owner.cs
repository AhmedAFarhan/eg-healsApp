namespace EGHeals.Domain.Models
{
    public class Owner : SystemEntity<OwnerId>
    {
        public string FullName { get; private set; } = default!;
        public string Mobile { get; private set; } = default!;
        public string NationalId { get; private set; } = default!;
        public UserActivity UserActivity { get; private set; } = UserActivity.RADIOLOGY;
        public Gender Gender { get; private set; } = Gender.MALE;
        public SystemUser SystemUser { get; private set; } = default!;

        public static Owner Create(string fullName, string mobile, string nationalId, UserActivity userActivity, Gender gender, SystemUser systemUser)
        {
            //Domain model validation
            Validation(fullName, mobile, nationalId, gender);

            var owner = new Owner
            {
                Id = OwnerId.Of(Guid.NewGuid()),
                FullName = fullName,
                Mobile = mobile,
                NationalId = nationalId,
                UserActivity = userActivity,
                Gender = gender,
                SystemUser = systemUser
            };

            return owner;
        }
        public void Update(string fullName, string mobile, string nationalId, Gender gender)
        {
            //Domain model validation
            Validation(fullName, mobile, nationalId, gender);

            FullName = fullName;
            Mobile = mobile;
            NationalId = nationalId;
            Gender = gender;
        }

        private static void Validation(string fullName, string mobile, string nationalId, Gender gender)
        {
            ArgumentException.ThrowIfNullOrEmpty(fullName);
            ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(fullName.Length, 150);
            ArgumentOutOfRangeException.ThrowIfLessThan(fullName.Length, 3);

            ArgumentException.ThrowIfNullOrEmpty(mobile);
            ArgumentException.ThrowIfNullOrWhiteSpace(mobile);
            ArgumentOutOfRangeException.ThrowIfNotEqual(mobile.Length, 11);

            ArgumentException.ThrowIfNullOrEmpty(nationalId);
            ArgumentException.ThrowIfNullOrWhiteSpace(nationalId);
            ArgumentOutOfRangeException.ThrowIfNotEqual(nationalId.Length, 14);

            if (!Enum.IsDefined<Gender>(gender))
            {
                throw new DomainException("gender value is out of range");
            }
        }
    }
}
