using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ExamenXamarin.model
{
    /// <summary>
    /// Clase Usuario
    /// </summary>
    [Table("USUARIOS")]
    class Usuario
    {
        /// <summary>
        /// Propiedad DNI
        /// </summary>
        [PrimaryKey, MaxLength(9)]
        public string DNI { get; set; }

        /// <summary>
        /// Propiedad nombre
        /// </summary>
        [NotNull, MaxLength(20)]
        public string NOMBRE { get; set; }

        /// <summary>
        /// Propiedad password
        /// </summary>
        [NotNull, MaxLength(9)]
        public string PASSWORD { get; set; }

        /// <summary>
        /// Propiedad HORARIO
        /// </summary>
        public int HORARIO{ get; set; }


        /// <summary>
        /// Propiedad edad
        /// </summary>
        public int EDAD { get; set; }

        /// <summary>
        /// Propiedad altura 
        /// </summary>
        public int ALTURA { get; set; }

        /// <summary>
        /// Propiedad PESO
        /// </summary>
        public double PESO { get; set; }

        /// <summary>
        /// Propiedad IMC
        /// </summary>
        public double IMC { get; set; }

        /// <summary>
        /// Propiedad OBJETIVO
        /// </summary>
        public int OBJETIVO { get; set; }

        /// <summary>
        /// Propiedad TIPO
        /// </summary>
        [MaxLength(7)]
        public string TIPO { get; set; }
    }
}
