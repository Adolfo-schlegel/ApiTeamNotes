//namespace TeamsNotesApi.CVM
//{
//    public class Cons
//    {
//        public partial struct MetodoAutenticacion
//        {
//            public const string CVM = "CVM";
//            public const string AD = "AD";
//        }

//        public enum idioma : int
//        {
//            Espanol = 1,
//            Portugues = 2,
//            Ingles = 3
//        }

//        public enum BotoneraEnum : int
//        {
//            Busqueda = 1,
//            Grilla = 2
//        }

//        public enum ModoLog : int
//        {
//            Normal = 1,
//            Detalle = 2,
//            Debug = 9
//        }

//        public partial struct SQL_cmd
//        {
//            public const string Insert_SQL = "INSERT ";
//            public const string Delete_SQL = "DELETE ";
//            public const string Insert_ACC = "INSERT INTO ";
//            public const string Delete_ACC = "DELETE * FROM ";
//            public const string Insert_ORA = "INSERT INTO ";
//            public const string Delete_ORA = "DELETE ";
//        }

//        public partial struct ModoEjecucion
//        {
//            public const string Desarrollo = "D";      // Desarrollo
//            public const string Test = "T";            // Test
//            public const string Produccion = "P";      // Producción
//        }

//        public partial struct SQL_Wilcard
//        {
//            public const string ACCESS_WILDCARD = "*";
//            public const string ORACLE_WILDCARD = "%";
//        }

//        public enum TP_Base
//        {
//            Access = 1,
//            SQL = 2,
//            DBF = 3,
//            Oracle = 4,
//            SinEspecificar = 0
//        }

//        public partial struct ABM
//        {
//            public const string Alta = "A";
//            public const string Baja = "B";
//            public const string Modifica = "M";
//            public const string Consulta = "C";
//            public const string Copia = "P";

//            public const string Anula = "N";
//            public const string Activa = "T";
//            public const string Ninguna = "Z";

//            // Const Nuevo = "V"               'RRR antes era "N" (mismo valor q ANULA)
//            public const string Nada = "Z";
//        }

//        public partial struct Requerido
//        {
//            public const string Si = "S";
//            public const string No = "N";
//            public const string Alerta = "A";
//        }

//        public enum SQL_Ope : int
//        {
//            Insert = 1,
//            Update = 2,
//            Delete = 3,
//            SelectF = 4,
//            SQL = 5 // es un SQL específico
//        }

//        public partial struct Mensajes
//        {
//            public const string Err_SinPermisos = "El usuario no tiene permiso para realizar esta operación";
//            public const string Err_FaltaEspecificarCriterios = "Error: Falta especificar criterios de búsqueda";
//        }

//        public partial struct Modulos
//        {
//            public const string Ninguno = "ZZZ";
//        }

//        public partial struct CE_Registro
//        {
//            public const string Nuevo = "N";
//            public const string Viejo = "V";
//        }

//        public enum ActivoEstado
//        {
//            Activo = 1,
//            Anulado = 0
//        }

//        public enum TxtFormatoArchivo : int
//        {
//            TXT_Delimitado = 1,
//            TXT_Fijo = 2
//        }

//        public enum TxtTipoCampo : int
//        {
//            Texto = 1,
//            Fecha = 2,
//            Numerico = 3,
//            Importe = 4
//        }

//        public partial struct TablaCampoTipo
//        {
//            public const string Fecha = "F";
//            public const string Numerico = "N";
//            public const string Texto = "T";
//        }

//        public enum Moneda
//        {
//            Peso = 1,
//            Dolar = 2,
//            Euro = 3,
//            Local = 1
//        }

//        public partial struct DestinoImpresion
//        {
//            public const string PDFSeparado = "P1";
//            public const string PDFAgrupado = "P2";
//            public const string Impresora = "I";
//            public const string VistaPreliminar = "V";
//        }

//        public partial struct ValidaTipo
//        {
//            public const string EsError = "E";
//            public const string EsAlerta = "A";
//        }

//    }
//}

