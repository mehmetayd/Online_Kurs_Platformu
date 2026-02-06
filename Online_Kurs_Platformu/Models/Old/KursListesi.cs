using System.Collections.Generic;

namespace OnlineCourse.UI.Models.Old
{
    public class KursListesi
    {
        public List<KursKayıt> list = new List<KursKayıt>();

        public int toplamKredi()
        {
            int total = 0;
            foreach (var item in list)
                if (item.BasariDurumu == false && item.KayitDurumu == true)
                    total += item.Kredi;
            return total;
        }
        /* Burada satın alma ekranı olabilir sepet tutar toplamı gibi.
         kaç tane kursumuzun olduğu bilgisi tutulabilir. */

        public int toplamBakiye()
        {
            int total = 100;
            foreach (var item in list)
                if (item.KayitDurumu == true)
                    total -= item.KursFiyat;
            return total;
        }
        /* Burada satın alma ekranı olabilir sepet tutar toplamı gibi.
         kaç tane kursumuzun olduğu bilgisi tutulabilir. */

    }
}
