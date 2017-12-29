using ForeignExchange.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ForeignExchange
{
   public  class ApiService
    {
        public async Task<Response> CheckConnection()
        {

            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Check your internet settings"
                    };


                }
            }
         
          
              catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }

            var response = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!response)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check your internet connection."
                };

            }
        

            return new Response
            {
                IsSuccess = true,
        
            };

            }
            public async Task<Response> GetList<T>(string urlBase,string controller)
        {


            try
            {
                var client = new HttpClient(); //instanciamos el cliente para consumir
                client.BaseAddress = new Uri(urlBase); //definimos la url base del servicio
                //hasta hora solo hemos puesto las balas a la pistola pero aun no hemos disparado
                var response = await client.GetAsync(controller); //aqui disparamos
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {

                        IsSuccess = false,
                        Message = result,
                    };
                }
                //si si hay resultado hay que convertir el string al objeto deseado en este caso una lista de rates
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {

                    IsSuccess = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {

                    IsSuccess = false,
                    Message = ex.Message,
                };

            }
        }
    }
}
