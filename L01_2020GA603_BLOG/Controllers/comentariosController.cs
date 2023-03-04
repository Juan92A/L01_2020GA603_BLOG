using L01_2020GA603_BLOG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace L01_2020GA603_BLOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly dbContext _dbContext;
        public comentariosController(dbContext contexto)
        {
            _dbContext = contexto;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult obtenerComentario()
        {
            List<comentarios> lstComent = (from db in _dbContext.comentarios
                                       select db).ToList();

            if (lstComent.Count == 0) { return NotFound(); }

            return Ok(lstComent);
        }

        [HttpGet]
        [Route("find/(usuario)")]
        public IActionResult buscaByUser(int usuarioId)
        {
            List<comentarios> lstComentario = (from e in _dbContext.comentarios
                                          where e.usuarioId == usuarioId                                        
                                          select e).ToList();

            if (lstComentario.Any())
            {
                return Ok(lstComentario);

            }
            return NotFound();
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Crear([FromBody] comentarios comentarioNuevo)
        {

            try
            {
                _dbContext.comentarios.Add(comentarioNuevo);
                _dbContext.SaveChanges();
                return Ok(comentarioNuevo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPut]
        [Route("actualizar(id)")]
        public IActionResult update(int id, [FromBody] comentarios comentNew)
        {
            comentarios? comentExist = (from e in _dbContext.comentarios
                                   where e.cometarioId == id
                                   select e).FirstOrDefault();

            if (comentExist == null)
            {
                return NotFound();
            }
            comentExist.publicacionId = comentNew.publicacionId;
            comentExist.comentario = comentNew.comentario;
            comentExist.usuarioId = comentNew.usuarioId;
            _dbContext.Entry(comentExist).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return Ok(comentExist);
        }

        [HttpDelete]
        [Route("delete(id)")]
        public IActionResult eliminar(int id)
        {
            comentarios? comentExist = (from e in _dbContext.comentarios
                                      where e.cometarioId == id
                                      select e).FirstOrDefault();
            if (comentExist == null)
            {
                return NotFound();
            }


            _dbContext.comentarios.Attach(comentExist);
            _dbContext.comentarios.Remove(comentExist);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
