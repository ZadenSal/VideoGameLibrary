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

        public IEnumerable<Game> FilterCollection(string genre, string platform, string rating)
        {
            throw new NotImplementedException();
        }
    }
}
