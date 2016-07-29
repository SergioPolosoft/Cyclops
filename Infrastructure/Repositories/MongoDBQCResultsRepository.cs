using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using QCEvaluation.Domain.Events;
using QCRoutine.Domain;

namespace Infrastructure.Repositories
{
    public class MongoDBQCResultsRepository : IQCResultsRepository
    {
        private readonly IMongoDatabase database;

        private IMongoCollection<QCResult> Collection
        {
            get { return this.database.GetCollection<QCResult>("QCResults"); }
        }

        public MongoDBQCResultsRepository()
        {
            var client = new MongoClient();
            this.database = client.GetDatabase("QCRoutine");
        }

        public IEnumerable<QCResult> GetResultsOrderedByDate(int numberOfResults)
        {
            return Collection.Find(x => true).Sort(new SortDefinitionBuilder<QCResult>().Descending(x=>x.MeasuredTime)).Limit(numberOfResults).ToList();
        }

        public void Add(QCResult qcResult)
        {
            Collection.InsertOneAsync(qcResult);
        }
    }
}