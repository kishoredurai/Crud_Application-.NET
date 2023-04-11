using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books
{
    public class CreateBookModel : PageModel
    {
        Books Book = new Books();
       public String ErrorMessage = "";
       public String SuccessMessage = "";
        public void OnGet()
        {
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
                    Book.book_edition = Convert.ToInt16(Request.Form["book_edition"]);
                    Book.Price = Convert.ToDouble(Request.Form["Price"]);
                    Book.Rack_Num = "A1";
                    Book.Date_arrival = Convert.ToDateTime("2012-05-11");
                    Book.Supplier_ID = "S04";



                string querry = $"insert into LMS_BOOK_DETAILS values('{Book.Id}','{Book.BookTitle}','{Book.category}','{Book.Author}'," +
                    $"'{Book.Pubication}','{Book.publish_date}',{Book.book_edition}," +
                    $"{Book.Price},'{Book.Rack_Num}','{Book.Date_arrival}','{Book.Supplier_ID}')";                    
                using (SqlCommand command = new SqlCommand(querry, sqlConnection))
                {
                    command.ExecuteNonQuery();
                   
                }
                SuccessMessage = "Book Created Successfully !!!";

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error : {ex.Message}");
                ErrorMessage = ex.Message;
            }

        }
    }
}
