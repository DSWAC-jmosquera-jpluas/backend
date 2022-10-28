using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WebAPI_CitaMedica.Repositories
{
    public class DataProvider : Repository
    {
        public static DataProvider Instance { get; set; }
        public static void CreateInstance(string dbPath)
        {
            Instance = new DataProvider(dbPath);
        }
        public static DataProvider GetInstance()
        {
            if (Instance == null)
                throw new Exception("No ha creado la instancia");
            return Instance;
        }
        public DataProvider(string dbPath)
        {
            //Log.Info("Inicio - CreateDataBase");           
            //Debug.WriteLine("Ruta SQLite: " + dbPath);
            _ = new AccesoBaseDatos(dbPath);//Actualizar Base Datos
            //Log.Info("Fin - CreateDataBase");
        }
    }
}
