using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Online_Kurs_Platformu.Models;

namespace Online_Kurs_Platformu.Controllers
{
    public class KursKayitController : Controller
    {
        private readonly IConfiguration configuration;

        public KursKayitController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("id") == null)
                return RedirectToAction("Index", "Login");
            List<Models.KursKayıt> list = new List<Models.KursKayıt>();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comm = new SqlCommand(@"SELECT
                                                d.id,
                                            	d.ad, 
                                            	d.kursFiyat,
                                                d.videoId,
                                                d.aciklama,
                                                kk.kursKayitId,
                                                d.kursSuresi,
                                            	CASE 
                                            		WHEN kk.kursKayitId is NULL then 0 
                                            		else 1 
                                            	end kayitoldu
                                            FROM kurslar d 
                                            INNER JOIN kategori k ON k.id = d.kategoriId
                                            INNER JOIN ogrenci o ON o.kategoriId = k.id
                                            LEFT JOIN kursKayıt kk ON kk.ogrenciId = o.id AND kk.kursId = d.id
                                            WHERE 
                                            	o.id= @id 
                                            ORDER BY d.ad", connection);

            if(!int.TryParse(HttpContext.Session.GetString("id"), out var id))
                return RedirectToAction("Index", "Login");

            comm.Parameters.AddWithValue("@id", id);
            adapter.SelectCommand = comm;
            
            connection.Open();

            adapter.Fill(dt);

            connection.Close();

            bool a;

            foreach (System.Data.DataRow row in dt.Rows)
            {
                a = Convert.ToBoolean(row["kayitoldu"]);
                if (a)
                {

                    list.Add(new Models.KursKayıt
                    {
                        KursId = Convert.ToInt32(row["id"]),
                        Id = Convert.ToInt32(row["kursKayitId"]),
                        //DersId = Convert.ToInt32(row["bolumId"]),
                        //OgrenciId = Convert.ToInt32(row["ogrenciId"]),
                        //BasariDurumu = Convert.ToBoolean(row["basariDurumu"]),
                        KayitDurumu = Convert.ToBoolean(row["kayitoldu"]),
                        Ad = row["ad"].ToString(),
                        KursSuresi = Convert.ToInt32(row["kursSuresi"]),
                        KursFiyat = Convert.ToInt32(row["kursFiyat"]),
                        VideoId = row["videoId"].ToString(),
                        Aciklama = row["aciklama"].ToString()

                        //AkademisyenId = Convert.ToInt32(row["akademisyenId"]),
                    }); ;
                }
                else
                {
                    list.Add(new Models.KursKayıt
                    {
                        KursId = Convert.ToInt32(row["id"]),
                        //Id = Convert.ToInt32(row["id"]),
                        //DersId = Convert.ToInt32(row["bolumId"]),
                        //OgrenciId = Convert.ToInt32(row["ogrenciId"]),
                        //BasariDurumu = Convert.ToBoolean(row["basariDurumu"]),
                        KayitDurumu = Convert.ToBoolean(row["kayitoldu"]),
                        Ad = row["ad"].ToString(),
                        KursSuresi = Convert.ToInt32(row["kursSuresi"]),
                        KursFiyat = Convert.ToInt32(row["kursFiyat"]),
                        VideoId = row["videoId"].ToString(),
                        Aciklama = row["aciklama"].ToString()
                        //AkademisyenId = Convert.ToInt32(row["akademisyenId"]),
                    });
                }


            }

            KursListesi kursListesi = new KursListesi();
            kursListesi.list = list;

            return View(kursListesi);

        }
        [HttpPost]
        public ActionResult KursKayit(KursKayıt kayit)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("id"));
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            System.Data.DataTable dt = new System.Data.DataTable();
            if (kayit.KayitDurumu)
            {
                SqlCommand command = new SqlCommand("DELETE FROM kursKayıt WHERE kursKayitId= @id", connection);
                command.Parameters.AddWithValue("@id", kayit.Id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                if (kayit.Total + kayit.Bakiye <= 0)
                {
                    // hata mesajı gönder
                    return RedirectToAction("Index");
                }
                SqlCommand command = new SqlCommand("INSERT INTO kursKayıt(kursId,ogrenciId) VALUES(@kursId,@ogrenciId)", connection);
                command.Parameters.AddWithValue("@ogrenciId", id);
                command.Parameters.AddWithValue("@kursId", kayit.KursId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }



            return RedirectToAction("Index");
        }
    }
}
