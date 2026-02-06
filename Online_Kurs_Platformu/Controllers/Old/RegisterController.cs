using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnlineCourse.UI.Models.Old;

namespace OnlineCourse.UI.Controllers.Old
{
	public class RegisterController : Controller
	{
		private readonly string connString;
		private readonly IConfiguration configuration;

		public RegisterController(IConfiguration configuration)
		{
			connString = configuration.GetConnectionString("DefaultConnectionString");
			this.configuration = configuration;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Egitmen()
		{
			SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
			DataTable dt = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter();
			SqlCommand comm = new SqlCommand(@"SELECT
                                                b.id,
                                                b.ad
                                            FROM kategori b
                                            ORDER BY b.ad", connection);
			adapter.SelectCommand = comm;
			connection.Open();
			adapter.Fill(dt);
			connection.Close();

			var kategoriList = new List<Kategori>();

			foreach (DataRow row in dt.Rows)
			{
				kategoriList.Add(new Kategori()
				{
					Ad = row["ad"].ToString(),
					Id = int.Parse(row["id"].ToString())
				});
			}
			return View(new RegisterEgitmenModel()
			{
				Kategoriler = kategoriList
			});
		}

		public IActionResult Ogrenci()
		{
			SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
			DataTable dt = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter();
			SqlCommand comm = new SqlCommand(@"SELECT
                                                b.id,
                                                b.ad
                                            FROM kategori b
                                            ORDER BY b.ad", connection);
			adapter.SelectCommand = comm;
			connection.Open();
			adapter.Fill(dt);
			connection.Close();

			var kategoriList = new List<Kategori>();

			foreach (DataRow row in dt.Rows)
			{
				kategoriList.Add(new Kategori()
				{
					Ad = row["ad"].ToString(),
					Id = int.Parse(row["id"].ToString())
				});
			}
			return View(new RegisterOgrenciModel()
			{
				Kategoriler = kategoriList
			});
		}
		public IActionResult Admin()
		{
			return View();
		}
		public IActionResult Kurs()
		{
			SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
			DataTable dt = new DataTable();
			SqlDataAdapter adapter = new SqlDataAdapter();
			SqlCommand comm = new SqlCommand(@"SELECT
                                                b.id,
                                                b.ad
                                            FROM kategori b
                                            ORDER BY b.ad", connection);
			adapter.SelectCommand = comm;
			connection.Open();
			adapter.Fill(dt);
			connection.Close();

			var kategoriList = new List<Kategori>();

			foreach (DataRow row in dt.Rows)
			{
				kategoriList.Add(new Kategori()
				{
					Ad = row["ad"].ToString(),
					Id = int.Parse(row["id"].ToString())
				});
			}
			return View(new RegisterKursModel()
			{
				Kategoriler = kategoriList
			});
		}

		public IActionResult UstKurulus()
		{
			return View();
		}

		public IActionResult Kategori()
		{
			return View();
		}

		[HttpPost]
		public IActionResult NewEgitmen(RegisterEgitmenModel egitmenModel)
		{
			if (egitmenModel.egitmen.Ad == "" || egitmenModel.egitmen.Soyad == "" || egitmenModel.egitmen.Sifre == "")
			{
				//personel.ErrorMessage = "Tüm alanları doldurun";
				//return View("Edit", personel);
			}
			if (egitmenModel.egitmen.Ad == null || egitmenModel.egitmen.Soyad == null || egitmenModel.egitmen.Sifre == null)
			{
				//personel.ErrorMessage = "Tüm alanları doldurun";
				//return View("Edit", personel);
			}
			egitmenModel.egitmen.Email = "E" + egitmenModel.egitmen.Ad + "." + egitmenModel.egitmen.Soyad + "@duzce.edu";
			var query = "INSERT INTO egitmen(ad,soyad,[e-mail],sifre,kategoriId) VALUES(@ad,@soyad,@email,@sifre,@kategoriId)";
			SqlConnection connection = new SqlConnection(connString);
			DataTable dt = new DataTable();
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@ad", egitmenModel.egitmen.Ad);
			command.Parameters.AddWithValue("@soyad", egitmenModel.egitmen.Soyad);
			command.Parameters.AddWithValue("@email", egitmenModel.egitmen.Email);
			command.Parameters.AddWithValue("@sifre", egitmenModel.egitmen.Sifre);
			command.Parameters.AddWithValue("@kategoriId", egitmenModel.egitmen.KategoriId);
			connection.Open();
			command.ExecuteNonQuery();
			connection.Close();
			return RedirectToAction("Index", "Login");


		}
		[HttpPost]
		public IActionResult NewOgr(RegisterOgrenciModel ogrenciModel)
		{
			//if (personel.Ad == "" || personel.Soyad == "" || personel.Sifre == "")
			//{
			//    //personel.ErrorMessage = "Tüm alanları doldurun";
			//    //return View("Edit", personel);
			//}
			//if (personel.Ad == null || personel.Soyad == null || personel.Sifre == null)
			//{
			//    //personel.ErrorMessage = "Tüm alanları doldurun";
			//    //return View("Edit", personel);
			//}
			ogrenciModel.Ogrenci.Email = "O" + ogrenciModel.Ogrenci.Ad + "." + ogrenciModel.Ogrenci.Soyad + "@duzce.edu";
			var query = "INSERT INTO ogrenci(ad,soyad,[e-mail],sifre, kategoriId) VALUES(@ad, @soyad, @email, @sifre, @kategoriId)";
			SqlConnection connection = new SqlConnection(connString);
			DataTable dt = new DataTable();
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@ad", ogrenciModel.Ogrenci.Ad);
			command.Parameters.AddWithValue("@soyad", ogrenciModel.Ogrenci.Soyad);
			command.Parameters.AddWithValue("@email", ogrenciModel.Ogrenci.Email);
			command.Parameters.AddWithValue("@sifre", ogrenciModel.Ogrenci.Sifre);
			command.Parameters.AddWithValue("@kategoriId", ogrenciModel.Ogrenci.KategoriId);
			connection.Open();
			command.ExecuteNonQuery();
			connection.Close();
			return RedirectToAction("Index", "Login");//egitmen anasayfasına dönecek
		}
		public IActionResult NewAdmin(Admin admin)
		{
			admin.Id = "A" + admin.Id;
			var query = "INSERT INTO admin(id,sifre) VALUES(@id, @sifre)";
			SqlConnection connection = new SqlConnection(connString);
			DataTable dt = new DataTable();
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@id", admin.Id);
			command.Parameters.AddWithValue("@sifre", admin.Sifre);
			connection.Open();
			command.ExecuteNonQuery();
			connection.Close();
			return RedirectToAction("Index", "Login");
		}
		public IActionResult NewKurs(RegisterKursModel kursModel)
		{
			var query = "INSERT INTO kurslar(kategoriId,ad,kursSuresi,kursFiyat,videoId,aciklama) VALUES(@kategoriid,@ad,@kurssuresi,@kursfiyat,@videoId,@aciklama)";
			SqlConnection connection = new SqlConnection(connString);
			DataTable dt = new DataTable();
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@kategoriid", kursModel.Kurs.KategoriId);
			command.Parameters.AddWithValue("@ad", kursModel.Kurs.Ad);
			command.Parameters.AddWithValue("@kursSuresi", kursModel.Kurs.KursSuresi);
			command.Parameters.AddWithValue("@kursFiyat", kursModel.Kurs.KursFiyat);
			command.Parameters.AddWithValue("@videoId", kursModel.Kurs.VideoId);
			command.Parameters.AddWithValue("@aciklama", kursModel.Kurs.Aciklama);
			connection.Open();
			command.ExecuteNonQuery();
			connection.Close();
			return RedirectToAction("Index", "Egitmen");
		}

		[HttpPost]
		public IActionResult NewUstKurulus(UstKurulus ustKurulus)
		{
			var query = "INSERT INTO ustKurulus(ad) VALUES(@ad)";
			SqlConnection connection = new SqlConnection(connString);
			DataTable dt = new DataTable();
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@ad", ustKurulus.Ad);
			connection.Open();
			command.ExecuteNonQuery();
			connection.Close();
			return RedirectToAction("Index", "Login");
		}

		[HttpPost]
		public IActionResult NewKategori(Kategori kategori)
		{
			var query = "INSERT INTO kategori(ad) VALUES(@ad)";
			SqlConnection connection = new SqlConnection(connString);
			DataTable dt = new DataTable();
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@ad", kategori.Ad);
			connection.Open();
			command.ExecuteNonQuery();
			connection.Close();
			return RedirectToAction("Kategori", "Register");
		}
	}
}
