using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books
{
    public class IndexBookModel : PageModel
    {
       public List<Books> BookList = new List<Books>();

        public void OnGet()
        {

            try
            {
                string connection = "Data Source=DESKTOP-7SPPOGN;Initial Catalog = LMS_DB ; Integrated Security=True;Encrypt=False;";
                SqlConnection sqlConnection = new SqlConnection(connection);
                sqlConnection.Open();
                string querry = "select BOOK_CODE , BOOK_TITLE, AUTHOR, PUBLICATION, PRICE from LMS_BOOK_DETAILS";
                using (SqlCommand command = new SqlCommand(querry, sqlConnection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Books Book = new Books();
                        Book.Id = reader.GetString(0);
                        Book.BookTitle = reader.GetString(1);
                        Book.Author = reader.GetString(2);
                        Book.Pubication = (string)reader["PUBLICATION"];
                        Book.Price = (double)reader["PRICE"];

                        BookList.Add(Book);
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }

        }
    }

    public class Books
    {
        public string Id { get; set; }
        public string BookTitle { get; set; }
        public string category { get; set; }
        public string Author { get; set; }
        public string Pubication { get; set; }
        public DateTime publish_date { get; set; }
        public int book_edition { get; set; }
        public double Price { get; set; }
        public string Rack_Num { get; set; }
        public DateTime Date_arrival { get; set; }
        public string Supplier_ID { get; set; }

    }

}