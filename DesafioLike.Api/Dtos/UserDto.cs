namespace DesafioLike.Api.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName {get; set;}
        
        private string userName;
        public string UserName
        {
            get { return Email; }
            set { userName = Email; }
        }


    }
}