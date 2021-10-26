using API.Models;
using CoaAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsuariosController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsers()
        {
            IEnumerable<Usuario> usuarios = Enumerable.Empty<Usuario>();
            try
            {
                return Ok( _userService.GetUserList());
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
           

        }

        [HttpGet]
        public ActionResult <Usuario> GetSpecificUser(int idUser)
         {

                var user = _userService.GetSpecifictUser(idUser);
                if(user != null)
                    return Ok(user);
                else
                    return BadRequest("No existe el usuario buscado");
        }


        [HttpDelete]
        public ActionResult DeleteUser( int idUser)
        {

            var user = _userService.GetSpecifictUser(idUser);
            if (user == null)
                return BadRequest("No existe el usuario a borrar");
            else
            {
                try
                {
                    _userService.DeleteUser(user);
                    return NoContent();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }               
        }

        [HttpPut]
        public ActionResult UpdateUser(int id,string userName,string nombre,string email,string telefono )
        {
            if(string.IsNullOrEmpty(id.ToString()) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email))
                return BadRequest("Datos incompletos para editar");

            var idUsado = _userService.FreeUserName(userName);

            if (idUsado != 0 && idUsado != id)
                return BadRequest("Nombre de usuario ya utilizado");

             idUsado = _userService.FreeEmail(email);

            if (idUsado != 0 && idUsado != id)
                return BadRequest("El mail se encuentra registrado");

            else
            {
                try
                {
                    Usuario nuevosDatos = new Usuario()
                    {
                        IdUsuario = id,
                        UserName = userName,
                        Nombre = nombre,
                        Email = email,
                        Telefono = telefono
                    };
                    _userService.UpdateUser(nuevosDatos);
                    return NoContent();
                }
                catch(NoUserException ex)
                {
                    return BadRequest(ex.Message);
                }

                catch (CookieException e)
                {
                    throw new Exception(e.Message);
                }

            }
        }


        [HttpPost]
        public ActionResult AddUser(string userName, string nombre, string email, string telefono)
        {

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email))
                return BadRequest("datos incompletos");

            if (_userService.FreeUserName(userName) != 0)
                return BadRequest("nombre de usuario utilizado"); 


            if (_userService.FreeEmail(email) != 0)
                return BadRequest("el mail se encuentra registrado");
            try
            {
                Usuario nuevosDatos = new Usuario()
                {
                    UserName = userName,
                    Nombre = nombre,
                    Email = email,
                    Telefono = telefono
                };
                _userService.CreateUser(nuevosDatos);
                return NoContent();
            }

            catch (CookieException e)
            {
                throw new Exception(e.Message);
            }
        }



    }

}
