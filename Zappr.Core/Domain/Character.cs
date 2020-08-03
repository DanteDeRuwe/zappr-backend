namespace Zappr.Core.Domain
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ActorName { get; set; }


        public Character() { }

        public Character(int id, string name, string actorName)
        {
            Id = id;
            Name = name;
            ActorName = actorName;
        }
    }
}
