using GraphQL.Server;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Zappr.Api.GraphQL;
using Zappr.Api.GraphQL.Mutations;
using Zappr.Api.GraphQL.Queries;
using Zappr.Api.GraphQL.Types;
using Zappr.Api.GraphQL.Types.InputTypes;
using Zappr.Application.GraphQL.Helpers;

namespace Zappr.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ISchema, ZapprSchema>();

            services.AddScoped<SeriesType>();
            services.AddScoped<UserType>();
            services.AddScoped<EpisodeType>();
            services.AddScoped<CommentType>();
            services.AddScoped<RatingType>();

            services.AddScoped<UserInputType>();

            services.AddScoped<UserQuery>();
            services.AddScoped<SeriesQuery>();
            services.AddScoped<ZapprQuery>();

            services.AddScoped<UserMutation>();
            services.AddScoped<SeriesMutation>();
            services.AddScoped<ZapprMutation>();

            services.AddGraphQL().AddUserContextBuilder(context => new GraphQLUserContext { User = context.User });

            services.AddScoped<TokenHelper>();
        }
    }
}
