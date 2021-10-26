using API.Models;
using CoaAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;

namespace CoaAPI.Services
{
    public class UserService : IUserService 
    {
        private readonly ChallengeContext _dbContext;

        public UserService(ChallengeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Usuario> GetUserList()
        {
            IEnumerable<Usuario> userList = Enumerable.Empty<Usuario>();
            try {
                userList = (from user in _dbContext.Usuarios orderby user.UserName
                            select user ).ToList();
            
            }
            catch(EntitySqlException ex)
            {
                throw new Exception(ex.ToString());
            }
            return userList;
        }

        public Usuario GetSpecifictUser(int idUser) {
            Usuario user = new Usuario();
            try
            {
                user = (from users in _dbContext.Usuarios where users.IdUsuario == idUser
                        select users).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return user;

        }

        public void DeleteUser(Usuario user)
        {         
            try
            {
                _dbContext.Usuarios.Remove(user);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void UpdateUser(Usuario nuevosDatos)
        {
            try
            {
                var userForEdit = GetSpecifictUser(nuevosDatos.IdUsuario);
                if (userForEdit == null)
                      throw new NoUserException(nuevosDatos.IdUsuario);


                userForEdit.Nombre = nuevosDatos.Nombre;
                userForEdit.UserName = nuevosDatos.UserName;
                userForEdit.Email = nuevosDatos.Email;
                userForEdit.Telefono = nuevosDatos.Telefono;

                _dbContext.Update(userForEdit);
                _dbContext.SaveChanges();
            }
            catch (NoUserException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            

        }

        public void CreateUser(Usuario user)
        {
            try
            {
                _dbContext.Usuarios.Add(user);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// Recibe el nombre de usuario a buscar, en caso de estar utilizado devuelve el idUsuario al cual pertenece
        /// </summary>
        public int FreeUserName(string userName) => (from user in _dbContext.Usuarios where user.UserName == userName select user.IdUsuario).FirstOrDefault();

        /// <summary>
        /// Recibe el email a buscar, en caso de estar utilizado devuelve el idUsuario al cual pertenece
        /// </summary>
        public int FreeEmail(string email) => (from user in _dbContext.Usuarios where user.Email == email select user.IdUsuario).FirstOrDefault();




    }
}
