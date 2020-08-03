
namespace Zappr.Core.Domain
{
    public abstract class UserSeries
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int SeriesId { get; set; }
        public Series Series { get; set; }

        public override bool Equals(object other) =>
            other?.GetType() == GetType()
            && UserId == ((UserSeries)other).UserId
            && SeriesId == ((UserSeries)other).SeriesId;

        public override int GetHashCode() => UserId.GetHashCode() ^ SeriesId.GetHashCode(); //https://stackoverflow.com/a/70375/12641623
    }
}