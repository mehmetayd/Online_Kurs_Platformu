using Microsoft.AspNetCore.Mvc;
using Online_Kurs_Platformu.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Online_Kurs_Platformu.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;

        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminKontrol()
        {
            var query = "SELECT * FROM Admin";
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            connection.Open();
            System.Data.DataTable table = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(query, connection);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            connection.Close();
            if (table.Rows.Count != 0)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return RedirectToAction("Admin", "Register");
            }
        }

        public IActionResult Egitmen()
        {
            return View();
        }
        public IActionResult Ogrenci()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AuthenticationEgitmen(Egitmen egitmen)
        {
            var query = "SELECT * FROM Egitmen WHERE [e-mail]=@Email AND sifre= @Sifre";
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            connection.Open();
            System.Data.DataTable table = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", egitmen.Email);
            command.Parameters.AddWithValue("@sifre", egitmen.Sifre);
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(table);
            }
            catch
            {
                return View("Egitmen", egitmen);
            }
            connection.Close();
            if (table.Rows.Count != 0)
            {
                HttpContext.Session.SetString("id", table.Rows[0]["id"].ToString());
                HttpContext.Session.SetString("kategoriId", table.Rows[0]["kategoriId"].ToString());
                HttpContext.Session.SetString("ad", table.Rows[0]["ad"].ToString());
                HttpContext.Session.SetString("soyad", table.Rows[0]["soyad"].ToString());
                HttpContext.Session.SetString("e-mail", table.Rows[0]["e-mail"].ToString());
                return RedirectToAction("Index", "Egitmen");
            }
            else
            {
                return View("Egitmen", egitmen);
            }
        }
        [HttpPost]
        public IActionResult AuthenticationOgrenci(Ogrenci ogrenci)
        {
            var query = "SELECT * FROM ogrenci WHERE [e-mail]=@Email AND sifre= @Sifre";
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            connection.Open();
            System.Data.DataTable table = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", ogrenci.Email);
            command.Parameters.AddWithValue("@sifre", ogrenci.Sifre);
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(table);
            }
            catch
            {
                return View("Ogrenci", ogrenci);
            }
            connection.Close();
            if (table.Rows.Count != 0)
            {
                HttpContext.Session.SetString("id", table.Rows[0]["id"].ToString());
                HttpContext.Session.SetString("ad", table.Rows[0]["ad"].ToString());
                HttpContext.Session.SetString("soyad", table.Rows[0]["soyad"].ToString());
                HttpContext.Session.SetString("kategoriId", table.Rows[0]["kategoriId"].ToString());
                HttpContext.Session.SetString("e-mail", table.Rows[0]["e-mail"].ToString());
                return RedirectToAction("Index", "Ogrenci");
            }
            else
            {
                return View("Ogrenci", ogrenci);
            }
        }
        [HttpPost]
        public IActionResult AuthenticationAdmin(Admin admin)
        {
            var query = "SELECT * FROM admin WHERE id=@id AND sifre= @Sifre";
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            connection.Open();
            System.Data.DataTable table = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", admin.Id);
            command.Parameters.AddWithValue("@sifre", admin.Sifre);
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(table);
            }
            catch
            {
                return View("Admin", admin);
            }
          
            if (table.Rows.Count != 0)
            {
                HttpContext.Session.SetString("id", table.Rows[0]["id"].ToString());
                HttpContext.Session.SetString("sifre", table.Rows[0]["sifre"].ToString());
                return RedirectToAction("Index", "admin");
            }
            else
            {
                return View("Admin", admin);
            }
            connection.Close();
        }
    }
}
