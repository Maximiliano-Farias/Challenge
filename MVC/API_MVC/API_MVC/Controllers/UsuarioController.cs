using API_MVC.Interfaces;
using API_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IApiService _iApiServices;

        public UsuarioController( IApiService iApiServices)
        {
            _iApiServices = iApiServices;
        }

        public IActionResult Index()
        {
            var datos =_iApiServices.GetUsuarios();
            ViewData["listaUsuarios"] = datos;
            return View();
        }

        public IActionResult DeleteUser(int idUser)
        {
            try
            {
                _iApiServices.DeleteUser(idUser);
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
            return Ok("Usuario borrado");
        }

        public IActionResult EditCreate(int idUser)
        {
            Usuario user = new Usuario();
            try
            {
                user= _iApiServices.GetSpecificUser(idUser);
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("400"))
                   throw new Exception (ex.Message);
            }

            ViewData["usuario"] = user;
            return  View();
        }

        public IActionResult CreateUser( string userName, string nombre, string email, string telefono )
        {
            try
            {
               _iApiServices.CrearUser(userName, nombre, email, telefono);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                    
            }

            return Ok();
        }

        public IActionResult UpdateUser(int  id, string userName, string nombre, string email, string telefono)
        {
            try
            {
                _iApiServices.UpdateUser(id, userName, nombre, email, telefono);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

            return Ok();
        }


    }
}
