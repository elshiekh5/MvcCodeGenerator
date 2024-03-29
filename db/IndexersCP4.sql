USE [IndexersCP4]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 06/26/2013 09:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [int] NOT NULL,
	[Title] [varchar](64) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[JournalsOwners]    Script Date: 06/26/2013 09:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JournalsOwners](
	[JournalOwner] [int] IDENTITY(1,1) NOT NULL,
	[OwnerName] [varchar](64) NULL,
 CONSTRAINT [PK_JournalsOwners] PRIMARY KEY CLUSTERED 
(
	[JournalOwner] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Journals]    Script Date: 06/26/2013 09:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Journals](
	[JournalCode] [varchar](20) NOT NULL,
	[FullTitle] [varchar](255) NOT NULL,
	[ShortTitle] [varchar](255) NULL,
	[JournalSubCode] [varchar](20) NOT NULL,
	[JournalOwner] [int] NOT NULL,
	[Note] [varchar](max) NULL,
 CONSTRAINT [PK_Journals] PRIMARY KEY CLUSTERED 
(
	[JournalCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IndexersOwners]    Script Date: 06/26/2013 09:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IndexersOwners](
	[OwnerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NOT NULL,
 CONSTRAINT [PK_IndexersOwners] PRIMARY KEY CLUSTERED 
(
	[OwnerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Indexers]    Script Date: 06/26/2013 09:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Indexers](
	[IndexerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[OwnerID] [int] NOT NULL,
 CONSTRAINT [PK_Indexers] PRIMARY KEY CLUSTERED 
(
	[IndexerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoundsLog]    Script Date: 06/26/2013 09:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoundsLog](
	[RoundID] [int] IDENTITY(1,1) NOT NULL,
	[IndexerID] [int] NOT NULL,
	[JournalCode] [varchar](20) NOT NULL,
	[RoundDate] [date] NOT NULL,
	[StatusID] [int] NOT NULL,
	[NextEvaRound] [nvarchar](128) NULL,
	[Notes] [nvarchar](2000) NULL,
 CONSTRAINT [PK_RoundsLog_RoundID] PRIMARY KEY CLUSTERED 
(
	[RoundID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_RoundsLog_IndexerID] ON [dbo].[RoundsLog] 
(
	[IndexerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_RoundsLog_JournalCode] ON [dbo].[RoundsLog] 
(
	[JournalCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IndexersWithJournals]    Script Date: 06/26/2013 09:44:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IndexersWithJournals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IndexerID] [int] NOT NULL,
	[JournalCode] [varchar](20) NOT NULL,
	[SubmissionDate] [date] NULL,
	[AcceptanceDate] [date] NULL,
	[RejectionDate] [date] NULL,
	[NextEvaRound] [nvarchar](128) NULL,
	[NoofEvaRound] [int] NULL,
	[Notes] [nvarchar](2000) NULL,
	[StatusID] [int] NOT NULL,
 CONSTRAINT [PK_IndexersWithJournals] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_IndexersWithJournals_IndexerID_JournalCode] ON [dbo].[IndexersWithJournals] 
(
	[IndexerID] ASC,
	[JournalCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_IndexersWithJournals_Status] ON [dbo].[IndexersWithJournals] 
(
	[StatusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Indexers_IndexersOwners]    Script Date: 06/26/2013 09:44:52 ******/
ALTER TABLE [dbo].[Indexers]  WITH CHECK ADD  CONSTRAINT [FK_Indexers_IndexersOwners] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[IndexersOwners] ([OwnerID])
GO
ALTER TABLE [dbo].[Indexers] CHECK CONSTRAINT [FK_Indexers_IndexersOwners]
GO
/****** Object:  ForeignKey [FK_IndexersWithJournals_Indexers]    Script Date: 06/26/2013 09:44:52 ******/
ALTER TABLE [dbo].[IndexersWithJournals]  WITH CHECK ADD  CONSTRAINT [FK_IndexersWithJournals_Indexers] FOREIGN KEY([IndexerID])
REFERENCES [dbo].[Indexers] ([IndexerID])
GO
ALTER TABLE [dbo].[IndexersWithJournals] CHECK CONSTRAINT [FK_IndexersWithJournals_Indexers]
GO
/****** Object:  ForeignKey [FK_IndexersWithJournals_Journals]    Script Date: 06/26/2013 09:44:52 ******/
ALTER TABLE [dbo].[IndexersWithJournals]  WITH CHECK ADD  CONSTRAINT [FK_IndexersWithJournals_Journals] FOREIGN KEY([JournalCode])
REFERENCES [dbo].[Journals] ([JournalCode])
GO
ALTER TABLE [dbo].[IndexersWithJournals] CHECK CONSTRAINT [FK_IndexersWithJournals_Journals]
GO
/****** Object:  ForeignKey [FK_IndexersWithJournals_Status]    Script Date: 06/26/2013 09:44:52 ******/
ALTER TABLE [dbo].[IndexersWithJournals]  WITH CHECK ADD  CONSTRAINT [FK_IndexersWithJournals_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[IndexersWithJournals] CHECK CONSTRAINT [FK_IndexersWithJournals_Status]
GO
/****** Object:  ForeignKey [FK_RoundsLog_Indexers]    Script Date: 06/26/2013 09:44:52 ******/
ALTER TABLE [dbo].[RoundsLog]  WITH CHECK ADD  CONSTRAINT [FK_RoundsLog_Indexers] FOREIGN KEY([IndexerID])
REFERENCES [dbo].[Indexers] ([IndexerID])
GO
ALTER TABLE [dbo].[RoundsLog] CHECK CONSTRAINT [FK_RoundsLog_Indexers]
GO
/****** Object:  ForeignKey [FK_RoundsLog_Journals]    Script Date: 06/26/2013 09:44:52 ******/
ALTER TABLE [dbo].[RoundsLog]  WITH CHECK ADD  CONSTRAINT [FK_RoundsLog_Journals] FOREIGN KEY([JournalCode])
REFERENCES [dbo].[Journals] ([JournalCode])
GO
ALTER TABLE [dbo].[RoundsLog] CHECK CONSTRAINT [FK_RoundsLog_Journals]
GO
/****** Object:  ForeignKey [FK_RoundsLog_Status]    Script Date: 06/26/2013 09:44:52 ******/
ALTER TABLE [dbo].[RoundsLog]  WITH CHECK ADD  CONSTRAINT [FK_RoundsLog_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([StatusID])
GO
ALTER TABLE [dbo].[RoundsLog] CHECK CONSTRAINT [FK_RoundsLog_Status]
GO
