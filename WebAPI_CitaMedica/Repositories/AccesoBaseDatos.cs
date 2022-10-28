using SQLite;
using WebAPI_CitaMedica.Modelos;

namespace WebAPI_CitaMedica.Repositories
{
    public class AccesoBaseDatos
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        public AccesoBaseDatos(string dbPath)
        {
            // create the tables
            CreateTable(dbPath);
        }

        public AccesoBaseDatos()
        {
        }

        private void CreateTable(string dbPath)
        {
            using (var ABD = new  SQLiteConnection(dbPath))
            {
                ABD.CreateTable<CitaMedica>();
                ABD.CreateTable<Doctor>();
                ABD.CreateTable<Paciente>();
               
            }
        }
    }
}
