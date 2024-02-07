using VideoGameLibrary.Interfaces;
using VideoGameLibrary.Models;

namespace VideoGameLibrary.Data
{
    public class GameListDAL : IDataAccessLayer
    {
        private VideoGameDBContext db;

        public GameListDAL(VideoGameDBContext indb)
        {
            db = indb;
        }        
        public void AddGame(Game game)
        {
            db.Games.Add(game);
            db.SaveChanges();
        }

        public void EditGame(Game game)
        {            
            db.Update(game);
            db.SaveChanges();
        }
        public void RemoveGame(int? id)
        {
            Game? foundGame = db.Games.Where(g => g.Id == id).FirstOrDefault();
            if (foundGame != null)
            {
                db.Games.Remove(foundGame);
                db.SaveChanges();
            }            
        }                
        public IEnumerable<Game> SearchForGames(string key)
        {
            IEnumerable<Game> foundGames = db.Games.Where(g => g.Title.ToLower().Contains(key.ToLower()));
            return foundGames;
        }
        public IEnumerable<Game> GetCollection()
        {
            return db.Games;
        }
        //public IEnumerable<Game> FilterCollection(string? genre, string? platform, string? rating)
        //{
        //    IEnumerable<Game> filteredGames = db.Games.Where(g => g.Genre.ToLower().Contains(genre.ToLower()) && g.Platform.ToLower().Contains(platform.ToLower()) && g.Rating.ToLower().Contains(rating.ToLower()));
        //    return filteredGames;
        //}
        public IEnumerable<Game> FilterCollection(string? genre, string? platform, string? rating)
        {
            if(genre == null) 
                genre = "";
            if (platform == null)
                platform = "";
            if(rating == null)            
                rating = "";
            if(genre == "" &&  platform == "" &&  rating == "")
                return GetCollection();

            IEnumerable<Game> gamesByGenre = GetCollection().Where(g => (!string.IsNullOrEmpty(g.Genre) && g.Genre.ToLower().Contains(genre.ToLower()))).ToList();

            IEnumerable<Game> gamesByPlatform = GetCollection().Where(g => (!string.IsNullOrEmpty(g.Platform) && g.Platform.ToLower().Contains(platform.ToLower()))).ToList();

            IEnumerable<Game> gamesByRating = GetCollection().Where(g => (!string.IsNullOrEmpty(g.Rating) && g.Rating.ToLower().Contains(rating.ToLower()))).ToList();

            IEnumerable<Game> gamesByGenreAndPlatform = gamesByGenre.Intersect(gamesByPlatform);

            return gamesByRating.Intersect(gamesByGenreAndPlatform); 
        }
    }
}
