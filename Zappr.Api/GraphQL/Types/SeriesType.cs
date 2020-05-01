using GraphQL.Types;
using Zappr.Api.Domain;

namespace Zappr.Api.GraphQL.Types
{

    /// <summary>
    /// A wrapper for the external API
    /// </summary>
    public class SeriesType : ObjectGraphType<Series>
    {

        public SeriesType()
        {
            Field(s => s.Id).Description("The series ID");

            Field(s => s.Name, nullable: true).Description("The name of the series");
            Field(s => s.Description, nullable: true).Description("A description for the series");
            Field(s => s.NumberOfSeasons, nullable: true).Description("The number of seasons of this series");
            Field(s => s.Network, nullable: true).Description("the network the series is currently airing on");
            Field(s => s.Ended, nullable: true).Description("TRUE if the series ended, FALSE if it is continuing");
            Field(s => s.Premiered, nullable: true).Description("The first airdate of this series");
            Field(s => s.AirTime, nullable: true).Description("The time this series typically airs on");
            Field(s => s.ImageUrl, nullable: true).Description("A url that provides a poster for the series");
            Field(s => s.Genres, nullable: true).Description("A list of genres for the series");
            Field(s => s.OfficialSite, nullable: true).Description("The official website of this series");


            //Field(s => s.Episodes).Description("The episodes that belong to this series");
            //Field(s => s.Characters).Description("The characters that play in this series");
        }


    }
}
