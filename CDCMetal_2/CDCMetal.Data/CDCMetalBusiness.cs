using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDCMetal.Data.Core;

namespace CDCMetal.Data
{
    public class CDCMetalBusiness : CDCMetalBusinessBase
    {
        public CDCMetalBusiness() : base() { }

        [DataContext]
        public void LeggiUtente(CDCDS ds, string User)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.LeggiUtente(ds, User);
        }

        [DataContext]
        public void FillCDC_DETTAGLIO(List<decimal> IDPRENOTAZIONE, CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_DETTAGLIO(IDPRENOTAZIONE, ds);
        }
        [DataContext]
        public void FillCDC_DETTAGLIO1(List<decimal> IDPRENOTAZIONE, CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_DETTAGLIO1(IDPRENOTAZIONE, ds);
        }
        [DataContext]
        public void FillCDC_ENTI(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_ENTI(ds);
        }
        [DataContext]
        public void FillCDC_BRANDS(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_BRANDS(ds);
        }
        [DataContext]
        public void FillCDC_COLORI(CDCDS ds, string colore)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_COLORI(ds, colore);
        }
        [DataContext]
        public void FillCDC_ARTICOLI(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_ARTICOLI(ds);
        }
        [DataContext]
        public void FillCDC_ARTICOLI_DIMENSIONI(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_ARTICOLI_DIMENSIONI(ds);
        }
        [DataContext]
        public void FillCDC_ARTICOLI_SPESSORI(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_ARTICOLI_SPESSORI(ds);
        }
        [DataContext]
        public void FillCDC_NUMEROCAMPIONI(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_NUMEROCAMPIONI(ds);
        }

        [DataContext]
        public void FillCDC_CERTIFICATIPIOMBO(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_CERTIFICATIPIOMBO(ds);
        }

        [DataContext(true)]
        public void UpdateCDC_EXCEL(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            //a.UpdateTable(ds.CDC_EXCEL.TableName, ds);
            a.Update_EXCEL_Table(ds);
        }

        [DataContext(true)]
        public void UpdateCDC_DETTAGLIO(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_DETTAGLIO.TableName, ds);
        }
        [DataContext(true)]
        public void UpdateCDC_DETTAGLIO_rel(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_DETTAGLIO_rel.TableName, ds);
        }
        [DataContext]
        public List<DataCollaudo> LeggiDateRiferimento()
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            return a.LeggiDateRiferimento();
        }
        [DataContext]
        public List<DataCollaudoSingle> LeggiDateRiferimentoSingle()
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            return a.LeggiDateRiferimentoSingle();
        }
        [DataContext]
        public List<BrandCollaudoSingle> LeggiBrandRiferimentoSingle(string datacollaudo)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            return a.LeggiBrandRiferimentoSingle(datacollaudo);
        }
        [DataContext]
        public List<ColoreSingle> LeggiColoreRiferimentoSingle()
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            return a.LeggiColoreRiferimentoSingle();
        }
        

        [DataContext]
        public void FillCDC(CDCDS ds, DataCollaudo DATARIFERIMENTO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_EXCEL(ds, DATARIFERIMENTO.Data, DATARIFERIMENTO.Brand);

            List<decimal> IDEXCEL = ds.CDC_EXCEL.Select(x => x.IDEXCEL).Distinct().ToList();
            a.FillCDC_DETTAGLIO(ds, IDEXCEL);
        }

        [DataContext]
        public void FillCDC_DATA_BRAND(CDCDS ds, DataCollaudoSTR DATARIFERIMENTO)
        { 
        CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_DETTAGLIO_DATA_BRAND(ds, DATARIFERIMENTO.Data, DATARIFERIMENTO.Brand);

            //CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            //a.FillCDC_EXCEL(ds, DATARIFERIMENTO.Data, DATARIFERIMENTO.Brand);

            //List<decimal> IDEXCEL = ds.CDC_EXCEL.Select(x => x.IDEXCEL).Distinct().ToList();
            //a.FillCDC_DETTAGLIO(ds, IDEXCEL);
        }

        [DataContext]
        public void FillCDC_CON_DESCRIZIONE(CDCDS ds, DateTime DATARIFERIMENTO, string Azienda)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_EXCEL(ds, DATARIFERIMENTO, Azienda);

            List<decimal> IDEXCEL = ds.CDC_EXCEL.Select(x => x.IDEXCEL).Distinct().ToList();
            a.FillCDC_DETTAGLIO_CON_DESCRIZIONE(ds, IDEXCEL);
        }
        [DataContext]
        public void FillCDC_CON_DESCRIZIONE_STR(CDCDS ds, string DATARIFERIMENTO, string Brand)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            //a.FillCDC_EXCEL(ds, DATARIFERIMENTO, Azienda);

            //List<decimal> IDEXCEL = ds.CDC_EXCEL.Select(x => x.IDEXCEL).Distinct().ToList();
            a.FillCDC_DETTAGLIO_CON_DESCRIZIONE_STR(ds,  DATARIFERIMENTO,  Brand);
        }

        [DataContext]
        public void FillCDC_CONFORMITA(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_CONFORMITA(ds, IDDETTAGLIO);
            a.FillCDC_CONFORMITA_DETTAGLIO(ds, IDDETTAGLIO);
        }

        [DataContext]
        public void FillCDC_CERTIFICATIPIOMBO_NonAssegnati(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_CERTIFICATIPIOMBO_NonAssegnati(ds);
        }

        [DataContext]
        public void FillCDC_ASSOCIAZIONEPIOMBO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_ASSOCIAZIONEPIOMBO(ds, IDDETTAGLIO);
        }

        [DataContext]
        public void FillCDC_ETICHETTE_DETTAGLIO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_ETICHETTE_DETTAGLIO(ds, IDDETTAGLIO);
        }
        [DataContext]
        public void CDC_PDF(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.CDC_PDF(ds, IDDETTAGLIO);
        }

        [DataContext]
        public void FillCDC_DIMEMSIONI(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_DIMEMSIONI(ds, IDDETTAGLIO);
        }
        [DataContext]
        public void FillCDC_DIMEMSIONI_MISURE(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_DIMEMSIONI_MISURE(ds, IDDETTAGLIO);
        }
        [DataContext(true)]
        public void UpdateBrands(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_BRANDS.TableName, ds);
        }
        [DataContext(true)]
        public void UpdateColori(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_COLORI.TableName, ds);
        }
        [DataContext(true)]
        public void UpdateEnti(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_ENTI.TableName, ds);
        }
        [DataContext(true)]
        public void UpdateArticoli(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_ARTICOLI.TableName, ds);
        }
        [DataContext(true)]
        public void UpdateArticoli_Dimensioni(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_ARTICOLI_DIMENSIONI.TableName, ds);
        }
        [DataContext(true)]
        public void UpdateArticoli_Spessori(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_ARTICOLI_SPESSORI.TableName, ds);
        }

        [DataContext(true)]
        public void UpdateConformita(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_CONFORMITA.TableName, ds);
            a.UpdateTable(ds.CDC_CONFORMITA_DETTAGLIO.TableName, ds);
        }
        [DataContext(true)]
        public void UpdateMateriaprima(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_MATERIAPRIMA.TableName, ds);
        }

        [DataContext(true)]
        public void UpdateCDC_PDF(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_PDF.TableName, ds);
        }
        [DataContext(true)]
        public void UpdateCDC_GALVANICA(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_GALVANICA.TableName, ds);
        }
        [DataContext(true)]
        public void UpdateDimensioni(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_DIMEMSIONI.TableName, ds);
            //a.UpdateTable(ds.CDC_DIMEMSIONI_MISURE.TableName, ds);
        }

        [DataContext]
        public void FillCDC_ANTIALLERGICO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_ANTIALLERGICO(ds, IDDETTAGLIO);
        }

        [DataContext]
        public void FillCDC_VERNICICOPRENTI(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_VERNICICOPRENTI(ds, IDDETTAGLIO);
        }

        [DataContext]
        public void FillCDC_TENUTACIDONITRICO(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_TENUTACIDONITRICO(ds, IDDETTAGLIO);
        }

        [DataContext(true)]
        public void UpdateCDC_TENUTACIDONITRICO(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_TENUTACIDONITRICO.TableName, ds);
        }

        [DataContext(true)]
        public void UpdateCDC_ASSOCIAZIONEPIOMBO(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_ASSOCIAZIONEPIOMBO.TableName, ds);
        }

        [DataContext(true)]
        public void UpdateCDC_ANTIALLERGICO(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_ANTIALLERGICO.TableName, ds);
        }

        [DataContext(true)]
        public void UpdateCDC_VERNICICOPRENTI(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_VERNICICOPRENTI.TableName, ds);
        }
        [DataContext]
        public void FillCDC_COLORE(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_COLORE(ds, IDDETTAGLIO);
        }

        [DataContext(true)]
        public void UpdateCDC_COLORE(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_COLORE.TableName, ds);
        }

        [DataContext(true)]
        public void UpdateCDC_ETICHETTE_DETTAGLIO(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_ETICHETTE_DETTAGLIO.TableName, ds);
        }

        [DataContext]
        public void FillCDC_GALVANICA(CDCDS ds, List<decimal> IDDETTAGLIO,string tipoCertificato)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_GALVANICA(ds, IDDETTAGLIO,tipoCertificato);
        }

        [DataContext]
        public void FillCDC_MISURE(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_MISURE(ds, IDDETTAGLIO);
        }

        [DataContext]
        public void FillCDC_SPESSORE(CDCDS ds, string tipoCertificato)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_SPESSORE(ds,tipoCertificato);
        }


        [DataContext]
        public void FillCDC_APPLICAZIONE(CDCDS ds, string tipoCertificato)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_APPLICAZIONE(ds,tipoCertificato);
        }

        [DataContext]
        public void FillCDC_MATERIAPRIMA(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_MATERIAPRIMA(ds);
        }

        [DataContext(true)]
        public void UpdateTabelleSpessore(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_GALVANICA.TableName, ds);
            a.UpdateTable(ds.CDC_SPESSORE.TableName, ds);
            a.UpdateTable(ds.CDC_MISURE.TableName, ds);
            a.UpdateTable(ds.CDC_APPLICAZIONE.TableName, ds);
        }

        [DataContext(true)]
        public void UpdateCertificatiPiombo(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_CERTIFICATIPIOMBO.TableName, ds);
        }
        [DataContext(true)]
        public long InsertCDC_CERTIFICATOPIOMBO(string ELEMENTO, string CODICE, string MATERIALE,
                                string LOTTO, decimal? LUNGHEZZA, decimal? LARGHEZZA, decimal? SPESSORE, string METODO, decimal PESOCAMPIONE, decimal MATRACCIOLO, decimal CONCENTRAZIONE,
                                decimal PBPPM, decimal CDPPM, string ESITO, DateTime DATACERTIFICATO, string UTENTE, DateTime DATAINSERIMENTO, string PATHFILE)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            return a.InsertCDC_CERTIFICATOPIOMBO(ELEMENTO, CODICE, MATERIALE,
                                 LOTTO, LUNGHEZZA, LARGHEZZA, SPESSORE, METODO, PESOCAMPIONE, MATRACCIOLO, CONCENTRAZIONE,
                                 PBPPM, CDPPM, ESITO, DATACERTIFICATO, UTENTE, DATAINSERIMENTO, PATHFILE);
        }
    }
}
