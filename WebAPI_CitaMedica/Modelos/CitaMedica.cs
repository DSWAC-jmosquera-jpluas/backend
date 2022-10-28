using SQLite;
using System.ComponentModel.DataAnnotations;
using WebAPI_CitaMedica.Models;

namespace WebAPI_CitaMedica.Modelos
{
    [Table("CitaMedica")]
    public class CitaMedica //: BaseModel
    {
        [PrimaryKey, AutoIncrement]
        //public int Key { get; set; }     //{ get { return string.Format("", CitaMedicaId); } }
        public long CitaMedicaId { get; set; }      
        public string CitaMedicaLugar { get; set; }
        public DateTime CitaMedicaFecha { get; set; }
        public string CitaMedicaHora { get; set; }
        public string CitaMedicaObservacion { get; set; }
        public bool CitaMedicaEstatus { get; set; }
        public bool CitaMedicaVisible { get; set; }
        [Ignore]
        public Doctor doctor { get; set; }
        [Ignore]
        public Paciente paciente { get; set; }

    }
}
