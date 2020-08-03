using GraphQL;
using GraphQL.Types;
using Zappr.Api.GraphQL.Mutations;
using Zappr.Api.GraphQL.Queries;

namespace Zappr.Api.GraphQL
{
    public class ZapprSchema : Schema
    {
        public ZapprSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ZapprQuery>();
            Mutation = resolver.Resolve<ZapprMutation>();
        }
    }
}
