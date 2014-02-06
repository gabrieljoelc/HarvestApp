namespace HarvestApp.Core
{
    public class Hash
    {
        public User User { get; set; }

        public override string ToString()
        {
            return User != null ? "user id: " + User.Id : "nothing";
        }
    }
}