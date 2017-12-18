namespace ConestogaVirtualGameStore.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Web.Controllers;
    using Web.Models;
    using Xunit;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Web.Data;
    using Web.Repository;
    using ConestogaVirtualGameStore.Web.Models.ViewModels;

    public class GameControllerTests
    {
        [Fact]
        public void Index_ReturnsAllGame_AllGamesAreReturned()
        {
            var mock = new Mock<IGameRepository>();

            mock.Setup(g => g.GetGames()).Returns(new List<Game>
            {
                new Game
                {
                    RecordId = 1,
                    Title = "Test",
                    Description = "Test",
                    Date = DateTime.Now,
                    Developer = "Test",
                    Publisher = "Test",
                    Price = 100,
                    ImageFileName = ""
                }
            });

            using (var controller = new GameController(mock.Object, null))
            {
                var result = controller.Index("") as ViewResult;

                Assert.NotNull(result);
                Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
            }
        }

        [Fact]
        public void Details_ReturnOneGame_OneGameIsReturned()
        {
            var mock = new Mock<IGameRepository>();

            mock.Setup(g => g.GetGame(1)).Returns(new Game
                {
                    RecordId = 1,
                    Title = "Game1",
                    Description = "Test",
                    Date = DateTime.Now,
                    Developer = "Test",
                    Publisher = "Test",
                    Price = 100,
                    ImageFileName = ""
                });

            using (var controller = new GameController(mock.Object, null))
            {
                var result = controller.Details(1) as ViewResult;

                Assert.NotNull(result);
                Assert.NotNull(result.Model);

                var model = result.Model as Game;
                Assert.Equal("Game1", model.Title);
            }
        }

        [Fact]
        public void Create_CreateAGame_OneGameIsCreated()
        {
            var mock = new Mock<IGameRepository>();

            var game = new Game
            {
                Title = "Game6",
                Description = "Description 6",
                Price = 100,
                Developer = "Developer 6",
                Publisher = "Publisher 6",
                ImageFileName = "",
                Date = DateTime.Now
            };

            var game1 = new GameCreateViewModel
            {
                Title = "Game6",
                Description = "Description 6",
                Price = 100,
                Developer = "Developer 6",
                Publisher = "Publisher 6",
                ImageFileName = "",
                Date = DateTime.Now
            };

            mock.Setup(g => g.AddGame(game));

            using (var controller = new GameController(mock.Object, null))
            {
                var result = controller.Create(game1) as ViewResult;
                Assert.Null(result);
            }
        }

        [Fact]
        public void Edit_EditAGame_OneGameIsEdited()
        {
            var mock = new Mock<IGameRepository>();

            var game = new Game
            {
                Title = "Game6",
                Description = "Description 6",
                Price = 100,
                Developer = "Developer 6",
                Publisher = "Publisher 6",
                ImageFileName = "",
                Date = DateTime.Now
            };

            mock.Setup(g => g.UpdateGame(game));

            using (var controller = new GameController(mock.Object, null))
            {
                var result = controller.Edit(game.RecordId, game) as ViewResult;

                Assert.Equal("Description 6", game.Description);
            }
        }

        [Fact]
        public void DeleteConfirmed_DeleteAGame_OneGameIsDeleted()
        {
            var mock = new Mock<IGameRepository>();

            var game = new Game
            {
                Title = "Game6",
                Description = "Description 6",
                Price = 100,
                Developer = "Developer 6",
                Publisher = "Publisher 6",
                ImageFileName = "",
                Date = DateTime.Now
            };

            mock.Setup(g => g.RemoveGame(game));

            using (var controller = new GameController(mock.Object, null))
            {
                var result = controller.DeleteConfirmed(3) as ViewResult;

                Assert.Null(result);
            }
        }
    }
}
