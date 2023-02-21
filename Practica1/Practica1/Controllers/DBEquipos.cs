using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Practica1.Controllers
{
    public class DBEquipos
    {
        //Global Variables
        readonly SQLiteAsyncConnection _connection;

        //Contructor Vacio
        public DBEquipos()
        { }

        public DBEquipos(string dbpath)
        {
            _connection = new SQLiteAsyncConnection(dbpath);

            //Creacion de Objetos de Base de Datos
            _connection.CreateTableAsync<Model.Equipos>().Wait();

        }

        //CRUD - Create / Read / Update / Delete
        public Task<int> StoreEquipo(Model.Equipos equipo)
        {
            if (equipo.Id == 0)
            {
                return _connection.InsertAsync(equipo);
            }
            else
            {
                return _connection.UpdateAsync(equipo);
            }
        }
        // Read List
        public Task<List<Model.Equipos>> ListaEquipos()
        {
            return _connection.Table<Model.Equipos>().ToListAsync();
        }


        //Get 
        public Task<Model.Equipos> GetEquipos(int pid)
        {
            return _connection.Table<Model.Equipos>().Where(i => i.Id == pid).FirstOrDefaultAsync();
        }


        // Delete
        public Task<int> DeleteEquipo(Model.Equipos equipos)
        {
            return _connection.DeleteAsync(equipos);
        }
    }
}
