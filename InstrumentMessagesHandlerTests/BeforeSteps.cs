using ApplicationServices;
using Infrastructure;
using Infrastructure.Repositories;
using InstrumentAdapter.Domain;
using InstrumentCommunication.Application;
using InstrumentCommunication.Sender;
using InstrumentCommunication.TsnAdapter;
using LabConfiguration.Adapters;
using LabConfiguration.Adapters.QCEvaluationServiceReference;
using LabConfiguration.Application;
using LabConfiguration.Domain;
using Moq;
using QCConfiguration.Application;
using QCConfiguration.Domain.Repositories;
using QCEvaluation.Adapters;
using QCEvaluation.Application;
using QCEvaluation.Application.Ports;
using QCEvaluation.Domain.Repositories;
using QCRoutine.Application;
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

            IEvaluationsRepository evaluationsRepository = new HardCodedEvaluationsRepository();
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

            IQCResultsRepository qcresultsRepository = new HardCodedQCResultsRepository();
            
            ILogger logger = new ConsoleLogger();

            IQCRoutineServices qcRoutineServices = new QCRoutineServices(evaluationServices, qcresultsRepository, logger, labConfigurationServices);
            ScenarioContext.Current.Set(qcRoutineServices);

            ScenarioContext.Current.Set(new LabConfigurationServicesReference.LabConfigurationServiceClient());

            var instrumentCommunicationServices = new InstrumentCommunicationServices(new HardCodedCommunicationStatusRepository(), tsnAgentAdapter.Object, messageSender.Object, messagesRepository.Object, new LabConfigurationAdapter.LabConfigurationAdapter(labConfigurationServices), qcRoutineServices);

            ScenarioContext.Current.Set(instrumentCommunicationServices);
            
            ScenarioContext.Current.Set(new QCEvaluationServiceClient());
        }
    }
}