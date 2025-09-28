using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using Marten;
using Api.Models;
using Marten.Services;

namespace Api.GraphQL.Queries;
[QueryType]
public class CalculationQuery
{
    public IQueryable<AlgebraCalculation> GetCalculations(IDocumentSession session)
    {
        return session.Query<AlgebraCalculation>();
    }
}