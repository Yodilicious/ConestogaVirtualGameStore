namespace ConestogaVirtualGameStore.Web.Repository
{
    using System.Collections.Generic;
    using Models;

    public interface IGameRepository : IRepository
    {
        List<Game> GetGames();
        List<Game> GetGames(string searchText);
        List<Game> GetLastNineGames();
        Game GetGame(long id);
        void AddGame(Game game);
        void UpdateGame(Game game);
        void RemoveGame(Game game);
        bool Exists(long id);
        Game AddGameToShoppingCart(long id, string user);
        void AddGameToWishlist(long id, string user);
        List<Game> GetMyGames(string username);

        void AddReview(Review review);
    }
}
