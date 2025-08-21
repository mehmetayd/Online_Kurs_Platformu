using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Online_Kurs_Platformu.Models
{
    public class Kategori
    {
        public int Id { get; set; }
        public string Ad { get; set; }
    }
}
