using ApplicationServices;

namespace InstrumentCommunication.Application.Commands
{
    public class StoreMainUnitFTPCredentialsCommand : ICommand
    {
        public StoreMainUnitFTPCredentialsCommand(string user, string basket, string password)
        {
            this.User = user;
            this.DataBasket = basket;
        }

        public string User { get; private set; }
        public string DataBasket { get; private set; }
    }
}