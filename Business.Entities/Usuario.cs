using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class Usuario : BusinessEntity
    {
        private String _NombreUsuario;
        private String _Clave;
        private String _Nombre;
        private String _Apellido;
        private String _EMail;
        private int? _IDPersona;
        private bool _Habilitado;

        public String NombreUsuario
        {  
            get { return _NombreUsuario; }
            set { _NombreUsuario = value; }
        }
        public String Clave 
        {
            get { return _Clave; }
            set { _Clave = value; }
        }
        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        public String Apellido
        {
            get { return _Apellido; }
            set { _Apellido = value; }
        }
        public String EMail
        {
            get { return _EMail; }
            set { _EMail = value; }
        }
        public int? IDPersona
        {
            get { return _IDPersona; }
            set { _IDPersona = value; }
        }
        public bool Habilitado
        {
            get { return _Habilitado; }
            set { _Habilitado = value; }
        }

    }
}
