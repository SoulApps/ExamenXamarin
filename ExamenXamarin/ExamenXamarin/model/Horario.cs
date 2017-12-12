using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ExamenXamarin.model
{
    /// <summary>
    /// Clase Horario
    /// </summary>
    [Table("HORARIOS")]
    class Horario
    {
        /// <summary>
        /// Propiedad ID
        /// </summary>
        [PrimaryKey, NotNull, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Propiedad horario
        /// </summary>
        [Unique, NotNull, MaxLength(15)]
        public string HORARIO { get; set; }
    }
}
