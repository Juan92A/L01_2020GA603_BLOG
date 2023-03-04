using L01_2020GA603_BLOG.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace L01_2020GA603_BLOG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly dbContext _dbContext;
        public usuariosController(dbContext contexto)
        {
            _dbContext = contexto;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult obtenerUsuarios()
        {
            List<usuarios> lstUsers = (from db in _dbContext.usuarios                                        
                                           select db).ToList();

            if (lstUsers.Count == 0) { return NotFound(); }

            return Ok(lstUsers);
        }

        [HttpGet]
        [Route("find/(filtro)")]
        public IActionResult buscarNombreApellido(string filtro)
        {
            List<usuarios> lstUsuarios = (from e in _dbContext.usuarios
                                           where e.nombre.Contains(filtro)   
                                           || e.apellido.Contains(filtro)
                                           select e).ToList();
           
            if (lstUsuarios.Any())
            {
                return Ok(lstUsuarios);

            }
            return NotFound();
        }
        
        [HttpGet]
        [Route("find/(rolId)")]
        public IActionResult buscarByRol(int rolId)
        {
            List<usuarios> lstUsuarios = (from e in _dbContext.usuarios
                                          where e.rolId == rolId
                                          select e).ToList();

            if (lstUsuarios.Any())
            {
                return Ok(lstUsuarios);

            }
            return NotFound();
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Crear([FromBody] usuarios userNuevos)
        {

            try
            {
                _dbContext.usuarios.Add(userNuevos);
                _dbContext.SaveChanges();
                return Ok(userNuevos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPut]
        [Route("actualizar(id)")]
        public IActionResult update(int id, [FromBody] usuarios userNew)
        {
            usuarios? userExist = (from e in _dbContext.usuarios
                                     where e.usuarioId == id
                                     select e).FirstOrDefault();

            if (userExist == null)
            {
                return NotFound();
            }
            userExist.nombre = userNew.nombre;
            userExist.clave = userNew.clave;
            userExist.rolId = userNew.rolId;
            userExist.apellido = userNew.apellido;
            _dbContext.Entry(userExist).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return Ok(userExist);
        }

        [HttpDelete]
        [Route("delete(id)")]
        public IActionResult eliminar(int id)
        {
            usuarios? equipoExiste = (from e in _dbContext.usuarios
                                     where e.usuarioId == id
                                     select e).FirstOrDefault();
            if (equipoExiste == null)
            {
                return NotFound();
            }
        
            
            _dbContext.usuarios.Attach(equipoExiste);
            _dbContext.usuarios.Remove(equipoExiste);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
