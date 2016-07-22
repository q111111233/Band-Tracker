USE [band_tracker]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 7/22/2016 11:58:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bands_venues]    Script Date: 7/22/2016 11:58:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands_venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[band_id] [int] NULL,
	[venue_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues]    Script Date: 7/22/2016 11:58:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[place] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[bands] ON 

INSERT [dbo].[bands] ([id], [name]) VALUES (1, N'qqqq')
SET IDENTITY_INSERT [dbo].[bands] OFF
SET IDENTITY_INSERT [dbo].[bands_venues] ON 

INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (4, 1, 2)
SET IDENTITY_INSERT [dbo].[bands_venues] OFF
SET IDENTITY_INSERT [dbo].[venues] ON 

INSERT [dbo].[venues] ([id], [place]) VALUES (2, N'lll')
SET IDENTITY_INSERT [dbo].[venues] OFF
