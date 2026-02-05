using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Kurs_Platformu.Models
{
    public class Egitmen
    {
        //public Personel(string ad, string soyad, int bolumid, string password, string email)
        //{
        //    this.Ad = ad;
        //    this.Soyad = soyad;
        //    this.Sifre = password;
        //}

        public int Id { get; }
        public int? KategoriId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
    }
}
