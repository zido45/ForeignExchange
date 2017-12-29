using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignExchange.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using ForeignExchange.Interfaces;
    using ForeignExchange.Models;
    using SQLite;
    using Xamarin.Forms;

    public class DataService
    {
   

        public bool DeleteAll<T>() where T : class
        {
            try
            {
                using (var da = new DataAccess())
                {

                    da.Delete();
                    //connection.QueryAsync<Rate>("DELETE FROM [Rate]");

                }

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        public void Save<T>(List<T> list) where T : class
        {
            using (var da = new DataAccess())
            {
               
                    da.Insert(list);
                
            }
        }



        public List<T> Get<T>() where T : new()
        {
            using (var da = new DataAccess())
            {
                return da.GetItems<T>().ToList();
            }
        }


       


    }

}
