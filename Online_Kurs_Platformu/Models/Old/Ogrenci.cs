namespace OnlineCourse.UI.Models.Old
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
