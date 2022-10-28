using SQLite;
using WebAPI_CitaMedica.Models;

namespace WebAPI_CitaMedica.Modelos
{
    [Table("Doctor")]
    public class Doctor //: BaseModel
    {
        [PrimaryKey]
        //public int Key { get; set; }//{ get { return string.Format("", DNI,"", CitaMedicaId); } }
        public long CitaMedicaId { get; set; }
        public double DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
    }
}
