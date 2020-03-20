/*using GraphQL.Types;
using Zappr.Api.Domain;

namespace Zappr.Api.GraphQL.Types
{
    public class EpisodeType : ObjectGraphType<Episode>
    {
        public EpisodeType()
        {
            Field(e => e.Id).Description("The Episode Id");
            Field(e => e.Series).Description("The series this episode belongs to");

            Field<StringGraphType>("episodeName", "The name of the episode");
            Field<StringGraphType>("description", "A description for the episode");
            Field<StringGraphType>("seasonNumber", "The number of the season the episode belongs to");
            Field<StringGraphType>("episodeNumber", "The index of the episode within the season");
            Field<StringGraphType>("absoluteNumber", "The index of the episode within the whole series");
            Field<StringGraphType>("airDate", "the first date the episode aired on");
            Field<ListGraphType<StringGraphType>>("guestStars", "A list of guest stars who starred in this episode");
            Field<ListGraphType<StringGraphType>>("directors", "A list of directors of the episode");
            Field<ListGraphType<StringGraphType>>("writers", "A list of writers of the episode");
            Field<StringGraphType>("imageUrl", "An image url for the episode");
            Field<StringGraphType>("imdbId", "The IMDB Id of the episode");
        }


    }
}*/