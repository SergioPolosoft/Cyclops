using ApplicationServices;
using Infrastructure.Repositories;
using InstrumentAdapter.Domain;
using InstrumentCommunication.Application;
using InstrumentCommunication.Application.Ports;
using InstrumentCommunication.Sender;
using InstrumentCommunication.TsnAdapter;
using InstrumentMessagesHandlerTests.Service_References.LabConfigurationServicesReference;
using InstrumentMessagesHandlerTests.Service_References.QCRoutineServicesReference;
using LabConfiguration.Adapters;
using LabConfiguration.Adapters.Service_References.QCEvaluationServiceReference;
using LabConfiguration.Application;
using LabConfiguration.Domain;
using Moq;
using QCConfiguration.Application;
using QCConfiguration.Domain.Repositories;
using QCEvaluation.Adapters;
using QCEvaluation.Application;
using QCEvaluation.Application.Ports;
using QCEvaluation.Domain.Repositories;
using QCRoutine.Adapter;
using TechTalk.SpecFlow;
using IQCResultsRepository = QCRoutine.Domain.IQCResultsRepository;
using LabConfigurationService = LabConfiguration.Application.LabConfigurationService;

namespace InstrumentMessagesHandlerTests
{
    [Binding]
    public class BeforeSteps
    {
        [BeforeScenario()]
        public static void InittializeQCConfiguration()
        {
            ScenarioContext.Current.Set(new MongoDBQCRulesRepository() as IQCRuleRepository);

            IQCApplicationRepository qcapplicationRepository = new MongoDBQCApplicationRepository();
            ScenarioContext.Current.Set(qcapplicationRepository);

            var hardCodedConfigurationRepository = new HardCodedConfigurationRepository();
            ScenarioContext.Current.Set(hardCodedConfigurationRepository as IConfigurationRepository);

            var hardCodedApplicationRepository = new MongoDBApplicationRepository();
            ScenarioContext.Current.Set(hardCodedApplicationRepository as IApplicationRepository);

            IEvaluationsRepository evaluationsRepository = new MongoDBQCEvaluationsRepository();
            ScenarioContext.Current.Set(evaluationsRepository);

            QCEvaluation.Domain.Repositories.IQCResultsRepository qcResultsEvaluationRepository = new HardCodedQCResultsEvaluationRepository();
            ScenarioContext.Current.Set(qcResultsEvaluationRepository);

            IQualityControlRepository qualityControlRepository = new MongoDBQCRepository();
            ScenarioContext.Current.Set(qualityControlRepository);

            IQCConfigurationServices configurationServices = new QCConfigurationServices(qualityControlRepository);
            ScenarioContext.Current.Set(configurationServices);

            IQCConfigurationServicesPort configurationServicesAdapter = new QCConfigurationServicesAdapter();

            IQCEvaluationServices evaluationServices = new QCEvaluationServices(new MongoDBQCRulesRepository(),
                qcapplicationRepository, evaluationsRepository, qcResultsEvaluationRepository, configurationServicesAdapter);
            ScenarioContext.Current.Set(evaluationServices);

            var labConfigurationServices = new LabConfigurationService(hardCodedConfigurationRepository, hardCodedApplicationRepository, new QCEvaluationAdapter());
            ScenarioContext.Current.Set(labConfigurationServices as ILabConfigurationServices);

            var tsnAgentAdapter = new Mock<ITSNAdapter>();
            ScenarioContext.Current.Set(tsnAgentAdapter);

            var messageSender = new Mock<IMessageSender>();
            ScenarioContext.Current.Set(messageSender);

            var messagesRepository = new Mock<IMessagesRepository>();

            IQCResultsRepository qcresultsRepository = new MongoDBQCResultsRepository();
            ScenarioContext.Current.Set(qcresultsRepository);

            ILogger logger = new ConsoleLogger();

            var qcRoutineServices = new QCRoutineServiceClient();
            ScenarioContext.Current.Set(qcRoutineServices);

            ScenarioContext.Current.Set(new LabConfigurationServiceClient());

            var qcRoutineServicesAdapter = new QCRoutineServicesAdapter();
            ScenarioContext.Current.Set(qcRoutineServicesAdapter as IQCRoutineServicePort);

            var instrumentCommunicationServices = new InstrumentCommunicationServices(new HardCodedCommunicationStatusRepository(), tsnAgentAdapter.Object, messageSender.Object, messagesRepository.Object, new LabConfigurationAdapter.LabConfigurationAdapter(labConfigurationServices), qcRoutineServicesAdapter);

            ScenarioContext.Current.Set(instrumentCommunicationServices);
            
            ScenarioContext.Current.Set(new QCEvaluationServiceClient());
        }
    }
}