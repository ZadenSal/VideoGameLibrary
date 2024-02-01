using VideoGameLibrary.Interfaces;
using VideoGameLibrary.Models;

namespace VideoGameLibrary.Data
{
    public class GameListDAL : IDataAccessLayer
    {
        private static List<Game> GameList = new List<Game>
        {
            new Game("Crash Bandicoot Mutant Island", "Mobile Platform", "Action", "E", 2009, "/images/crash.webp", null, null),
            new Game("Super Mario 64", "Nintendo", "3D Platformer", "E", 1996, "/images/mario64.webp", null, null),
            new Game("Tetris", "Electronika", "Puzzle", "E", 1985, "/images/tetris.webp", null, null),
            new Game("Metroid Zero Mission", "Game Boy Advance", "Action-Adventure", "E", 2004, "/images/metroid.webp", null, null),
            new Game("Crash Bandicoot Twinsanity", "PlayStation 2/Xbox", "2D Platformer", "E", 2004, "/images/crash2.webp", null, null),
            new Game("Spyro The Dragon", "PlayStation", "2D Platformer", "E", 1998, "/images/spyro.jpg", null, null)            
        };
        public void AddGame(Game game)
        {
            GameList.Add(game);
        }

        public void EditGame(Game game)
        {
            int i = GameList.FindIndex(x =>  x.Id == game.Id);
            GameList[i] = game;
        }
        public void RemoveGame(int? id)
        {
            Game? foundGame = GameList.Where(g => g.Id == id).FirstOrDefault();
            if (foundGame != null)
            {
                GameList.Remove(foundGame);
            }            
        }                
        public IEnumerable<Game> SearchForGames(string key)
        {
            IEnumerable<Game> foundGames = GameList.Where(g => g.Title.ToLower().Contains(key.ToLower()));
            return foundGames;
        }
        public IEnumerable<Game> GetCollection()
        {
            return GameList;
        }
    }
}
