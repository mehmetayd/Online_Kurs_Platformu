namespace OnlineCourse.UI.Models.Old
{
    public class KursKayıt
    {
        public int Id { get; set; }
        public int KursId { get; set; }
        public int OgrenciId { get; set; }
        public bool BasariDurumu { get; set; }
        public bool KayitDurumu { get; set; }
        public int Kredi { get; set; }
        public string Ad { get; set; }
        public int Donem { get; set; }
        public int AkademisyenId { get; set; }
        public int Total { get; set; }
        public int KursSuresi { get; set; }
        public int KursFiyat { get; set; }
        public int Bakiye { get; set; }
        public string VideoId { get; set; }
        public string Aciklama { get; set; }
    }
}
