USE [ConestogaVirtualGameStoreDb]
GO
INSERT [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (N'0273A775-EBAE-422E-861D-75716726893A', N'E451142D-63FA-4BEC-A5CE-CE1A82A2BC74', N'Employee', N'EMPLOYEE')
INSERT [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName]) VALUES (N'2777CDCE-4BFA-4E2E-A146-A950CA9DE088', N'0D87070F-E445-44F9-9EC6-CDA4A34FE960', N'Member', N'MEMBER')
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'6a65fc85-22e7-443b-9ac7-c3c5d24a1c8a', 0, N'2e52aa68-4a17-49df-833e-b62e9342459b', N'member@gmail.com', 0, 1, NULL, N'MEMBER@GMAIL.COM', N'MEMBER@GMAIL.COM', N'AQAAAAEAACcQAAAAENCDmNdCOp4cXW/mRHkNeuZfoLNZu+d2y9vZk65enGY3CD5iPF+PCE9teUMX+SYxAg==', NULL, 0, N'a7ebce74-d6c1-4596-a524-a9de186ef007', 0, N'member@gmail.com')
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName]) VALUES (N'9e924d90-df19-4789-97f5-7e542e1627ea', 0, N'd9009f3c-82c9-4403-ba32-5adfa9d7603c', N'employee@gmail.com', 0, 1, NULL, N'EMPLOYEE@GMAIL.COM', N'EMPLOYEE@GMAIL.COM', N'AQAAAAEAACcQAAAAED6rp812vzBDCvJzHHJIVxtzgWE+HirtpFtCW1Vxg5MK8mEMkiA4smHqwH3nLUuoAA==', NULL, 0, N'c451a391-984a-4520-a6f3-31fa54f63acd', 0, N'employee@gmail.com')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9e924d90-df19-4789-97f5-7e542e1627ea', N'0273A775-EBAE-422E-861D-75716726893A')
