namespace Infrastructure.DTOs
{
    public class FTPCredentialsMessageDTO : MessageDTO
    {
        
        public string User { get; set; }

        public string DataBasket { get; set; }
        public string Password { get; set; }
    }
}