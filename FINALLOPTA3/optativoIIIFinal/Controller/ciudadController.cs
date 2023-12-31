﻿using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Service.Service;
using Infra.Modelos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPTA3FINALSWAGGER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private const string connectionString = "Host=localhost;User Id=postgres;Password=Temporal2022;Database=OP3FINAL;";
        private CiudadServicio ciudadService;

        public CiudadController()
        {
            ciudadService = new CiudadServicio(connectionString);
        }

        // Busqueda por ID
        [HttpGet("buscarid/{id}")]
        public IActionResult obtenerCiudadporIdAccion([FromRoute] int id)
        {
            var ciudad = ciudadService.obtenerCiudadporId(id); // Llama al método en el servicio
            return Ok(ciudad);
        }

        //Listado de registros
        [HttpGet("listado/")]
        public IActionResult obtenerCiudadesAccion()
        {
            var ciudades = ciudadService.obtenerCiudades();
            return Ok(ciudades);
        }


        // POST api/<ValuesController>
        [HttpPost("insertar")]
        public IActionResult insertarCiudadAccion([FromBody] CiudadModel ciudad)
        {
            ciudadService.insertarCiudad(ciudad);
            return Created("Regsitro insertado con exito!", ciudad);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("modificar/{id}")]
        public IActionResult ModificarCiudadAccion([FromBody] CiudadModel ciudad)
        {
            ciudadService.modificarCiudad(ciudad);
            return Ok("Registro editado con exito!");
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("borrar/{id}")]
        public IActionResult eliminarCiudadAccion(int id)
        {
            ciudadService.eliminarCiudad(id);
            return Ok("Registro eliminado con exito!");
        }
    }
}
