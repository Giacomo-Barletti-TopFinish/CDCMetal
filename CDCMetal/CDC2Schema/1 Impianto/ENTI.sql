﻿CREATE TABLE [dbo].[ENTI]
(
	[IDENTE] INT NOT NULL PRIMARY KEY, 
	[IDBRAND] INT NULL, 
    [CODICE] VARCHAR(4) NOT NULL, 
    [DESCRIZIONE] VARCHAR(25) NULL, 
    [MAGAZZINO] VARCHAR(4) NOT NULL,
	[CANCELLATO] BIT NOT NULL, 
    [DATAMODIFICA] DATETIME NOT NULL, 
    [UTENTEMODIFICA] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_ENTI_BRAND] FOREIGN KEY (IDBRAND) REFERENCES BRAND(IDBRAND)

)

GO

CREATE UNIQUE INDEX [IX_ENTI_CODICE] ON [dbo].[ENTI] (CODICE)
