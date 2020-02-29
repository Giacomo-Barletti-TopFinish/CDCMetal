
alter table cdc_certificatipiombo add PATHFILE varchar2(300) null;

alter table cdc_certificatipiombo MODIFY (ELEMENTO null);
alter table cdc_certificatipiombo MODIFY (LUNGHEZZA null);
alter table cdc_certificatipiombo MODIFY (LARGHEZZA null);

