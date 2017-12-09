namespace ConestogaVirtualGameStore.Web.Models
{
    public class Wishlist : BaseModel
    {
        public string User { get; set; }
        public long GameId { get; set; }
        public Game Game { get; set; }
    }
}
