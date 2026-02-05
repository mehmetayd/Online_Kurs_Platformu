using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Kurs_Platformu.Models
{
    public class KursModel
    {
        public int Id { get; set; }
        public int KategoriId { get; set; }
        public string Ad { get; set; }
        public int Donem { get; set; }
        public int Kredi { get; set; }

        public int KursSuresi { get; set; }
        public int KursFiyat{ get; set; }
        public string VideoId { get; set; }
        public string Aciklama { get; set; }
    }
}
