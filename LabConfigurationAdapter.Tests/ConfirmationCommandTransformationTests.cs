using System;
using System.Xml;
using InstrumentCommunication.Application.Commands;
using LabConfiguration.Application;
using LabConfiguration.Application.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabConfigurationAdapter.Tests
{
    [TestClass]
    public class ConfirmationCommandTransformationTests
    {
        private const string BaseParameterTransactionString = "<DmMsg><ParameterTransaction Transaction=\"{0}\"> <e801_ApplicationParameter><e801ApplicationParameterHeader ApplicationDTO=\"{1}\"/></e801_ApplicationParameter></ParameterTransaction></DmMsg>";

        private static readonly string InstallationXmlString = string.Format(BaseParameterTransactionString, "Add", "1221");

        private static readonly string UpdateXmlString = string.Format(BaseParameterTransactionString, "Update", "2156");
        private readonly string DeleteXmlString = string.Format(BaseParameterTransactionString, "Delete", "2131");

        [TestMethod]
        public void WhenTheConfirmationCommandIsAConfirmationOfInstalltionItIsConvertedToConfirmInstallationCommand()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(InstallationXmlString);
            var command = new CommandTranslator(new ParameterConfirmationCommand(xmlDocument)).Translate();

            Assert.IsInstanceOfType(command, typeof(ConfirmApplicationInstallationCommand));
        }

        [TestMethod]
        public void WhenTheConfirmationCommandIsAConfirmationOfDeletionItIsConvertedToConfirmDeletionCommand()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<DmMsg>  <ParameterTransaction Transaction=\"Delete\">  </ParameterTransaction></DmMsg>");
            var command = new CommandTranslator(new ParameterConfirmationCommand(xmlDocument)).Translate();

            Assert.IsInstanceOfType(command, typeof(ConfirmApplicationDeletionCommand));
        }

        [TestMethod]
        public void WhenTheConfirmationCommandIsAConfirmationOfUpdateItIsConvertedToConfirmUpdateCommand()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<DmMsg>  <ParameterTransaction Transaction=\"Update\">  </ParameterTransaction></DmMsg>");
            var command = new CommandTranslator(new ParameterConfirmationCommand(xmlDocument)).Translate();

            Assert.IsInstanceOfType(command, typeof(ConfirmApplicationUpdateCommand));
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownCommandException))]
        public void WhenTheConfirmationCommandHasAnEmptyXmlAnExceptionIsThrown()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<DmMsg></DmMsg>");
            var command = new CommandTranslator(new ParameterConfirmationCommand(xmlDocument)).Translate();
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownCommandException))]
        public void WhenTheConfirmationCommandIsUnknownAnExceptionIsThrown()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<DmMsg>  <ParameterTransaction Transaction=\"Install\">  </ParameterTransaction></DmMsg>");
            var command = new CommandTranslator(new ParameterConfirmationCommand(xmlDocument)).Translate();
        }

        [TestMethod]
        public void WhenTheConfirmationCommandIsAnInstallationTheApplicationCodeIsReadFromTheXml()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(InstallationXmlString);
            var command = new CommandTranslator(new ParameterConfirmationCommand(xmlDocument)).Translate();

            Assert.AreEqual(1221,((ConfirmApplicationInstallationCommand)command).ApplicationDTO.TestCode);
        }

        [TestMethod]
        public void WhenTheConfirmationCommandIsAnUpdateTheApplicationCodeIsReadFromTheXml()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(UpdateXmlString);
            var command = new CommandTranslator(new ParameterConfirmationCommand(xmlDocument)).Translate();

            Assert.AreEqual(2156, ((ConfirmApplicationUpdateCommand)command).ApplicationCode);
        }

        [TestMethod]
        public void WhenTheConfirmationCommandIsADeletionTheApplicationCodeIsReadFromTheXml()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(DeleteXmlString);
            var command = new CommandTranslator(new ParameterConfirmationCommand(xmlDocument)).Translate();

            Assert.AreEqual(2131, ((ConfirmApplicationDeletionCommand)command).ApplicationCode);
        }
    }
}
