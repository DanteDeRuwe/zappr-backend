using GraphQL.Types;
using Zappr.Core.Entities;

namespace Zappr.Api.GraphQL.Types
{
    public class EpisodeType : ObjectGraphType<Episode>
    {
        public EpisodeType()
        {
            Field(e => e.Id).Description("The Episode Id");

            Field(e => e.Name, nullable: true).Description("The name of the episode");
            Field(e => e.Summary, nullable: true).Description("A description for the episode");
            Field(e => e.Season, nullable: true).Description("The number of the season the episode belongs to");
            Field(e => e.Number, nullable: true).Description("The number of the episode within the season");
            Field(e => e.AirDate, nullable: true).Description("the date the episode aired on");
            Field(e => e.AirTime, nullable: true).Description("the time the episode aires on");
            Field(e => e.Runtime, nullable: true).Description("the duration in minutes");
            Field(e => e.Image, nullable: true).Description("An image url for the episode");

            Field<SeriesType>("series", resolve: context => context.Source.Series);
        }


    }
}