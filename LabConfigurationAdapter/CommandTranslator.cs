using System;
using System.Linq;
using ApplicationServices;
using InstrumentCommunication.Application.Commands;
using LabConfiguration.Application;
using LabConfiguration.Application.Commands;

namespace LabConfigurationAdapter
{
    public class CommandTranslator
    {
        private readonly ParameterConfirmationCommand command;

        public CommandTranslator(ParameterConfirmationCommand command)
        {
            this.command = command;
        }

        private const string ParameterTransactionNode = "ParameterTransaction";
        private const string TransactionAttribute = "Transaction";

        public ICommand Translate()
        {
            ICommand returnValue;
            if (IsAnInstallationCommand)
            {                
                returnValue = new ConfirmApplicationInstallationCommand(new ApplicationDTO(ApplicationCode));
            }
            else if (IsDeletionConfirmation)
            {
                returnValue = new ConfirmApplicationDeletionCommand(ApplicationCode);
            }
            else if (IsUpdateConfirmation)
            {
                returnValue = new ConfirmApplicationUpdateCommand(ApplicationCode);
            }
            else
            {
                throw new UnknownCommandException();
            }
            return returnValue;
        }

        public int ApplicationCode
        {
            get
            {
                return Convert.ToInt32((from descendant in command.XmlDocument.Descendants() where descendant.Attribute("ApplicationDTO") != null select descendant.Attribute("ApplicationDTO").Value).FirstOrDefault());
            }
        }

        private bool IsAnInstallationCommand
        {
            get { return ContainsParameterTransactionValue("Add"); }
        }

        private bool ContainsParameterTransactionValue(string transactionAttributeValue)
        {
            return
                command.XmlDocument.Descendants()
                    .Any(
                        descendant =>
                            descendant.Name == ParameterTransactionNode &&
                            descendant.Attributes()
                                .Any(x => x.Name == TransactionAttribute && x.Value == transactionAttributeValue));
        }

        private bool IsDeletionConfirmation
        {
            get { return ContainsParameterTransactionValue("Delete"); }
        }

        private bool IsUpdateConfirmation
        {
            get { return ContainsParameterTransactionValue("Update"); }
        }
    }
}