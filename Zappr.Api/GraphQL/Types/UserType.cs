using GraphQL.Types;
using System.Linq;
using Zappr.Api.Domain;
using Zappr.Api.Services;

namespace Zappr.Api.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        private readonly TvdbService _tvdb;

        public UserType(TvdbService tvdbService)
        {

            _tvdb = tvdbService;

            Field(u => u.Id);
            Field(u => u.FullName);
            Field(u => u.Email);

            // We resolve these ourselves to skip the joined entity
            Field<ListGraphType<SeriesType>>("favoriteSeries", "The series this user has favorited", resolve: c => c.Source.FavoriteSeries.Select(us => us.Series));

        }
    }
}
