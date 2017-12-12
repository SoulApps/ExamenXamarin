using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamenXamarin.model;
using SQLite;

namespace ExamenXamarin
{
    /// <summary>
    /// Clase DBRepository que gestionara la llamada
    /// a la BBDD.
    /// </summary>
    public class DBRepository
    {
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;

        /// <summary>
        /// Este método iniciará una nueva conexión a la BBDD
        /// </summary>
        /// <param name="dbPath">Ruta de la BBDD</param>
        public DBRepository(string dbPath)
        {
            conn = new SQLiteAsyncConnection(dbPath);
            CreateTables(conn);
        }

        //---------------- [ table creations ] -----------------
        /// <summary>
        /// Método que llama a las distintas funciones de creación de tablas
        /// </summary>
        /// <param name="conn">Objeto de la conexión de la BBDD</param>
        private void CreateTables(SQLiteAsyncConnection conn)
        {
            conn.CreateTableAsync<Horario>().Wait();
            conn.CreateTableAsync<Objetivo>().Wait();
            conn.CreateTableAsync<Usuario>().Wait();
        }

        //---------------- [ table creations ] -----------------

        //---------------- [ GET DATA ] -----------------
        /// <summary>
        /// Método que obtiene un usuario en función de su nick
        /// </summary>
        /// <remarks>
        /// Creamos una sentencia SQL con el código de SQLite en la cual obtendremos
        /// el usuario en función de su nombre
        /// </remarks>
        /// <returns>Retorna el usuario consultado</returns>
        internal async Task<Usuario> GetUserByDNIAsync(string dni)
        {
            Usuario u = null;
            try
            {
                var sql = from p in conn.Table<Usuario>()
                           where p.DNI == dni
                           select p;
                u = await sql.FirstAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return u;
        }


        /// <summary>
        /// Método que obtiene un usuario en función de su nick
        /// </summary>
        /// <remarks>
        /// Creamos una sentencia SQL con el código de SQLite en la cual obtendremos
        /// el usuario en función de su nombre
        /// </remarks>
        /// <returns>Retorna el usuario consultado</returns>
        internal async Task<Horario> GetHorarioById(int id)
        {
            Horario u = null;
            try
            {
                var sql = from p in conn.Table<Horario>()
                           where p.ID == id
                           select p;
                u = await sql.FirstAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return u;
        }


        /// <summary>
        /// Método que obtiene un objetivo en función de su id
        /// </summary>
        /// <remarks>
        /// Creamos una sentencia SQL con el código de SQLite en la cual obtendremos
        /// el usuario en función de su nombre
        /// </remarks>
        /// <returns>Retorna el usuario consultado</returns>
        internal async Task<Objetivo> GetObjetivoById(int id)
        {
            Objetivo u = null;
            try
            {
                var sql = from p in conn.Table<Objetivo>()
                          where p.ID == id
                          select p;
                u = await sql.FirstAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return u;
        }

        /// <summary>
        /// Método que obtiene todos los objetivos
        /// </summary>
        /// <remarks>
        /// Llamamos al método ToListAsync para hacer una llamada asíncrona,
        /// añadiendo el await. Muestra una excepción si no se han podido recibir
        /// los datos.
        /// </remarks>
        /// <returns>Retorna la lista de objetivos</returns>
        internal async Task<List<Objetivo>> GetAllObjetivosAsync()
        {
            //Variable con lista de cases
            List<Objetivo> objetivoList = new List<Objetivo>();

            try
            {
                objetivoList = await conn.Table<Objetivo>().ToListAsync();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            //Se devuelve la lista de personas
            return objetivoList;
        }

        /// <summary>
        /// Método que obtiene todos los usuarios
        /// </summary>
        /// <remarks>
        /// Llamamos al método ToListAsync para hacer una llamada asíncrona,
        /// añadiendo el await. Muestra una excepción si no se han podido recibir
        /// los datos.
        /// </remarks>
        /// <returns>Retorna la lista de usuarios</returns>
        internal async Task<List<Usuario>> GetAllUsersAsync()
        {
            //Variable con lista de cases
            List<Usuario> usersList = new List<Usuario>();

            try
            {
                usersList = await conn.Table<Usuario>().ToListAsync();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            //Se devuelve la lista de personas
            return usersList;
        }

        /// <summary>
        /// Método que obtiene todos los horarios
        /// </summary>
        /// <remarks>
        /// Llamamos al método ToListAsync para hacer una llamada asíncrona,
        /// añadiendo el await. Muestra una excepción si no se han podido recibir
        /// los datos.
        /// </remarks>
        /// <returns>Retorna la lista de horarios</returns>
        internal async Task<List<Horario>> GetAllHorariosAsync()
        {
            //Variable con lista de cases
            List<Horario> horariosList = new List<Horario>();

            try
            {
                horariosList = await conn.Table<Horario>().ToListAsync();

            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            //Se devuelve la lista de personas
            return horariosList;
        }
        //---------------- [ GET DATA ] -----------------
    }
}
