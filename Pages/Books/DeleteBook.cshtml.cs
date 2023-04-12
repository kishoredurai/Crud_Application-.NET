using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Books
{
    public class DeleteBookModel : PageModel
    {
        Books Book = new Books();
        public void OnGet()
        {
            try
            {
                Book.Id = Request.Query["BookCode"];
                string connection = "Data Source=DESKTOP-7SPPOGN;Initial Catalog = LMS_DB ; Integrated Security=True;Encrypt=False;";
                SqlConnection sqlConnection = new SqlConnection(connection);
                sqlConnection.Open();
                string querry = $"delete from LMS_BOOK_DETAILS where BOOK_CODE = '{Book.Id}'";
                using (SqlCommand command = new SqlCommand(querry, sqlConnection))
                {
                    int n = command.ExecuteNonQuery();
                    if(n>0)
                    {
                        Response.Redirect("/Books/IndexBook");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
        }
    }
}
