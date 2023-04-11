using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books
{
    public class UpdateBookModel : PageModel
    {
        public Books Book = new Books();
        public String ErrorMessage = "";
        public String SuccessMessage = "";
        public void OnGet()
        {
            try
            {
                Book.Id = Request.Query["BookCode"];
                string connection = "Data Source=DESKTOP-7SPPOGN;Initial Catalog = LMS_DB ; Integrated Security=True;Encrypt=False;";
                SqlConnection sqlConnection = new SqlConnection(connection);
                sqlConnection.Open();

                string querry = $"select * from LMS_BOOK_DETAILS where BOOK_CODE = '{Book.Id}'";
                using (SqlCommand command = new SqlCommand(querry, sqlConnection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                                       
                        Book.BookTitle = (string)reader["BOOK_TITLE"];
                        Book.category = (string)reader["CATEGORY"];
                        Book.Author = (string)reader["AUTHOR"];
                        Book.Pubication = (string)reader["PUBLICATION"];
                        Book.publish_date = (DateTime)reader["PUBLISH_DATE"];
                        Book.Price = (double)reader["PRICE"];
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }

        public void OnPost()
        {

            try
            {
                string connection = "Data Source=DESKTOP-7SPPOGN;Initial Catalog = LMS_DB ; Integrated Security=True;Encrypt=False;";
                SqlConnection sqlConnection = new SqlConnection(connection);
                sqlConnection.Open();


                Book.Id = Request.Form["id"];
                Book.BookTitle = Request.Form["title"];
                Book.category = Request.Form["category"];
                Book.Author = Request.Form["Author"];
                Book.Pubication = Request.Form["Pubication"];
                Book.publish_date = Convert.ToDateTime(Request.Form["publish_date"]);
                Console.WriteLine(Book.publish_date);
                Book.Price = Convert.ToDouble(Request.Form["Price"]);

                string querry = $"update LMS_BOOK_DETAILS set BOOK_TITLE = '{Book.BookTitle}',CATEGORY = '{Book.category}'," +
                    $"AUTHOR = '{Book.Author}', PUBLICATION = '{Book.Pubication}' , PUBLISH_DATE = '{Book.publish_date}' , PRICE = {Book.Price}" +
                    $"WHERE BOOK_CODE = '{Book.Id}'";


                
                using (SqlCommand command = new SqlCommand(querry, sqlConnection))
                {
                    command.ExecuteNonQuery();

                }
                SuccessMessage = "Book Updated Successfully !!!";

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error : {ex.Message}");
                ErrorMessage = ex.Message;
            }

        }
    }
}
