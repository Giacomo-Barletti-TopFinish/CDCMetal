using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.Data
{
    public class CDCMetalAdapter : CDCMetalAdapterBase
    {
        public CDCMetalAdapter(System.Data.IDbConnection connection, IDbTransaction transaction) :
            base(connection, transaction)
        { }

        public void LeggiUtente(CDCDS ds, string User)
        {
            string select = @"SELECT * FROM GRUPPO.USR_USER WHERE UIDUSER= $P<UTENTE>";
            ParamSet ps = new ParamSet();

            ps.AddParam("UTENTE", DbType.String, User);
            using (DbDataAdapter da = BuildDataAdapter(select, ps))
            {
                da.Fill(ds.USR_USER);
            }
        }

        public void FillCDC_DETTAGLIO(List<decimal> IDPRENOTAZIONE, CDCDS ds)
        {
            string selezione = ConvertToStringForInCondition(IDPRENOTAZIONE);
            string select = @"SELECT * FROM CDC_DETTAGLIO WHERE IDPRENOTAZIONE IN ({0})";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DETTAGLIO);
            }
        }
        public void FillCDC_DETTAGLIO1(List<decimal> IDPRENOTAZIONE, CDCDS ds)
        {
            string selezione = ConvertToStringForInCondition(IDPRENOTAZIONE);
            string select = @"SELECT * FROM CDC_DETTAGLIO WHERE IDPRENOTAZIONE IN ({0})";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DETTAGLIO1);
            }
        }
        public void UpdateTable(string tablename, CDCDS ds)
        {
            string tablenameDB = tablename;

            if (tablename.ToUpper() == "CDC_DETTAGLIO_REL")
                tablenameDB = "CDC_DETTAGLIO";

            string query = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM {0}", tablenameDB);

            using (DbDataAdapter a = BuildDataAdapter(query))
            {
                try
                {
                    a.ContinueUpdateOnError = false;
                    DataTable dt = ds.Tables[tablename];
                    DbCommandBuilder cmd = BuildCommandBuilder(a);
                    a.AcceptChangesDuringFill = true;
                    a.UpdateCommand = cmd.GetUpdateCommand();
                    a.DeleteCommand = cmd.GetDeleteCommand();
                    a.InsertCommand = cmd.GetInsertCommand();

                    a.Update(dt);
                }
                catch (DBConcurrencyException ex)
                {

                }
                catch
                {
                    throw;
                }
            }
        }

        //excel
        public void Update_EXCEL_Table(CDCDS ds)
        {
            string query = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM $T<{0}>", ds.CDC_EXCEL.TableName);

            using (DbDataAdapter a = BuildDataAdapter(query))
            {
                try
                {
                    InstallRowUpdatedHandler(a, Update_EXCEL_ElementHandler);
                    a.ContinueUpdateOnError = false;
                    DataTable dt = ds.CDC_EXCEL;
                    DbCommandBuilder cmd = BuildCommandBuilder(a);
                    a.UpdateCommand = cmd.GetUpdateCommand();
                    a.DeleteCommand = cmd.GetDeleteCommand();
                    a.InsertCommand = cmd.GetInsertCommand();
                    a.Update(dt);

                }
                catch (DBConcurrencyException ex)
                {

                }
                catch
                {
                    throw;
                }
            }
        }

        private void Update_EXCEL_ElementHandler(object sender, RowUpdatedEventArgs e)
        {
            if ((e.Status == UpdateStatus.Continue) && (e.StatementType == StatementType.Insert))
            {
                //ArticleDS.SPELEMENTIRow row = (ArticleDS.SPELEMENTIRow)e.Row;
                CDCDS.CDC_EXCELRow row = (CDCDS.CDC_EXCELRow)e.Row;

                //ArticleDS.SPELEMENTIDataTable dt = row.Table as ArticleDS.SPELEMENTIDataTable;
                CDCDS.CDC_EXCELDataTable dt = row.Table as CDCDS.CDC_EXCELDataTable;

                //bool isIdentityReadOnly = dt.ID_SPELEMColumn.ReadOnly;
                bool isIdentityReadOnly = dt.IDEXCELColumn.ReadOnly;

                //dt.ID_SPELEMColumn.ReadOnly = false;
                dt.IDEXCELColumn.ReadOnly = false;

                try
                {
                    //row.ID_SPELEM = (long)RetrievePostUpdateID<decimal>(e.Command, row);
                    row.IDEXCEL = (long)RetrievePostUpdateID<decimal>(e.Command, row);
                }
                finally
                {
                    //dt.ID_SPELEMColumn.ReadOnly = isIdentityReadOnly;
                    dt.IDEXCELColumn.ReadOnly = isIdentityReadOnly;
                }
            }

        }
        //conformita


        public List<DataCollaudo> LeggiDateRiferimento()
        {
            List<DataCollaudo> date = new List<DataCollaudo>();
            //string select = @"SELECT DISTINCT DATARIFERIMENTO,AZIENDA FROM CDC_EXCEL ORDER BY DATARIFERIMENTO DESC";
            string select = @"SELECT DISTINCT E.DATARIFERIMENTO, B.CODICE FROM CDC_EXCEL E
                LEFT JOIN CDC_DETTAGLIO D  ON E.IDEXCEL = D.IDEXCEL
                LEFT JOIN CDC_BRANDS B ON D.IDBRAND = B.IDBRAND
                WHERE D.IDBRAND IS NOT NULL
                ORDER BY E.DATARIFERIMENTO DESC";

            using (DbCommand cmd = BuildCommand(select))
            {
                DbDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    DateTime dt = (DateTime)dataReader.GetValue(0);
                    string Brand = (string)dataReader.GetValue(1);
                    date.Add(new DataCollaudo(Brand, dt));
                }
                dataReader.Close();
            }

            return date;
        }

        public List<DataCollaudoSingle> LeggiDateRiferimentoSingle()
        {
            List<DataCollaudoSingle> date = new List<DataCollaudoSingle>();
            string select = @"SELECT DISTINCT E.DATARIFERIMENTO FROM CDC_EXCEL E
                LEFT JOIN CDC_DETTAGLIO D  ON E.IDEXCEL = D.IDEXCEL
                LEFT JOIN CDC_BRANDS B ON D.IDBRAND = B.IDBRAND
                WHERE D.IDBRAND IS NOT NULL
                ORDER BY E.DATARIFERIMENTO DESC";

            using (DbCommand cmd = BuildCommand(select))
            {
                DbDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    DateTime dt = (DateTime)dataReader.GetValue(0);
                    date.Add(new DataCollaudoSingle(dt));
                }
                dataReader.Close();
            }
            return date;
        }

        public List<BrandCollaudoSingle> LeggiBrandRiferimentoSingle(string datacollaudo)
        {
            List<BrandCollaudoSingle> brand = new List<BrandCollaudoSingle>();
            string select = @"SELECT DISTINCT B.CODICE FROM CDC_DETTAGLIO D 
                LEFT JOIN CDC_BRANDS B ON D.IDBRAND = B.IDBRAND
                WHERE D.IDBRAND IS NOT NULL AND D.DATACOLLAUDO = @DATACOLLAUDO
                ORDER BY B.CODICE";

            using (DbCommand cmd = BuildCommand(select))
            {

                DbParameter parm = cmd.CreateParameter();
                parm.ParameterName = "DATACOLLAUDO";
                parm.Value = datacollaudo;
                parm.DbType = DbType.String;
                cmd.Parameters.Add(parm);

                DbDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    brand.Add(new BrandCollaudoSingle(dataReader.GetValue(0).ToString()));
                }
                dataReader.Close();
            }
            return brand;

        }

        public List<ColoreSingle> LeggiColoreRiferimentoSingle()
        {
            List<ColoreSingle> colore = new List<ColoreSingle>();

            string select = @"SELECT DISTINCT COLORE FROM CDC_DETTAGLIO 
                                UNION 
                                select distinct colorecomponente as COLORE from cdc_articoli
                                order by COLORE";

            using (DbCommand cmd = BuildCommand(select))
            {


                DbDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    colore.Add(new ColoreSingle(dataReader.GetValue(0).ToString()));
                }
                dataReader.Close();
            }
            return colore;

        }


        public void FillCDC_DETTAGLIO(CDCDS ds, List<decimal> IDEXCEL)
        {
            string selezione = ConvertToStringForInCondition(IDEXCEL);
            string select = @"SELECT * FROM CDC_DETTAGLIO WHERE IDEXCEL IN ({0}) ORDER BY PREFISSO, PARTE, COLORE, MISURA,COMMESSAORDINE";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DETTAGLIO);
            }
        }

        public void FillCDC_DETTAGLIO_DATA_BRAND(CDCDS ds, string DATARIFERIMENTO, string Brand)
        {
            string select = @"SELECT D.* FROM CDC_DETTAGLIO D 
                LEFT JOIN CDC_BRANDS B on D.IDBRAND = B.IDBRAND
                WHERE D.DATACOLLAUDO = $P<DATARIFERIMENTO> AND B.CODICE = $P<BRAND>
                ORDER BY PREFISSO, PARTE, COLORE, MISURA, COMMESSAORDINE";

            ParamSet ps = new ParamSet();
            ps.AddParam("DATARIFERIMENTO", DbType.String, DATARIFERIMENTO);
            ps.AddParam("BRAND", DbType.String, Brand);

            using (DbDataAdapter da = BuildDataAdapter(select, ps))
            {
                da.Fill(ds.CDC_DETTAGLIO);
            }


        }
        public void FillCDC_EXCEL(CDCDS ds, DateTime DATARIFERIMENTO, string Azienda)
        {
            string select = @"SELECT * FROM CDC_EXCEL WHERE DATARIFERIMENTO = $P<DATARIFERIMENTO> AND AZIENDA = $P<AZIENDA>";

            ParamSet ps = new ParamSet();
            ps.AddParam("DATARIFERIMENTO", DbType.DateTime, DATARIFERIMENTO);
            ps.AddParam("AZIENDA", DbType.String, Azienda);

            using (DbDataAdapter da = BuildDataAdapter(select, ps))
            {
                da.Fill(ds.CDC_EXCEL);
            }
        }

        public void FillCDC_CERTIFICATIPIOMBO_NonAssegnati(CDCDS ds)
        {
            string select = @"SELECT * FROM CDC_CERTIFICATIPIOMBO 
                            WHERE IDCERTIFICATIPIOMBO NOT IN (SELECT DISTINCT IDCERTIFICATIPIOMBO FROM CDC_ASSOCIAZIONEPIOMBO ) 
                            ORDER BY DATACERTIFICATO ";

            using (DbDataAdapter da = BuildDataAdapter(select))
            {
                da.Fill(ds.CDC_CERTIFICATIPIOMBO);
            }
        }

        public void FillCDC_CONFORMITA(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_CONFORMITA WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_CONFORMITA);
            }
        }

        public void FillCDC_ASSOCIAZIONEPIOMBO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_ASSOCIAZIONEPIOMBO WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_ASSOCIAZIONEPIOMBO);
            }
        }
        public void FillCDC_DETTAGLIO_CON_DESCRIZIONE(CDCDS ds, List<decimal> IDEXCEL)
        {
            string selezione = ConvertToStringForInCondition(IDEXCEL);
            string select = @"SELECT cdc.*, nvl(( select NVL(desmagazz,'') from gruppo.magazz ma where rownum = 1 and  ma.modello = ma.modellobase and ma.modello like cdc.prefisso||'-'||cdc.parte||'-'||cdc.colore||'%'),' ') as descrizione
                                FROM CDC_DETTAGLIO cdc
                                WHERE IDEXCEL IN ({0}) ORDER BY PREFISSO, PARTE, COLORE, MISURA,COMMESSAORDINE";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DETTAGLIO1);
            }
        }
        public void FillCDC_DETTAGLIO_CON_DESCRIZIONE_STR(CDCDS ds, string datacollaudo, string brand)
        {
            //string selezione = ConvertToStringForInCondition(IDEXCEL);
            string select = @"SELECT cdc.*, nvl(( select NVL(desmagazz,'') from gruppo.magazz ma where rownum = 1 and  ma.modello = ma.modellobase and ma.modello like cdc.prefisso||'-'||cdc.parte||'-'||cdc.colore||'%'),' ') as descrizione
                                FROM CDC_DETTAGLIO cdc
                                LEFT JOIN CDC_BRANDS B ON B.IDBRAND = cdc.IDBRAND
                                WHERE DATACOLLAUDO = '{0}' AND B.CODICE = '{1}' ORDER BY PREFISSO, PARTE, COLORE, MISURA,COMMESSAORDINE";

            string query = string.Format(select, datacollaudo, brand);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DETTAGLIO1);
            }
        }

        public void CDC_PDF(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_PDF WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_PDF);
            }
        }

        public void FillCDC_CONFORMITA_DETTAGLIO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT DISTINCT DET.* FROM CDC_CONFORMITA_DETTAGLIO DET
                                INNER JOIN CDC_DETTAGLIO CD ON CD.PREFISSO = DET.PREFISSO AND CD.PARTE = DET.PARTE AND CD.COLORE = DET.COLORE 
                                WHERE CD.IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_CONFORMITA_DETTAGLIO);
            }
        }

        public void FillCDC_ETICHETTE_DETTAGLIO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT DET.* FROM CDC_ETICHETTE_DETTAGLIO DET
                                INNER JOIN CDC_DETTAGLIO CD ON CD.PREFISSO = DET.PREFISSO AND CD.PARTE = DET.PARTE AND CD.COLORE = DET.COLORE 
                                WHERE CD.IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_ETICHETTE_DETTAGLIO);
            }
        }

        public void FillCDC_DIMEMSIONI(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_DIMEMSIONI WHERE IDDETTAGLIO IN ({0}) ORDER BY RIFERIMENTO";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DIMEMSIONI);
            }
        }

        public void FillCDC_DIMEMSIONI_MISURE(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT DISTINCT DET.* FROM CDC_DIMEMSIONI_MISURE DET
                                INNER JOIN CDC_DETTAGLIO CD ON CD.PARTE = DET.PARTE 
                                WHERE CD.IDDETTAGLIO IN ({0}) ORDER BY DET.RIFERIMENTO";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_DIMEMSIONI_MISURE);
            }
        }

        public void FillCDC_ANTIALLERGICO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_ANTIALLERGICO WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_ANTIALLERGICO);
            }
        }

        public void FillCDC_VERNICICOPRENTI(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_VERNICICOPRENTI WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_VERNICICOPRENTI);
            }
        }

        public void FillCDC_TENUTACIDONITRICO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_TENUTACIDONITRICO WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_TENUTACIDONITRICO);
            }
        }

        public void FillCDC_COLORE(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_COLORE WHERE IDDETTAGLIO IN ({0}) ";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_COLORE);
            }
        }

        public void FillCDC_BRANDS(CDCDS ds)
        {
            string select = @"SELECT * FROM CDC_BRANDS ";

            using (DbDataAdapter da = BuildDataAdapter(select))
            {
                da.Fill(ds.CDC_BRANDS);
            }
        }
        public void FillCDC_ENTI(CDCDS ds)
        {
            string select = @"SELECT * FROM CDC_ENTI ";

            using (DbDataAdapter da = BuildDataAdapter(select))
            {
                da.Fill(ds.CDC_ENTI);
            }
        }

        public void FillCDC_ARTICOLI(CDCDS ds)
        {
            string select = @"SELECT * FROM CDC_ARTICOLI WHERE DELETED IS NULL OR DELETED <>'S'";

            using (DbDataAdapter da = BuildDataAdapter(select))
            {
                da.Fill(ds.CDC_ARTICOLI);
            }
        }
        public void FillCDC_ARTICOLI_DIMENSIONI(CDCDS ds)
        {
            string select = @"SELECT * FROM CDC_ARTICOLI_DIMENSIONI WHERE DELETED IS NULL OR DELETED <>'S'";

            using (DbDataAdapter da = BuildDataAdapter(select))
            {
                da.Fill(ds.CDC_ARTICOLI_DIMENSIONI);
            }
        }

        public void FillCDC_ARTICOLI_SPESSORI(CDCDS ds)
        {
            string select = @"SELECT * FROM CDC_ARTICOLI_SPESSORI WHERE DELETED IS NULL OR DELETED <>'S'";

            using (DbDataAdapter da = BuildDataAdapter(select))
            {
                da.Fill(ds.CDC_ARTICOLI_SPESSORI);
            }
        }
        public void FillCDC_NUMEROCAMPIONI(CDCDS ds)
        {
            string select = @"SELECT * FROM CDC_NUMEROCAMPIONI ";

            using (DbDataAdapter da = BuildDataAdapter(select))
            {
                da.Fill(ds.CDC_NUMEROCAMPIONI);
            }
        }
        public void FillCDC_COLORI(CDCDS ds, string colore)
        {
            string select = "";

            if (colore != "")
            {
                select = @"SELECT * FROM CDC_COLORI WHERE COLORECOMPONENTE = $P<COLORECOMPONENTE> ORDER BY COLORECOMPONENTE, CODICE";
                ParamSet ps = new ParamSet();
                ps.AddParam("COLORECOMPONENTE", DbType.String, colore);
                using (DbDataAdapter da = BuildDataAdapter(select, ps))
                {
                    da.Fill(ds.CDC_COLORI);
                }
            }
            else
            {
                select = @"SELECT * FROM CDC_COLORI  ORDER BY COLORECOMPONENTE, CODICE";
                using (DbDataAdapter da = BuildDataAdapter(select))
                {
                    da.Fill(ds.CDC_COLORI);
                }
            }

          

        }
        public void FillCDC_CERTIFICATIPIOMBO(CDCDS ds)
        {

            string select = @"SELECT * FROM CDC_CERTIFICATIPIOMBO order by datacertificato desc, elemento";

            using (DbDataAdapter da = BuildDataAdapter(select))
            {
                da.Fill(ds.CDC_CERTIFICATIPIOMBO);
            }
        }

        public void FillCDC_GALVANICA(CDCDS ds, List<decimal> IDDETTAGLIO, string tipoCertificato)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT * FROM CDC_GALVANICA WHERE IDDETTAGLIO IN ({0}) AND CERTIFICATO = $P<CERTIFICATO> ";

            string query = string.Format(select, selezione);

            ParamSet ps = new ParamSet();
            ps.AddParam("CERTIFICATO", DbType.String, tipoCertificato);
            using (DbDataAdapter da = BuildDataAdapter(query, ps))
            {
                da.Fill(ds.CDC_GALVANICA);
            }
        }

        public void FillCDC_MISURE(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            string selezione = ConvertToStringForInCondition(IDDETTAGLIO);
            string select = @"SELECT DISTINCT DET.* FROM CDC_MISURE DET
                                INNER JOIN CDC_GALVANICA CD ON CD.IDGALVANICA = DET.IDGALVANICA 
                                WHERE CD.IDDETTAGLIO IN ({0}) ORDER BY DET.IDGALVANICA,DET.NMISURA,DET.NCOLONNA";

            string query = string.Format(select, selezione);

            using (DbDataAdapter da = BuildDataAdapter(query))
            {
                da.Fill(ds.CDC_MISURE);
            }
        }

        public void FillCDC_SPESSORE(CDCDS ds, string tipoCertificato)
        {
            string select = @"SELECT * FROM CDC_SPESSORE WHERE CERTIFICATO = $P<CERTIFICATO>";

            ParamSet ps = new ParamSet();
            ps.AddParam("CERTIFICATO", DbType.String, tipoCertificato);
            using (DbDataAdapter da = BuildDataAdapter(select, ps))
            {
                da.Fill(ds.CDC_SPESSORE);
            }
        }


        public void FillCDC_APPLICAZIONE(CDCDS ds, string tipoCertificato)
        {
            string select = @"SELECT * FROM CDC_APPLICAZIONE WHERE CERTIFICATO = $P<CERTIFICATO>";

            ParamSet ps = new ParamSet();
            ps.AddParam("CERTIFICATO", DbType.String, tipoCertificato);

            using (DbDataAdapter da = BuildDataAdapter(select, ps))
            {
                da.Fill(ds.CDC_APPLICAZIONE);
            }
        }

        public void FillCDC_MATERIAPRIMA(CDCDS ds)
        {
            string select = @"SELECT * FROM CDC_MATERIAPRIMA";

            using (DbDataAdapter da = BuildDataAdapter(select))
            {
                da.Fill(ds.CDC_MATERIAPRIMA);
            }
        }

        public long InsertCDC_CERTIFICATOPIOMBO(string ELEMENTO, string CODICE, string MATERIALE,
                                string LOTTO, decimal? LUNGHEZZA, decimal? LARGHEZZA, decimal? SPESSORE, string METODO, decimal PESOCAMPIONE, decimal MATRACCIOLO, decimal CONCENTRAZIONE,
                                decimal PBPPM, decimal CDPPM, string ESITO, DateTime DATACERTIFICATO, string UTENTE, DateTime DATAINSERIMENTO, string PATHFILE)
        {
            long ret;
            string query = @"INSERT INTO CDC_CERTIFICATIPIOMBO ( IDCERTIFICATIPIOMBO, ELEMENTO, CODICE, MATERIALE, 
                                LOTTO, LUNGHEZZA, LARGHEZZA, SPESSORE, METODO, PESOCAMPIONE, MATRACCIOLO, CONCENTRAZIONE, 
                                PBPPM, CDPPM, ESITO, DATACERTIFICATO, UTENTE, DATAINSERIMENTO, PATHFILE ) VALUES ( 
                                null, $P<ELEMENTO>, $P<CODICE>, $P<MATERIALE>, 
                                $P<LOTTO>, $P<LUNGHEZZA>, $P<LARGHEZZA>, $P<SPESSORE>, $P<METODO>, $P<PESOCAMPIONE>, $P<MATRACCIOLO>, $P<CONCENTRAZIONE>, 
                                $P<PBPPM>, $P<CDPPM>, $P<ESITO>, $P<DATACERTIFICATO>, $P<UTENTE>, $P<DATAINSERIMENTO>, $P<PATHFILE>)";

            ParamSet ps = new ParamSet();

            ps.AddParam("ELEMENTO", DbType.String, string.IsNullOrEmpty(ELEMENTO) ? (object)DBNull.Value : ELEMENTO);
            ps.AddParam("CODICE", DbType.String, CODICE);
            ps.AddParam("MATERIALE", DbType.String, MATERIALE);
            ps.AddParam("LOTTO", DbType.String, LOTTO);
            ps.AddParam("LUNGHEZZA", DbType.Decimal, LUNGHEZZA.HasValue ? LUNGHEZZA.Value : (object)DBNull.Value);
            ps.AddParam("LARGHEZZA", DbType.Decimal, LARGHEZZA.HasValue ? LARGHEZZA.Value : (object)DBNull.Value);
            ps.AddParam("SPESSORE", DbType.Decimal, SPESSORE.HasValue ? SPESSORE.Value : (object)DBNull.Value);
            ps.AddParam("METODO", DbType.String, METODO);
            ps.AddParam("PESOCAMPIONE", DbType.Decimal, PESOCAMPIONE);
            ps.AddParam("MATRACCIOLO", DbType.Decimal, MATRACCIOLO);
            ps.AddParam("CONCENTRAZIONE", DbType.Decimal, CONCENTRAZIONE);
            ps.AddParam("PBPPM", DbType.Decimal, PBPPM);
            ps.AddParam("CDPPM", DbType.Decimal, CDPPM);
            ps.AddParam("ESITO", DbType.String, ESITO);

            ps.AddParam("DATACERTIFICATO", DbType.DateTime, DATACERTIFICATO);
            ps.AddParam("UTENTE", DbType.String, UTENTE);
            ps.AddParam("DATAINSERIMENTO", DbType.DateTime, DATAINSERIMENTO);
            ps.AddParam("PATHFILE", DbType.String, PATHFILE);

            using (DbCommand cmd = BuildCommand(query, ps))
            {
                object o = cmd.ExecuteScalar();
                ret = Convert.ToInt64(o);
            }
            return ret;
        }
    }
}
