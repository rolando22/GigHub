namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PoblarUsers : DbMigration
    {
        public override void Up()
        {
            Sql("@" +
                "INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ArtisticName], [Image]) VALUES (N'0e965451-df81-4a7f-af8f-102cc01269cf', N'jose@gmail.com', 0, N'ACnjbMivFUPPUtwk+1fwWbScKnoBsotPqusNgUdRR6cx2cW0EVJe4X6AsQKHmDcjWg==', N'06d6f592-2115-4397-b564-eefbe13e683d', NULL, 0, 0, NULL, 1, 0, N'jose@gmail.com', N'AC/DC', N'BXXI-ACDC-FOTO-1-300x300.jpg')" +
                "INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ArtisticName], [Image]) VALUES(N'287fa5e0-63a9-493d-9a6a-63f54b5bd8b3', N'rolando@gmail.com', 0, N'AI4Mf5RFioNUvooCJ6DXLz/rFP8KY6fUNjB8zxzOvVjdW46RG576xjliTRMYAuomsQ==', N'bbfa94a3-faba-468f-b57f-61b6e2140288', NULL, 0, 0, NULL, 1, 0, N'rolando@gmail.com', N'Nirvana', N'index.jpg')" +
                "INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ArtisticName], [Image]) VALUES(N'5d6fc813-e19e-40be-a2e9-4ea5a5e40063', N'marcos@gmail.com', 0, N'AIxDKEkD10eshamfAktHo00+S8WMDkA4m4R63twqbBtgEGA2yR99FFhmvFeStGVPJQ==', N'86f35d40-29e4-4bb8-b266-20d26425a5a3', NULL, 0, 0, NULL, 1, 0, N'marcos@gmail.com', N'System of a Down', N'soad-300x300.jpg')" +
                "INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ArtisticName], [Image]) VALUES(N'609901b5-1bc7-478d-8d24-f38fef0a02be', N'pedro@gmail.com', 0, N'AJ0Sm8ryVMygA28NwWRSb08g6XlDanUhXvT+QbTaRtooSA2bZGylzqYAhZ9d+EUcBg==', N'984976fb-8bd5-4cb0-9eae-b8e733c00749', NULL, 0, 0, NULL, 1, 0, N'pedro@gmail.com', N'Linkin Park', N'img-thing.jpg')" +
                "INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ArtisticName], [Image]) VALUES(N'f3d65fcc-07de-4a71-a1ab-ccf1faf64599', N'juan@gmail.com', 0, N'AD6JNdKsNRgNOBcQfv/4Xx4Ce+gLeDopW3iwU0MwRKxbId91hei/bp9o3y6VWHCtXA==', N'5098b3a4-11fa-4740-88a8-30a5790c2e0e', NULL, 0, 0, NULL, 1, 0, N'juan@gmail.com', N'Green Day', N'Green_Day-American_Idiot.jpg')"
            );
        }
        
        public override void Down()
        {
        }
    }
}
