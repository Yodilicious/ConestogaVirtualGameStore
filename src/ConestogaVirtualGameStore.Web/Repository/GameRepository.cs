namespace ConestogaVirtualGameStore.Web.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class GameRepository : IGameRepository
    {
        public List<Game> GetGames()
        {
            return this.context.Games.ToList();
        }

        public List<Game> GetGames(string searchText)
        {
            return this.context.Games.Where(g => g.Title.Contains(searchText)).ToList();
        }

        public List<Game> GetLastNineGames()
        {
            return this.context.Games.Take(9).OrderByDescending(g => g.Date).ToList();
        }

        public Game GetGame(long id)
        {
            return this.context.Games.Include(g => g.Reviews).SingleOrDefault(m => m.RecordId == id);
        }

        public void AddGame(Game game)
        {
            this.context.Games.Add(game);
        }

        public void UpdateGame(Game game)
        {
            this.context.Games.Update(game);
        }

        public void RemoveGame(Game game)
        {
            this.context.Games.Remove(game);
        }
        public Game AddGameToShoppingCart(long id, string user)
        {
            var cart = this.context.ShoppingCarts.Include(s => s.ShoppingCartItems).FirstOrDefault(s => s.HasPaid == false && s.User == user);

            if (cart == null)
            {
                cart = new ShoppingCart();
                cart.User = user;

                if (cart.ShoppingCartItems == null)
                {
                    cart.ShoppingCartItems = new List<ShoppingCartItem>();
                }

                this.context.ShoppingCarts.Add(cart);
            }

            var game = this.context.Games.FirstOrDefault(g => g.RecordId == id);

            if (game != null)
            {
                var item = new ShoppingCartItem();

                item.Game = game;
                item.ShoppingCart = cart;
                item.Price = game.Price;
                item.AddedOn = DateTime.Now;

                cart.ShoppingCartItems.Add(item);

                this.context.SaveChanges();

                return game;
            }

            return null;
        }
        public Game AddGameToWishlist(long id)
        {
            var game = this.context.Games.FirstOrDefault(a => a.RecordId == id);
            if (game != null)
            {
                if (game.Wishlist == null)
                {
                    game.Wishlist = new List<Wishlist>();
                }
                var wishlist = new Wishlist();
                wishlist.Title = game.Title;
                wishlist.ImageFileName = game.ImageFileName;
                wishlist.Price = game.Price;
                game.Wishlist.Add(wishlist);
                this.context.SaveChanges();

                return game;
            }




            return null;
        }
        public void AddReview(Review review)
        {
            this.context.Reviews.Add(review);
            this.context.SaveChanges();
        }
        public Game addGametoWishlist(String Title)
        {

            var game = this.context.Games.FirstOrDefault(g => g.Title == Title);

            if (game != null)
            {
                var item = new Wishlist();

                item.Title = game.Title;
                item.ImageFileName = game.ImageFileName;
                item.Price = game.Price;

                game.Wishlist.Add(item);

                this.context.SaveChanges();

                return game ;
            }

            return null;
        }

        public bool Exists(long id)
        {
            return this.context.Games.FirstOrDefault(g => g.RecordId == id) != null;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public GameRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private readonly ApplicationDbContext context;
    }
}
