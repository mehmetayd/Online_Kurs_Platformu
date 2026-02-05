using System.Collections.Generic;

namespace Online_Kurs_Platformu.Models
{
    public class RegisterKursModel
    {
        public List<Kategori> Kategoriler { get; set; }
        public KursModel Kurs { get; set; }
    }
}
