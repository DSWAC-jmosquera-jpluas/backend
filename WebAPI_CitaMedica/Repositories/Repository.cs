using SQLite;
using System.Diagnostics;
using WebAPI_CitaMedica.Modelos;

namespace WebAPI_CitaMedica.Repositories
{
    public class Repository 
    {
        #region Base Datos
        private static SQLiteConnection _ADB = SetConexion(string.Empty);

        public static SQLiteConnection SetConexion(string dataBaseFilePathTransaccional)
        {
            if (!string.IsNullOrEmpty(dataBaseFilePathTransaccional) && _ADB == null)
            {
                _ADB = new SQLiteConnection(dataBaseFilePathTransaccional);
            }
            return _ADB;
        }


        public static void ADBBeginTransaction()
        {
            _ADB.BeginTransaction();
        }

        public static void ADBCommit()
        {
            _ADB.Commit();
        }

        public static void ADBRollback()
        {
            _ADB.Rollback();
        }
        #endregion 
        private static BaseRepository<CitaMedica> _citaMedica = _ADB == null ? null : new BaseRepository<CitaMedica>(_ADB);
        private static BaseRepository<Doctor> _doctor = _ADB == null ? null : new BaseRepository<Doctor>(_ADB);
        private static BaseRepository<Paciente> _paciente = _ADB == null ? null : new BaseRepository<Paciente>(_ADB);

        #region Repositorios

        public static BaseRepository<CitaMedica> _CitaMedica
        {
            get
            {
                if (_citaMedica == null)
                {
                    _citaMedica = new BaseRepository<CitaMedica>(_ADB);
                }
                return _citaMedica;
            }
        }

        public static BaseRepository<Doctor> _Doctor
        {
            get
            {
                if (_doctor == null)
                {
                    _doctor = new BaseRepository<Doctor>(_ADB);
                }
                return _doctor;
            }
        }

        public static BaseRepository<Paciente> _Paciente
        {
            get
            {
                if (_paciente == null)
                {
                    _paciente = new BaseRepository<Paciente>(_ADB);
                }
                return _paciente;
            }
        }
        #endregion

    }
}
