using SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebAPI_CitaMedica.Models
{
    abstract public class BaseModel
    {
        abstract public string Key { get; }
    }
}
