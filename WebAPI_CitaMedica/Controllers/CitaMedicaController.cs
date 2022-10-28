using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI_CitaMedica.Modelos;
using WebAPI_CitaMedica.Repositories;

namespace WebAPI_CitaMedica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitaMedicaController : Controller
    {
        private readonly ILogger<CitaMedicaController> _logger;

        public CitaMedicaController(IWebHostEnvironment webHostEnvironment, ILogger<CitaMedicaController> logger)
        {
            _logger = logger;

            //var ruta = "C:\\Users\\fedep";//webHostEnvironment.ContentRootPath;
            var ruta = Path.Combine(webHostEnvironment.ContentRootPath, "CitaMedicaV3.db3");
            DataProvider.CreateInstance(ruta);
            Repository.SetConexion(ruta);
        }

        // GET: CitaMedica/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var listCitaMedica = new List<CitaMedica>();
            try
            {
                listCitaMedica = Repository._CitaMedica.GetItems().Where(x=>x.CitaMedicaVisible).ToList();
                var listDoctor = Repository._Doctor.GetItems();
                var listPaciente = Repository._Paciente.GetItems();

                if(listCitaMedica != null && listCitaMedica.Any())
                {
                    listCitaMedica.ForEach(x => x.doctor = listDoctor.FirstOrDefault(y => y.CitaMedicaId == x.CitaMedicaId));
                    listCitaMedica.ForEach(x => x.paciente = listPaciente.FirstOrDefault(y => y.CitaMedicaId == x.CitaMedicaId));
                }
                else
                {
                    return NoContent();
                }
                
            }
            catch (Exception ex)
            {
                return Conflict(ex);
            }

            return Ok(listCitaMedica);
        }

        // GET: CitaMedica/GetAll
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(long CitaMedicaId)
        {
            var citaMedica = new CitaMedica();
            try
            {
                citaMedica = Repository._CitaMedica.GetItems().FirstOrDefault(x => x.CitaMedicaId == CitaMedicaId);
                var listDoctor = Repository._Doctor.GetItems();
                var listPaciente = Repository._Paciente.GetItems();

                if (citaMedica != null)
                {
                    citaMedica.doctor = listDoctor.FirstOrDefault(y => y.CitaMedicaId == CitaMedicaId);
                    citaMedica.paciente = listPaciente.FirstOrDefault(y => y.CitaMedicaId == CitaMedicaId);

                    //listCitaMedica.ForEach(x => x.doctor = listDoctor.FirstOrDefault(y => y.CitaMedicaId == x.CitaMedicaId));
                    //listCitaMedica.ForEach(x => x.paciente = listPaciente.FirstOrDefault(y => y.CitaMedicaId == x.CitaMedicaId));
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                return Conflict(ex);
            }

            return Ok(citaMedica);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CitaMedica citaMedica)
        {
            try
            {
                //long id = 0;
                //if(citaMedicaDB != null && citaMedicaDB.Any())
                //{
                //    id = citaMedicaDB.Max(x => x.CitaMedicaId) + 1;
                //}
                //else
                //{
                //    id = 1;
                //}

                //citaMedica.CitaMedicaId = id;
                Repository.ADBBeginTransaction();
                citaMedica.CitaMedicaVisible = true;
                Repository._CitaMedica.SaveItem(citaMedica);

                long id = Repository._CitaMedica.GetItems().Max(x=>x.CitaMedicaId);
                citaMedica.doctor.CitaMedicaId = id;
                citaMedica.paciente.CitaMedicaId = id;
                Repository._Paciente.SaveItem(citaMedica.paciente);
                Repository._Doctor.SaveItem(citaMedica.doctor);
                Repository.ADBCommit();

            }
            catch (Exception ex)
            {
                Repository.ADBRollback();
                return Conflict(ex);
            }

            return Created(string.Empty, citaMedica);

        }

        [HttpPut]
        public async Task<IActionResult> Put(CitaMedica citaMedica)
        {
            try
            {
                var citaMedicaDB = Repository._CitaMedica.GetItems();
                //long id = 0;
                if (citaMedicaDB != null && citaMedicaDB.Any())
                {
                    Repository.ADBBeginTransaction();
                    citaMedica.CitaMedicaVisible = true;
                    Repository._CitaMedica.UpdateItem(citaMedica);
                    Repository._Doctor.UpdateItem(citaMedica.doctor);
                    Repository._Paciente.UpdateItem(citaMedica.paciente);
                    Repository.ADBCommit();
                }
                else
                {
                    //id = 1;
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Repository.ADBRollback();
                return Conflict(ex);
            }

            return Ok(citaMedica);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long CitaMedicaId)
        {
            try
            {
                var citasMedicaDB = Repository._CitaMedica.GetItems();
                if(citasMedicaDB != null)
                {
                    var citaMedica = citasMedicaDB.FirstOrDefault(x => x.CitaMedicaId == CitaMedicaId);
                    //long id = 0;
                    if(citaMedica != null)
                    {
                        citaMedica.CitaMedicaVisible = false;
                        long output = Repository._CitaMedica.UpdateItem(citaMedica);
                        //long output = Repository._CitaMedica.Delete(citaMedica);
                        //Repository._Doctor.Delete(citaMedica.doctor);
                        //Repository._Paciente.Delete(citaMedica.paciente);
                        if (output == 0)
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Conflict(ex);
            }

            return Ok(CitaMedicaId);
        }

    }
}
