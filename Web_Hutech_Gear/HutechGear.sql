USE [Web_Hutech_Gear]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Adv]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Adv](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Image] [nvarchar](500) NOT NULL,
	[Link] [nvarchar](500) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsActivate] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tb_Adv] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Comment]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[NewsId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tb_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Contact]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Message] [nvarchar](4000) NOT NULL,
	[IsRead] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsActivate] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tb_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Messages]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReceiverId] [nvarchar](128) NULL,
	[Timestamp] [datetime] NOT NULL,
	[SenderId] [nvarchar](128) NULL,
	[Message] [nvarchar](max) NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tb_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_News]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_News](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[NewsCategoryId] [int] NOT NULL,
	[IsHome] [bit] NOT NULL,
	[IsSale] [bit] NOT NULL,
	[IsHot] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsActivate] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tb_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_NewsCategory]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_NewsCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsActivate] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tb_NewsCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Order]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[TypePayment] [int] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsOrder] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tb_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_OrderDetail]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_OrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tb_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Product]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](250) NULL,
	[OriginalPrice] [decimal](18, 2) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PriceSale] [decimal](18, 2) NULL,
	[Quantity] [int] NOT NULL,
	[ProductCategoryId] [int] NOT NULL,
	[SupplierId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[IsHome] [bit] NOT NULL,
	[IsSale] [bit] NOT NULL,
	[IsHot] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsActivate] [bit] NOT NULL,
	[IsStatus] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tb_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ProductCategory]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ProductCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsActivate] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tb_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ProductImage]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ProductImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Image] [nvarchar](max) NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tb_ProductImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Rated]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Rated](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[ProductId] [int] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Rating] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.tb_Rated] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Reply]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Reply](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](128) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[RatedId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tb_Reply] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Subscribe]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Subscribe](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.tb_Subscribe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Supplier]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Supplier](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IsActivate] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tb_Supplier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_SystemSetting]    Script Date: 5/27/2023 12:07:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_SystemSetting](
	[SettingKey] [nvarchar](50) NOT NULL,
	[SettingValue] [nvarchar](4000) NULL,
 CONSTRAINT [PK_dbo.tb_SystemSetting] PRIMARY KEY CLUSTERED 
(
	[SettingKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_Adv] ADD  DEFAULT ((0)) FOR [IsActivate]
GO
ALTER TABLE [dbo].[tb_Comment] ADD  DEFAULT ((0)) FOR [NewsId]
GO
ALTER TABLE [dbo].[tb_Contact] ADD  DEFAULT ((0)) FOR [IsActivate]
GO
ALTER TABLE [dbo].[tb_Messages] ADD  DEFAULT ((0)) FOR [IsRead]
GO
ALTER TABLE [dbo].[tb_News] ADD  DEFAULT ((0)) FOR [IsActivate]
GO
ALTER TABLE [dbo].[tb_NewsCategory] ADD  DEFAULT ((0)) FOR [IsActivate]
GO
ALTER TABLE [dbo].[tb_Order] ADD  DEFAULT ((0)) FOR [IsOrder]
GO
ALTER TABLE [dbo].[tb_Product] ADD  DEFAULT ((0)) FOR [IsActivate]
GO
ALTER TABLE [dbo].[tb_Product] ADD  DEFAULT ((0)) FOR [IsStatus]
GO
ALTER TABLE [dbo].[tb_ProductCategory] ADD  DEFAULT ((0)) FOR [IsActivate]
GO
ALTER TABLE [dbo].[tb_Reply] ADD  DEFAULT ((0)) FOR [RatedId]
GO
ALTER TABLE [dbo].[tb_Supplier] ADD  DEFAULT ((0)) FOR [IsActivate]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[tb_Comment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_Comment_dbo.AspNetUsers_User_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tb_Comment] CHECK CONSTRAINT [FK_dbo.tb_Comment_dbo.AspNetUsers_User_Id]
GO
ALTER TABLE [dbo].[tb_Comment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_Comment_dbo.tb_News_NewsId] FOREIGN KEY([NewsId])
REFERENCES [dbo].[tb_News] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_Comment] CHECK CONSTRAINT [FK_dbo.tb_Comment_dbo.tb_News_NewsId]
GO
ALTER TABLE [dbo].[tb_Messages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_Messages_dbo.AspNetUsers_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tb_Messages] CHECK CONSTRAINT [FK_dbo.tb_Messages_dbo.AspNetUsers_SenderId]
GO
ALTER TABLE [dbo].[tb_Messages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_Messages_dbo.AspNetUsers_UserId] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tb_Messages] CHECK CONSTRAINT [FK_dbo.tb_Messages_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[tb_News]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_News_dbo.tb_NewsCategory_NewsCategoryId] FOREIGN KEY([NewsCategoryId])
REFERENCES [dbo].[tb_NewsCategory] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_News] CHECK CONSTRAINT [FK_dbo.tb_News_dbo.tb_NewsCategory_NewsCategoryId]
GO
ALTER TABLE [dbo].[tb_Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_Order_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tb_Order] CHECK CONSTRAINT [FK_dbo.tb_Order_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[tb_OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_OrderDetail_dbo.tb_Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[tb_Order] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_OrderDetail] CHECK CONSTRAINT [FK_dbo.tb_OrderDetail_dbo.tb_Order_OrderId]
GO
ALTER TABLE [dbo].[tb_OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_OrderDetail_dbo.tb_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tb_Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_OrderDetail] CHECK CONSTRAINT [FK_dbo.tb_OrderDetail_dbo.tb_Product_ProductId]
GO
ALTER TABLE [dbo].[tb_Product]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_Product_dbo.tb_ProductCategory_ProductCategoryId] FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[tb_ProductCategory] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_Product] CHECK CONSTRAINT [FK_dbo.tb_Product_dbo.tb_ProductCategory_ProductCategoryId]
GO
ALTER TABLE [dbo].[tb_Product]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_Product_dbo.tb_Supplier_SupplierId] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[tb_Supplier] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_Product] CHECK CONSTRAINT [FK_dbo.tb_Product_dbo.tb_Supplier_SupplierId]
GO
ALTER TABLE [dbo].[tb_ProductImage]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_ProductImage_dbo.tb_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tb_Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_ProductImage] CHECK CONSTRAINT [FK_dbo.tb_ProductImage_dbo.tb_Product_ProductId]
GO
ALTER TABLE [dbo].[tb_Rated]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_Rated_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tb_Rated] CHECK CONSTRAINT [FK_dbo.tb_Rated_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[tb_Rated]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_Rated_dbo.tb_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tb_Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_Rated] CHECK CONSTRAINT [FK_dbo.tb_Rated_dbo.tb_Product_ProductId]
GO
ALTER TABLE [dbo].[tb_Reply]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_Reply_dbo.AspNetUsers_User_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[tb_Reply] CHECK CONSTRAINT [FK_dbo.tb_Reply_dbo.AspNetUsers_User_Id]
GO
ALTER TABLE [dbo].[tb_Reply]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_Reply_dbo.tb_Rated_RatedId] FOREIGN KEY([RatedId])
REFERENCES [dbo].[tb_Rated] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_Reply] CHECK CONSTRAINT [FK_dbo.tb_Reply_dbo.tb_Rated_RatedId]
GO
