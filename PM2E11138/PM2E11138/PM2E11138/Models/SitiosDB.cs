using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PM2E11138.Models;
using SQLite;

namespace PM2E11138.Models
{
    public class SitiosDB
    {
        readonly SQLiteAsyncConnection db;

        public SitiosDB()
        { }

        public SitiosDB(String pathbasedatos)
        {
            db = new SQLiteAsyncConnection(pathbasedatos);
            db.CreateTableAsync <Sitios>();
        }

        public Task<List<Sitios>> ListaSitios()
        {
            return db.Table<Sitios>().ToListAsync();
        }

        public Task<Sitios> ObtenerSitio(Int32 pcodigo)
        {
            return db.Table<Sitios>().Where(i => i.Codigo == pcodigo).FirstOrDefaultAsync();
        }

        public Task<Sitios> ObtenerDescripcion(String pdescripcion)
        {
            return db.Table<Sitios>().Where(i => i.Descripcion == pdescripcion).FirstOrDefaultAsync();
        }

        public Task<Sitios> ObtenerLatitud(float pLatitud)
        {
            return db.Table<Sitios>().Where(i => i.Latitud == pLatitud).FirstOrDefaultAsync();
        }

        public Task<Sitios> ObtenerLongitud(float pLongitud)
        {
            return db.Table<Sitios>().Where(i => i.Longitud == pLongitud).FirstOrDefaultAsync();
        }


        public Task<Int32> GuardarSitio(Sitios site)
        {
            if (site.Codigo!=0) {
                return db.UpdateAsync(site);
            }
            else
            {
                return db.InsertAsync(site);
            }
        }

        public Task<Int32> EliminarSitio(Sitios site)
        {
            return db.DeleteAsync(site);
        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

    }
}
