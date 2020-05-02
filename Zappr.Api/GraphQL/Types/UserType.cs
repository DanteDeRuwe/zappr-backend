using GraphQL.Types;
using System.Linq;
using Zappr.Api.Domain;

namespace Zappr.Api.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {

        public UserType()
        {

            Field(u => u.Id);
            Field(u => u.FullName);
            Field(u => u.Email);

            // We resolve these ourselves to skip the joined entity
            Field<ListGraphType<SeriesType>>("favoriteSeries", "The series this user has favorited", resolve: c => c.Source.FavoriteSeries.Select(us => us.Series));
            Field<ListGraphType<EpisodeType>>("watchedEpisodes", "The episodes this user has watched", resolve: c => c.Source.WatchedEpisodes.Select(ue => ue.Episode));
        }
    }
}
