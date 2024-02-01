using VideoGameLibrary.Models;

namespace VideoGameLibrary.Interfaces
{
    public interface IDataAccessLayer
    {
        void AddGame(Game game);
        void EditGame(Game game);
        void RemoveGame(int? id);        
        IEnumerable<Game> SearchForGames(string key);
        IEnumerable<Game> GetCollection();
    }
}
