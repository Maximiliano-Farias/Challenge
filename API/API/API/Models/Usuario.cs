using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }

    [Serializable]
    class NoUserException : Exception
    {
        public NoUserException() { }

        public NoUserException(int idUser)
            : base(String.Format("idUsuario Inexitente: {0}", idUser))
        {

        }
    }
}
