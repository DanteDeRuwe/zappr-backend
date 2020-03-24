using GraphQL;
using GraphQL.Types;

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
