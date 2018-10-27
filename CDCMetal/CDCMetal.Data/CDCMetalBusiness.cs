﻿using CDCMetal.Entities;
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

        [DataContext(true)]
        public void UpdateCDC_EXCEL(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_EXCEL.TableName, ds);
        }

        [DataContext(true)]
        public void UpdateCDC_DETTAGLIO(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_DETTAGLIO.TableName, ds);
        }

        [DataContext]
        public List<DateTime> LeggiDateRiferimento()
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            return a.LeggiDateRiferimento();
        }

        [DataContext]
        public void FillCDC(CDCDS ds, DateTime DATARIFERIMENTO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_EXCEL(ds,DATARIFERIMENTO);

            List<decimal> IDEXCEL = ds.CDC_EXCEL.Select(x => x.IDEXCEL).Distinct().ToList();
            a.FillCDC_DETTAGLIO(ds, IDEXCEL);
        }

        [DataContext]
        public void FillCDC_CONFORMITA(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.FillCDC_CONFORMITA(ds, IDDETTAGLIO);
            a.FillCDC_CONFORMITA_DETTAGLIO(ds, IDDETTAGLIO);
        }

        [DataContext]
        public void CDC_PDF(CDCDS ds, List<decimal> IDDETTAGLIO)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.CDC_PDF(ds, IDDETTAGLIO);
        }

        [DataContext(true)]
        public void UpdateConformita(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_CONFORMITA.TableName, ds);
            a.UpdateTable(ds.CDC_CONFORMITA_DETTAGLIO.TableName, ds);
        }

        [DataContext(true)]
        public void UpdateCDC_PDF(CDCDS ds)
        {
            CDCMetalAdapter a = new CDCMetalAdapter(DbConnection, DbTransaction);
            a.UpdateTable(ds.CDC_PDF.TableName, ds);
        }
    }
}
