using LoginDemoServer.Models;
namespace LoginDemoServer.DTO
{
    public class UsersDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Name { get; set; }

        public Models.Users ToModelsUser()
        {
            return new Models.Users() { Email = Email, Password = Password, PhoneNumber = PhoneNumber, BirthDate = BirthDate, Name = Name };
        }

        public UsersDTO() { }
        public UsersDTO(Models.Users modelUser)
        {
            this.PhoneNumber = modelUser.PhoneNumber;
            this.Name = modelUser.Name;
            this.Email = modelUser.Email;
            this.Password = modelUser.Password;
            this.BirthDate = modelUser.BirthDate;
        }
    }
}
