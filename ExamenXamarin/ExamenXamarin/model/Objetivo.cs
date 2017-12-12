using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ExamenXamarin.model
{
    /// <summary>
    /// Tabla objetivos
    /// </summary>
    [Table ("OBJETIVOS")]
    class Objetivo
    {
        /// <summary>
        /// Propiedad ID
        /// </summary>
        [PrimaryKey, NotNull, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Propiedad objetivo
        /// </summary>
        [Unique, NotNull, MaxLength(20)]
        public string OBJETIVO { get; set; }
    }
}
