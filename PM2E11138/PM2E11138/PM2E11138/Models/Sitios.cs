using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PM2E11138.Models
{
    public class Sitios
    {
        [PrimaryKey, AutoIncrement]
        public int Codigo { get; set; }

        public byte[] Imagen { get; set; }

        public float Latitud { get; set; }

        public float Longitud { get; set; }

        public String Descripcion { get; set; }

    }
}
