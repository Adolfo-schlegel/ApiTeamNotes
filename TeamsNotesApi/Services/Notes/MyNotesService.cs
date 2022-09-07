using Dapper;
using System.Data.SqlClient;
using TeamsNotesApi.Models.Notas;
using TeamsNotesApi.Services.Connections;
using TeamsNotesApi.Services.Interfaces;

namespace TeamsNotesApi.Services.Notes
{
    public class MyNotesService : IMyNotesService
    {
        private SqlConnection? conn;
        private string? query = "";
        public MyNotesService()
        {
            conn = CVM_Connection.Connect();
        }

        public object GetOpcionsUser()
        {         
            query = "SELECT ds_destinatario, id_destinatario FROM DESTINATARIOS WHERE 1 = 1 AND id_usuario = 5;" +
                   " SELECT ds_nota_grupo, id_nota_grupo FROM NOTA_GRUPOS WHERE 1 = 1; ";

            List<Destinatario> lstDest;
            List<Grupo> lstGrup;

            try
            {
                using (var result = conn.QueryMultiple(query))
                {
                    lstDest = result.Read<Destinatario>().ToList();
                    lstGrup = result.Read<Grupo>().ToList();

                    conn.Close();
                    return new { Destinatarios = lstDest, Grupos = lstGrup };
                }                           
            }
            catch(SqlException ex)
            {
                //---log----
                return null;
            }
        }

        //agregar los grupos de cada nota
        public object GetNotes(int id_user, int status, int page = 0)
        {
            query = "select r1.dt_alta,r1.ds_destinatario, r1.dh_alta, r1.ds_nota, r1.ds_detalle, r1.ds_lectura_estado, r1.id_Reg_destino, qt_anexo = (SELECT COUNT(*) FROM reg_anexos X WHERE x.id_reg_nota = r1.id_reg_nota) from vs_reg_destinos1 r1 where r1.cd_medio = 'M'  AND r1.id_usuario = '" + id_user+ "' [ESTADO] and dt_alta != '' ORDER BY r1.dt_alta DESC OFFSET " + page+" ROWS FETCH NEXT 10 ROWS ONLY ";                      
            
            if(status == 3)
                query = query.Replace("[ESTADO]", "and r1.id_lectura_estado = '" + status + "'");
            else if (status == 0)
                query = query.Replace("[ESTADO]", " ");
            
            
            List<RegistroNotas> lst;

            try
            {
                using (conn)
                {
                    lst = conn.Query<RegistroNotas>(query).ToList();                    
                    conn.Close();
                    return lst;
                }                
            }
            catch(Exception ex)
            {
                //---log----
                return null;
            }                
        }

        public object GetFilterNotes(ConsuNotas model, int id_user, int page = 0)
        {
            query = "select r1.dt_alta,r1.ds_destinatario, r1.dh_alta, r1.ds_nota, r1.ds_detalle, r1.ds_lectura_estado, r1.id_Reg_destino, qt_anexo = (SELECT COUNT(*) FROM reg_anexos X WHERE x.id_reg_nota = r1.id_reg_nota) from vs_reg_destinos1 r1 where r1.cd_medio = 'M'  AND r1.id_usuario = '" + id_user.ToString() + "' [ESTADO] and dt_alta != ''  ORDER BY r1.dt_alta DESC  OFFSET "+page+" ROWS FETCH NEXT 10 ROWS ONLY";

            if (model.estado == 3)
                query = query.Replace("[ESTADO]", "and r1.id_lectura_estado = '"+model.estado+"'");
            else if(model.estado == 0)
                query = query.Replace("[ESTADO]", " ");
                       
            List<RegistroNotas> lst;

            try
            {
                using(conn)
                {
                    lst = conn.Query<RegistroNotas>(query).ToList();

                    conn.Close();
                    return lst;
                }                
            }
            catch (Exception ex)
            {
                //---log----
                return null;
            }
        }

        public object GetInfoNote(int idRegDestino)
        {
            //select ds_detalle, ds_destinatario from vs_reg_destinos1;
            List<RegistroNotasAnexos>? lstAnexos = new();
            List<RegistroNotasDatos> lstDatos = new();
            List<RegistroNotasDetalles> lstDetalles = new();

            query = "SELECT rn5.ds_nota, rn5.ds_reg_nota_medio, rn5.dt_alta, rn5.dh_alta, rn5.id_reg_nota FROM vs_reg_notas_medio1 RN5 WHERE rn5.id_reg_destino = '"+idRegDestino.ToString()+"';";
            
            try
            {
               var notasMedio = conn.Query<RegistroNotasMedio>(query).FirstOrDefault();

               query = "SELECT id_reg_anexo, r1.ds_reg_nota_anexo FROM vs_reg_anexos1 R1 WHERE r1.id_reg_nota = '" + notasMedio.id_reg_nota.ToString() + "'" +
                       "SELECT ds_dato, ds_valor FROM vs_reg_datos1 R1 WHERE  r1.id_reg_nota = '" + notasMedio.id_reg_nota.ToString() + "'" +
                       "SELECT ds_nota_parte, ds_nota_detalle FROM vs_reg_detalle1 R1 WHERE  r1.id_reg_nota = '" + notasMedio.id_reg_nota.ToString() + "' AND r1.id_medio = 1 ORDER BY id_nota_parte";
                       
                using (var reader = conn.QueryMultiple(query))
                {
                    lstAnexos = reader.Read<RegistroNotasAnexos>().ToList();
                    lstDatos = reader.Read<RegistroNotasDatos>().ToList();
                    lstDetalles = reader.Read<RegistroNotasDetalles>().ToList();
                }
                
                var result = new
                {
                    Medio = notasMedio,
                    Anexos = lstAnexos,
                    Datos = lstDatos,
                    Detalles = lstDetalles,
                };

                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                //----log----
                return null;
            }   
        }
       
        public object GetFileAnexo(int id_reg_anexo)
        {
            query = "Select DS_PATH,DS_ARCHIVO,CD_archivo_tipo from vs_reg_anexos1 where id_reg_anexo = '"+id_reg_anexo+"'";
            //2744 nota id
            using (conn)
            {
                var result = conn.Query<Models.Files.File>(query).FirstOrDefault();
                conn.Close();
                return new { pathfile = (result.DS_PATH + result.DS_ARCHIVO), contentype = result.CD_archivo_tipo.ToLower() };
            }           
        }

        public int UpdateStatusNote(int IdRegDestino)
        {
            query = "UPDATE vs_reg_destinos1  SET id_lectura_estado = 3  WHERE id_reg_destino = @idRegDestino";
            int result = conn.Execute(query, new { IdRegDestino });
            conn.Close();
            return result;
        }
    }
}
