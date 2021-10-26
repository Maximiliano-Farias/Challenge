using API_MVC.Interfaces;
using API_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace API_MVC.Services
{
    public class ApiService : IApiService
    {
        public IEnumerable<Usuario> GetUsuarios()
        {
            IEnumerable<Usuario> userList = Enumerable.Empty<Usuario>();

            var url = $"https://localhost:44329/api/Usuarios/GetUsers";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            userList = JsonConvert.DeserializeObject<IEnumerable<Usuario>>(responseBody);

                        }
                    }
                }
            }
            catch (WebException ex)
            {
                return null;
            }

            return userList;

        }


        public void DeleteUser(int idUser)
        {
            string URL = "https://localhost:44329/api/Usuarios/DeleteUser?idUser="+idUser;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "DELETE";
            request.ContentType = "application/json";
            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();

                responseReader.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Usuario GetSpecificUser(int idUser)
        {
            Usuario user = new Usuario();
            string URL = "https://localhost:44329/api/Usuarios/GetSpecificUser?idUser=" + idUser;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            user = JsonConvert.DeserializeObject<Usuario>(responseBody);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;

        }

        public void CrearUser(string userName, string nombre, string email, string telefono)
        {
            string parametros = "userName=" + userName + "&nombre=" + nombre + "&email=" + email + "&telefono=" + telefono;
            string URL = "https://localhost:44329/api/Usuarios/AddUser?" + parametros;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            try { 
                using (WebResponse response = request.GetResponse())
                {
                   
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            throw new Exception("no se pudo registrar");
                    }
                }
            }
            catch (WebException ex)
            {
                StreamReader sr = new StreamReader(ex.Response.GetResponseStream());
               var mensaje = sr.ReadToEnd();
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void UpdateUser(int id ,string userName, string nombre, string email, string telefono)
        {
            string parametros = "id="+id +"&userName=" + userName + "&nombre=" + nombre + "&email=" + email + "&telefono=" + telefono;
            string URL = "https://localhost:44329/api/Usuarios/UpdateUser?" + parametros;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "PUT";
            request.ContentType = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {

                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                            throw new Exception("no se pudo registrar");
                    }
                }
            }
            catch (WebException ex)
            {
                StreamReader sr = new StreamReader(ex.Response.GetResponseStream());
                var mensaje = sr.ReadToEnd();
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
