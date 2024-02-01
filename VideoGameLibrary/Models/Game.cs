using System.ComponentModel.DataAnnotations;

namespace VideoGameLibrary.Models
{
    public class Game
    {
        private static int nextID = 0;
        public Game()
        {

        }
        public Game(string title, string platform, string genre, string rating, int year, string image, string? loanedTo, DateTime? loanDate)
        {
            Title = title;
            Platform = platform;
            Genre = genre;
            Rating = rating;
            Year = year;
            Image = image;
            LoanedTo = loanedTo;
            LoanDate = loanDate;
        }

        public int? Id { get; set; } = nextID++;
        [Required(ErrorMessage = "Game title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter a valid platform")]
        public string Platform { get; set; }
        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "ESRB rating is required")]
        public string Rating { get; set; }
        [Required(ErrorMessage = "Please enter a valid year")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Enter a valid image URL")]
        public string Image { get; set; }        
        public string? LoanedTo { get; set; }
        public DateTime? LoanDate { get; set; }
    }
}
