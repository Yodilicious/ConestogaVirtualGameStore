namespace ConestogaVirtualGameStore.Web.Repository
{
    using System.Collections.Generic;
    using Models;

    public interface IGameRepository : IRepository
    {
        List<Game> GetGames();
        List<Game> GetLastNineGames();
        Game GetGame(long id);
        void AddGame(Game game);
        void UpdateGame(Game game);
        void RemoveGame(Game game);
        bool Exists(long id);
        Game AddGameToShoppingCart(long id);
    }
}
