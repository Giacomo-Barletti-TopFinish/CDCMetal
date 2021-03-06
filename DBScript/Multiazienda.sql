CREATE TABLE CDC_BRANDS
(
  CODICE VARCHAR2(10) NOT NULL,
  DESCRIZIONE VARCHAR2(50) NOT NULL
);
CREATE INDEX IDX_CDC_BRANDS ON CDC_BRANDS(CODICE);

INSERT INTO CDC_BRANDS(CODICE,DESCRIZIONE) VALUES('GUCCI','GUCCI');
INSERT INTO CDC_BRANDS(CODICE,DESCRIZIONE) VALUES('YSL','YSL');
INSERT INTO CDC_BRANDS(CODICE,DESCRIZIONE) VALUES('BALENCIAGA','BALENCIAGA');
INSERT INTO CDC_BRANDS(CODICE,DESCRIZIONE) VALUES('MCQUEEN','MCQUEEN');

COMMIT;

alter table cdc_excel add AZIENDA varchar(10) null;

UPDATE CDC_EXCEL SET AZIENDA = 'GUCCI';


alter table cdc_excel MODIFY (AZIENDA NOT null);
COMMIT;

alter table CDC_GALVANICA MODIFY (SPESSORE null);

alter table CDC_GALVANICA MODIFY (APPLICAZIONE null);

alter table CDC_APPLICAZIONE MODIFY (APPLICAZIONE null);

