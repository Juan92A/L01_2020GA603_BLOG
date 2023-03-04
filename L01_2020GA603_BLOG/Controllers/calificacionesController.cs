using L01_2020GA603_BLOG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace L01_2020GA603_BLOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly dbContext _dbContext;
        public calificacionesController(dbContext contexto)
        {
            _dbContext = contexto;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult obtenerCalificacion()
        {
            List<calificaciones> lstComent = (from db in _dbContext.calificaciones
                                           select db).ToList();

            if (lstComent.Count == 0) { return NotFound(); }

            return Ok(lstComent);
        }

        [HttpGet]
        [Route("find/(publicacionID)")]
        public IActionResult buscarCalificacion(int publicacionID)
        {
            List<calificaciones> lstCalificacion = (from e in _dbContext.calificaciones
                                               where e.publicacionId == publicacionID
                                               select e).ToList();

            if (lstCalificacion.Any())
            {
                return Ok(lstCalificacion);

            }
            return NotFound();
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Crear([FromBody] calificaciones calificacionNew)
        {

            try
            {
                _dbContext.calificaciones.Add(calificacionNew);
                _dbContext.SaveChanges();
                return Ok(calificacionNew);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPut]
        [Route("actualizar(id)")]
        public IActionResult update(int id, [FromBody] calificaciones calificaNew)
        {
            calificaciones? calificaExist = (from e in _dbContext.calificaciones
                                        where e.calificacionId == id
                                        select e).FirstOrDefault();

            if (calificaExist == null)
            {
                return NotFound();
            }
            calificaExist.publicacionId = calificaNew.publicacionId;
            calificaExist.usuarioId = calificaNew.usuarioId;
            calificaExist.calificacion = calificaNew.calificacion;
            _dbContext.Entry(calificaExist).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return Ok(calificaExist);
        }

        [HttpDelete]
        [Route("delete(id)")]
        public IActionResult eliminar(int id)
        {
            calificaciones? califexist = (from e in _dbContext.calificaciones
                                        where e.calificacionId == id
                                        select e).FirstOrDefault();
            if (califexist == null)
            {
                return NotFound();
            }


            _dbContext.calificaciones.Attach(califexist);
            _dbContext.calificaciones.Remove(califexist);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
