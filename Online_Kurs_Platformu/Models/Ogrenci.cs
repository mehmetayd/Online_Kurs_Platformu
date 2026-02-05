using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Online_Kurs_Platformu.Models
{
    public class Ogrenci
    {
        public int Id { get; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int? KategoriId { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
    }
}
