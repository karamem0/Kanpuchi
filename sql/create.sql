IF OBJECT_ID(N'[kanpuchi].[dbo].[FK_AccessLog_Device]', N'F') IS NOT NULL
	ALTER TABLE [AccessLog] DROP CONSTRAINT [FK_AccessLog_Device]
GO

IF OBJECT_ID(N'[kanpuchi].[dbo].[FK_MatomeEntry_MatomeSite]', N'F') IS NOT NULL
	ALTER TABLE [MatomeEntry] DROP CONSTRAINT [FK_MatomeEntry_MatomeSite]
GO

IF OBJECT_ID(N'[kanpuchi].[dbo].[FK_TwitterStatus_TwitterUser]', N'F') IS NOT NULL
	ALTER TABLE [TwitterStatus] DROP CONSTRAINT [FK_TwitterStatus_TwitterUser]
GO

IF OBJECT_ID(N'[kanpuchi].[dbo].[AccessLog]', N'U') IS NOT NULL
    DROP TABLE [dbo].[AccessLog]
GO

CREATE TABLE [dbo].[AccessLog] (
	[LogId] [uniqueidentifier] NOT NULL,
	[DeviceId] [uniqueidentifier] NULL,
	[Url] [nvarchar](1000) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	CONSTRAINT [PK_AccessLog] PRIMARY KEY
	(
		[LogId] ASC
	)
)
GO

IF OBJECT_ID(N'[kanpuchi].[dbo].[Device]', N'U') IS NOT NULL
    DROP TABLE [dbo].[Device]
GO

CREATE TABLE [dbo].[Device] (
	[DeviceId] [uniqueidentifier] NOT NULL,
	[DeviceKey] [nvarchar](80) NULL,
	[FirmwareVersion] [nvarchar](20) NULL,
	[HardwareVersion] [nvarchar](20) NULL,
	[Manufacturer] [nvarchar](40) NULL,
	[Name] [nvarchar](40) NULL,
	[AppVersion] [nvarchar](20) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	CONSTRAINT [PK_Device] PRIMARY KEY
	(
		[DeviceId] ASC
	)
)
GO

IF OBJECT_ID(N'[kanpuchi].[dbo].[MatomeEntry]', N'U') IS NOT NULL
    DROP TABLE [dbo].[MatomeEntry]
GO

CREATE TABLE [dbo].[MatomeEntry] (
	[EntryId] [uniqueidentifier] NOT NULL,
	[SiteId] [integer] NOT NULL,
	[Url] [nvarchar](1000) NULL,
	[Title] [nvarchar](200) NULL,
	[Content] [nvarchar](MAX) NULL,
	[ThumbnailUpdated] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	CONSTRAINT [PK_MatomeEntry] PRIMARY KEY
	(
		[EntryId] ASC
	)
)
GO

IF OBJECT_ID(N'[kanpuchi].[dbo].[MatomeSite]', N'U') IS NOT NULL
    DROP TABLE [dbo].[MatomeSite]
GO

CREATE TABLE [dbo].[MatomeSite] (
	[SiteId] [integer] NOT NULL,
	[SiteName] [nvarchar](200) NULL,
	[SiteUrl] [nvarchar](1000) NULL,
	[FeedUrl] [nvarchar](1000) NULL,
	[FeedFormat] [nvarchar](20) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	CONSTRAINT [PK_MatomeSite] PRIMARY KEY
	(
		[SiteId] ASC
	)
)
GO

IF OBJECT_ID(N'[kanpuchi].[dbo].[TwitterStatus]', N'U') IS NOT NULL
    DROP TABLE [dbo].[TwitterStatus]
GO

CREATE TABLE [dbo].[TwitterStatus] (
	[StatusId] [nvarchar](40) NOT NULL,
	[UserId] [nvarchar](40) NULL,
	[Text] [nvarchar](200) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	CONSTRAINT [PK_TwitterStatus] PRIMARY KEY
	(
		[StatusId] ASC
	)
)
GO

IF OBJECT_ID(N'[kanpuchi].[dbo].[TwitterUser]', N'U') IS NOT NULL
    DROP TABLE [dbo].[TwitterUser]
GO

CREATE TABLE [dbo].[TwitterUser] (
	[UserId] [nvarchar](40) NOT NULL,
	[UserName] [nvarchar](20) NULL,
	[ScreenName] [nvarchar](40) NULL,
	[ProfileImageUrl] [nvarchar](1000) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	CONSTRAINT [PK_TwitterUser] PRIMARY KEY
	(
		[UserId] ASC
	)
)
GO

ALTER TABLE [dbo].[AccessLog] ADD CONSTRAINT [FK_AccessLog_Device] FOREIGN KEY
(
	[DeviceId]
)
REFERENCES [dbo].[Device]
(
	[DeviceId]
)
GO

ALTER TABLE [dbo].[MatomeEntry] ADD CONSTRAINT [FK_MatomeEntry_MatomeSite] FOREIGN KEY
(
	[SiteId]
)
    REFERENCES [dbo].[MatomeSite]
(
	[SiteId]
)
GO

ALTER TABLE [dbo].[TwitterStatus] ADD CONSTRAINT [FK_TwitterStatus_TwitterUser] FOREIGN KEY
(
	[UserId]
)
REFERENCES [dbo].[TwitterUser]
(
	[UserId]
)
GO
