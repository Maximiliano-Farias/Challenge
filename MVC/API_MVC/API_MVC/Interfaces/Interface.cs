using API_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_MVC.Interfaces
{
    public interface IApiService
    {
        IEnumerable<Usuario> GetUsuarios();

        void DeleteUser(int idUser);

        Usuario GetSpecificUser(int idUser);

        void CrearUser(string userName, string nombre, string email, string telefono);

        void UpdateUser(int id, string userName, string nombre, string email, string telefono);
    }
}
