

namespace ForeignExchange.Data
{
    using ForeignExchange.Interfaces;
    using Models;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class DataAccess : IDisposable
    {
        readonly SQLiteConnection connection;
        public DataAccess()
        {
     
           var ruta = DependencyService.Get<IConfig>().GetLocalFilePath("TodoSQLite.db3");
            connection = new SQLiteConnection(ruta);
            connection.CreateTable<Rate>();

        }

        public void Insert<T>(List<T> model)
        {

            // connection.InsertAsync(model);
            connection.InsertAll(model);
        }

        public void Update<T>(T model)
        {
            connection.Update(model);
        }

        public void Delete()
        {
            //connection.DeleteAsync(model);
            connection.Query<Rate>("DELETE FROM [Rate]");
        }

        //public Task<Rate> GetItemAsync(int id)
        //{
        //    return connection.Table<Rate>().Where(i => i.RateId == id).FirstOrDefaultAsync();
        //}

        public List<T> GetItems<T>() where T : new()
        {
           
            return connection.Table<T>().ToList();
        }


  


        public void Dispose()
        {
          
        }
    }
}
