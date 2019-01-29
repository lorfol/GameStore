using GameStore.Domain.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Data.Seeds
{
    public class GameStoreDbInitializer : DropCreateDatabaseIfModelChanges<GameStoreDbContext>
    {
        protected override void Seed(GameStoreDbContext dbContext) // TODO : seed
        {
            if (!dbContext.Publishers.Any())
            {
                var publishers = new List<Publisher>
                {
                    new Publisher
                    {
                        CompanyName = "EA Games",
                        Description = "EA Description",
                        HomePage = "www.ea.com"
                    },

                    new Publisher
                    {
                        CompanyName = "2K",
                        Description = "2K Description",
                        HomePage = "www.2k.com"
                    },

                    new Publisher
                    {
                        CompanyName = "Bethesda",
                        Description = "Bethesda Description",
                        HomePage = "www.bethesda.com"
                    }
                };

                foreach (var item in publishers)
                {
                    dbContext.Publishers.Add(item);
                }

                dbContext.SaveChanges();
            }

            if (!dbContext.Comments.Any())
            {
                var commentaries = new List<Comment>
                {
                    new Comment
                    {
                        Name = "Alex",
                        Body = "qerqewr"
                    },

                    new Comment
                    {
                        Name = "Alex",
                        Body = "QQQQQQQ",
                        CommentId = 1
                    },

                    new Comment
                    {
                        Name = "Alex",
                        Body = "11111111111111"
                    }
                };

                foreach (var commentary in commentaries)
                {
                    dbContext.Comments.Add(commentary);
                }

                dbContext.SaveChanges();
            }

            if (!dbContext.Genres.Any())
            {
                var genres = new List<Genre>
                {
                    //new Genre {Id = 1, Name = "Strategy"},
                    //new Genre {Id = 2, Name = "RTS", ParentGenreId = 1},
                    //new Genre {Id = 3, Name = "TBS", ParentGenreId = 1},

                    //new Genre {Id = 4, Name = "RPG"},
                    //new Genre {Id = 5, Name = "Sports"},
                    //new Genre {Id = 6, Name = "Races"},
                    //new Genre {Id = 7, Name = "Rally", ParentGenreId = 6},
                    //new Genre {Id = 8, Name = "Arcade", ParentGenreId = 6},
                    //new Genre {Id = 9, Name = "Formula", ParentGenreId = 6},
                    //new Genre {Id = 10, Name = "Offroad", ParentGenreId = 6},

                    //new Genre {Id = 11, Name = "Action"},
                    //new Genre {Id = 12, Name = "FPS", ParentGenreId = 11},
                    //new Genre {Id = 13, Name = "TPS", ParentGenreId = 11},
                    //new Genre {Id = 14, Name = "Other", ParentGenreId = 11},

                    //new Genre {Id = 16, Name = "Adventure"},
                    //new Genre {Id = 17, Name = "Puzzle & Skill"},
                    //new Genre {Id = 18, Name = "Misc"}

                    new Genre {Name = "RTS", Category = "Strategy"},
                    new Genre {Name = "TBS", Category = "Strategy"},

                    new Genre {Name = "RPG"},
                    new Genre {Name = "Sports"},
                    new Genre {Name = "Rally", Category = "Races"},
                    new Genre {Name = "Arcade", Category = "Races"},
                    new Genre {Name = "Formula", Category = "Races"},
                    new Genre {Name = "Off-road", Category = "Races"},

                    new Genre {Name = "FPS", Category = "Action"},
                    new Genre {Name = "TPS", Category = "Action"},

                    new Genre {Name = "Adventure"},
                    new Genre {Name = "Puzzle & Skill"},
                    new Genre {Name = "Other"}
                };

                foreach (var item in genres)
                {
                    dbContext.Genres.Add(item);
                }

                dbContext.SaveChanges();
            }

            if (!dbContext.Platforms.Any())
            {
                var platforms = new List<Platform>
                {
                    new Platform
                    {
                        Type = "Desktop"
                    },
                    new Platform
                    {
                        Type = "Mobile"
                    },
                    new Platform
                    {
                        Type = "Console"
                    },
                    new Platform
                    {
                        Type = "Browser"
                    }
                };

                foreach (var platformType in platforms)
                {
                    dbContext.Platforms.Add(platformType);
                }

                dbContext.SaveChanges();
            }

            if (!dbContext.Games.Any())
            {
                var games = new List<Game>
                {
                    new Game
                    {
                        Description = "description",
                        Key = "PUBG",
                        Name = "Game Name",
                        Comments = dbContext.Comments.Take(2).ToList(),
                        Publishers = dbContext.Publishers.Take(2).ToList(),
                        Genres = dbContext.Genres.Take(3).ToList(),
                        Platforms = dbContext.Platforms.Take(2).ToList(),
                        Price = 29.99M,
                        UnitsInStock = 15,
                        Discontinued = false,
                        AddingDate = DateTime.UtcNow.AddMonths(-3)
                    },

                    new Game
                    {
                        Description = "description1",
                        Key = "DOKA2",
                        Name = "Game Name1",
                        Comments = dbContext.Comments.OrderBy(comment => comment.Id).Skip(2).ToList(),
                        Publishers = dbContext.Publishers.Take(2).ToList(),
                        Genres = dbContext.Genres.Take(1).ToList(),
                        Platforms = dbContext.Platforms.OrderBy(x=>x.Type).Skip(2).Take(2).ToList(),
                        Price = 29.99M,
                        UnitsInStock = 97,
                        Discontinued = false,
                        AddingDate = DateTime.UtcNow.AddMonths(-1)
                    },

                    new Game
                    {
                        Description = "description1",
                        Key = "CSGO",
                        Name = "Counter-Strike : Global Offensive",
                        Publishers = dbContext.Publishers.OrderBy(pub => pub.CompanyName).Skip(1).Take(2).ToList(),
                        Genres = dbContext.Genres.Take(5).ToList(),
                        Platforms = dbContext.Platforms.OrderBy(x=>x.Type).Skip(2).Take(1).ToList(),
                        Price = 9.99M,
                        UnitsInStock = 1001,
                        Discontinued = false,
                        AddingDate = DateTime.UtcNow.AddYears(-4)
                    }
                };

                foreach (var item in games)
                {
                    dbContext.Games.Add(item);
                }

                dbContext.SaveChanges();
            }

            dbContext.SaveChanges();
        }
    }
}
