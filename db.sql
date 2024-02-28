USE [ProjectDb]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ccfb9db2-0751-4ccb-9c21-ffd293cc052f', N'Yonetici', N'YONETICI', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Name], [Surname], [DateOfBirth], [Gender], [LastLoginDate], [LastLoginIP], [isBanned], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'47842b9e-3783-4864-ac39-2452cd7957f6', N'Onur', N'Özer', CAST(N'2005-02-12T00:00:00.0000000' AS DateTime2), 2, CAST(N'2024-02-28T17:17:07.0909421' AS DateTime2), N'::1', 0, N'nia123', N'NIA123', N'onurozer1881@gmail.com', N'ONUROZER1881@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEP0HSTIHHiGN9hSBo0YdGbK1fXGLwP+zxBdUJRrlBV9FVMYYhFAPYKv8TZimxFy/nA==', N'ZG5A5C2IHIEUSLFRNNKA7CV2G7UYHK2C', N'ca166d89-5826-4633-9187-74d6e4e10307', N'(551) 255-8756', 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'47842b9e-3783-4864-ac39-2452cd7957f6', N'ccfb9db2-0751-4ccb-9c21-ffd293cc052f')
GO
SET IDENTITY_INSERT [dbo].[UserAddress] ON 

INSERT [dbo].[UserAddress] ([ID], [Title], [NameSurname], [Phone], [Address], [City], [Zipcode], [UserID]) VALUES (43, N'İş', N'Onur Özer', N'(213) 211-2321', N'Şişli Mecidiyeköy', 40, N'34444', N'47842b9e-3783-4864-ac39-2452cd7957f6')
SET IDENTITY_INSERT [dbo].[UserAddress] OFF
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([ID], [Name], [DisplayIndex]) VALUES (1, N'Koton', 1)
INSERT [dbo].[Brand] ([ID], [Name], [DisplayIndex]) VALUES (3, N'Zara', 2)
INSERT [dbo].[Brand] ([ID], [Name], [DisplayIndex]) VALUES (4, N'Mavi', 3)
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (23, NULL, N'Giyim', 1)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (24, 23, N'Gömlek', 25)
INSERT [dbo].[Category] ([ID], [ParentID], [Name], [DisplayIndex]) VALUES (25, 23, N'Kazak', 3)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [Name], [UnitsInStock], [Description], [Price], [Details], [ShippingDetails], [CategoryId], [BrandId]) VALUES (8, N'Mavi Gömlek', 5, N'Mükemmel Gömlek', CAST(129.90 AS Decimal(18, 2)), N'<p>Zara nın erkek koleksiyonundan Lacivert G&ouml;mlek. &Uuml;r&uuml;n &Ouml;zellikleri, d&uuml;ğmesiz g&ouml;mlek yaka.</p>
', N'<p>XXXX Kargo ile g&ouml;nderilecektir.</p>
', 24, 3)
INSERT [dbo].[Product] ([ID], [Name], [UnitsInStock], [Description], [Price], [Details], [ShippingDetails], [CategoryId], [BrandId]) VALUES (10, N'Kazak', 50, N'Mavi nin kız çocuk koleksiyonundan Mavi Logo Baskılı Lacivert Çizgili Sweatshirt. Ürün özellikleri, şardonsuz.', CAST(399.90 AS Decimal(18, 2)), N'<p>Kumaş Bilgileri</p>

<ul>
	<li>%66 Pamuk</li>
	<li>%34 Polyester</li>
</ul>
', N'<p>XXXX Kargo ile g&ouml;nderilecektir.</p>
', 25, 4)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductPicture] ON 

INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (14, N'mvgmlk', N'/img/ProductPicture/mavigomlek.webp', 1, 8)
INSERT [dbo].[ProductPicture] ([ID], [Name], [Picture], [DisplayIndex], [ProductID]) VALUES (15, N'kzk1', N'/img/ProductPicture/kazak1.webp', 1, 10)
SET IDENTITY_INSERT [dbo].[ProductPicture] OFF
GO
SET IDENTITY_INSERT [dbo].[Slides] ON 

INSERT [dbo].[Slides] ([ID], [Slogan], [Title], [Description], [Picture], [Link], [LinkTitle], [DisplayIndex]) VALUES (5, N'Ürünlerimizi İnceleyin', N'INDIRIM', N'%15 INDIRIM FIRSATI!', N'/img/slide/01.png', N'https://localhost:7110/category/23', N'HEMEN GIT', 1)
INSERT [dbo].[Slides] ([ID], [Slogan], [Title], [Description], [Picture], [Link], [LinkTitle], [DisplayIndex]) VALUES (6, N'FIRSAT', N'X ÜRÜNLERİ', N'X KATEGORISINDE %50 INDIRIM', N'/img/slide/banner02.png', N'https://localhost:7110/category/23', N'HEMEN GIT', 2)
SET IDENTITY_INSERT [dbo].[Slides] OFF
GO
