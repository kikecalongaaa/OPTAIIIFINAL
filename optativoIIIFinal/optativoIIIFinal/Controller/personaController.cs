using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Infra.Modelos;
using Service.Service;



namespace OPTA3FINALSWAGGER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private const string connectionString = "Host=localhost;User Id=postgres;Password=Temporal2022;Database=OP3FINAL;";
        private PersonasServicio PersonaService;

        public PersonaController()
        {
            PersonaService = new PersonasServicio(connectionString);
        }

        // Busqueda por ID
        [HttpGet("buscarcedula/{cedula}")]
        public IActionResult obtenerPersonaporIdAccion([FromRoute] string cedula)
        {
            var Persona = PersonaService.obtenerPersonaPorCedula(cedula); // Llama al método en el servicio
            return Ok(Persona);
        }

        //Listado de registros
        [HttpGet("listado/")]
        public IActionResult obtenerPersonaesAccion()
        {
            var Personas = PersonaService.obtenerPersonas();
            return Ok(Personas);
        }


        // POST api/<ValuesController>
        [HttpPost("insertar")]
        public IActionResult insertarPersonaAccion([FromBody] PersonaModel Persona)
        {
            PersonaService.insertarPersona(Persona);
            return Created("Regsitro insertado con exito!", Persona);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("modificar/{cedula}")]
        public IActionResult ModificarPersonaAccion([FromBody] PersonaModel Persona)
        {
            PersonaService.modificarPersona(Persona);
            return Ok("Registro editado con exito!");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("borrar/{cedula}")]
        public IActionResult eliminarPersonaAccion(string cedula)
        {
            PersonaService.eliminarPersona(cedula);
            return Ok("Registro eliminado con exito!");
        }
    }
}
