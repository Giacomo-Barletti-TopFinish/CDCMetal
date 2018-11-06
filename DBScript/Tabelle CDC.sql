drop SEQUENCE CDC_SEQUENCE;
CREATE SEQUENCE CDC_SEQUENCE START WITH 1 INCREMENT BY 1 CACHE 2;
drop table CDC_DETTAGLIO;

drop table CDC_EXCEL;
CREATE TABLE CDC_EXCEL
(
  IDEXCEL NUMBER(10) NOT NULL,
  NOMEFILE VARCHAR2(200) NOT NULL,
  DATAACQUISIZIONE DATE NOT NULL,
  DATARIFERIMENTO DATE NOT NULL,
  UTENTE VARCHAR2(50) NOT NULL,
  DATI BLOB NOT NULL,
  CONSTRAINT PK_IDEXCEL PRIMARY KEY (IDEXCEL)
);

CREATE INDEX IDX_CDC_EXCEL_2 ON CDC_EXCEL(DATARIFERIMENTO);


CREATE TABLE CDC_DETTAGLIO
(
  IDDETTAGLIO NUMBER GENERATED BY DEFAULT ON NULL AS IDENTITY,
  IDPRENOTAZIONE NUMBER(10) NOT NULL,
  IDEXCEL NUMBER(10) NOT NULL,
  IDVERBALE VARCHAR2(10) NULL,
  DATACOLLAUDO VARCHAR2(10) NOT NULL,
  ACCESSORISTA VARCHAR2(30) NOT NULL,
  PREFISSO VARCHAR2(7) NOT NULL,
  PARTE VARCHAR2(6) NOT NULL,
  COLORE VARCHAR2(5) NOT NULL,
  MISURA NUMBER (4),
  LIVELLODIFF NUMBER (1),
  COMMESSAORDINE VARCHAR2(18) NOT NULL,
  ENTE VARCHAR2(5) NOT NULL,
  UM VARCHAR2(2),
  QUANTITA NUMBER(6),
  ASSEGN VARCHAR2(2),
  AUTO VARCHAR2(5),
  COLLTO VARCHAR2(5),
   QUANTITAVALIDITA VARCHAR2(6),
  LOTTOBLOCCATO VARCHAR2(2),
  VERBALEBLOCCATO VARCHAR2(2),
  DEROGA VARCHAR2(2),
  CONTROLLODIMENSIONALE VARCHAR2(2),
  CONTROLLOESTETICO VARCHAR2(2),
  CONTROLLOFUNZIONALE VARCHAR2(2),
  ESITOTESTCHIMICO VARCHAR2(2),
  TESTFISICO VARCHAR2(2),
  NOTECOLLAUDO VARCHAR2(100),
  ASSEGNAZIONE VARCHAR2(500),
    CONSTRAINT PK_IDDETTAGLIO PRIMARY KEY (IDDETTAGLIO),
  FOREIGN KEY (IDEXCEL)
	  REFERENCES CDC_EXCEL (IDEXCEL) ENABLE
);


CREATE INDEX IDX_CDC_DETTAGLIO_1 ON CDC_DETTAGLIO(IDPRENOTAZIONE);
CREATE INDEX IDX_CDC_DETTAGLIO_2 ON CDC_DETTAGLIO(IDEXCEL);

CREATE TABLE CDC_PDF
(
IDCDCPDF NUMBER GENERATED BY DEFAULT ON NULL AS IDENTITY,
  IDDETTAGLIO NUMBER NOT NULL,
  TIPO VARCHAR2(25) NOT NULL,
  NOMEFILE VARCHAR2(200) NOT NULL,
   CONSTRAINT PK_IDCDCPDF PRIMARY KEY (IDCDCPDF),
  FOREIGN KEY (IDDETTAGLIO)
	  REFERENCES CDC_DETTAGLIO (IDDETTAGLIO) ENABLE
);

CREATE INDEX IDX_CDC_PDF_1 ON CDC_PDF(IDDETTAGLIO);

CREATE TABLE CDC_CONFORMITA
(
    IDCONFORMITA NUMBER GENERATED BY DEFAULT ON NULL AS IDENTITY,
    IDDETTAGLIO NUMBER NOT NULL,
    UTENTE VARCHAR2(50) NOT NULL,
    DATAINSERIMENTO DATE NOT NULL,
    FISICOCHIMICO VARCHAR2(1) NOT NULL,
    FUNZIONALE VARCHAR2(1) NOT NULL,
    DIMENSIONALE VARCHAR2(1) NOT NULL,
    ESTETICO VARCHAR2(1) NOT NULL,
    ACCONTO VARCHAR2(1) NOT NULL,
    SALDO VARCHAR2(1) NOT NULL,
    DESCRIZIONE VARCHAR2(50) NOT NULL,
    ALTRO VARCHAR2(50) NULL,
    CERTIFICATI VARCHAR2(50) NULL,
     CONSTRAINT PK_IDCONFORMITA PRIMARY KEY (IDCONFORMITA),
    FOREIGN KEY (IDDETTAGLIO)
    REFERENCES CDC_DETTAGLIO (IDDETTAGLIO) ENABLE
);

CREATE INDEX IDX_CDC_CONFORMITA_1 ON CDC_CONFORMITA(IDDETTAGLIO);

CREATE TABLE CDC_CONFORMITA_DETTAGLIO
(
  IDCDCDET NUMBER GENERATED BY DEFAULT ON NULL AS IDENTITY,
  PREFISSO VARCHAR2(7) NOT NULL,
  PARTE VARCHAR2(6) NOT NULL,
  COLORE VARCHAR2(5) NOT NULL,
  DESCRIZIONE VARCHAR2(50) NOT NULL,
   CONSTRAINT PK_IDCDCDET PRIMARY KEY (IDCDCDET)
);

CREATE INDEX IDX_CDC_CONFORMITA_DET_1 ON CDC_CONFORMITA_DETTAGLIO(PREFISSO, PARTE,COLORE);

CREATE TABLE CDC_DIMEMSIONI
(
    IDDIMENSIONALE NUMBER GENERATED BY DEFAULT ON NULL AS IDENTITY,
    IDDETTAGLIO NUMBER NOT NULL,
    UTENTE VARCHAR2(50) NOT NULL,
    DATAINSERIMENTO DATE NOT NULL,
    RIFERIMENTO VARCHAR2(2) NOT NULL,
    GRANDEZZA VARCHAR2(30) NOT NULL,
    RICHIESTO VARCHAR2(5) NOT NULL,
    TOLLERANZA VARCHAR2(5)  NULL,
    MINIMO VARCHAR2(5)  NULL,
    MASSIMO VARCHAR2(5)  NULL,
    TAMPONE VARCHAR2(50)  NULL,
    CONTAMPONE VARCHAR2(1) NOT NULL,
    CONFORME VARCHAR2(1) NOT NULL,
     CONSTRAINT PK_IDDIMENSIONALE PRIMARY KEY (IDDIMENSIONALE),
    FOREIGN KEY (IDDETTAGLIO)
    REFERENCES CDC_DETTAGLIO (IDDETTAGLIO) ENABLE
);

CREATE INDEX IDX_CDC_DIMEMSIONI_1 ON CDC_DIMEMSIONI(IDDETTAGLIO);

CREATE TABLE CDC_DIMEMSIONI_MISURE
(
    IDCDCMIS NUMBER GENERATED BY DEFAULT ON NULL AS IDENTITY,
    PARTE VARCHAR2(6) NOT NULL,
    RIFERIMENTO VARCHAR2(2) NOT NULL,
    GRANDEZZA VARCHAR2(30) NOT NULL,
    RICHIESTO VARCHAR2(5) NOT NULL,
    TOLLERANZA VARCHAR2(5)  NULL,
    MINIMO VARCHAR2(5)  NULL,
    MASSIMO VARCHAR2(5)  NULL,
    TAMPONE VARCHAR2(50)  NULL,
    CONTAMPONE VARCHAR2(1) NULL,
     CONSTRAINT PK_IDCDCMIS PRIMARY KEY (IDCDCMIS)
);

CREATE INDEX IDX_CDC_DIMEMSIONI_MISURE_1 ON CDC_DIMEMSIONI_MISURE(PARTE);


CREATE TABLE CDC_ANTIALLERGICO
(
    IDNICHELFREE NUMBER GENERATED BY DEFAULT ON NULL AS IDENTITY,
    IDDETTAGLIO NUMBER NOT NULL,
    UTENTE VARCHAR2(50) NOT NULL,
    DATAINSERIMENTO DATE NOT NULL,
    DATAPRODUZIONE DATE NOT NULL,
    NICHELFREE VARCHAR2(1) NOT NULL,
     CONSTRAINT PK_IDNICHELFREE PRIMARY KEY (IDNICHELFREE),
    FOREIGN KEY (IDDETTAGLIO)
    REFERENCES CDC_DETTAGLIO (IDDETTAGLIO) ENABLE
);

CREATE INDEX IDX_CDC_ANTIALLERGICO_1 ON CDC_ANTIALLERGICO(IDDETTAGLIO);
