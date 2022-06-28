//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Diagnostics;
//using System.Linq;
//using System.Runtime.InteropServices;
//using Microsoft.VisualBasic; // Install-Package Microsoft.VisualBasic
//using Microsoft.VisualBasic.CompilerServices; // Install-Package Microsoft.VisualBasic

//namespace TeamsNotesApi.CVM
//{
//    public partial class Sql
//    {

//        // -- ------------------------------------------------------------------------------------------
//        #region Propiedades
//        // -- ------------------------------------------------------------------------------------------

//        // PPP Public Resultado As cls_CVM.Resultado
//        // Private mdba_base As SqlConnection

//        private int mint_id_tipo_base;
//        private string mstr_cd_tabla;
//        private CVM.Cons.SQL_Ope mint_operacion;
//        private string mstr_sql;
//        private bool mbln_lg_distinct = false;
//        private int mint_top = 0;
//        private bool mbln_OrderIgualGroup;
//        private bool mbln_GroupIgualSelect;
//        private string mstr_LlamadoDesde;

//        private Collection mclc_campos;
//        private Collection mclc_select;
//        private Collection mclc_where;
//        private Collection mclc_from;
//        private Collection mclc_groupby;
//        private Collection mclc_having;
//        private Collection mclc_orderby;
//        private Collection mclc_replace;

//        private Collection mclc_tmp_campos;
//        private Collection mclc_sp_param;

//        private string mstr_into;

//        private List<string> mlst_sql = new List<string>();

//        #endregion

//        // -- ------------------------------------------------------------------------------------------
//        #region New y Finalize
//        // -- ------------------------------------------------------------------------------------------

//        public Sql(string pstr_tabla = "", int pint_tipo_base = 0, int pint_operacion = 0, string pstr_llamado_desde = "")
//        {

//            // PPP Resultado = New cls_CVM.Resultado

//            // PPP migrar WEB
//            // Me.TipoBase = CVM.RT.TipoBase

//            mstr_sql = "";
//            if (!string.IsNullOrEmpty(pstr_tabla))
//                Tabla = pstr_tabla;
//            if (pint_tipo_base != 0)
//                TipoBase = pint_tipo_base;
//            if (pint_operacion != 0)
//                Operacion = pint_operacion;
//            if (!string.IsNullOrEmpty(pstr_llamado_desde))
//                LlamadoDesde = pstr_llamado_desde;

//            ps_iniciar_clc();

//        }

//        public void clc_select_iniciar()
//        {
//            mclc_select = new Collection();
//        }

//        #endregion

//        // -- ------------------------------------------------------------------------------------------
//        #region Métodos Públicos
//        // -- ------------------------------------------------------------------------------------------

//        public Collection clc_select()
//        {
//            return mclc_select;
//        }

//        public void CopiarSELECT(Sql psql, bool pbln_iniciar)
//        {

//            if (pbln_iniciar)
//                mclc_select = new Collection();

//            foreach (CVM.Valor lobj_valor in psql.clc_select)
//                mclc_select.Add(lobj_valor);

//        }

//        public Collection clc_where()
//        {
//            return mclc_where;
//        }

//        public void CopiarWHERE(CVM.Sql psql, bool pbln_iniciar)
//        {

//            if (pbln_iniciar)
//                mclc_where = new Collection();

//            foreach (CVM.Valor lobj_valor in psql.clc_where)
//                mclc_where.Add(lobj_valor);

//        }

//        public Collection clc_FROM()
//        {
//            return mclc_from;
//        }

//        public void CopiarFROM(CVM.Sql psql, bool pbln_iniciar)
//        {

//            if (pbln_iniciar)
//                mclc_from = new Collection();

//            foreach (CVM.Valor lobj_valor in psql.clc_FROM)
//                mclc_from.Add(lobj_valor);

//        }

//        public Collection clc_GroupBy()
//        {
//            return mclc_groupby;
//        }
//        public void CopiarGroupBy(CVM.Sql psql, bool pbln_iniciar)
//        {

//            if (pbln_iniciar)
//                mclc_groupby = new Collection();

//            foreach (CVM.Valor lobj_valor in psql.clc_GroupBy)
//                mclc_groupby.Add(lobj_valor);

//        }

//        public void q_SELECT_COUNT(string pstr_alias, bool pbln_condicion = true)
//        {

//            q_SELECT("COUNT(*)", pstr_alias, pbln_condicion);

//        }

//        public void q_SELECT_SUM(string pstr_campo, string pstr_alias, bool pbln_condicion = true)
//        {

//            pstr_campo = "SUM(" + pstr_campo + ")";

//            q_SELECT(pstr_campo, pstr_alias, pbln_condicion);

//        }

//        public void q_SELECT_AbsSUM(string pstr_campo, string pstr_alias, bool pbln_condicion = true)
//        {

//            pstr_campo = "ABS(SUM(" + pstr_campo + "))";

//            q_SELECT(pstr_campo, pstr_alias, pbln_condicion);

//        }

//        public void q_SELECT_MAX(string pstr_campo, string pstr_alias, bool pbln_condicion = true)
//        {

//            pstr_campo = "MAX(" + pstr_campo + ")";

//            q_SELECT(pstr_campo, pstr_alias, pbln_condicion);

//        }
//        public void q_SELECT_MIN(string pstr_campo, string pstr_alias, bool pbln_condicion = true)
//        {

//            pstr_campo = "MIN(" + pstr_campo + ")";

//            q_SELECT(pstr_campo, pstr_alias, pbln_condicion);

//        }

//        public void q_SELECT(string pstr_campo = "*", string pstr_alias = "", bool pbln_condicion = true)
//        {

//            if (!pbln_condicion)
//                return;


//            var lobj_valor = new CVM.Valor();

//            if (string.IsNullOrEmpty(pstr_campo))
//                return;

//            lobj_valor.ds_valor1 = pstr_campo;
//            lobj_valor.ds_valor2 = pstr_alias;

//            mclc_select.Add(lobj_valor);

//            lobj_valor = default;

//        }

//        public void q_INTO(string pstr_INTO)
//        {

//            mstr_into = pstr_INTO;

//        }

//        public void q_FROM(string pstr_tabla, string pstr_alias = "")
//        {

//            ps_agregar_FROM(pstr_tabla, pstr_alias, "FROM");

//        }

//        public void q_INNERjoin(string pstr_tabla, string pstr_alias, string pstr_relacion_on, bool pbln_condicion = true)


//        {

//            if (pbln_condicion)
//            {
//                ps_agregar_FROM(pstr_tabla, pstr_alias, "INNER JOIN", pstr_relacion_on);
//            }

//        }

//        public void q_LEFTjoin(string pstr_tabla, string pstr_alias, string pstr_relacion_on, bool pbln_condicion = true)


//        {

//            if (pbln_condicion)
//            {
//                ps_agregar_FROM(pstr_tabla, pstr_alias, "LEFT JOIN", pstr_relacion_on);
//            }

//        }

//        public void q_SET_cod_detalle(string pstr_campo, long plng_ID, string pstr_CD, string pstr_tabla, bool pbln_condicion = true)
//        {

//            string lstr_sql = "";

//            if (!pbln_condicion)
//                return;

//            if (plng_ID == 0L)
//            {
//                lstr_sql = "dbo.fn_id_cod_detalle('[tabla]', '[codigo]')";

//                if (TipoBase == CVM.Cons.TP_Base.Oracle)
//                {
//                    lstr_sql = lstr_sql.Replace("dbo.fn_id_cod_detalle", "fn_id_cod_detalle2");
//                }
//                lstr_sql = lstr_sql.Replace("[tabla]", pstr_tabla);
//                lstr_sql = lstr_sql.Replace("[codigo]", pstr_CD);

//                q_SET(pstr_campo, lstr_sql, "V");
//            }
//            else
//            {
//                q_SET(pstr_campo, plng_ID, "N");
//            }

//        }

//        public void q_SET(string pstr_campo, object pvar_valor, string pstr_tipo_dato = "T", string pstr_formato = "", bool pbln_NullSiEsVacio = false, bool pbln_condicion = true)
//        {

//            if (!pbln_condicion)
//                return;

//            mclc_campos.Add(mfobj_SetCampo(pstr_campo, pvar_valor, pstr_tipo_dato, pstr_formato, "", pbln_NullSiEsVacio));

//        }

//        public void q_SET_T(string pstr_campo, string pstr_valor, int pint_largo = 0, bool pbln_condicion = true)
//        {

//            if (!pbln_condicion)
//                return;

//            if (pint_largo > 0)
//                pstr_valor = Strings.Left(pstr_valor, pint_largo);

//            mclc_campos.Add(mfobj_SetCampo(pstr_campo, pstr_valor, "T", "", ""));

//        }

//        public void q_SET_ID(string pstr_campo, long plng_ID, bool pbln_NullSiEsCero = true, bool pbln_condicion = true)
//        {

//            q_SET(pstr_campo, plng_ID, "N", "", pbln_NullSiEsCero, pbln_condicion);

//        }

//        public void q_SET_Activo(bool pbln_activo, bool pbln_condicion = true)
//        {

//            q_SET("ID_ACTIVO_ESTADO", Interaction.IIf(pbln_activo, 1, 0), "N", pbln_condicion: pbln_condicion);

//        }

//        public void q_SET_Anulado(bool pbln_anulado, bool pbln_condicion = true)
//        {

//            q_SET("LG_ANULADO", Interaction.IIf(pbln_anulado, 1, 0), "N", pbln_condicion: pbln_condicion);

//        }

//        public void q_SET_LGN(string pstr_campo, bool pbln, bool pbln_condicion = true)
//        {

//            q_SET(pstr_campo, Interaction.IIf(pbln, 1, 0), "N", pbln_condicion: pbln_condicion);

//        }

//        public void q_SET_LogConRT(string pstr_tp_ope)
//        {

//            // PPP migrar WEB
//            // If pstr_tp_ope = CVM.Cons.ABM.Alta Then
//            // Me.q_SET_ID("ID_USUARIO_ALTA", CVM.RT.Usuario.ID)
//            // Me.q_SET("DT_ALTA", CVM.RT.Hoy, "F")
//            // Me.q_SET("DH_ALTA", CVM.RT.Hora, "T")
//            // End If

//            // Me.q_SET_ID("ID_USUARIO_MODIFICA", CVM.RT.Usuario.ID)
//            // Me.q_SET("DT_MODIFICA", CVM.RT.Hoy, "F")
//            // Me.q_SET("DH_MODIFICA", CVM.RT.Hora, "T")

//        }

//        public void q_SET_TablaLog(CVM.TablaLog pTablaLog, string pstr_tp_ope)
//        {

//            if (pstr_tp_ope == CVM.Cons.ABM.Alta)
//            {
//                this.q_SET("ID_USUARIO_ALTA", pTablaLog.id_usuario_alta, "N");
//                this.q_SET("DT_ALTA", pTablaLog.dt_alta, "F");
//                this.q_SET("DH_ALTA", pTablaLog.dh_alta, "T");
//            }

//            this.q_SET("ID_USUARIO_MODIFICA", pTablaLog.id_usuario_modifica, "N");
//            this.q_SET("DT_MODIFICA", pTablaLog.dt_modifica, "F");
//            this.q_SET("DH_MODIFICA", pTablaLog.dh_modifica, "T");

//        }

//        public void w_WHERE(string pstr_sql, bool pbln_condicion = true)
//        {

//            w_AND(pstr_sql, pbln_condicion);

//        }

//        public void w_AND_ConDic(Dictionary<string, object> pDic, string pstr_Alias)
//        {

//            // si viene alias, le agrego el . 
//            if (!string.IsNullOrEmpty(pstr_Alias) & !pstr_Alias.Contains("."))
//                pstr_Alias += ".";

//            foreach (KeyValuePair<string, object> lobj in pDic)
//                ps_agregar_AND(mclc_where, Conversions.ToString(lobj.Value.replace("[alias].", pstr_Alias)));

//        }

//        public void w_AND(string pstr_sql, bool pbln_condicion = true)
//        {

//            ps_agregar_AND(mclc_where, pstr_sql, pbln_condicion);

//        }

//        public void w_OR(string pstr_sql, bool pbln_condicion = true)
//        {

//            ps_agregar_OR(mclc_where, pstr_sql, pbln_condicion);

//        }
//        public void w_ANDn(string pstr_operador, string pstr_sql1, string pstr_sql2, string pstr_sql3 = "", string pstr_sql4 = "", string pstr_sql5 = "", string pstr_sql6 = "", string pstr_sql7 = "", string pstr_sql8 = "", string pstr_sql9 = "", string pstr_sql10 = "")









//        {

//            ps_agregar_ANDn(mclc_where, pstr_operador, pstr_sql1, pstr_sql2, pstr_sql3, pstr_sql4, pstr_sql5, pstr_sql6, pstr_sql7, pstr_sql8, pstr_sql9, pstr_sql10);


//        }

//        public string SqlCondicionF(string pstr_campo, string pstr_valor, string pstr_comparativo = "=", bool pbln_QuitarHorasEnFecha = false, bool pbln_exc_vacio = true)
//        {

//            if (pbln_exc_vacio & !Information.IsDate(pstr_valor))
//                return "";

//            return SqlCondicion(pstr_campo, pstr_valor, "F", pstr_comparativo, pbln_QuitarHorasEnFecha);

//        }

//        public string SqlCondicionT(string pstr_campo, string pstr_valor, string pstr_comparativo = "=", bool pbln_exc_vacio = true)
//        {

//            if (pbln_exc_vacio & string.IsNullOrEmpty(pstr_valor))
//                return "";

//            return SqlCondicion(pstr_campo, pstr_valor, "T", pstr_comparativo, pbln_ExcluirTextoVacio: pbln_exc_vacio);

//        }

//        public string SqlCondicion_ID(string pstr_campo, double pdbl_valor, string pstr_comparativo = "=")
//        {

//            if (pdbl_valor == -1 | pdbl_valor == 0d)
//            {
//                return "";
//            }

//            return SqlCondicion(pstr_campo, pdbl_valor, "N", pstr_comparativo);

//        }

//        public string SqlCondicion_CD(string pstr_campo, string pstr_valor, string pstr_comparativo = "=")
//        {

//            if (string.IsNullOrEmpty(pstr_valor))
//            {
//                return "";
//            }

//            return SqlCondicion(pstr_campo, pstr_valor, "T", pstr_comparativo);

//        }

//        public string SqlCondicionN(string pstr_campo, double pdbl_valor, string pstr_comparativo = "=", bool pbln_NoAplicaSiEs_menos1 = false, bool pbln_NoAplicaSiEs_cero = false)
//        {

//            if (pbln_NoAplicaSiEs_menos1 & pdbl_valor == -1 | pbln_NoAplicaSiEs_cero & pdbl_valor == 0d)
//            {
//                return "";
//            }

//            return SqlCondicion(pstr_campo, pdbl_valor, "N", pstr_comparativo);

//        }

//        public string SqlCondicion_LG_SiNo(string pstr_campo, string pstr_valor)
//        {

//            if (string.IsNullOrEmpty(pstr_valor))
//                return "";

//            return SqlCondicion(pstr_campo, "0", "N", Conversions.ToString(Interaction.IIf(pstr_valor == "S", "<>", "=")));

//        }

//        public string SqlCondicion_LG_True(string pstr_campo)
//        {

//            return pstr_campo + "<>0";

//        }

//        public string SqlCondicion_LG_False(string pstr_campo)
//        {

//            return pstr_campo + "=0";

//        }

//        public string SqlCondicion_Activo(string pstr_alias = "")
//        {

//            if (!string.IsNullOrEmpty(pstr_alias))
//                pstr_alias += ".";

//            return this.SqlCondicion(pstr_alias + "id_activo_estado", CVM.Cons.ActivoEstado.Activo, "N");

//        }

//        public string SqlCondicion_ActivoNo(string pstr_alias = "")
//        {

//            if (!string.IsNullOrEmpty(pstr_alias))
//                pstr_alias += ".";

//            return this.SqlCondicion(pstr_alias + "id_activo_estado", CVM.Cons.ActivoEstado.Activo, "N", "<>");

//        }

//        public string SqlCondicion_Anulado(string pstr_alias = "")
//        {

//            if (!string.IsNullOrEmpty(pstr_alias))
//                pstr_alias += ".";

//            return SqlCondicion(pstr_alias + "lg_anulado", 0, "N", "<>");

//        }

//        public string SqlCondicion_AnuladoNo(string pstr_alias = "")
//        {

//            if (!string.IsNullOrEmpty(pstr_alias))
//                pstr_alias += ".";

//            return SqlCondicion(pstr_alias + "lg_anulado", 0, "N", "=");

//        }

//        public string SqlCondicion(string pstr_campo, object pvar_valor, string pstr_tipo_dato = "T", string pstr_comparativo = "=", bool pbln_QuitarHorasEnFecha = false, bool pbln_ExcluirTextoVacio = true)
//        {

//            if (pstr_tipo_dato == "T" && string.IsNullOrEmpty(pvar_valor.ToString().Trim()) && pbln_ExcluirTextoVacio)
//                return "";

//            return SetCondicion(pstr_campo, pvar_valor, pstr_tipo_dato, pstr_comparativo, pbln_QuitarHorasEnFecha).Condicion;

//        }

//        public string SqlCondicionRangoN(string pstr_campo, double pdbl_desde, double pdbl_hasta, string pstr_OperadorDesde = ">=", string pstr_OperadorHasta = "<=", bool pbln_OmitirDesdeSiEsCero = true, bool pbln_OmitirHastaSiEsCero = true, bool pbln_OmitirMenos1 = true)
//        {

//            string lstr_desde = "";
//            string lstr_hasta = "";

//            if (!(pdbl_desde == 0d & pbln_OmitirDesdeSiEsCero | pdbl_desde == -1 & pbln_OmitirMenos1))
//            {
//                lstr_desde = SqlCondicionN(pstr_campo, pdbl_desde, pstr_OperadorDesde);
//            }

//            if (!(pdbl_hasta == 0d & pbln_OmitirHastaSiEsCero | pdbl_hasta == -1 & pbln_OmitirMenos1))
//            {
//                lstr_hasta = SqlCondicionN(pstr_campo, pdbl_hasta, pstr_OperadorHasta);
//            }

//            return Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(lstr_desde) | string.IsNullOrEmpty(lstr_hasta), lstr_desde + lstr_hasta, lstr_desde + " AND " + lstr_hasta));

//        }

//        public string SqlCondicionRangoT(string pstr_campo, string pstr_desde, string pstr_hasta, string pstr_OperadorDesde = ">=", string pstr_OperadorHasta = "<=", bool pbln_OmitirDesdeSiEsVacio = true, bool pbln_OmitirHastaSiEsVacio = true, int pint_ceros_izq = 0)
//        {

//            string lstr_desde = "";
//            string lstr_hasta = "";

//            if (pint_ceros_izq > 0 && Information.IsNumeric(pstr_desde))
//                pstr_desde = Strings.Format(Conversions.ToLong(pstr_desde), Strings.StrDup(pint_ceros_izq, "0"));
//            if (pint_ceros_izq > 0 && Information.IsNumeric(pstr_hasta))
//                pstr_hasta = Strings.Format(Conversions.ToLong(pstr_hasta), Strings.StrDup(pint_ceros_izq, "0"));

//            if (!(string.IsNullOrEmpty(pstr_desde) & pbln_OmitirDesdeSiEsVacio))
//            {
//                lstr_desde = SqlCondicionT(pstr_campo, pstr_desde, pstr_OperadorDesde, false);
//            }

//            if (!(string.IsNullOrEmpty(pstr_hasta) & pbln_OmitirHastaSiEsVacio))
//            {
//                lstr_hasta = SqlCondicionT(pstr_campo, pstr_hasta, pstr_OperadorHasta, false);
//            }

//            return Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(lstr_desde) | string.IsNullOrEmpty(lstr_hasta), lstr_desde + lstr_hasta, lstr_desde + " AND " + lstr_hasta));

//        }

//        public string sql_sel_rango(string pstr_campo, object pvar_valor1, object pvar_valor2, string pstr_tipo_dato = "T", int pint_ceros_izq = 0)
//        {

//            string lstr_desde = "";
//            string lstr_hasta = "";

//            if (pint_ceros_izq > 0 && Information.IsNumeric(pvar_valor1))
//            {
//                pvar_valor1 = Strings.Format(Conversions.ToLong(pvar_valor1), Strings.StrDup(pint_ceros_izq, "0"));
//                pvar_valor2 = Strings.Format(Conversions.ToLong(pvar_valor2), Strings.StrDup(pint_ceros_izq, "0"));
//            }

//            // Desde
//            if (pstr_tipo_dato == "F")
//            {
//                lstr_desde = SqlCondicionF(pstr_campo, Conversions.ToString(pvar_valor1), ">=");
//            }
//            else
//            {
//                lstr_desde = SqlCondicion(pstr_campo, pvar_valor1, pstr_tipo_dato, ">=");
//            }

//            // Hasta
//            if (pstr_tipo_dato == "F")
//            {
//                lstr_hasta = SqlCondicionF(pstr_campo, Conversions.ToString(pvar_valor2), "<=");
//            }
//            else
//            {
//                lstr_hasta = SqlCondicion(pstr_campo, pvar_valor2, pstr_tipo_dato, "<=");
//            }

//            return Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(lstr_desde) | string.IsNullOrEmpty(lstr_hasta), lstr_desde + lstr_hasta, lstr_desde + " AND " + lstr_hasta));

//            // sql_sel_rango = Me.SqlCondicion(pstr_campo, pvar_valor1, pstr_tipo_dato, ">=") & " AND " _
//            // & Me.SqlCondicion(pstr_campo, pvar_valor2, pstr_tipo_dato, "<=")

//        }

//        public string sql_sel_lista(string pstr_campo, string pstr_seleccion, string pstr_tipo_dato = "T", int pint_pos = 201, int pint_long = 0)



//        {
//            string sql_sel_listaRet = default;

//            string lstr_sql;

//            if (Strings.Mid(pstr_seleccion, pint_pos, 4) == "TODO")
//            {
//                lstr_sql = "";
//            }

//            else if (Strings.Left(pstr_seleccion, 1) == "(")
//            {
//                lstr_sql = SqlCondicion(pstr_campo, pstr_seleccion, "V", "IN");
//            }

//            else if (pint_long == 0)
//            {
//                lstr_sql = SqlCondicion(pstr_campo, Strings.Mid(pstr_seleccion, pint_pos), pstr_tipo_dato);
//            }
//            else
//            {
//                lstr_sql = SqlCondicion(pstr_campo, Strings.Mid(pstr_seleccion, pint_pos, pint_long), pstr_tipo_dato);

//            }

//            sql_sel_listaRet = lstr_sql;
//            return sql_sel_listaRet;

//        }

//        public string sql_sel_IN(string pstr_campo, string pstr_lista, string pstr_tipo_dato = "T", bool pbln_IN = true, bool pbln_quitar_espacios = true)
//        {
//            string sql_sel_INRet = default;

//            // Si esta vacio
//            if (string.IsNullOrEmpty(pstr_lista))
//                return "";

//            // Si tiene un solo elemento, lo transformo en condicion común
//            if (!pstr_lista.Contains(","))
//            {
//                return SqlCondicion(pstr_campo, pstr_lista, pstr_tipo_dato, Conversions.ToString(Interaction.IIf(pbln_IN, "=", "<>")));
//            }

//            // si tiene una coma al comienzo, se la quito
//            if (Strings.Left(pstr_lista, 1) == ",")
//                pstr_lista = Strings.Mid(pstr_lista, 2);

//            if (pstr_tipo_dato == "T")
//            {

//                // quito comillas, parentesis y espacios
//                pstr_lista = Strings.Replace(pstr_lista, "'", "");
//                if (pbln_quitar_espacios)
//                    pstr_lista = Strings.Replace(pstr_lista, " ", "");

//                if (Strings.Left(pstr_lista, 1) == "(")
//                    pstr_lista = Strings.Mid(pstr_lista, 2);
//                if (Strings.Right(pstr_lista, 1) == ")")
//                    pstr_lista = Strings.Left(pstr_lista, Strings.Len(pstr_lista) - 1);

//                // le agrego parentesis y comillas de inicio y fin
//                pstr_lista = Strings.Replace(pstr_lista, ",", "','");
//                pstr_lista = "('" + pstr_lista + "')";
//            }

//            else if (pstr_tipo_dato == "N")
//            {

//                // quito espacios
//                pstr_lista = Strings.Replace(pstr_lista, " ", "");

//                if (Strings.Left(pstr_lista, 1) != "(")
//                    pstr_lista = "(" + pstr_lista + ")";

//            }

//            sql_sel_INRet = SqlCondicion(pstr_campo, pstr_lista, "V", Conversions.ToString(Interaction.IIf(pbln_IN, "IN", "NOT IN")));
//            return sql_sel_INRet;

//        }

//        public string sql_sel_NOT_IN(string pstr_campo, string pstr_lista, string pstr_tipo_dato = "T", bool pbln_quitar_espacios = true)


//        {
//            string sql_sel_NOT_INRet = default;

//            sql_sel_NOT_INRet = sql_sel_IN(pstr_campo, pstr_lista, pstr_tipo_dato, false, pbln_quitar_espacios);



//            return sql_sel_NOT_INRet;

//        }

//        public string sql_Exists(string pstr_tabla, string pstr_alias, string pstr_relacion, string pstr_where = "1=1")
//        {

//            string lstr_sql = "";

//            lstr_sql = "EXISTS(SELECT * from [tabla] [alias] WHERE [relacion] AND [where])";

//            lstr_sql = lstr_sql.Replace("[tabla]", pstr_tabla);
//            lstr_sql = lstr_sql.Replace("[alias]", pstr_alias);
//            lstr_sql = lstr_sql.Replace("[relacion]", pstr_relacion);
//            lstr_sql = lstr_sql.Replace("[where]", pstr_where);

//            return lstr_sql;

//        }

//        private string PPP_sql_sel_combo(string pstr_campo, object lpcbo_combo, string pstr_tipo_dato = "T", int pint_pos = 0, int pint_long = 0)



//        {

//            // PUBLIC

//            // PPP Pendiente de desarrollar

//            // , lpcbo_combo As Control
//            // 
//            // If Not gfint_combo_todo(lpcbo_combo, pint_pos) _
//            // And Not gfint_combo_nuevo(CStr(lpcbo_combo), pint_pos) Then

//            // sql_sel_combo = Me.SqlCondicion(pstr_campo, gfstr_combo_codigo(lpcbo_combo, pint_pos, pint_long), pstr_tipo_dato, "=")

//            // End If

//            // sql_sel_combo = ""
//            return null;

//        }

//        private string PPP_sel_combo()
//        {
//            // public
//            // _
//            // ByVal pstr_campo As String _
//            // , lpcbo_combo As cls_cvm_combo _
//            // , Optional ByVal pstr_tipo_dato As String = "N" _
//            // , Optional ByVal pstr_cd_campo_buscar As String = "" _
//            // ) As String
//            // COMBO
//            // -- --------------------------------------------------
//            // -- reemplaza a .Sql_Sel_Combo // usa cls_cvm_combo()
//            // -- --------------------------------------------------
//            // If pstr_tipo_dato = "N" And lpcbo_combo.ValorN(pstr_cd_campo_buscar) = 0 _
//            // Or pstr_tipo_dato = "T" And lpcbo_combo.Valor(pstr_cd_campo_buscar) = "" _
//            // Or lpcbo_combo.CD = GC_COMBO_CODIGO_TODO Then
//            // sel_combo = ""
//            // Else
//            // sel_combo = Me.SqlCondicion(pstr_campo, lpcbo_combo.Valor(pstr_cd_campo_buscar), pstr_tipo_dato, "=")
//            // End If

//            // sel_combo = ""

//            return null;
//        }

//        public string sel_string(string pstr_campo, string pstr_buscar, string pstr_wildcard = "*", bool pbln_sensitivo = false, int pint_tipo_base = 0)



//        {
//            string sel_stringRet = default;

//            string lstr_sql;

//            sel_stringRet = "";

//            if (string.IsNullOrEmpty(Strings.Trim(pstr_buscar)))
//                return sel_stringRet;

//            if (pint_tipo_base == 0)
//                pint_tipo_base = mint_id_tipo_base;

//            pstr_buscar = Strings.Trim(pstr_buscar);
//            if (!pbln_sensitivo)
//            {
//                pstr_buscar = Strings.UCase(pstr_buscar);
//                pstr_campo = "UPPER(" + pstr_campo + ")";
//            }

//            // si tiene algún wildcard lo reemplazo por "%"
//            if ((Strings.Left(pstr_buscar, 1) ?? "") == (pstr_wildcard ?? ""))
//                pstr_buscar = "%" + Strings.Mid(pstr_buscar, 2);
//            if ((Strings.Right(pstr_buscar, 1) ?? "") == (pstr_wildcard ?? ""))
//                pstr_buscar = Strings.Left(pstr_buscar, Strings.Len(pstr_buscar) - 1) + "%";

//            // si no tenia wildcard se lo agrego antes y despues
//            if (Strings.Left(pstr_buscar, 1) != "%" & Strings.Right(pstr_buscar, 1) != "%")
//            {
//                pstr_buscar = "%" + pstr_buscar + "%";
//            }

//            lstr_sql = pstr_campo + " LIKE '" + pstr_buscar + "'";

//            if (pint_tipo_base == (int)CVM.Cons.TP_Base.Access)
//            {
//                lstr_sql = Strings.Replace(lstr_sql, "%", CVM.Cons.SQL_Wilcard.ACCESS_WILDCARD);
//            }

//            sel_stringRet = lstr_sql;
//            return sel_stringRet;

//        }

//        public string sqlLIKE(string pstr_campo, string pstr_valor, string pstr_tipo = "A", bool pbln_sensitivo = false)
//        {

//            string lstr_sql = "";

//            if (!pbln_sensitivo)
//            {
//                pstr_valor = Strings.UCase(pstr_valor);
//                pstr_campo = "UPPER(" + pstr_campo + ")";
//            }

//            if (pstr_tipo == "I" | pstr_tipo == "A")
//                pstr_valor = "%" + pstr_valor;
//            if (pstr_tipo == "D" | pstr_tipo == "A")
//                pstr_valor += "%";

//            lstr_sql = pstr_campo + " LIKE '" + pstr_valor + "'";

//            return lstr_sql;

//        }

//        public void q_GROUPBy(string pstr_campo, bool pbln_condicion = true)
//        {

//            var lobj_valor = new CVM.Valor();

//            if (!pbln_condicion)
//                return;
//            if (string.IsNullOrEmpty(Strings.Trim(pstr_campo)))
//                return;

//            lobj_valor.ds_valor1 = pstr_campo;

//            mclc_groupby.Add(lobj_valor);

//            lobj_valor = default;

//        }


//        public void q_ORDERBy(string pstr_campo, bool pbln_condicion = true, bool pbln_ordenDESC = false)
//        {

//            var lobj_valor = new CVM.Valor();

//            if (!pbln_condicion)
//                return;
//            if (string.IsNullOrEmpty(Strings.Trim(pstr_campo)))
//                return;

//            if (pbln_ordenDESC & !pstr_campo.Contains(" DESC"))
//            {
//                pstr_campo += " DESC";
//            }

//            lobj_valor.ds_valor1 = pstr_campo;

//            mclc_orderby.Add(lobj_valor);

//            lobj_valor = default;

//        }

//        public void q_REPLACE(string pstr_buscar, string pstr_reemplazar_por)
//        {

//            var lobj_valor = new CVM.Valor();

//            if (string.IsNullOrEmpty(Strings.Trim(pstr_buscar)))
//                return;

//            lobj_valor.ds_valor1 = pstr_buscar;
//            lobj_valor.ds_valor2 = pstr_reemplazar_por;

//            mclc_replace.Add(lobj_valor);

//            lobj_valor = default;

//        }

//        public void h_HAVING(string pstr_sql, bool pbln_condicion = true)
//        {

//            h_AND(pstr_sql, pbln_condicion);

//        }

//        public void h_AND(string pstr_sql, bool pbln_condicion = true)
//        {

//            ps_agregar_AND(mclc_having, pstr_sql, pbln_condicion);

//        }

//        public void h_OR(string pstr_sql, bool pbln_condicion = true)
//        {

//            ps_agregar_OR(mclc_having, pstr_sql, pbln_condicion);

//        }

//        public void h_ANDn(string pstr_operador, string pstr_sql1, string pstr_sql2, string pstr_sql3 = "", string pstr_sql4 = "", string pstr_sql5 = "")




//        {

//            ps_agregar_ANDn(mclc_having, pstr_operador, pstr_sql1, pstr_sql2, pstr_sql3, pstr_sql4, pstr_sql5);

//        }

//        public void TMP_ADD(string pstr_campo, string pstr_tipo)
//        {

//            var lobj_valor = new CVM.Valor();

//            if (string.IsNullOrEmpty(Strings.Trim(pstr_campo)))
//                return;

//            lobj_valor.ds_valor1 = pstr_campo;
//            lobj_valor.ds_valor2 = pstr_tipo;

//            mclc_tmp_campos.Add(lobj_valor);

//            lobj_valor = default;

//        }
//        public void SP_Param_Agregar(string pstr_campo, object pobj_valor)
//        {

//            var lobj_valor = new CVM.Valor();

//            if (string.IsNullOrEmpty(Strings.Trim(pstr_campo)))
//                return;
//            if (Strings.Left(pstr_campo, 1) != "@")
//                pstr_campo = "@" + pstr_campo;

//            lobj_valor.ds_valor1 = pstr_campo;
//            lobj_valor.ds_valor2 = pobj_valor;

//            mclc_sp_param.Add(lobj_valor);

//            lobj_valor = default;

//        }

//        public string sql_CREATE_TMP(string pstr_tabla = "")
//        {
//            string sql_CREATE_TMPRet = default;

//            string lstr_sql;
//            string lstr_campos = "";

//            sql_CREATE_TMPRet = "";

//            // SELECT convert(int, null) as tipo, convert(varchar(50), null) as codigo INTO #listas

//            if (!string.IsNullOrEmpty(pstr_tabla))
//                Tabla = pstr_tabla;
//            if (string.IsNullOrEmpty(Tabla))
//                return sql_CREATE_TMPRet;

//            if (Strings.Left(Tabla, 1) != "#")
//                Tabla = "#" + Tabla;

//            lstr_sql = "SELECT <campos> INTO <tabla>;DELETE <tabla>";

//            foreach (CVM.Valor lobj_valor in mclc_tmp_campos)
//                lstr_campos = lstr_campos + ", CONVERT(" + lobj_valor.ds_valor2 + ", NULL) AS " + lobj_valor.ds_valor1 + Constants.vbCrLf;

//            lstr_sql = Strings.Replace(lstr_sql, "<campos>", Strings.Mid(lstr_campos, 3));
//            lstr_sql = Strings.Replace(lstr_sql, "<tabla>", Tabla);

//            sql_CREATE_TMPRet = lstr_sql;
//            return sql_CREATE_TMPRet;

//        }


//        public int ValorUpdateBool(object pvar_valor)
//        {
//            int ValorUpdateBoolRet = default;

//            // COPIADO de cvm0.sqlformato_logico_update()

//            // ZZZ no funciona bien
//            // If pvar_valor = vbNull Then
//            // pvar_valor = 0
//            // End If
//            pvar_valor = CVM.Numeros.ValorInt(pvar_valor);

//            switch (TipoBase)
//            {
//                case var @case when @case == CVM.Cons.TP_Base.SQL:
//                    {
//                        ValorUpdateBoolRet = Conversions.ToInteger(Interaction.IIf(Conversions.ToBoolean(pvar_valor), -1, 0));
//                        break;
//                    }

//                case var case1 when case1 == CVM.Cons.TP_Base.Oracle:
//                    {
//                        ValorUpdateBoolRet = Conversions.ToInteger(Interaction.IIf(Conversions.ToBoolean(pvar_valor), 1, 0));
//                        break;
//                    }

//                case var case2 when case2 == CVM.Cons.TP_Base.Access:
//                    {
//                        ValorUpdateBoolRet = Conversions.ToInteger(pvar_valor);
//                        break;
//                    }

//                default:
//                    {
//                        ValorUpdateBoolRet = 0;
//                        break;
//                    }
//            }

//            return ValorUpdateBoolRet;

//        }

//        public CVM.SqlCondicion SetCondicion(string pstr_campo, object pvar_valor, string pstr_tipo_dato = "T", string pstr_comparativo = "=", bool pbln_QuitarHorasEnFecha = false)



//        {
//            CVM.SqlCondicion SetCondicionRet = default;

//            var lobj_condicion = new CVM.SqlCondicion();

//            if (pstr_tipo_dato == "F" & Information.IsDate(pvar_valor))
//            {
//            }

//            if (pstr_tipo_dato == "F" & TipoBase == CVM.Cons.TP_Base.Oracle)
//            {

//            }



//            // pstr_campo = Replace(pstr_campo, "WHERE ", "WHERE TO_CHAR(")
//            // pstr_campo = Replace(pstr_campo, "AND ", "AND TO_CHAR(")
//            // pstr_campo = Replace(pstr_campo, "OR ", "OR TO_CHAR(")

//            // If InStr(pstr_campo, "TO_CHAR") = 0 Then
//            // pstr_campo = "TO_CHAR(" & pstr_campo  '''& ", 'yyyymmdd')"
//            // End If

//            // pstr_campo = pstr_campo & ", 'yyyymmdd')"


//            if (pstr_tipo_dato == "F" & pbln_QuitarHorasEnFecha)
//            {
//                pstr_campo = Strings.Replace("CONVERT(varchar(8), <campo>, 112)", "<campo>", pstr_campo);
//            }

//            lobj_condicion.TipoBase = TipoBase;
//            lobj_condicion.Campo = pstr_campo;
//            lobj_condicion.TipoDato = pstr_tipo_dato;

//            lobj_condicion.Comparativo = pstr_comparativo;
//            lobj_condicion.Valor = pvar_valor;

//            SetCondicionRet = lobj_condicion;
//            return SetCondicionRet;

//        }

//        public string sql_SELECT()
//        {
//            string sql_SELECTRet = default;

//            string lstr_sql_completo = "";
//            string lsql = "";
//            string lstr = "";
//            string lstr_groupby = "";
//            CVM.Valor lobj_valor;

//            mlst_sql = new List<string>();
//            Operacion = CVM.Cons.SQL_Ope.SelectF;

//            lstr_sql_completo = "<SELECT> " + " <cols>" + Constants.vbCrLf + " <into>" + Constants.vbCrLf + " <from>" + " <where>" + " <groupby>" + " <having>" + " <orderby>";

//            lsql = "SELECT ";
//            if (Select_DISTINCT)
//                lsql += " DISTINCT ";
//            if (Select_TOP > 0)
//                lsql += " TOP " + Select_TOP.ToString();

//            lstr_sql_completo = lstr_sql_completo.Replace("<SELECT>", lsql);
//            mlst_sql.Add(lsql);

//            // -- ------------------------------------------------------------
//            // -- SELECT
//            // -- ------------------------------------------------------------
//            lsql = "";
//            foreach (CVM.Valor currentLobj_valor in mclc_select)
//            {
//                lobj_valor = currentLobj_valor;

//                // Select
//                lstr = lobj_valor.ds_valor1;

//                // Group By
//                if (GroupIgualSelect && !lstr.Contains("SUM") && !lstr.Contains("COUNT"))
//                    lstr_groupby += ", " + lstr;

//                // Alias
//                if (lobj_valor.ds_valor2 != "")
//                {
//                    lstr = "(" + lstr + ") AS " + lobj_valor.ds_valor2;
//                }

//                if (!string.IsNullOrEmpty(lsql))
//                    lsql += ", ";
//                lsql += lstr;
//            }
//            if (string.IsNullOrEmpty(lsql))
//                lsql = "*";
//            lstr_sql_completo = Strings.Replace(lstr_sql_completo, "<cols>", lsql);
//            mlst_sql.Add(lsql);

//            // -- ------------------------------------------------------------
//            // -- INTO
//            // -- ------------------------------------------------------------
//            if (string.IsNullOrEmpty(mstr_into))
//            {
//                lstr_sql_completo = Strings.Replace(lstr_sql_completo, "<into>", "");
//            }
//            else
//            {
//                lsql = " INTO " + mstr_into;
//                lstr_sql_completo = Strings.Replace(lstr_sql_completo, "<into>", lsql);
//                mlst_sql.Add(lsql);
//            }

//            // -- ------------------------------------------------------------
//            // -- FROM
//            // -- ------------------------------------------------------------
//            lsql = "";
//            foreach (CVM.Valor currentLobj_valor1 in mclc_from)
//            {
//                lobj_valor = currentLobj_valor1;
//                lstr = lobj_valor.ds_valor3 + " " + lobj_valor.ds_valor1 + " " + lobj_valor.ds_valor2;

//                if (lobj_valor.ds_valor4 != "")
//                    lstr += " ON " + lobj_valor.ds_valor4;

//                mlst_sql.Add(lstr);
//                lsql += " " + lstr + " ";
//            }

//            lstr_sql_completo = Strings.Replace(lstr_sql_completo, "<from>", lsql);

//            // -- ------------------------------------------------------------
//            // -- WHERE
//            // -- ------------------------------------------------------------
//            lsql = sql_WHERE();
//            mlst_sql.Add(lsql);
//            lstr_sql_completo = Strings.Replace(lstr_sql_completo, "<where>", lsql);

//            // -- ------------------------------------------------------------
//            // -- GROUP BY
//            // -- ------------------------------------------------------------
//            if (GroupIgualSelect)
//            {
//                if (!string.IsNullOrEmpty(lstr_groupby))
//                    lstr_groupby = "GROUP BY " + lstr_groupby.Substring(2);
//            }
//            else
//            {
//                lstr_groupby = sql_GROUPBY();
//            }
//            mlst_sql.Add(lstr_groupby);
//            lstr_sql_completo = Strings.Replace(lstr_sql_completo, "<groupby>", lstr_groupby);

//            // -- ------------------------------------------------------------
//            // -- HAVING
//            // -- ------------------------------------------------------------
//            lsql = mfstr_sql_HAVING();
//            mlst_sql.Add(lsql);
//            lstr_sql_completo = Strings.Replace(lstr_sql_completo, "<having>", lsql);

//            // -- ------------------------------------------------------------
//            // -- ORDER BY
//            // -- ------------------------------------------------------------
//            if (OrderIgualGroup)
//            {
//                lsql = Strings.Replace(lstr_groupby, "GROUP BY", "ORDER BY");
//            }
//            else
//            {
//                lsql = sql_ORDERBY();
//            }

//            mlst_sql.Add(lsql);
//            lstr_sql_completo = Strings.Replace(lstr_sql_completo, "<orderby>", lsql);

//            // -- ------------------------------------------------------------
//            // -- FIN: Revision
//            // -- ------------------------------------------------------------
//            sql_SELECTRet = pf_revisar_sql(lstr_sql_completo);
//            return sql_SELECTRet;

//        }

//        public string Log_SQL(string pstr_titulo = "", int pint_operacion = 0, bool pbln_armar_sql = true, string pstr_detalle1 = "", string pstr_detalle2 = "", string pstr_detalle3 = "")
//        {
//            return default;

//            // PPP migrar WEB

//        }

//        public void LogEscribirSql(CVM.Log plog, string pstr_titulo = "")
//        {

//            if (!string.IsNullOrEmpty(pstr_titulo))
//            {
//                plog.EscribirLineaVacia();
//                plog.Escribir(pstr_titulo);
//            }

//            foreach (string lstr in mlst_sql)

//                plog.Escribir(pf_revisar_sql(lstr));

//        }

//        private void ZZZ_190424_DebugPrint_SQL()
//        {
//            // Public

//            // Console.WriteLine("===================================")

//            // For Each lstr As String In mlst_sql
//            // lstr = pf_revisar_sql(lstr)
//            // Console.WriteLine(lstr)
//            // Next

//            // Console.WriteLine("===================================")

//        }

//        private string ZZZ_190424_sql_SELECT_debug()
//        {
//            return null;

//            // Public

//            // Dim lstr_sql As String = ""

//            // Me.sql_SELECT()

//            // lstr_sql = "-- ------------------------------"

//            // For Each lstr As String In mlst_sql
//            // lstr = pf_revisar_sql(lstr)
//            // lstr_sql += lstr + vbCr
//            // Next

//            // lstr_sql += "-- ------------------------------" + vbCr

//            // Return lstr_sql

//        }

//        public string sql_INSERT_SELECT(string pstr_tabla)
//        {
//            string sql_INSERT_SELECTRet = default;

//            string lstr_sql;
//            string lstr_campos = "";

//            Operacion = CVM.Cons.SQL_Ope.SelectF;

//            lstr_sql = "INSERT INTO <tabla> (<campos>) " + Constants.vbCrLf + "<select>";

//            // / campos /
//            foreach (CVM.Valor lobj_valor in mclc_select)
//            {

//                if (!string.IsNullOrEmpty(lstr_campos))
//                    lstr_campos = lstr_campos + ", ";

//                if (lobj_valor.ds_valor2 == "")
//                {
//                    lstr_campos = lstr_campos + lobj_valor.ds_valor1;
//                }
//                else
//                {
//                    lstr_campos = lstr_campos + lobj_valor.ds_valor2;
//                }

//            }

//            lstr_sql = Strings.Replace(lstr_sql, "<tabla>", pstr_tabla);
//            lstr_sql = Strings.Replace(lstr_sql, "<campos>", lstr_campos);
//            lstr_sql = Strings.Replace(lstr_sql, "<select>", sql_SELECT());

//            sql_INSERT_SELECTRet = pf_revisar_sql(lstr_sql);
//            return sql_INSERT_SELECTRet;

//        }

//        public string sql_INSERT()
//        {
//            string lstr_sql = "";
//            string lstr_campo = "";
//            string lstr_valor = "";

//            mlst_sql = new List<string>();

//            Operacion = CVM.Cons.SQL_Ope.Insert;

//            mlst_sql.Add("INSERT");

//            foreach (CVM.Valor lobj_valor in mclc_campos)
//            {

//                mlst_sql.Add("[" + lobj_valor.ds_valor1 + "] [" + lobj_valor.ds_valor2 + "] Len [" + Operators.SubtractObject(Len(lobj_valor.ds_valor2), Interaction.IIf(Strings.Left(lobj_valor.ds_valor2, 1) == "'", 2, 0)) + "]");

//                lstr_campo = lstr_campo + ", " + lobj_valor.ds_valor1;
//                lstr_valor = lstr_valor + ", " + lobj_valor.ds_valor2;

//            }
//            if (false)
//                Debug.Print("--------------------");

//            lstr_campo = Strings.Mid(lstr_campo, 2);
//            lstr_valor = Strings.Mid(lstr_valor, 2);

//            // / si hay SELECTs dentro de los valores cambio el INSERT /
//            if (CVM.Textos.TextoIncluido("SELECT", lstr_valor))
//            {
//                lstr_sql = pf_operacion() + mstr_cd_tabla + " (" + lstr_campo + ") SELECT " + lstr_valor;

//                if (mint_id_tipo_base == CVM.Cons.TP_Base.Access)
//                {
//                    lstr_sql = lstr_sql + " FROM DUAL ";
//                }
//            }

//            else
//            {
//                lstr_sql = pf_operacion() + mstr_cd_tabla + " (" + lstr_campo + ") VALUES (" + lstr_valor + ")";
//            }

//            lstr_sql = pf_revisar_sql(lstr_sql);

//            mlst_sql.Add(lstr_sql);

//            return lstr_sql;

//        }

//        public string sql_UPDATE()
//        {

//            string lstr_sql;

//            Operacion = CVM.Cons.SQL_Ope.Update;

//            mlst_sql = new List<string>();
//            lstr_sql = "";

//            foreach (CVM.Valor lobj_valor in mclc_campos)
//            {
//                mlst_sql.Add("[" + lobj_valor.ds_valor1 + "] [" + lobj_valor.ds_valor2 + "] Len [" + Operators.SubtractObject(Len(lobj_valor.ds_valor2), Interaction.IIf(Strings.Left(lobj_valor.ds_valor2, 1) == "'", 2, 0)) + "]");

//                lstr_sql = lstr_sql + ", " + lobj_valor.ds_valor1 + " = " + lobj_valor.ds_valor2 + Constants.vbCrLf;
//            }

//            lstr_sql = pf_operacion() + mstr_cd_tabla + " SET " + Constants.vbCrLf + Strings.Mid(lstr_sql, 2);

//            lstr_sql = lstr_sql + sql_WHERE();

//            lstr_sql = pf_revisar_sql(lstr_sql);
//            mlst_sql.Add(lstr_sql);

//            return lstr_sql;

//        }

//        public string sql_DELETE()
//        {

//            string lstr_sql;

//            mlst_sql = new List<string>();
//            lstr_sql = "";

//            Operacion = CVM.Cons.SQL_Ope.Delete;

//            lstr_sql = pf_revisar_sql(pf_operacion() + mstr_cd_tabla + sql_WHERE());

//            mlst_sql.Add(lstr_sql);

//            return lstr_sql;

//        }

//        public string sql_WHERE(bool pbln_prefijo_WHERE = true)
//        {
//            string sql_WHERERet = default;

//            string lstr_sql = "";

//            if (pbln_prefijo_WHERE)
//                lstr_sql = " WHERE ";

//            lstr_sql = lstr_sql + "1=1" + Constants.vbCrLf;

//            foreach (CVM.Valor lobj_valor in mclc_where)
//                lstr_sql = lstr_sql + lobj_valor.ds_valor1 + Constants.vbCrLf;

//            sql_WHERERet = lstr_sql;
//            return sql_WHERERet;

//        }
//        public string sql_GROUPBY(bool pbln_prefijo_GroupBy = true)
//        {
//            string sql_GROUPBYRet = default;

//            string lstr_sql;

//            lstr_sql = "";
//            foreach (CVM.Valor lobj_valor in mclc_groupby)
//                lstr_sql = lstr_sql + ", " + lobj_valor.ds_valor1 + Constants.vbCrLf;

//            if (!string.IsNullOrEmpty(lstr_sql))
//            {
//                if (pbln_prefijo_GroupBy)
//                    lstr_sql = "GROUP BY " + Strings.Mid(lstr_sql, 2);
//            }

//            sql_GROUPBYRet = lstr_sql;
//            return sql_GROUPBYRet;

//        }
//        public string sql_ORDERBY(bool pbln_prefijo_orderby = true)
//        {
//            string sql_ORDERBYRet = default;

//            string lstr_sql;

//            lstr_sql = "";
//            foreach (CVM.Valor lobj_valor in mclc_orderby)
//                lstr_sql = lstr_sql + ", " + lobj_valor.ds_valor1 + Constants.vbCrLf;

//            if (!string.IsNullOrEmpty(lstr_sql))
//            {
//                if (pbln_prefijo_orderby)
//                    lstr_sql = "ORDER BY " + Strings.Mid(lstr_sql, 2);
//            }

//            sql_ORDERBYRet = lstr_sql;
//            return sql_ORDERBYRet;

//        }

//        public string SetFecha(string pstr_valor, int pint_tipo_base = 0)
//        {
//            string SetFechaRet = default;

//            SetFechaRet = CVM.Otros.FormatoSQL_Fecha(pstr_valor, pint_tipo_base);
//            return SetFechaRet;

//        }

//        public string EjecutarUpdate([Optional, DefaultParameterValue(null)] ref SqlConnection pCnn, [Optional, DefaultParameterValue(null)] ref SqlTransaction pTrn, string pstr_BD = "")
//        {

//            string lstr_error = "";

//            this.EjecutarSQLCmd(CVM.Cons.SQL_Ope.Update, ref lstr_error, pCnn, pTrn, pstr_BD: pstr_BD);

//            return Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(lstr_error), "OK", lstr_error));

//        }

//        public string EjecutarDelete([Optional, DefaultParameterValue(null)] ref SqlConnection pCnn, [Optional, DefaultParameterValue(null)] ref SqlTransaction pTrn, string pstr_BD = "")
//        {

//            string lstr_error = "";

//            this.EjecutarSQLCmd(CVM.Cons.SQL_Ope.Delete, ref lstr_error, pCnn, pTrn, pstr_BD: pstr_BD);

//            return Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(lstr_error), "OK", lstr_error));

//        }

//        public string EjecutarInto(string pstr_Into = "#temp", SqlConnection pCnn = null, SqlTransaction pTrn = null, string pstr_BD = "")
//        {

//            q_INTO(pstr_Into);

//            return EjecutarSelectF(pCnn, pTrn, pstr_BD);

//        }

//        public string EjecutarSelectF(SqlConnection pCnn = null, SqlTransaction pTrn = null, string pstr_BD = "")
//        {

//            return this.Ejecutar(CVM.Cons.SQL_Ope.SelectF, pCnn, pTrn, pstr_BD: pstr_BD);

//        }

//        public string EjecutarSQL(SqlConnection pCnn = null, SqlTransaction pTrn = null, string pstr_BD = "")
//        {

//            return this.Ejecutar(CVM.Cons.SQL_Ope.SQL, pCnn, pTrn, pstr_BD: pstr_BD);

//        }

//        public string Ejecutar(CVM.Cons.SQL_Ope pint_ope, SqlConnection pCnn = null, SqlTransaction pTrn = null, bool pbln_identity = false, [Optional, DefaultParameterValue(-1)] ref long plng_key, string pstr_BD = "")
//        {

//            string lstr_res = "";

//            EjecutarSQLCmd(pint_ope, ref lstr_res, pCnn, pTrn, pbln_identity, ref plng_key, pstr_BD);

//            if (string.IsNullOrEmpty(lstr_res))
//                lstr_res = "OK";

//            // PPP migrar WEB
//            // If lstr_res <> "OK" AndAlso CVM.RT.Parametros.ValorBool("LogErrorSql") Then
//            // Me.Log_SQL("Error en SQL",,, lstr_res)
//            // End If

//            return lstr_res;

//        }

//        private void zzz_181023_EjecutarSQLCmd()
//        {
//            // Public 
//            // pint_ope As CVM.Cons.SQL_Ope,
//            // Optional ByRef pstr_error As String = "",
//            // Optional ByVal pCnn As SqlConnection = Nothing,
//            // Optional ByVal pTrn As SqlTransaction = Nothing,
//            // Optional ByVal pbln_identity As Boolean = False,
//            // Optional ByRef plng_key As Long = -1
//            // )

//            // Dim lvlng_regs_afectado As Long
//            // Dim lbln_abrir_base As Boolean

//            // If pint_ope <> 0 Then Me.Operacion = pint_ope


//            // Me.SQL = Me.SQLSegunOperacion(pint_ope)

//            // If Me.SQL = "" Then Exit Sub

//            // If pint_ope = CVM.Cons.SQL_Ope.Insert AndAlso pbln_identity Then
//            // Me.SQL += "; Select Scope_Identity()"
//            // End If

//            // lbln_abrir_base = (pCnn Is Nothing)

//            // Try
//            // Dim MyCommand As SqlCommand

//            // If lbln_abrir_base Then
//            // CVM.BdSql.AbrirCnn(pCnn)
//            // End If

//            // If pTrn Is Nothing Then
//            // MyCommand = New SqlCommand(Me.SQL, pCnn)
//            // Else
//            // MyCommand = New SqlCommand(Me.SQL, pCnn, pTrn)
//            // End If

//            // 'Verificar. Llamar a uno u otro en caso de insert o update
//            // MyCommand.CommandType = CommandType.Text

//            // If pint_ope = CVM.Cons.SQL_Ope.Insert AndAlso pbln_identity Then
//            // plng_key = MyCommand.ExecuteScalar
//            // Else
//            // lvlng_regs_afectado = MyCommand.ExecuteNonQuery()
//            // End If

//            // Catch ex As SqlException
//            // pstr_error = ex.Message
//            // End Try

//            // If lbln_abrir_base Then
//            // CVM.BdSql.CerrarCnn(pCnn)
//            // End If

//            // Return

//        }

//        private void zzz_181023_EjecutarSQLCmd2()
//        {
//            // Public  _
//            // ByVal pint_operacion As CVM.Cons.SQL_Ope, _
//            // Optional ByRef pstr_error As String = "", _
//            // Optional ByRef pCnn As SqlConnection = Nothing, _
//            // Optional ByRef PTrn As SqlTransaction = Nothing, _
//            // Optional ByVal pbln_identity As Boolean = False,
//            // Optional ByRef plng_key As Long = -1,
//            // Optional ByRef pbln_abrir_base As Boolean = True,
//            // Optional ByRef pbln_cerrar_base As Boolean = True
//            // )

//            // Dim lvlng_regs_afectado As Long

//            // If pint_operacion <> 0 Then Me.Operacion = pint_operacion

//            // Me.SQL = Me.SQLSegunOperacion(pint_operacion)

//            // If Me.SQL = "" Then Exit Sub

//            // If pint_operacion = CVM.Cons.SQL_Ope.Insert AndAlso pbln_identity Then
//            // Me.SQL += "; Select Scope_Identity()"
//            // End If

//            // Try
//            // Dim MyCommand As SqlCommand

//            // If pbln_abrir_base Then
//            // CVM.BdSql.AbrirCnn(pCnn)
//            // End If

//            // If PTrn Is Nothing Then
//            // MyCommand = New SqlCommand(Me.SQL, pCnn)
//            // Else
//            // MyCommand = New SqlCommand(Me.SQL, pCnn, PTrn)
//            // End If

//            // 'Verificar. Llamar a uno u otro en caso de insert o update
//            // MyCommand.CommandType = CommandType.Text

//            // If pint_operacion = CVM.Cons.SQL_Ope.Insert AndAlso pbln_identity Then
//            // plng_key = MyCommand.ExecuteScalar
//            // Else
//            // lvlng_regs_afectado = MyCommand.ExecuteNonQuery()
//            // End If

//            // Catch ex As SqlException
//            // pstr_error = ex.Message
//            // End Try

//            // If pbln_cerrar_base Then
//            // CVM.BdSql.CerrarCnn(pCnn)
//            // End If

//            // Return

//        }

//        public void EjecutarSQLCmd(CVM.Cons.SQL_Ope pint_ope, [Optional, DefaultParameterValue("")] ref string pstr_error, SqlConnection pCnn = null, SqlTransaction pTrn = null, bool pbln_identity = false, [Optional, DefaultParameterValue(-1)] ref long plng_key, string pstr_BD = "")
//        {

//            long lvlng_regs_afectado;
//            bool lbln_abrir_base;

//            if (pint_ope != 0)
//                Operacion = pint_ope;


//            SQL = SQLSegunOperacion(pint_ope);

//            if (string.IsNullOrEmpty(SQL))
//                return;

//            if (pbln_identity)
//            {
//                SQL += "; Select Scope_Identity()";
//            }

//            lbln_abrir_base = pCnn is null;

//            try
//            {
//                SqlCommand MyCommand;

//                if (lbln_abrir_base)
//                {
//                    CVM.BdSql.AbrirCnn(pCnn, default, default, pstr_BD);
//                }

//                if (pTrn is null)
//                {
//                    MyCommand = new SqlCommand(SQL, pCnn);
//                }
//                else
//                {
//                    MyCommand = new SqlCommand(SQL, pCnn, pTrn);
//                }

//                // Verificar. Llamar a uno u otro en caso de insert o update
//                MyCommand.CommandType = CommandType.Text;

//                if (pbln_identity)
//                {
//                    plng_key = CVM.Numeros.ValorLng(MyCommand.ExecuteScalar());
//                }
//                else
//                {
//                    lvlng_regs_afectado = MyCommand.ExecuteNonQuery();
//                }
//            }

//            catch (SqlException ex)
//            {
//                pstr_error = ex.Message;
//            }

//            if (lbln_abrir_base)
//            {
//                CVM.BdSql.CerrarCnn(pCnn);
//            }

//            return;

//        }

//        public string SQLSegunOperacion(CVM.Cons.SQL_Ope pint_operacion)
//        {
//            string SQLSegunOperacionRet = default;

//            switch (pint_operacion)
//            {
//                case var @case when @case == CVM.Cons.SQL_Ope.Insert:
//                    {
//                        SQLSegunOperacionRet = sql_INSERT();
//                        break;
//                    }
//                case var case1 when case1 == CVM.Cons.SQL_Ope.Update:
//                    {
//                        SQLSegunOperacionRet = sql_UPDATE();
//                        break;
//                    }
//                case var case2 when case2 == CVM.Cons.SQL_Ope.Delete:
//                    {
//                        SQLSegunOperacionRet = sql_DELETE();
//                        break;
//                    }
//                case var case3 when case3 == CVM.Cons.SQL_Ope.SelectF:
//                    {
//                        SQLSegunOperacionRet = sql_SELECT();
//                        break;
//                    }
//                case var case4 when case4 == CVM.Cons.SQL_Ope.SQL:
//                    {
//                        SQLSegunOperacionRet = pf_revisar_sql(SQL);
//                        break;
//                    }

//                default:
//                    {
//                        SQLSegunOperacionRet = "";
//                        break;
//                    }
//            }

//            return SQLSegunOperacionRet;

//        }

//        public void CopiarSQLEnClipboard()
//        {

//            // System.Windows.Forms.Clipboard.Clear()
//            // System.Windows.Forms.Clipboard.SetText(Me.SQL)

//        }

//        public string EjecutarDT(ref DataTable pTab, SqlConnection pCnn = null, SqlTransaction pTrn = null, string pstr_BD = "")
//        {

//            SqlDataAdapter lAda;
//            bool lbln_base_abrir = false;
//            string lstr_res = "OK";
//            string lstr_res_cerrar = "";

//            if (string.IsNullOrEmpty(SQL))
//            {
//                SQL = sql_SELECT();
//            }
//            else
//            {
//                SQL = pf_revisar_sql(SQL);
//            }

//            lstr_res = CVM.BdSql.AbrirCnn(pCnn, true, lbln_base_abrir, pstr_BD);

//            if (lstr_res == "OK")
//            {
//                try
//                {
//                    lAda = new SqlDataAdapter(SQL, pCnn);
//                    lAda.SelectCommand.CommandType = CommandType.Text;

//                    if (pTrn is not null)
//                    {
//                        lAda.SelectCommand.Transaction = pTrn;
//                    }

//                    pTab = new DataTable();
//                    lAda.Fill(pTab);
//                }

//                catch (Exception ex)
//                {
//                    lstr_res = ex.Message;

//                }

//                if (lbln_base_abrir)
//                {
//                    lstr_res_cerrar = CVM.BdSql.CerrarCnn(pCnn);
//                    pCnn = null;
//                    lstr_res = Conversions.ToString(Interaction.IIf(lstr_res == "OK", lstr_res_cerrar, lstr_res));
//                }
//            }

//            return lstr_res;

//        }

//        public string EjecutarDT2([Optional, DefaultParameterValue(default)] ref DataTable pTab, SqlConnection pCnn = null, SqlTransaction pTrn = null, string pstr_BD = "")
//        {

//            SqlDataAdapter lAda;
//            bool lbln_base_abrir = false;
//            string lstr_res;

//            if (string.IsNullOrEmpty(SQL))
//                SQL = sql_SELECT();

//            lstr_res = CVM.BdSql.IniciarOperacion(pCnn, pTrn, false, lbln_base_abrir, pstr_BD);

//            if (lstr_res == "OK")
//            {
//                try
//                {
//                    lAda = new SqlDataAdapter(SQL, pCnn);
//                    lAda.SelectCommand.CommandType = CommandType.Text;

//                    if (pTrn is not null)
//                    {
//                        lAda.SelectCommand.Transaction = pTrn;
//                    }

//                    pTab = new DataTable();
//                    lAda.Fill(pTab);
//                }

//                catch (Exception ex)
//                {
//                    lstr_res = ex.Message;

//                }
//            }

//            lstr_res = CVM.BdSql.CerrarOperacion(pCnn, pTrn, lbln_base_abrir, false, false, lstr_res);

//            return lstr_res;

//        }

//        public string EjecutarDT3(ref DataTable pTab, [Optional, DefaultParameterValue(null)] ref SqlConnection pCnn, [Optional, DefaultParameterValue(null)] ref SqlTransaction pTrn, [Optional, DefaultParameterValue(true)] ref bool pbln_abrir_base, [Optional, DefaultParameterValue(true)] ref bool pbln_cerrar_base, string pstr_BD = "")
//        {

//            SqlDataAdapter lAda;
//            bool lbln_base_abrir = false;
//            string lstr_res = "OK";

//            if (string.IsNullOrEmpty(SQL))
//                SQL = sql_SELECT();

//            if (pbln_abrir_base)
//            {
//                lstr_res = CVM.BdSql.AbrirCnn(pCnn, default, default, pstr_BD);
//            }

//            if (lstr_res == "OK")
//            {
//                try
//                {
//                    lAda = new SqlDataAdapter(SQL, pCnn);
//                    lAda.SelectCommand.CommandType = CommandType.Text;

//                    if (pTrn is not null)
//                    {
//                        lAda.SelectCommand.Transaction = pTrn;
//                    }

//                    pTab = new DataTable();
//                    lAda.Fill(pTab);
//                }

//                catch (Exception ex)
//                {
//                    lstr_res = ex.Message;

//                }

//                if (lstr_res == "OK" & pbln_cerrar_base)
//                {
//                    lstr_res = CVM.BdSql.CerrarCnn(pCnn);
//                }

//            }

//            return lstr_res;

//        }

//        public string EjecutarDT_Edit(ref SqlConnection pCnn, ref SqlTransaction pTrn, ref SqlDataAdapter pAda, ref DataTable pTab, string pstr_BD = "")
//        {

//            bool lbln_base_abrir = false;
//            string lstr_res;

//            if (string.IsNullOrEmpty(SQL))
//                SQL = sql_SELECT();

//            lstr_res = CVM.BdSql.IniciarOperacion(pCnn, pTrn, false, lbln_base_abrir, pstr_BD);

//            if (lstr_res == "OK")
//            {
//                try
//                {
//                    pAda = new SqlDataAdapter(SQL, pCnn);

//                    SqlClient.SqlCommandBuilder lCB;
//                    lCB = new SqlClient.SqlCommandBuilder(pAda);

//                    pAda.SelectCommand.CommandType = CommandType.Text;

//                    if (pTrn is not null)
//                    {
//                        pAda.SelectCommand.Transaction = pTrn;
//                    }

//                    pTab = new DataTable();
//                    pAda.Fill(pTab);
//                }

//                catch (Exception ex)
//                {
//                    lstr_res = ex.Message;

//                }
//            }

//            lstr_res = CVM.BdSql.CerrarOperacion(pCnn, pTrn, lbln_base_abrir, false, false, lstr_res);

//            return lstr_res;

//        }

//        public string Ejecutar_DR(ref DataRow lReg, [Optional, DefaultParameterValue(null)] ref SqlConnection pCnn, [Optional, DefaultParameterValue(null)] ref SqlTransaction pTrn, string pstr_BD = "")
//        {

//            var lTab = new DataTable();
//            string lstr_res;

//            lstr_res = EjecutarDT(ref lTab, pCnn, pTrn, pstr_BD);

//            if (lstr_res == "OK" && lTab.Rows.Count > 0)
//            {
//                lReg = lTab.Rows(0);
//            }
//            else
//            {
//                lReg = default;
//            }

//            lTab = default;

//            return lstr_res;

//        }

//        public string DR_Valor(string pstr_campo, ref string pstr_res, [Optional, DefaultParameterValue(null)] ref SqlConnection pCnn, [Optional, DefaultParameterValue(null)] ref SqlTransaction pTrn, string pstr_BD = "")
//        {

//            DataRow lReg = default;
//            string lstr_valor = "";

//            pstr_res = Ejecutar_DR(ref lReg, ref pCnn, ref pTrn, pstr_BD);

//            if (pstr_res == "OK" && lReg is not null)
//            {
//                lstr_valor = CVM.DR.Str(lReg, pstr_campo);
//            }
//            else
//            {
//                lReg = default;
//            }

//            lReg = default;

//            return lstr_valor;

//        }

//        public static bool EvalCondicion(string pstr_ExpresionSql, ref string pstr_res)
//        {

//            var lsql = new CVM.Sql();
//            bool lbln_res = false;

//            pstr_ExpresionSql = "SELECT lg_res = CASE WHEN (" + pstr_ExpresionSql + ") THEN 1 ELSE 0 END";
//            pstr_res = "OK";

//            {
//                ref var withBlock = ref lsql;
//                withBlock.SQL = pstr_ExpresionSql;
//                lbln_res = CVM.Numeros.ValorInt(withBlock.DR_Valor("LG_RES", pstr_res)) == 1;
//            }

//            if (pstr_res != "OK")
//            {
//                pstr_res = pstr_res; // ddd 
//            }

//            lsql = default;

//            return lbln_res;

//        }

//        public static string EvalFormula(string pstr_sql, ref string pstr_res)
//        {

//            var lsql = new CVM.Sql();
//            string lstr_valor = "";

//            pstr_res = "OK";

//            pstr_sql = pstr_sql.Trim();
//            if (Strings.Left(pstr_sql, 1) != "(")
//                pstr_sql = "(" + pstr_sql + ")";
//            // If Right(pstr_sql, 1) <> ")" Then pstr_sql += ")"

//            {
//                ref var withBlock = ref lsql;
//                withBlock.SQL = "SELECT Valor = " + pstr_sql;
//                lstr_valor = withBlock.DR_Valor("VALOR", pstr_res);
//            }

//            if (pstr_res != "OK")
//            {
//                pstr_res = pstr_res; // ddd 
//            }

//            lsql = default;

//            return lstr_valor;

//        }

//        public bool ExistenRegs([Optional, DefaultParameterValue("")] ref string pstr_res, SqlConnection pCnn = null, SqlTransaction pTrn = null, string pstr_BD = "")
//        {
//            bool ExistenRegsRet = default;

//            var lTab = new DataTable();

//            pstr_res = EjecutarDT2(ref lTab, pCnn, pTrn, pstr_BD);

//            ExistenRegsRet = pstr_res == "OK" && lTab.Rows.Count > 0;

//            lTab = default;
//            return ExistenRegsRet;

//        }

//        public string EjecutarSP(string pstr_SP, ref DataTable pTab, SqlConnection pCnn = null, SqlTransaction pTrn = null, string pstr_BD = "")
//        {

//            string lstr_res = "OK";
//            var lbln_base_abrir = default(bool);
//            SqlDataAdapter lAda;

//            // -- --------------------------------------------------
//            // -- apertura de base de datos
//            // -- --------------------------------------------------
//            CVM.BdSql.IniciarOperacion(pCnn, pTrn, false, lbln_base_abrir, pstr_BD);

//            try
//            {
//                lAda = new SqlDataAdapter(pstr_SP, pCnn);
//                lAda.SelectCommand.CommandType = CommandType.StoredProcedure;

//                foreach (CVM.Valor lobj_valor in mclc_sp_param)
//                    lAda.SelectCommand.Parameters.AddWithValue(lobj_valor.ds_valor1, lobj_valor.ds_valor2);

//                pTab = new DataTable();
//                lAda.Fill(pTab);
//            }

//            catch (Exception ex)
//            {
//                lstr_res = ex.Message;

//            }

//            CVM.BdSql.CerrarOperacion(pCnn, pTrn, lbln_base_abrir, false, true);

//            return lstr_res;

//        }

//        public static string EjecutarDTConParametros(ref DataTable pdt_res, string lstr_sql, List<object> vParametros, SqlConnection pCnn = null, SqlTransaction pTrn = null, string pstr_BD = "")
//        {

//            SqlDataAdapter lAda;
//            bool lbln_base_abrir = false;
//            int i;
//            string lstr_res = "OK";

//            lstr_res = CVM.BdSql.AbrirCnn(pCnn, true, lbln_base_abrir, pstr_BD);

//            if (lstr_res == "OK")
//            {

//                try
//                {
//                    lAda = new SqlDataAdapter(lstr_sql, pCnn);
//                    lAda.SelectCommand.CommandType = CommandType.Text;

//                    var loopTo = vParametros.Count - 1;
//                    for (i = 0; i <= loopTo; i++)
//                        lAda.SelectCommand.Parameters.AddWithValue("@p" + Strings.Trim(Conversion.Str(i)), vParametros[i]);

//                    if (pTrn is not null)
//                    {
//                        lAda.SelectCommand.Transaction = pTrn;
//                    }

//                    pdt_res = new DataTable();
//                    lAda.Fill(pdt_res);
//                }

//                catch (Exception ex)
//                {
//                    lstr_res = ex.Message;

//                }

//                if (lstr_res == "OK" & lbln_base_abrir)
//                {
//                    lstr_res = CVM.BdSql.CerrarCnn(pCnn);
//                }
//            }

//            return lstr_res;

//        }


//        private string zzz_RDS_Valor()
//        {
//            // PPP para pensar en migrar
//            // RDS_Valor = ""
//            // 
//            // _
//            // ByVal pstr_campo As String, _
//            // pbln_abrir_bd As Boolean, _
//            // Optional pbln_bd_local As Boolean, _
//            // Optional ByVal pstr_servidor As String, _
//            // Optional ByVal pstr_base As String, _
//            // Optional pdba_ado As ADODB.Connection _
//            // ) As String

//            // Dim ords_reg As ADODB.Recordset
//            // Dim lstr_valor As String

//            // '''Me.EjecutarRecordset ords_reg, pbln_abrir_bd, pbln_bd_local, pstr_servidor, pstr_base
//            // If pdba_ado Is Nothing Then
//            // Me.EjecutarRecordset(ords_reg, pbln_abrir_bd, pbln_bd_local, pstr_servidor, pstr_base, Me.SQL)
//            // '''Me.EjecutarRDS ords_reg, pbln_abrir_bd, pbln_bd_local, pstr_servidor, pstr_base, Me.SQL
//            // Else
//            // Me.EjecutarRDS(ords_reg, pbln_abrir_bd, pbln_bd_local, pstr_servidor, pstr_base, Me.SQL, , pdba_ado)
//            // End If

//            // If Not ords_reg.EOF Then

//            // Select Case UCase(CVM.Otros.ValorSTR(ords_reg(pstr_campo)))
//            // Case "TRUE" : lstr_valor = "1"
//            // Case "FALSE" : lstr_valor = "0"
//            // Case Else : lstr_valor = CVM.Otros.ValorSTR(ords_reg(pstr_campo))
//            // End Select

//            // End If

//            // Me.CerrarRecordset(ords_reg, pbln_abrir_bd, pbln_bd_local)

//            // RDS_Valor = lstr_valor

//            return null;
//        }

//        private string zzz_rdsValorStr(string pstr_campo)
//        {
//            // PPP para pensar en migrar
//            // Public 
//            // rdsValorStr = ""

//            // rdsValorStr = CVM.Otros.ValorSTR(Me.RDS(pstr_campo))

//            return null;
//        }

//        private double zzz_rdsValorN(string pstr_campo)
//        {
//            // PPP para pensar en migrar
//            // Public 

//            // rdsValorN = 0

//            // 
//            // rdsValorN = CVM.Numeros.ValorDbl(Me.RDS(pstr_campo))

//            return default;
//        }

//        private bool zzz_rdsValorBln(string pstr_campo)
//        {

//            // PPP para pensar en migrar
//            // Public 
//            // rdsValorBln = False

//            // 
//            // rdsValorBln = CVM.Otros.ValorBln(Me.RDS(pstr_campo))

//            return default;
//        }

//        #endregion

//        // -- ------------------------------------------------------------------------------------------
//        #region Métodos Privados
//        // -- ------------------------------------------------------------------------------------------

//        private void ps_iniciar_clc()
//        {

//            mclc_campos = new Collection();
//            mclc_select = new Collection();
//            mclc_where = new Collection();
//            mclc_from = new Collection();
//            mclc_groupby = new Collection();
//            mclc_having = new Collection();
//            mclc_orderby = new Collection();
//            mclc_replace = new Collection();
//            mclc_tmp_campos = new Collection();
//            mclc_sp_param = new Collection();

//        }

//        private void ps_agregar_FROM(string pstr_tabla, string pstr_alias = "", string pstr_relacion = "", string pstr_relacion_on = "")


//        {

//            var lobj_valor = new CVM.Valor();

//            if (Strings.InStr(Strings.UCase(pstr_tabla), "SELECT") > 0)
//            {
//                pstr_tabla = "(" + pstr_tabla + ")";
//            }

//            lobj_valor.ds_valor1 = pstr_tabla;
//            lobj_valor.ds_valor2 = pstr_alias;
//            lobj_valor.ds_valor3 = pstr_relacion;
//            lobj_valor.ds_valor4 = pstr_relacion_on;

//            mclc_from.Add(lobj_valor);

//            lobj_valor = default;

//        }

//        private CVM.Valor mfobj_SetCampo(string pstr_campo, object pvar_valor, string pstr_tipo_dato, string pstr_formato, string pstr_operador = "=", bool pbln_NullSiEsVacio = false)




//        {
//            CVM.Valor mfobj_SetCampoRet = default;

//            var lobj_valor = new CVM.Valor();

//            lobj_valor.ds_valor1 = pstr_campo;

//            switch (pstr_tipo_dato ?? "")
//            {
//                case "T":    // Texto
//                    {
//                        lobj_valor.ds_valor2 = Interaction.IIf(Operators.AndObject(pbln_NullSiEsVacio, Operators.ConditionalCompareObjectEqual(pvar_valor, "", false)), "NULL", CVM.Otros.FormatoSQL_Texto(pvar_valor));
//                        break;
//                    }

//                case "N":    // Numerico
//                    {
//                        // / si dice NULL lo dejo /
//                        if (!Information.IsNumeric(pvar_valor) && Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(pvar_valor, "NULL", false)))
//                        {
//                            lobj_valor.ds_valor2 = "NULL";
//                        }
//                        else
//                        {
//                            lobj_valor.ds_valor2 = Interaction.IIf(pbln_NullSiEsVacio & CVM.Numeros.ValorDbl(pvar_valor) == 0, "NULL", CVM.Numeros.ValorDbl(pvar_valor));
//                        }

//                        break;
//                    }

//                case "F":
//                    {
//                        lobj_valor.ds_valor2 = CVM.Otros.FormatoSQL_Fecha(pvar_valor, mint_id_tipo_base);
//                        break;
//                    }

//                case "H":    // Hora
//                    {
//                        lobj_valor.ds_valor2 = "'" + Strings.Format(pvar_valor, "HH:MM") + "'";
//                        break;
//                    }

//                case "B":    // Boolean
//                    {
//                        // .ds_valor2 = sqlformato_logico_update(pvar_valor, mint_id_tipo_base)
//                        lobj_valor.ds_valor2 = ValorUpdateBool(pvar_valor);
//                        break;
//                    }

//                case "V":    // Valor sin convertir (por ejemplo a) " = dbo.FN_...)", b) " = (SELECT id_... FROM tabla WHERE ...)"
//                    {
//                        lobj_valor.ds_valor2 = CVM.Otros.ValorSTR(pvar_valor);
//                        if (CVM.Textos.TextoIncluido("SELECT", UCase(lobj_valor.ds_valor2)))
//                        {
//                            lobj_valor.ds_valor2 = "(" + lobj_valor.ds_valor2 + ")";
//                        }

//                        break;
//                    }

//            }

//            if (!string.IsNullOrEmpty(pstr_formato))
//            {
//                lobj_valor.ds_valor2 = Strings.Format(lobj_valor.ds_valor2, pstr_formato);
//            }

//            lobj_valor.ds_valor3 = pstr_operador;

//            mfobj_SetCampoRet = lobj_valor;
//            return mfobj_SetCampoRet;

//        }

//        private string mfstr_sql_HAVING()
//        {
//            string mfstr_sql_HAVINGRet = default;

//            string lstr_sql = "";

//            if (mclc_having.Count > 0)
//            {
//                lstr_sql = "HAVING 1=1" + Constants.vbCrLf;
//            }

//            foreach (CVM.Valor lobj_valor in mclc_having)
//                lstr_sql = lstr_sql + lobj_valor.ds_valor1 + Constants.vbCrLf;

//            mfstr_sql_HAVINGRet = lstr_sql;
//            return mfstr_sql_HAVINGRet;

//        }

//        private string pf_revisar_sql(string pstr_sql)
//        {
//            string pf_revisar_sqlRet = default;

//            foreach (CVM.Valor lobj_valor in mclc_replace)
//                pstr_sql = Strings.Replace(pstr_sql, lobj_valor.ds_valor1, lobj_valor.ds_valor2);

//            pf_revisar_sqlRet = pstr_sql;
//            return pf_revisar_sqlRet;

//        }

//        private void ps_agregar_AND(Collection lpclc_lista, string pstr_sql, bool pbln_condicion = true)
//        {

//            if (!string.IsNullOrEmpty(pstr_sql))
//            {
//                ps_agregar_condicion(lpclc_lista, "AND", pstr_sql, pbln_condicion);
//            }

//        }
//        private void ps_agregar_OR(Collection lpclc_lista, string pstr_sql, bool pbln_condicion = true)
//        {

//            if (!string.IsNullOrEmpty(pstr_sql))
//            {
//                ps_agregar_condicion(lpclc_lista, "OR", pstr_sql, pbln_condicion);
//            }

//        }
//        private void ps_agregar_ANDn(Collection lpclc_lista, string pstr_operador, string pstr_sql1, string pstr_sql2, string pstr_sql3 = "", string pstr_sql4 = "", string pstr_sql5 = "", string pstr_sql6 = "", string pstr_sql7 = "", string pstr_sql8 = "", string pstr_sql9 = "", string pstr_sql10 = "")










//        {

//            string lstr_sql;

//            pstr_operador = " " + pstr_operador + " ";

//            lstr_sql = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("(" + pstr_sql1, Interaction.IIf(string.IsNullOrEmpty(pstr_sql2), "", pstr_operador + pstr_sql2)), Interaction.IIf(string.IsNullOrEmpty(pstr_sql3), "", pstr_operador + pstr_sql3)), Interaction.IIf(string.IsNullOrEmpty(pstr_sql4), "", pstr_operador + pstr_sql4)), Interaction.IIf(string.IsNullOrEmpty(pstr_sql5), "", pstr_operador + pstr_sql5)), Interaction.IIf(string.IsNullOrEmpty(pstr_sql6), "", pstr_operador + pstr_sql6)), Interaction.IIf(string.IsNullOrEmpty(pstr_sql7), "", pstr_operador + pstr_sql7)), Interaction.IIf(string.IsNullOrEmpty(pstr_sql8), "", pstr_operador + pstr_sql8)), Interaction.IIf(string.IsNullOrEmpty(pstr_sql9), "", pstr_operador + pstr_sql9)), Interaction.IIf(string.IsNullOrEmpty(pstr_sql10), "", pstr_operador + pstr_sql10)), ")"));










//            ps_agregar_condicion(lpclc_lista, "AND", lstr_sql);

//        }

//        private void ps_agregar_condicion(Collection lpclc_lista, string pstr_operador, string pstr_sql, bool pbln_condicion = true)
//        {

//            // / agrega un SQL de condicion en una lista (WHERE o HAVING) /

//            var lobj_valor = new CVM.Valor();

//            if (pbln_condicion)
//            {
//                lobj_valor.ds_valor1 = pstr_operador + " " + pstr_sql;
//                lpclc_lista.Add(lobj_valor);
//            }

//            lobj_valor = default;

//        }

//        private string pf_operacion()
//        {

//            string lstr_sql = "";

//            switch (mint_operacion)
//            {
//                case var @case when @case == CVM.Cons.SQL_Ope.Insert:
//                    {
//                        switch (mint_id_tipo_base)
//                        {
//                            case var case1 when case1 == CVM.Cons.TP_Base.Access:
//                                {
//                                    lstr_sql = CVM.Cons.SQL_cmd.Insert_ACC;
//                                    break;
//                                }
//                            case var case2 when case2 == CVM.Cons.TP_Base.SQL:
//                                {
//                                    lstr_sql = CVM.Cons.SQL_cmd.Insert_SQL;
//                                    break;
//                                }
//                            case var case3 when case3 == CVM.Cons.TP_Base.Oracle:
//                                {
//                                    lstr_sql = CVM.Cons.SQL_cmd.Insert_ORA;
//                                    break;
//                                }
//                        }

//                        break;
//                    }

//                case var case4 when case4 == CVM.Cons.SQL_Ope.Update:
//                    {
//                        lstr_sql = "UPDATE ";
//                        break;
//                    }

//                case var case5 when case5 == CVM.Cons.SQL_Ope.Delete:
//                    {
//                        switch (mint_id_tipo_base)
//                        {
//                            case var case6 when case6 == CVM.Cons.TP_Base.Access:
//                                {
//                                    lstr_sql = CVM.Cons.SQL_cmd.Delete_ACC;
//                                    break;
//                                }
//                            case var case7 when case7 == CVM.Cons.TP_Base.SQL:
//                                {
//                                    lstr_sql = CVM.Cons.SQL_cmd.Delete_SQL;
//                                    break;
//                                }
//                            case var case8 when case8 == CVM.Cons.TP_Base.Oracle:
//                                {
//                                    lstr_sql = CVM.Cons.SQL_cmd.Delete_ORA;
//                                    break;
//                                }
//                        }

//                        break;
//                    }

//                default:
//                    {
//                        lstr_sql = "###ERROR###";
//                        break;
//                    }

//            }

//            return lstr_sql;

//        }

//        private bool zzz_mfbln_bd_abierta()
//        {
//            // PPP es necesario hacerlo?
//            // mfbln_bd_abierta = False

//            // On Error GoTo error_handle

//            // mfbln_bd_abierta = (mvdba_base.ConnectionString <> "")

//            // Exit Function

//            // error_handle:
//            // mfbln_bd_abierta = False

//            return default;
//        }

//        #endregion

//        // -- ------------------------------------------------------------------------------------------
//        #region Métodos Shared
//        // -- ------------------------------------------------------------------------------------------
//        public static bool ExistenRegistros(string pstr_tabla, string pstr_where, SqlClient.SqlConnection pCnn = default, SqlClient.SqlTransaction pTrn = default, [Optional, DefaultParameterValue("OK")] ref string lstr_res)
//        {

//            var lsql = new CVM.Sql();
//            var lTab = new DataTable();

//            {
//                ref var withBlock = ref lsql;
//                withBlock.q_SELECT();
//                withBlock.q_FROM(pstr_tabla);
//                withBlock.w_WHERE(pstr_where);
//                lstr_res = withBlock.EjecutarDT(lTab, pCnn, pTrn);
//            }
//            lsql = default;

//            return lstr_res == "OK" && lTab.Rows.Count > 0;

//        }

//        public static bool ExistenRegistros(string pstr_sql, SqlClient.SqlConnection pCnn = default, SqlClient.SqlTransaction pTrn = default, [Optional, DefaultParameterValue("OK")] ref string pstr_res)
//        {

//            var lsql = new CVM.Sql();
//            var lTab = new DataTable();

//            {
//                ref var withBlock = ref lsql;
//                withBlock.SQL = pstr_sql;
//                pstr_res = withBlock.EjecutarDT(lTab, pCnn, pTrn);
//            }
//            lsql = default;

//            return pstr_res == "OK" && lTab.Rows.Count > 0;

//        }

//        public static string DT(ref DataTable pTab, string pstr_tabla, string pstr_where, string pstr_campos = "*", string pstr_OrderBy = "", bool pbln_PuedeHaberMasde1 = true, bool pbln_PuedeNoExistir = true, SqlClient.SqlConnection pCnn = default, SqlClient.SqlTransaction pTrn = default)
//        {

//            var lsql = new CVM.Sql();
//            string lstr_res;

//            {
//                ref var withBlock = ref lsql;
//                withBlock.q_SELECT(pstr_campos);
//                withBlock.q_FROM(pstr_tabla);
//                withBlock.w_WHERE(pstr_where);
//                withBlock.q_ORDERBy(pstr_OrderBy);
//                lstr_res = withBlock.EjecutarDT(pTab, pCnn, pTrn);
//            }
//            lsql = default;

//            if (lstr_res == "OK")
//            {
//                if (!pbln_PuedeNoExistir & pTab.Rows.Count == 0)
//                {
//                    lstr_res = "No existen Registros";
//                }
//                else if (!pbln_PuedeHaberMasde1 & pTab.Rows.Count > 1)
//                {
//                    lstr_res = "Existen mas de 1 Registro";
//                }
//            }

//            return lstr_res;

//        }

//        public static string DR(ref DataRow pReg, string pstr_tabla, string pstr_where, string pstr_campos = "*", string pstr_OrderBy = "", bool pbln_PuedeHaberMasde1 = true, bool pbln_PuedeNoExistir = true, SqlClient.SqlConnection pCnn = default, SqlClient.SqlTransaction pTrn = default)
//        {

//            // Dim lsql As New CVM.Sql
//            var lTab = new DataTable();
//            string lstr_res = "OK";

//            // With lsql
//            // .q_SELECT(pstr_campos)
//            // .q_FROM(pstr_tabla)
//            // .w_WHERE(pstr_where)
//            // .q_ORDERBy(pstr_OrderBy)
//            // lstr_res = .EjecutarDT(lTab, pCnn, pTrn)
//            // End With
//            // lsql = Nothing

//            // If lstr_res = "OK" Then
//            // If Not pbln_PuedeNoExistir And lTab.Rows.Count = 0 Then
//            // lstr_res = "No existen Registros"
//            // ElseIf Not pbln_PuedeHaberMasde1 And lTab.Rows.Count > 1 Then
//            // lstr_res = "Existen mas de 1 Registro"
//            // End If
//            // End If

//            lstr_res = CVM.Sql.DT(lTab, pstr_tabla, pstr_where, pstr_campos, pstr_OrderBy, pbln_PuedeHaberMasde1, pbln_PuedeNoExistir, pCnn, pTrn);

//            if (lstr_res == "OK" & lTab.Rows.Count > 0)
//            {
//                pReg = lTab.Rows(0);
//            }
//            else
//            {
//                pReg = default;
//            }

//            return lstr_res;

//        }

//        public static string DR_Campo(ref string pstr_res, string pstr_tabla, string pstr_where, string pstr_campo, string pstr_OrderBy = "", bool pbln_PuedeHaberMasde1 = true, bool pbln_PuedeNoExistir = true, SqlClient.SqlConnection pCnn = default, SqlClient.SqlTransaction pTrn = default)
//        {

//            DataRow lreg = default;
//            string lstr_res = "";

//            pstr_res = CVM.Sql.DR(lreg, pstr_tabla, pstr_where, pstr_campo, pstr_OrderBy, pbln_PuedeHaberMasde1, pbln_PuedeNoExistir, pCnn, pTrn);

//            if (pstr_res == "OK")
//            {
//                lstr_res = CVM.DR.Str(lreg, pstr_campo);
//            }

//            lreg = default;

//            return lstr_res;

//        }

//        public static string TablaContador(ref long plng_ID, string pstr_tabla, string pstr_id, string pstr_where = "", long plng_desde = 0L, long plng_hasta = 0L, SqlClient.SqlConnection pCnn = default, SqlClient.SqlTransaction pTrn = default)
//        {

//            var lsql = new CVM.Sql();
//            var lTab = new DataTable();
//            string lstr_res;

//            plng_ID = 0L;

//            {
//                ref var withBlock = ref lsql;
//                withBlock.q_SELECT("TOP 1 " + pstr_id);
//                withBlock.q_FROM(pstr_tabla);

//                withBlock.w_WHERE(pstr_where, !string.IsNullOrEmpty(pstr_where));

//                withBlock.w_AND(withBlock.SqlCondicion(pstr_id, plng_desde, "N", ">="), plng_desde > 0L);
//                withBlock.w_AND(withBlock.SqlCondicion(pstr_id, plng_hasta, "N", "<="), plng_hasta > 0L);

//                withBlock.q_ORDERBy(pstr_id + " DESC");

//                lstr_res = withBlock.EjecutarDT(lTab, pCnn, pTrn);

//            }
//            lsql = default;

//            if (lstr_res == "OK")
//            {
//                if (lTab.Rows.Count > 0)
//                {
//                    plng_ID = lTab.Rows(0)(pstr_id) + 1;
//                }
//                else
//                {
//                    plng_ID = Conversions.ToLong(Interaction.IIf(plng_desde == 0L, 1, plng_desde));
//                }
//            }

//            return lstr_res;

//        }

//        public static string EjecutarLstSQL(List<string> plst_sql, [Optional, DefaultParameterValue(default)] ref SqlClient.SqlConnection pCnn, [Optional, DefaultParameterValue(default)] ref SqlClient.SqlTransaction pTrn)
//        {

//            bool lbln_abrir = pCnn is null;
//            string lstr_res = "OK";

//            // -- --------------------------------------------------
//            // -- apertura de base de datos
//            // -- --------------------------------------------------
//            if (lbln_abrir)
//            {
//                lstr_res = CVM.BdSql.IniciarOperacion(pCnn, pTrn, true, true);
//            }

//            // -- --------------------------------------------------
//            // -- Ejecuto SQLs
//            // -- --------------------------------------------------
//            if (lstr_res == "OK")
//            {
//                foreach (string lstr_sql in plst_sql)
//                {
//                    lstr_res = CVM.Sql.EjecutarSQL(lstr_sql, pCnn, pTrn);
//                    if (lstr_res != "OK")
//                        break;
//                }
//            }

//            // -- --------------------------------------------------
//            // -- cierro
//            // -- --------------------------------------------------
//            if (lbln_abrir)
//            {
//                lstr_res = CVM.BdSql.CerrarOperacion(pCnn, pTrn, true, true, lstr_res == "OK", lstr_res);
//            }

//            return lstr_res;

//        }

//        public static string EjecutarSQL(string pstr_sql, SqlConnection pCnn = null, SqlTransaction pTrn = null, bool pbln_BeginTrans = false, [Optional, DefaultParameterValue(0L)] ref long plng_regs, bool pbln_identity = false, [Optional, DefaultParameterValue(-1)] ref long plng_key, string pstr_BD = "")
//        {

//            SqlCommand lcmd;
//            var lbln_base_abrir = default(bool);
//            string lstr_res = "OK";

//            if (pbln_identity)
//                pstr_sql += "; Select Scope_Identity()";

//            lstr_res = CVM.BdSql.IniciarOperacion(pCnn, pTrn, pbln_BeginTrans, lbln_base_abrir, pstr_BD);

//            try
//            {
//                if (pTrn is null)
//                {
//                    lcmd = new SqlCommand(pstr_sql, pCnn);
//                }
//                else
//                {
//                    lcmd = new SqlCommand(pstr_sql, pCnn, pTrn);
//                }

//                // Verificar. Llamar a uno u otro en caso de insert o update
//                lcmd.CommandType = CommandType.Text;

//                if (pbln_identity)
//                {
//                    plng_key = Conversions.ToLong(lcmd.ExecuteScalar());
//                }
//                else
//                {
//                    plng_regs = lcmd.ExecuteNonQuery();
//                }
//            }

//            catch (SqlException ex)
//            {
//                lstr_res = ex.Message;

//            }

//            // -- --------------------------------------------------
//            // -- cierro
//            // -- --------------------------------------------------
//            lstr_res = CVM.BdSql.CerrarOperacion(pCnn, pTrn, lbln_base_abrir, pbln_BeginTrans, lstr_res == "OK", lstr_res);


//            return lstr_res;

//        }

//        public static string EjecutarDTConCnnStr(ref DataTable pTab, string pstr_sql, string pstr_CnnStr)
//        {

//            SqlClient.SqlConnection lCnn = default;
//            SqlDataAdapter lAda;
//            string lstr_res = "OK";
//            string lstr_res_cerrar = "";

//            lstr_res = CVM.BdSql.AbrirConCnnStr(lCnn, pstr_CnnStr, default, true);

//            if (lstr_res == "OK")
//            {
//                try
//                {
//                    lAda = new SqlDataAdapter(pstr_sql, lCnn);
//                    lAda.SelectCommand.CommandType = CommandType.Text;

//                    pTab = new DataTable();
//                    lAda.Fill(pTab);
//                }

//                catch (Exception ex)
//                {
//                    lstr_res = ex.Message;

//                }

//                if (lstr_res == "OK")
//                {
//                    lstr_res_cerrar = CVM.BdSql.CerrarCnn(lCnn);
//                    lCnn = default;
//                    lstr_res = lstr_res_cerrar;
//                }
//            }

//            return lstr_res;

//        }

//        public static string EjecutarConCnnStr(string pstr_sql, string pstr_CnnStr, int pint_CommandTimeOut = 0)
//        {

//            SqlClient.SqlConnection lCnn = default;
//            SqlCommand lCmd = null;
//            string lstr_res = "OK";
//            string lstr = "";

//            lstr_res = CVM.BdSql.AbrirConCnnStr(lCnn, pstr_CnnStr, default, true);

//            if (lstr_res == "OK")
//            {
//                try
//                {
//                    lCmd = new SqlCommand(pstr_sql, lCnn);
//                    lCmd.CommandType = CommandType.Text;
//                    if (pint_CommandTimeOut > 0)
//                        lCmd.CommandTimeout = pint_CommandTimeOut;
//                    lCmd.ExecuteNonQuery();
//                }

//                catch (SqlException ex)
//                {
//                    lstr_res = ex.Message;
//                }

//                lstr = CVM.BdSql.CerrarCnn(lCnn);
//                if (lstr_res == "OK")
//                    lstr_res = lstr;
//            }

//            return lstr_res;

//        }

//        public static string AgregarCondicion(string pstr_where, string pstr_campo, object pobj_valor, string pstr_tipo_dato = "T", string pstr_comparativo = "=", string pstr_operador = "AND", bool pbln_condicion = true)
//        {

//            string lstr_where = pstr_where;

//            if (pbln_condicion)
//            {
//                if (!string.IsNullOrEmpty(lstr_where))
//                    lstr_where = " " + pstr_operador;

//                lstr_where += " " + pstr_campo + " " + pstr_comparativo + " " + CVM.Otros.ValorCondicionSQL(pobj_valor.ToString(), pstr_tipo_dato, CVM.Cons.TP_Base.SQL);
//            }

//            return lstr_where;

//        }

//        public static string LstSqlJerarquia(ref List<string> plst_sql, string pstr_tabla, string pstr_campo_id, string pstr_campo_id_padre, int plng_ID_padre, string pstr_ds_jerarquia, int pint_no_nivel, ref int pint_no_orden_carga, string pstr_campo_jerarquia = "ds_jerarquia", string pstr_campo_nivel = "no_nivel", string pstr_campo_orden = "no_orden", string pstr_campo_orden_carga = "no_orden_carga")
//        {

//            CVM.Sql lsql;
//            string lstr_sql_basico = "";
//            string lstr_sql = "";
//            var lTab = new DataTable();
//            int lint_no_orden = 0;
//            string lstr_ds_jerarquia = "";
//            string lstr_res = "OK";

//            // -- ------------------------------
//            // -- 
//            // -- ------------------------------
//            lstr_sql_basico = "UPDATE [tabla] SET [jerarquia], [nivel] [orden_carga]";
//            lstr_sql_basico = lstr_sql_basico.Replace("[tabla]", pstr_tabla);
//            lstr_sql_basico = lstr_sql_basico.Replace("[jerarquia]", pstr_campo_jerarquia);
//            lstr_sql_basico = lstr_sql_basico.Replace("[nivel]", pstr_campo_nivel);
//            lstr_sql_basico = lstr_sql_basico.Replace("[orden_carga]", Conversions.ToString(Interaction.IIf(string.IsNullOrEmpty(pstr_campo_orden_carga), "", ", " + pstr_campo_orden_carga)));

//            // -- ------------------------------
//            // -- 
//            // -- ------------------------------
//            if (plst_sql.Count == 0)
//            {
//                lstr_sql = lstr_sql_basico;
//                lstr_sql = lstr_sql.Replace(pstr_campo_jerarquia, pstr_campo_jerarquia + "=''");
//                lstr_sql = lstr_sql.Replace(pstr_campo_nivel, pstr_campo_nivel + "=0");
//                if (!string.IsNullOrEmpty(pstr_campo_orden_carga))
//                    lstr_sql = lstr_sql.Replace(pstr_campo_orden_carga, pstr_campo_orden_carga + "=0");
//                plst_sql.Add(lstr_sql);
//            }

//            // -- ------------------------------
//            // -- 
//            // -- ------------------------------
//            if (!string.IsNullOrEmpty(pstr_ds_jerarquia))
//                pstr_ds_jerarquia += ".";
//            pint_no_nivel += 1;

//            lsql = new CVM.Sql();
//            {
//                ref var withBlock = ref lsql;
//                withBlock.q_SELECT(pstr_campo_id);
//                withBlock.q_SELECT(pstr_campo_orden);
//                withBlock.q_SELECT("SELECT COUNT(*) FROM [tabla] X2 WHERE x2.[campo_id_padre] = x1.[campo_id]", "qt_hijos");
//                withBlock.q_FROM(pstr_tabla, "X1");
//                withBlock.w_WHERE(withBlock.SqlCondicionN("ISNULL(x1.[campo_id_padre], 0)", plng_ID_padre));
//                withBlock.q_ORDERBy(pstr_campo_orden);

//                withBlock.q_REPLACE("[tabla]", pstr_tabla);
//                withBlock.q_REPLACE("[campo_id]", pstr_campo_id);
//                withBlock.q_REPLACE("[campo_id_padre]", pstr_campo_id_padre);

//                lstr_res = withBlock.EjecutarDT(lTab);
//            }
//            lsql = default;

//            // -- ------------------------------
//            // -- 
//            // -- ------------------------------
//            if (lstr_res == "OK")
//            {

//                foreach (DataRow lReg in lTab.Rows)
//                {
//                    lint_no_orden = Conversions.ToInteger(Interaction.IIf(lReg(pstr_campo_orden) > lint_no_orden, lReg(pstr_campo_orden), lint_no_orden + 1));
//                    lstr_ds_jerarquia = pstr_ds_jerarquia + Strings.Format(lint_no_orden, "00");
//                    pint_no_orden_carga += 1;

//                    // UPDATE
//                    lstr_sql = lstr_sql_basico;
//                    lstr_sql = lstr_sql.Replace(pstr_campo_jerarquia, pstr_campo_jerarquia + "='" + lstr_ds_jerarquia + "'");
//                    lstr_sql = lstr_sql.Replace(pstr_campo_nivel, pstr_campo_nivel + "=" + pint_no_nivel.ToString());
//                    if (!string.IsNullOrEmpty(pstr_campo_orden_carga))
//                        lstr_sql = lstr_sql.Replace(pstr_campo_orden_carga, pstr_campo_orden_carga + "=" + pint_no_orden_carga.ToString());
//                    lstr_sql += " WHERE " + pstr_campo_id + " = " + lReg(pstr_campo_id).ToString;

//                    plst_sql.Add(lstr_sql);

//                    if (CVM.DR.Int(lReg, "QT_HIJOS") > 0)
//                    {
//                        lstr_res = CVM.Sql.LstSqlJerarquia(plst_sql, pstr_tabla, pstr_campo_id, pstr_campo_id_padre, lReg(pstr_campo_id), lstr_ds_jerarquia, pint_no_nivel, pint_no_orden_carga, pstr_campo_jerarquia, pstr_campo_nivel, pstr_campo_orden, pstr_campo_orden_carga);

//                        if (lstr_res != "OK")
//                            break;
//                    }
//                }
//            }

//            return lstr_res;

//        }

//        #endregion
//        // -- ------------------------------------------------------------------------------------------
//        #region Sets y Gets
//        // -- ------------------------------------------------------------------------------------------

//        public int Operacion
//        {
//            get
//            {
//                int OperacionRet = default;
//                OperacionRet = mint_operacion;
//                return OperacionRet;
//            }
//            set
//            {
//                mint_operacion = value;
//            }
//        }

//        public int TipoBase
//        {
//            get
//            {
//                int TipoBaseRet = default;
//                TipoBaseRet = mint_id_tipo_base;
//                return TipoBaseRet;
//            }
//            set
//            {
//                mint_id_tipo_base = value;
//            }
//        }

//        public string Tabla
//        {
//            get
//            {
//                string TablaRet = default;
//                TablaRet = mstr_cd_tabla;
//                return TablaRet;
//            }
//            set
//            {
//                mstr_cd_tabla = value;
//            }
//        }

//        public bool Select_DISTINCT
//        {
//            get
//            {
//                bool Select_DISTINCTRet = default;
//                Select_DISTINCTRet = mbln_lg_distinct;
//                return Select_DISTINCTRet;
//            }
//            set
//            {
//                mbln_lg_distinct = value;
//            }
//        }

//        public int Select_TOP
//        {
//            get
//            {
//                int Select_TOPRet = default;
//                Select_TOPRet = mint_top;
//                return Select_TOPRet;
//            }
//            set
//            {
//                mint_top = value;
//            }
//        }

//        public bool OrderIgualGroup
//        {
//            get
//            {
//                bool OrderIgualGroupRet = default;
//                OrderIgualGroupRet = mbln_OrderIgualGroup;
//                return OrderIgualGroupRet;
//            }
//            set
//            {
//                mbln_OrderIgualGroup = value;
//            }
//        }

//        public bool GroupIgualSelect
//        {
//            get
//            {
//                bool GroupIgualSelectRet = default;
//                GroupIgualSelectRet = mbln_GroupIgualSelect;
//                return GroupIgualSelectRet;
//            }
//            set
//            {
//                mbln_GroupIgualSelect = value;
//            }
//        }

//        public string SQL
//        {
//            get
//            {
//                string SQLRet = default;
//                SQLRet = mstr_sql;
//                return SQLRet;
//            }
//            set
//            {
//                mstr_sql = value;
//            }
//        }

//        public string LlamadoDesde
//        {
//            get
//            {
//                string LlamadoDesdeRet = default;
//                LlamadoDesdeRet = "";       // mobj_MiEjecucion.objeto & "|" & mobj_MiEjecucion.metodo & "|" & mobj_MiEjecucion.submetodo
//                return LlamadoDesdeRet;

//            }
//            set
//            {
//                mstr_LlamadoDesde = value;
//            }
//        }

//        public List<string> lst_SQL
//        {
//            get
//            {
//                List<string> lst_SQLRet = default;
//                lst_SQLRet = mlst_sql;
//                return lst_SQLRet;
//            }
//        }

//        #endregion

//    }
//}
