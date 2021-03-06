alter table CDC_APPLICAZIONE add CERTIFICATO varchar(10) null;
alter table CDC_SPESSORE add CERTIFICATO varchar(10) null;
alter table CDC_GALVANICA add CERTIFICATO varchar(10) null;

UPDATE CDC_APPLICAZIONE SET CERTIFICATO = 'SPESSORE';
UPDATE CDC_SPESSORE SET CERTIFICATO = 'SPESSORE';
UPDATE CDC_GALVANICA SET CERTIFICATO = 'SPESSORE';


alter table CDC_APPLICAZIONE MODIFY (CERTIFICATO NOT null);
alter table CDC_SPESSORE MODIFY (CERTIFICATO NOT null);
alter table CDC_GALVANICA MODIFY (CERTIFICATO NOT null);
COMMIT;

drop index IDX_CDC_APPLICAZIONE_1;
create index IDX_CDC_APPLICAZIONE_1 on CDC_APPLICAZIONE(PARTE,COLORE,CERTIFICATO);

drop index IDX_CDC_SPESSORE_1;
create index IDX_CDC_SPESSORE_1 on CDC_SPESSORE(PARTE,COLORE,CERTIFICATO);

DROP INDEX IDX_CDC_GALVANICA_1;
CREATE INDEX IDX_CDC_GALVANICA_1 ON CDC_GALVANICA (IDDETTAGLIO, CERTIFICATO);