namespace EGHeals.Models.Dtos.Users.Requests
{
    public class UpdateSubUserRequestDto
    {
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "Username is required.")]
        //[MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
        //[MaxLength(150, ErrorMessage = "Username cannot exceed 150 characters.")]
        public string Username { get; set; } = default!;

        //[DataType(DataType.Password)]
        //[Required(ErrorMessage = "Password is required.")]
        //[MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        //[MaxLength(150, ErrorMessage = "Password cannot exceed 150 characters.")]
        public string Password { get; set; } = default!;

        //[DataType(DataType.Password)]
        //[Required(ErrorMessage = "Confirm password is required.")]
        //[Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
