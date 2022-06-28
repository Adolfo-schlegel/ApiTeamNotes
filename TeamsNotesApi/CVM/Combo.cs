//using System.Data;

//namespace TeamsNotesApi.CVM_SQL_Utilities
//{
//    public class Combo
//    {

//        public static void Iniciar(ref data pcbo_lst, string pstr_cd_tabla, string pstr_cd_campo_mostrar, string pstr_cd_campo_id, string pstr_cd_campo_cd = "", string pstr_cd_campo_orden = "", string pstr_where = "", bool pbln_lg_todos = true, bool pbln_lg_limpiar = true, int pint_top_nro = 0, bool pbln_Distinct = false)
//        {
//            DataTable lTab = new DataTable();
//            DataRow lReg;

//            CVM.Sql lsql = new CVM.Sql();
//            string lstr_res = "";

//            if (pbln_lg_limpiar)
//                pcbo_lst.Items.Clear();

//            pcbo_lst.DataTextField = "DS";
//            pcbo_lst.DataValueField = "ID";

//            lsql = new CVM.Sql();
//            {
//                var withBlock = lsql;
//                withBlock.Select_DISTINCT = pbln_Distinct;
//                withBlock.Select_TOP = pint_top_nro;

//                withBlock.q_SELECT(pstr_cd_campo_mostrar, "DS");
//                withBlock.q_SELECT(pstr_cd_campo_id, "ID");

//                withBlock.q_FROM(pstr_cd_tabla);

//                withBlock.w_WHERE(pstr_where, (pstr_where != ""));

//                withBlock.q_ORDERBy(pstr_cd_campo_orden, (pstr_cd_campo_orden != ""));

//                lstr_res = withBlock.EjecutarDT(lTab);
//            }
//            lsql = null/* TODO Change to default(_) if this is not a reference type */;

//            if (lstr_res == "OK" && lTab.Rows.Count > 0)
//            {
//                if (pbln_lg_todos)
//                {
//                    lReg = lTab.NewRow();
//                    lReg("DS") = "[Todo]";
//                    lReg("ID") = "0";
//                    lTab.Rows.InsertAt(lReg, 0);
//                }

//                pcbo_lst.DataSource = lTab;
//                pcbo_lst.DataBind();
//            }

//            lTab = null/* TODO Change to default(_) if this is not a reference type */;
//        }
//    }
//}
