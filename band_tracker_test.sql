USE [band_tracker_test]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 7/22/2016 11:59:40 AM ******/
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
/****** Object:  Table [dbo].[bands_venues]    Script Date: 7/22/2016 11:59:41 AM ******/
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
/****** Object:  Table [dbo].[venues]    Script Date: 7/22/2016 11:59:41 AM ******/
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
SET IDENTITY_INSERT [dbo].[bands_venues] ON 

INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (1, 24, 21)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (2, 25, 21)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (3, 26, 23)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (6, 30, 30)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (7, 31, 32)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (8, 35, 34)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (9, 36, 34)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (10, 37, 36)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (13, 43, 43)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (14, 44, 45)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (16, 49, 47)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (17, 50, 49)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (18, 54, 51)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (19, 55, 51)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (20, 56, 53)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (22, 61, 60)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (23, 62, 60)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (24, 63, 62)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (27, 69, 69)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (28, 70, 71)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (30, 75, 73)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (31, 76, 75)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (32, 80, 77)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (33, 81, 77)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (34, 82, 79)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (37, 88, 86)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (38, 89, 88)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (39, 93, 90)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (40, 94, 90)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (41, 95, 92)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (44, 101, 99)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (45, 102, 101)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (46, 106, 103)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (47, 107, 103)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (48, 108, 105)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (51, 114, 112)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (52, 115, 114)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (53, 119, 116)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (54, 120, 116)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (55, 121, 118)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (58, 127, 125)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (59, 128, 127)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (60, 132, 129)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (61, 133, 129)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (62, 134, 131)
SET IDENTITY_INSERT [dbo].[bands_venues] OFF
