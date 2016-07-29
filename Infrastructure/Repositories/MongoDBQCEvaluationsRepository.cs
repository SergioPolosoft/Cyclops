using System;
using MongoDB.Driver;
using QCEvaluation.Domain;
using QCEvaluation.Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class MongoDBQCEvaluationsRepository : IEvaluationsRepository
    {
         private readonly IMongoDatabase database;

        private IMongoCollection<Evaluation> Collection
        {
            get { return this.database.GetCollection<Evaluation>("Evaluations"); }
        }

        public MongoDBQCEvaluationsRepository()
        {
            var client = new MongoClient();
            this.database = client.GetDatabase("Evaluation");
        }

        public EvaluationResult GetEvaluationFor(Guid qcResultId)
        {
            return this.Collection.FindSync(x=>x.QCResultId==qcResultId).First().EvaluationResult;
        }

        public void Add(Evaluation evaluation)
        {
            this.Collection.InsertOneAsync(evaluation);
        }
    }
}