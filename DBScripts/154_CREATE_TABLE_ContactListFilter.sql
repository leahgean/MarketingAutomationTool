CREATE TABLE [dbo].[ContactListFilter](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FilterName] [varchar](50) NOT NULL,
	[FilterCriteria] [text] NOT NULL,
	[FilterType] [varchar](20) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ContactListId] [int] NOT NULL,
	[IsGlobal] [bit] NOT NULL,
	[DeleteFilter] [bit] NULL,
	[SearchType] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ContactListFilter] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[ContactListFilter]  WITH CHECK ADD FOREIGN KEY([ContactListId])
REFERENCES [dbo].[ContactList] ([ID])
GO

ALTER TABLE [dbo].[ContactListFilter]  WITH CHECK ADD FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[ContactListFilter]  WITH CHECK ADD FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[User] ([UserID])
GO


