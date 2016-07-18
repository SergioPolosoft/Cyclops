using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationServices;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Events;
using QCEvaluation.Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class MongoDBQCRulesRepository:IQCRuleRepository
    {
        private readonly IMongoDatabase _database;

        public MongoDBQCRulesRepository()
        {
            var client = new MongoClient();
            _database = client.GetDatabase("QCEvaluation");

            RegisterClass<QCRuleCommentUpdated>();
            RegisterClass<QCRuleLimitsUpdated>();
            RegisterClass<QCRuleNumberOfControlsUpdated>();
            RegisterClass<QCRuleDeactivated>();
            RegisterClass<QCRuleReactivate>();
        }

        private static void RegisterClass<T>()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof (T)) == false)
            {
                BsonClassMap.RegisterClassMap<T>();
            }
        }

        public QCRule GetRuleByName(string ruleName)
        {
            var results = GetQCRuleEventsWithName(ruleName);
            var ruleId = results.First().QCRuleId;
            return GetRuleById(ruleId);
        }

        private List<QCRuleEvent> GetQCRuleEventsWithName(string ruleName)
        {
            var results = QCRuleEventsCollection.Find(x => ((QCStandardDeviationRuleCreated) x).Name == ruleName).ToList();
            return results;
        }

        private IMongoCollection<QCRuleEvent> QCRuleEventsCollection
        {
            get
            {
                return _database.GetCollection<QCRuleEvent>("QCRulesEvents");
            }
        }

        public void Save(IList<AggreggateEvent> events)
        {
            foreach (var aggreggateEvent in events)
            {
                QCRuleEventsCollection.InsertOneAsync(aggreggateEvent as QCRuleEvent);
            }
            
        }

        public bool ExistARuleWithName(string ruleName)
        {
            return GetQCRuleEventsWithName(ruleName).Count > 0;
        }

        public StandardDeviationRule GetStandardDeviationRuleByName(string ruleName)
        {
            var events = GetAllTheEventsByName(ruleName);
            return new StandardDeviationRule(events);
        }

        private List<QCRuleEvent> GetAllTheEventsByName(string ruleName)
        {
            var events = GetQCRuleEventsWithName(ruleName);
            List<QCRuleEvent> eventsById = null;
            if (events.Count>0)
            {
                eventsById = QCRuleEventsCollection.FindSync(x => x.QCRuleId == events.First().QCRuleId).ToList();    
            }

            return eventsById;
        }

        public QCRule GetRuleById(Guid ruleId)
        {
            var eventsByRuleId = QCRuleEventsCollection.FindSync(x => x.QCRuleId == ruleId).ToList();
            return new StandardDeviationRule(eventsByRuleId);
        }

        public bool ExistsRule(Guid ruleId)
        {
            return QCRuleEventsCollection.FindSync(x => x.QCRuleId == ruleId).ToList().Any();
        }

        public void DeleteRuleByName(string ruleName)
        {
            var events = GetAllTheEventsByName(ruleName);
            if (events!=null && events.Count>0)
            {
                foreach (var qcRuleEvent in events)
                {
                    var @event = qcRuleEvent;
                    QCRuleEventsCollection.FindOneAndDeleteAsync(x => x.Id == @event.Id);
                }    
            }
            
        }
    }
}
