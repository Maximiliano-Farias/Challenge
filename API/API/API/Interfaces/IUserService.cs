using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoaAPI.Interfaces
{
    public interface IUserService
    {
        IEnumerable<Usuario> GetUserList();
        Usuario GetSpecifictUser(int id);

        void DeleteUser(Usuario user);

        void UpdateUser(Usuario nuevosDatos);

        int FreeUserName(string userName);

        int FreeEmail(string email);

        void CreateUser(Usuario nuevosDatos);

    }
}
