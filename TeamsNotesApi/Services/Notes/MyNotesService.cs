using System.Data.SqlClient;
using TeamsNotesApi.Models.Notas;
using TeamsNotesApi.Services.Connections;
using TeamsNotesApi.Services.Interfaces;

namespace TeamsNotesApi.Services.Notes
{
    public class MyNotesService : IMyNotesService
    {
        private SqlConnection? conn;
        private SqlCommand? cmd;
        private SqlDataReader? reader;
        private string? query = "";
        public MyNotesService()
        {
            conn = CVM_Connection.Connect();
        }

        public object GetOpcionsUser()
        {         
            query = "SELECT (ds_destinatario) AS DS, (id_destinatario) AS ID FROM DESTINATARIOS WHERE 1 = 1 AND id_usuario = 5;" +
                   " SELECT (ds_nota_grupo) AS DS, (id_nota_grupo) AS ID FROM NOTA_GRUPOS WHERE 1 = 1; ";

            Grupo grup;
            Destinatario dest;

            List<object> lstDest;
            List<object> lstGrup;

            cmd = new SqlCommand(query, conn);

            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    lstDest = new();
                    lstGrup = new();

                    if (reader.HasRows)
                    {                        
                        while (reader.Read())
                        {
                            dest = new Destinatario();

                            dest.destinatario = reader.GetString(0);
                            dest.id = (int)reader.GetDecimal(1);

                            lstDest.Add(dest);
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                grup = new Grupo();

                                grup.grupo = reader.GetString(0);
                                grup.id = (int)reader.GetDecimal(1);

                                lstGrup.Add(grup);
                            }
                        }
                    }

                }            
                
                conn.Close();
                return new { Destinatarios = lstDest, Grupos = lstGrup };                            
            }
            catch(SqlException ex)
            {
                //---log----
                return null;
            }
        }

        public object GetNotes(int id_user)
        {
           // query = "select r1.dt_alta, r1.dh_alta, r1.ds_nota, r1.ds_detalle, r1.ds_lectura_estado, r1.id_Reg_destino, qt_anexo = (SELECT COUNT(*) FROM reg_anexos X WHERE x.id_reg_nota = r1.id_reg_nota) FROM vs_reg_destinos1 r1 WHERE  1 = 1 and id_usuario = 5 and cd_medio = 'M' and dt_alta != ''  ORDER BY r1.dt_alta DESC";
            query = "select r1.dt_alta, r1.dh_alta, r1.ds_nota, r1.ds_detalle, r1.ds_lectura_estado, r1.id_Reg_destino, qt_anexo = (SELECT COUNT(*) FROM reg_anexos X WHERE x.id_reg_nota = r1.id_reg_nota) from vs_reg_destinos1 r1 where r1.cd_medio = 'M'  AND r1.id_usuario = '"+id_user+"' and dt_alta != ''  ORDER BY r1.dt_alta DESC ";            

            RegistroNotas? registro;
            List<RegistroNotas> lst;
            cmd = new SqlCommand(query, conn);

            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    lst = new();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            registro = new();

                            registro.dt_alta = reader.GetDateTime(0);
                            registro.dh_alta = reader.GetString(1);
                            registro.ds_nota = reader.GetString(2);
                            registro.ds_detalle = reader.GetString(3);
                            registro.ds_lectura_estado = reader.GetString(4);
                            registro.id_Reg_Destino = (int)reader.GetDecimal(5);
                            registro.ct_anexos = (int)reader.GetInt32(6);

                            lst.Add(registro);
                        }
                    }
                }

                return lst;
            }
            catch(Exception ex)
            {
                //---log----
                return null;
            }                
        }

        public object GetFilterNotes(ConsuNotas model, int id_user)
        {
            query = "select r1.dt_alta, r1.dh_alta, r1.ds_nota, r1.ds_detalle, r1.ds_lectura_estado, r1.id_Reg_destino, qt_anexo = (SELECT COUNT(*) FROM reg_anexos X WHERE x.id_reg_nota = r1.id_reg_nota) from vs_reg_destinos1 r1 where r1.cd_medio = 'M'  AND r1.id_usuario = '" + id_user.ToString() + "'and r1.id_lectura_estado = '"+model.estado+ "' and dt_alta != ''  ORDER BY r1.dt_alta DESC";

            RegistroNotas? registro;
            List<RegistroNotas> lst;
            cmd = new SqlCommand(query, conn);

            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    lst = new();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            registro = new();

                            registro.dt_alta = reader.GetDateTime(0);
                            registro.dh_alta = reader.GetString(1);
                            registro.ds_nota = reader.GetString(2);
                            registro.ds_detalle = reader.GetString(3);
                            registro.ds_lectura_estado = reader.GetString(4);
                            registro.id_Reg_Destino = (int)reader.GetDecimal(5);
                            registro.ct_anexos = (int)reader.GetInt32(6);

                            lst.Add(registro);
                        }
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                //---log----
                return null;
            }
        }

        public object GetInfoNote(int idRegDestino)
        {
            RegistroNotasMedio? notasMedio = new();
            RegistroNotasAnexos? notasAnexos;
            RegistroNotasDatos? notasDatos;
            RegistroNotasDetalles? notasDetalles;

            List<RegistroNotasAnexos>? lstAnexos = new();
            List<RegistroNotasDatos> lstDatos = new();
            List<RegistroNotasDetalles> lstDetalles = new();

            query = "SELECT rn5.ds_nota, rn5.ds_reg_nota_medio, rn5.dt_alta, rn5.dh_alta, rn5.id_reg_nota FROM vs_reg_notas_medio1 RN5 WHERE rn5.id_reg_destino = '"+idRegDestino.ToString()+"';";
            cmd = new SqlCommand(query, conn);

            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            notasMedio.ds_nota = reader.GetString(0);
                            notasMedio.ds_reg_nota_medio = reader.GetString(1);
                            notasMedio.dt_alta = reader.GetDateTime(2);
                            notasMedio.dh_alta = reader.GetString(3);
                            notasMedio.id_reg_nota = (int)reader.GetDecimal(4);
                        }
                    }
                }

                query = "SELECT id_reg_anexo, r1.ds_reg_nota_anexo FROM vs_reg_anexos1 R1 WHERE  r1.id_reg_nota = '" + notasMedio.id_reg_nota.ToString() + "'" +
                        "SELECT ds_dato, ds_valor FROM vs_reg_datos1 R1 WHERE  r1.id_reg_nota = '" + notasMedio.id_reg_nota.ToString() + "'" +
                        "SELECT ds_nota_parte, ds_nota_detalle FROM vs_reg_detalle1 R1 WHERE  r1.id_reg_nota = '" + notasMedio.id_reg_nota.ToString() + "' AND r1.id_medio = 1 ORDER BY id_nota_parte";

                cmd = new SqlCommand(query, conn);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            notasAnexos = new();

                            notasAnexos.id_reg_anexo = (int)reader.GetDecimal(0);
                            notasAnexos.ds_reg_nota_anexo = reader.GetString(1);

                            lstAnexos.Add(notasAnexos);
                        }
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            notasDatos = new();
                            notasDatos.ds_dato = reader.GetString(0);
                            notasDatos.ds_valor = reader.GetString(1);

                            lstDatos.Add(notasDatos);
                        }
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            notasDetalles = new();
                            notasDetalles.ds_nota_parte = reader.GetString(0);
                            notasDetalles.ds_nota_detalle = reader.GetString(1);
                            lstDetalles.Add(notasDetalles);
                        }
                    }
                }

                var result = new
                {
                    Medio = notasMedio,
                    Anexos = lstAnexos,
                    Datos = lstDatos,
                    Detalles = lstDetalles,
                };

                return result;
            }
            catch(Exception ex)
            {
                //---log---
                return null;
            }            
        }
       
        public object GetFileAnexo(int id_reg_anexo)
        {
            string pathFile = "";
            string nameFile = "";
            string type = "";

            query = "Select rn2.DS_PATH,DS_ARCHIVO,CD_archivo_tipo from vs_reg_anexos1 rn2 where rn2.id_reg_anexo = '"+id_reg_anexo+"'";
            cmd = new SqlCommand(query, conn);

            using (reader = cmd.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        pathFile = reader.GetString(0);
                        nameFile = reader.GetString(1);
                        type = reader.GetString(2);
                    }
                }
            }

            return new { pathfile = (pathFile + nameFile), contentype = type.ToLower() };
        }

        public int UpdateStatusNote(int IdRegDestino)
        {
            query = "UPDATE  vs_reg_destinos1  SET id_lectura_estado = 3  WHERE id_reg_destino = '" + IdRegDestino+"'";
            cmd = new SqlCommand(query, conn);

            if(cmd.ExecuteNonQuery() != 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
