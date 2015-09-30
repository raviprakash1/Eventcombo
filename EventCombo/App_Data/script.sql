USE [EventCombo]
GO
ALTER TABLE [dbo].[Profile] DROP CONSTRAINT [FK_Profile_Profile]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
--ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
/****** Object:  Table [dbo].[User_Permission_Detail]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[User_Permission_Detail]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[Status]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[Profile]
GO
/****** Object:  Table [dbo].[Permission_Detail]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[Permission_Detail]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[Messages]
GO
/****** Object:  Table [dbo].[Event_Detail]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[Event_Detail]
GO
/****** Object:  Table [dbo].[Email_Template]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[Email_Template]
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[Email_Tag]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[Country]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP TABLE [dbo].[__MigrationHistory]
GO
/****** Object:  StoredProcedure [dbo].[GetSetUserRole]    Script Date: 9/30/2015 8:48:43 PM ******/
DROP PROCEDURE [dbo].[GetSetUserRole]
GO
/****** Object:  StoredProcedure [dbo].[GetSetUserRole]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[GetSetUserRole](
	@user_Id nvarchar(500),
	@GETSET varchar(3),
	@Role_Id varchar(50)
)
AS 
BEGIN
	Declare @RId varchar(50)
	SET @RId =''
	IF (UPPER(@GETSET) = 'GET')
	BEGIN
		SELECT @RID =RoleId FROM AspNetUserRoles WHERE UserId = @user_Id 
	END
	ELSE IF (UPPER(@GETSET) = 'SET')
	BEGIN
		SELECT @RID = Isnull(RoleId,'') FROM AspNetUserRoles WHERE USERID = @user_Id 
		IF (@RID ='')
		BEGIN
			DELETE from AspNetUserRoles where USERID = @user_Id
			INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES(@user_Id,@Role_Id)
		END
		ELSE
			UPDATE AspNetUserRoles SET RoleId = @Role_Id WHERE UserId= @user_Id 
	END
	Select @RId

END

GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [varchar](150) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/30/2015 8:48:43 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/30/2015 8:48:43 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [varchar](150) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Country]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[CountryID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Country] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Email_Tag]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Email_Tag](
	[Tag_Id] [nchar](150) NOT NULL,
	[Tag_Name] [nvarchar](200) NULL,
 CONSTRAINT [PK_Email_Tag] PRIMARY KEY CLUSTERED 
(
	[Tag_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Email_Template]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Email_Template](
	[TemplateId] [varchar](150) NOT NULL,
	[Template_Name] [varchar](300) NULL,
	[To] [varchar](300) NULL,
	[CC] [varchar](300) NULL,
	[Bcc] [varchar](300) NULL,
	[Subject] [varchar](300) NULL,
	[TemplateHtml] [nvarchar](max) NULL,
	[From] [varchar](300) NULL,
 CONSTRAINT [PK_Email_Template] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event_Detail]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Event_Detail](
	[EventID] [varchar](250) NOT NULL,
	[Event_Category] [varchar](250) NULL,
	[Event_SubCategory] [varchar](250) NULL,
	[Title] [varchar](250) NULL,
	[Byline] [varchar](250) NULL,
	[Flyer] [varchar](250) NULL,
	[Multiple_Days] [varchar](250) NULL,
	[Multiple_Start_Times] [varchar](250) NULL,
	[Multiple_End_Times] [varchar](250) NULL,
	[Venue_Name] [varchar](250) NULL,
	[Venue_Address] [varchar](250) NULL,
	[Venue_City] [varchar](250) NULL,
	[Venue_State] [varchar](250) NULL,
	[Venue_Zip] [varchar](250) NULL,
	[Venue_Country] [varchar](250) NULL,
	[NoLocation] [varchar](250) NULL,
	[Multiple_Venue_Names] [varchar](250) NULL,
	[Mutiple_Venue_Addresses] [varchar](250) NULL,
	[Mutiple_Venue_City] [varchar](250) NULL,
	[Mutiple_Venue_State] [varchar](250) NULL,
	[Mutiple_Venue_Zip] [varchar](250) NULL,
	[Mutiple_Venue_Country] [varchar](250) NULL,
	[Description] [varchar](250) NULL,
	[EventURL] [varchar](250) NULL,
	[Event_Host_Id] [varchar](250) NULL,
	[Event_Contact_Name] [varchar](250) NULL,
	[Event_Contact_Email] [varchar](250) NULL,
	[Event_Contact_Phone] [varchar](250) NULL,
	[Facebook_URL] [varchar](250) NULL,
	[Twitter_URL] [varchar](250) NULL,
	[LinkedIn_URL] [varchar](250) NULL,
	[Googl_URL] [varchar](250) NULL,
 CONSTRAINT [PK_Event_Detail] PRIMARY KEY CLUSTERED 
(
	[EventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Form_Name] [varchar](200) NULL,
	[Form_Tag] [varchar](200) NULL,
	[Message] [varchar](500) NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Permission_Detail]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Permission_Detail](
	[Permission_Id] [int] IDENTITY(1,1) NOT NULL,
	[Permission_Desc] [varchar](100) NULL,
	[Permission_Category] [varchar](50) NULL,
 CONSTRAINT [PK_Permission_Detail] PRIMARY KEY CLUSTERED 
(
	[Permission_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Profile](
	[ProfileID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[StreetAddressLine1] [varchar](256) NULL,
	[StreetAddressLine2] [varchar](256) NULL,
	[City] [varchar](256) NULL,
	[State] [varchar](256) NULL,
	[Zip] [varchar](10) NULL,
	[CountryID] [tinyint] NOT NULL,
	[MainPhone] [varchar](20) NULL,
	[SecondPhone] [varchar](20) NULL,
	[WebsiteURL] [varchar](100) NULL,
	[TwitterURL] [varchar](100) NULL,
	[FacebookURL] [varchar](100) NULL,
	[GooglePlusURL] [varchar](100) NULL,
	[LinkedInURL] [varchar](100) NULL,
	[UserProfileImage] [varchar](50) NULL,
	[OrganizerName] [varchar](50) NULL,
	[SecondaryEmail] [varchar](50) NULL,
	[OrganizerDescription] [varchar](max) NULL,
	[AddedOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[WorkPhone] [varchar](20) NULL,
	[ContentType] [varchar](50) NULL,
	[Gender] [varchar](50) NULL,
	[DateofBirth] [varchar](50) NULL,
	[Organiser] [varchar](2) NULL,
	[Merchant] [varchar](2) NULL,
	[UserStatus] [varchar](2) NULL,
 CONSTRAINT [PK__Profile__290C888415502E78] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Status]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Status](
	[StatusID] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusName] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User_Permission_Detail]    Script Date: 9/30/2015 8:48:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Permission_Detail](
	[UP_Id] [int] IDENTITY(1,1) NOT NULL,
	[UP_User_Id] [nvarchar](128) NULL,
	[UP_Permission_Id] [int] NOT NULL,
	[UP_Flag] [nchar](1) NULL,
 CONSTRAINT [PK_User_Permission_Detail] PRIMARY KEY CLUSTERED 
(
	[UP_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201508241139135_InitialCreate', N'EventKombo.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5CDB6EE436127D5F60FF41D0D3EEC269F99219CC1AED044EDBDE3532BE60DA13ECDB802DB1DBC248A422518E8D205F96877C527E618B1275E345976EB9BB1D0C30B0C8E2A962B148168BC5FEF3F73FA6DF3F8781F584E3C4A7E4CC3E9A1CDA16262EF57CB23AB353B6FCE683FDFD777FFFDBF4D20B9FAD9F0ABA134E072D4972663F32169D3A4EE23EE2102593D077639AD0259BB8347490479DE3C3C37F3B47470E06081BB02C6BFA2925CC0F71F6019F334A5C1CB1140537D4C34122CAA1669EA15AB728C449845C7C665F3E61C27EA4E1824E7262DB3A0F7C0482CC71B0B42D440865888198A79F133C673125AB7904052878788930D02D51906021FE6945DEB72787C7BC274ED5B08072D384D17020E0D189508D23375F4BC176A93A50DE252899BDF05E670A3CB3AF3D9C157DA201284066783A0B624E7C66DF942CCE93E816B349D17092435EC500F70B8DBF4EEA880756EF7607A5291D4F0EF9BF036B96062C8DF119C1298B517060DDA78BC0777FC42F0FF42B266727478BE5C98777EF9177F2FE5B7CF2AEDE53E82BD0350AA0E83EA6118E4136BC2CFB6F5B4EB39D23372C9BD5DAE45A015B8259615B37E8F923262BF608F3E5F8836D5DF9CFD82B4A84717D263E4C2268C4E2143E6FD320408B0097F54E2B4FFE7F0BD7E377EF47E17A8B9EFC5536F4127F983831CCAB4F38C86A93473FCAA75763BCBF08B2AB9886FCBB695F79ED97394D639777861A491E50BCC2AC29DDD4A98CB7974973A8F1CDBA40DD7FD3E692AAE6AD25E51D5A6726142CB63D1B0A795F976F6F8B3B8F2218BCCCB4B846DA0C4ED9AB2652E303AB22A90CE7A8AFE110E8D05F791DBC0C911F8CB010F6E0022EC8D28F435CF6F2070A6687C86099EF5192C03AE0FD17258F2DA2C39F23883EC76E1A8379CE190AA357E776FF4809BE4DC305B7FAEDF11A6D681E7EA157C86534BE24BCD5C6781FA9FB95A6EC92781788E1CFCC2D00F9E7831FF60718459C73D7C5497205C68CBD19050FBB00BC26ECE478301C5F9F76ED88CC02E4877A4F445A49BF14A49537A2A7503C120399CE2B6913F5235DF9A49FA805A959D49CA25354413654540ED64F524169163423E89433A71ACDCFCB46687C472F83DD7F4F6FB3CDDBB416D4D438871512FF07131CC332E6DD23C6704CAA11E8B36EECC259C8868F337DF5BD29E3F4130AD2B159AD351BB24560FCD990C1EEFF6CC8C484E227DFE35E498FE34F410CF0BDE8F527ABEE392749B6EDE9D0E8E6B6996F670D304D97F324A1AE9FCD024DE04B842D9AF2830F6775C730F2DEC87110E81818BACFB73C2881BED9B251DD910B1C6086AD73370F0CCE50E2224F552374C81B2058B1A36A04ABE2214DE1FEA5F0044BC7316F84F821288199EA13A64E0B9FB87E84824E2D492D7B6E61BCEF250FB9E602479870869D9AE8C35C1FFEE002947CA441E9D2D0D4A9595CBB211ABC56D39877B9B0D5B82B5189ADD86487EF6CB04BE1BFBD8A61B66B6C0BC6D9AE923E02184379BB30507156E96B00F2C165DF0C543A31190C54B8545B31D0A6C67660A04D95BC3903CD8FA87DC75F3AAFEE9B79360FCADBDFD65BD5B503DB6CE863CF4C33F73DA10D83163856CDF362C12BF133D31CCE404E713E4B84AB2B9B08079F63D60CD954FEAED60F75DA4164236A03AC0CAD03545C022A40CA841A205C11CB6B954E781103608BB85B2BAC58FB25D89A0DA8D8F5CBD01AA1F9CA5436CE5EA78FB267A5352846DEEBB050C3D11884BC78353BDE4329A6B8ACAA983EBEF0106FB8D63131182D0AEAF05C0D4A2A3A33BA960AD3ECD692CE211BE2926DA425C97D3268A9E8CCE85A1236DAAD248D5330C02DD84845CD2D7CA4C956443ACADDA6AC9B3A798A9428983A865CAAE90D8A229FAC6AB955A2C49AE78955B36FE6C3538EC21CC371134DE651296DC989D118ADB0540BAC41D22B3F4ED805626881789C67E6850A99766F352CFF05CBFAF6A90E62B10F14D4FC6F7129AC5CDD37B65AD517111057D0C1903B3459145D33FCFAE6164F7543018A3581FB190DD29098FD2B73EBFCFAAEDE3E2F5111A68E24BFE23F29CA52BCDCA6E67B8D8B3A27C619A3D27B597F9CCC10266D17BE675DDF267FD48C5284A7EA28A690D5CEC6CDE4C60C192BD9411C3E549D08AF33AB44564A1D40140DC4A825362860B5BAFEA8CDDC933A66B3A63FA29460528794AA0648594F23690859AF580BCFA0513D457F0E6AE2481D5DADED8FAC4921A9436BAAD7C0D6C82CD7F547D56499D48135D5FDB1AB9413790DDDE37DCB786C5977E3CA0FB69BED5C068CD75910C7D9F86AF7F775A05AF1402C7143AF8089F2BD3426E3E96E5D63CAC3199B199301C3BCEE342EBE9BCB4EEB6DBD19B3719BDD58DADB6EF3CD78C34CF6550D4339DBC92425F7F28C279DE5A6E25CD5FD78463968E524B655A811B6F59784E170C20926F39F8359E063BE8817043788F84B9CB03C83C33E3E3C3A961EE0ECCF63182749BC40732E35BD88698ED91692B1C8138ADD4714ABA9111B3C18A94095A8F335F1F0F399FD6BD6EA340B60F0BFB2E203EB3AF94CFC9F53A87888536CFDA6A67A8E9340DF7EC2DAD3E70EFDB57AFDBF2F79D303EB2E8619736A1D4ABA5C67849B8F2006499337DD409AB59F46BCDD09D57879A0459526C4FA0F0D163E1BE5914121E53F42F4FCCFA1A2691F126C84A8792C3016DE282A343D065807CBF810C0834F963D0418D659FDC3807544333E0AF0C97030F94940FF65A868B9C3AD467324DAC69294E9B933A57AA3FCCA5DEF4D4AE6F546135DCDAE1E00B74106F51A96F1C6928F47DB1D35B9C5A361EFD2B45F3DA1785F7288ABEC8EDDA60E6F335BB8E54EE82F9524BC07696D9A349DDDA7026FDBD64C61DC3DCFA71C96F0BB67C62692B7769FD6BB6D63338579F7DCD80625EFEE99ADED6AFFDCB1A5F5DE42779E8AAB661519AE6374B1E0AE54DB3C700E27FC050523C83DCAFC85A43EB7CBC4AC321623C38AC4CCD49C54263356268EC257A168673BACAF62C36FEDACA069676B48C56CE32DD6FF56DE82A69DB721C1711749C2DA14435DE276C73AD69601F59692821B3DE9C841EFF2595BEFD6DF520EF0284A69CC1EC31DF1DB49F91D4525634E9D0129BEEA752FEC9DB55F5484FD3BF1571504FF7D4582DDC6AE59D25C93252D366F49A282448AD0DC60863CD852CF63E62F91CBA09AC798B327DE59DC8EDF742CB0774DEE5216A50CBA8CC345D008787127A08D7F96C7DC94797A1765BF56324617404C9FC7E6EFC80FA91F78A5DC579A989001827B1722A2CBC792F1C8EEEAA544BAA5A42790505FE9143DE0300A002CB92373F484D7910DCCEF235E21F7A58A009A40BA07A2A9F6E9858F56310A138151B5874FB0612F7CFEEEFFB9B517EC58540000, N'6.1.3-40302')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'Member')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Super Admin')
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Google', N'104044562490672561869', N'7bd93525-a288-43fd-84f8-40ec4b7c50bd')
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Google', N'112550343958679869633', N'7bfc728a-345c-4fc2-95e1-099f788920ec')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7d3d080d-934a-49c7-9c5c-01fd9f29d8ea', N'1')
GO
--INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', N'2')
--GO
--INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'eb93e099-b969-4404-8fca-6a5218028044', N'2')
--GO
--INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5efeb250-156b-44da-a474-f50b55c5dcb0', N'3')
--GO
--INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'99fbc0a5-24bf-47f5-ac9e-f4250ea70067', N'3')
--GO
--INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5efeb250-156b-44da-a474-f50b55c5dcb0', N'shwfgsgndhu@kiwitech.com', 0, N'AC6rpQHi1UxRfOod2b4vWRyNGxtbEZmfuNrqGK+L9PB4aqnefzxoTNu+jbClmg4cgg==', N'85b29958-79b3-4d05-9e10-0de0c5907d57', NULL, 0, 0, NULL, 1, 0, N'shwfgsgndhu@kiwitech.com')
--GO
--INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', N'shweta.sindhu123@kiwitech.com', 0, NULL, N'fd19a647-60d3-4a05-8ca3-3d5ce6f0309f', NULL, 0, 0, NULL, 1, 0, N'shweta.sindhu123@kiwitech.com')
--GO
--INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7bfc728a-345c-4fc2-95e1-099f788920ec', N'just.shweta29@gmail.com', 0, NULL, N'8dcd65fd-62d3-4a31-be0b-855d0957c92b', NULL, 0, 0, NULL, 1, 0, N'just.shweta29@gmail.com')
--GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7d3d080d-934a-49c7-9c5c-01fd9f29d8ea', N'shweta.sindhu123567@kiwitech.com', 0, N'AFMyApJH2slITa4orP0HwC5o4X3WirWz3Jm1xA/Opd6s41rgM5GOCzHHDEUB+ZeGFA==', N'67754a82-b62d-400c-b995-a5a0a652fce7', NULL, 0, 0, NULL, 1, 0, N'shweta.sindhu123567@kiwitech.com')
GO
--INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'99fbc0a5-24bf-47f5-ac9e-f4250ea70067', N'g@g.com', 0, N'AEfGT4L+N21wLzupixK7ewltc3BZpglpFyxfRzg7ZyaK7iCuJ5THsy1+t6m7KrrYFw==', N'0129bcd6-d3fc-45ec-9838-819e004b8d5d', NULL, 0, 0, NULL, 1, 0, N'g@g.com')
--GO
--INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'eb93e099-b969-4404-8fca-6a5218028044', N'w@w.com', 0, N'AG8Nr8ASfGRo6OBcWdZ7kgnTPIlzwXBXRjnoCYzX45HcKXMX+d55QAGLn1lDsY9Svw==', N'86208fa7-1bdc-4059-bc41-490db6018a58', NULL, 0, 0, NULL, 1, 0, N'w@w.com')
--GO
SET IDENTITY_INSERT [dbo].[Country] ON 

GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (1, N' United States                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (2, N' Afghanistan                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (3, N' Albania                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (4, N' Algeria                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (5, N' American Samoa                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (6, N' Andorra                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (7, N' Angola                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (8, N' Anguilla                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (9, N' Antigua and Barbuda                   ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (10, N' Argentina                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (11, N' Armenia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (12, N' Aruba                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (13, N' Australia                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (14, N' Austria                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (15, N' Azerbaijan                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (16, N' The Bahamas                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (17, N' Bahrain                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (18, N' Bangladesh                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (19, N' Barbados                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (20, N' Belarus                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (21, N' Belgium                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (22, N' Belize                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (23, N' Benin                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (24, N' Bermuda                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (25, N' Bhutan                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (26, N' Bolivia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (27, N' Bosnia and Herzegovina                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (28, N' Botswana                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (29, N' Brazil                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (30, N' Brunei                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (31, N' Bulgaria                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (32, N' Burkina Faso                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (33, N' Burundi                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (34, N' Cambodia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (35, N' Cameroon                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (36, N' Canada                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (37, N' Cape Verde                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (38, N' Cayman Islands                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (39, N' Central African Republic              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (40, N' Chad                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (41, N' Chile                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (42, N' People Republic of China            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (43, N' Republic of China                     ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (44, N' Christmas Island                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (45, N' Cocos (Keeling) Islands               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (46, N' Colombia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (47, N' Comoros                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (48, N' Congo                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (49, N' Cook Islands                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (50, N' Costa Rica                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (51, N' Cote dIvoire                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (52, N' Croatia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (53, N' Cuba                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (54, N' Cyprus                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (55, N' Czech Republic                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (56, N' Denmark                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (57, N' Djibouti                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (58, N' Dominica                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (59, N' Dominican Republic                    ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (60, N' Ecuador                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (61, N' Egypt                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (62, N' El Salvador                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (63, N' Equatorial Guinea                     ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (64, N' Eritrea                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (65, N' Estonia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (66, N' Ethiopia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (67, N' Falkland Islands                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (68, N' Faroe Islands                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (69, N' Fiji                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (70, N' Finland                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (71, N' France                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (72, N' French Polynesia                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (73, N' Gabon                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (74, N' The Gambia                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (75, N' Georgia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (76, N' Germany                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (77, N' Ghana                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (78, N' Gibraltar                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (79, N' Greece                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (80, N' Greenland                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (81, N' Grenada                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (82, N' Guadeloupe                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (83, N' Guam                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (84, N' Guatemala                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (85, N' Guernsey                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (86, N' Guinea                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (87, N' Guinea-Bissau                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (88, N' Guyana                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (89, N' Haiti                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (90, N' Honduras                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (91, N' Hong Kong                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (92, N' Hungary                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (93, N' Iceland                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (94, N' India                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (95, N' Indonesia                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (96, N' Iran                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (97, N' Iraq                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (98, N' Ireland                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (99, N' Israel                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (100, N' Italy                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (101, N' Jamaica                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (102, N' Japan                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (103, N' Jersey                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (104, N' Jordan                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (105, N' Kazakhstan                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (106, N' Kenya                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (107, N' Kiribati                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (108, N' North Korea                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (109, N' South Korea                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (110, N' Kosovo                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (111, N' Kuwait                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (112, N' Kyrgyzstan                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (113, N' Laos                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (114, N' Latvia                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (115, N' Lebanon                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (116, N' Lesotho                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (117, N' Liberia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (118, N' Libya                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (119, N' Liechtenstein                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (120, N' Lithuania                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (121, N' Luxembourg                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (122, N' Macau                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (123, N' Macedonia                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (124, N' Madagascar                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (125, N' Malawi                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (126, N' Malaysia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (127, N' Maldives                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (128, N' Mali                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (129, N' Malta                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (130, N' Marshall Islands                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (131, N' Martinique                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (132, N' Mauritania                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (133, N' Mauritius                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (134, N' Mayotte                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (135, N' Mexico                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (136, N' Micronesia                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (137, N' Moldova                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (138, N' Monaco                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (139, N' Mongolia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (140, N' Montenegro                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (141, N' Montserrat                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (142, N' Morocco                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (143, N' Mozambique                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (144, N' Myanmar                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (145, N' Nagorno-Karabakh                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (146, N' Namibia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (147, N' Nauru                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (148, N' Nepal                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (149, N' Netherlands                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (150, N' Netherlands Antilles                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (151, N' New Caledonia                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (152, N' New Zealand                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (153, N' Nicaragua                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (154, N' Niger                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (155, N' Nigeria                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (156, N' Niue                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (157, N' Norfolk Island                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (158, N' Turkish Republic of Northern Cyprus   ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (159, N' Northern Mariana                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (160, N' Norway                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (161, N' Oman                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (162, N' Pakistan                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (163, N' Palau                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (164, N' Palestine                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (165, N' Panama                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (166, N' Papua New Guinea                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (167, N' Paraguay                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (168, N' Peru                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (169, N' Philippines                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (170, N' Pitcairn Islands                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (171, N' Poland                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (172, N' Portugal                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (173, N' Puerto Rico                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (174, N' Qatar                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (175, N' Romania                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (176, N' Russia                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (177, N' Rwanda                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (178, N' Saint Barthelemy                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (179, N' Saint Helena                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (180, N' Saint Kitts and Nevis                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (181, N' Saint Lucia                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (182, N' Saint Martin                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (183, N' Saint Pierre and Miquelon             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (184, N' Saint Vincent and the Grenadines      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (185, N' Samoa                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (186, N' San Marino                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (187, N' Sao Tome and Principe                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (188, N' Saudi Arabia                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (189, N' Senegal                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (190, N' Serbia                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (191, N' Seychelles                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (192, N' Sierra Leone                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (193, N' Singapore                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (194, N' Slovakia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (195, N' Slovenia                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (196, N' Solomon Islands                       ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (197, N' Somalia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (198, N' Somaliland                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (199, N' South Africa                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (200, N' South Ossetia                         ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (201, N' Spain                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (202, N' Sri Lanka                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (203, N' Sudan                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (204, N' Suriname                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (205, N' Svalbard                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (206, N' Swaziland                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (207, N' Sweden                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (208, N' Switzerland                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (209, N' Syria                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (210, N' Taiwan                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (211, N' Tajikistan                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (212, N' Tanzania                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (213, N' Thailand                              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (214, N' Timor-Leste                           ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (215, N' Togo                                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (216, N' Tokelau                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (217, N' Tonga                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (218, N' Transnistria Pridnestrovie            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (219, N' Trinidad and Tobago                   ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (220, N' Tristan da Cunha                      ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (221, N' Tunisia                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (222, N' Turkey                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (223, N' Turkmenistan                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (224, N' Turks and Caicos Islands              ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (225, N' Tuvalu                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (226, N' Uganda                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (227, N' Ukraine                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (228, N' United Arab Emirates                  ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (229, N' United Kingdom                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (230, N' Uruguay                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (231, N' Uzbekistan                            ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (232, N' Vanuatu                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (233, N' Vatican City                          ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (234, N' Venezuela                             ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (235, N' Vietnam                               ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (236, N' British Virgin Islands                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (237, N' US Virgin Islands                     ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (238, N' Wallis and Futuna                     ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (239, N' Western Sahara                        ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (240, N' Yemen                                 ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (241, N' Zambia                                ')
GO
INSERT [dbo].[Country] ([CountryID], [Country]) VALUES (242, N' Zimbabwe                              ')
GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From]) VALUES (N'401e6a18-e77c-4bee-9991-e982be07daf4', N'email_friend', N'just.shweta29@gmail.com', NULL, NULL, N'sdfsadf', N'<p>sadfasdfasdfasdfsdaf dsfsdfsdfsdafasdf</p>
', NULL)
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From]) VALUES (N'51478eed-9e17-492e-b945-d78ad1aa4732', NULL, N'just.shweta29@gmail.com', NULL, NULL, N' $$DealOrderNumberID$$', N'<p>sdfasdfasdfsadfsdfasdfasdfasdf</p>

<p>$$TicketOrderNumberID$$</p>

<p>$$EventAddressID$$</p>

<p>&nbsp;</p>

<p>$$DealOrderNumberID$$</p>
', NULL)
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From]) VALUES (N'800e24d9-93b8-41f6-aa95-998280afd943', N'email_welcome', N'just.shweta29@gmail.com', NULL, NULL, N'asdasdasd $$DealOrderNumberID$$', N'<p>$$asdasdasdasdasdasdasdUser$$ &nbsp; &nbsp; &nbsp;$$TicketOrderNumberID$$</p>
', NULL)
GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From]) VALUES (N'8970f6ce-3ab1-411d-bfa7-a105679d3e15', N'''email_welcome''', N'dfgsfg', N'dfgdsfg', N'dfgdfg', N'fgdfg', N'<p>dfgdsfgsdfgdsfgdsfgdsfgdsfg</p>
', NULL)
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (1, N'MyAccount', N'MyAccountEmailRequiredUI', N'fdgvdfgghfg')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (2, N'MyAccount', N'MyAccountEmailValidationUI', N'dfgsdfgfghfgh')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (3, N'MyAccount', N'MyAccountEmailAlreadyExistSY', N'dsfgsdfgfghfgh')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (4, N'MyAccount', N'MyAccountSuccessInitSY', N'sdfgsdfgfghfgh')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (5, N'MyAccount', N'MyAccountSuccessEmailSY', N'dsfgsdfgfghfgh')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (6, N'MyAccount', N'MyAccountSuccessPasswordSY', N'sdfgsdfgfghfgh')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (7, N'Login', N'LoginEmailValidationUI', N'Invalid Email')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (8, N'Login', N'LoginPasswordValidationUI', N'Invalid Password')
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (9, N'Login', N'LoginEmailRequiredUI', NULL)
GO
INSERT [dbo].[Messages] ([Id], [Form_Name], [Form_Tag], [Message]) VALUES (10, N'Login', N'LoginEmailNotExistSy', NULL)
GO
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[Permission_Detail] ON 

GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (1, N'Event', N'APP')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (2, N'Deal', N'APP')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (3, N'Users', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (4, N'Events', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (5, N'Tickets', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (6, N'EMails', N'CMS')
GO
INSERT [dbo].[Permission_Detail] ([Permission_Id], [Permission_Desc], [Permission_Category]) VALUES (7, N'Messages', N'CMS')
GO
SET IDENTITY_INSERT [dbo].[Permission_Detail] OFF
GO
SET IDENTITY_INSERT [dbo].[Profile] ON 

--GO
--INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus]) VALUES (33, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', N'Test', N'Last', N'shweta.sindhu123@kiwitech.com', N'fddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddgdddddddddddddddddddddddddddddddddddddddddddddddddddddddddbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn', N'fddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddgdddddddddddddddddddddddddddddddddddddddddddddddddddddddddbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn', N'hajjah', N'jhjhj', NULL, 1, N'121-212-1212', NULL, N'http:www.url.com', NULL, NULL, NULL, NULL, N'DSC01345.JPG', NULL, NULL, NULL, NULL, NULL, N'121-212-1212', N'image/jpeg', NULL, N'--', N'N', N'Y', N'Y')
--GO
--INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus]) VALUES (36, N'5efeb250-156b-44da-a474-f50b55c5dcb0', N'FIrst', N'Last', N'shwfgsgndhu@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'--', N'N', N'N', N'N')
GO
INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus]) VALUES (40, N'7d3d080d-934a-49c7-9c5c-01fd9f29d8ea', N'SuperAdmin', N'SuperAdmin', N'shweta.sindhu123567@kiwitech.com', NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
--GO
--INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus]) VALUES (41, N'7bfc728a-345c-4fc2-95e1-099f788920ec', N'Shweta', N's', N'just.shweta29@gmail.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Desert.jpg', NULL, NULL, NULL, NULL, NULL, NULL, N'image/jpeg', NULL, N'--', N'Y', N'Y', N'Y')
--GO
--INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus]) VALUES (42, N'99fbc0a5-24bf-47f5-ac9e-f4250ea70067', N'Google', N'Yahoo', N'g@g.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'--', N'N', N'N', N'N')
--GO
--INSERT [dbo].[Profile] ([ProfileID], [UserID], [FirstName], [LastName], [Email], [StreetAddressLine1], [StreetAddressLine2], [City], [State], [Zip], [CountryID], [MainPhone], [SecondPhone], [WebsiteURL], [TwitterURL], [FacebookURL], [GooglePlusURL], [LinkedInURL], [UserProfileImage], [OrganizerName], [SecondaryEmail], [OrganizerDescription], [AddedOn], [UpdateOn], [WorkPhone], [ContentType], [Gender], [DateofBirth], [Organiser], [Merchant], [UserStatus]) VALUES (43, N'eb93e099-b969-4404-8fca-6a5218028044', N'James', N'Martin', N'w@w.com', NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'--', N'N', N'Y', N'Y')
GO
SET IDENTITY_INSERT [dbo].[Profile] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

GO
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (1, N'Active')
GO
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (2, N'Not Active')
GO
INSERT [dbo].[Status] ([StatusID], [StatusName]) VALUES (3, N'Pending')
GO
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[User_Permission_Detail] ON 

--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (53, N'5c64d07f-bf17-40df-9853-53fdee89659c', 3, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (56, N'5efeb250-156b-44da-a474-f50b55c5dcb0', 1, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (57, N'5efeb250-156b-44da-a474-f50b55c5dcb0', 2, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (61, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 1, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (62, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 2, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (63, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 3, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (64, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 4, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (65, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 5, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (66, N'266bce4b-f1de-4c49-a0dc-9b2cddaee09c', 7, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (72, N'99fbc0a5-24bf-47f5-ac9e-f4250ea70067', 1, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (73, N'99fbc0a5-24bf-47f5-ac9e-f4250ea70067', 2, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (74, N'99fbc0a5-24bf-47f5-ac9e-f4250ea70067', 3, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (78, N'eb93e099-b969-4404-8fca-6a5218028044', 1, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (79, N'eb93e099-b969-4404-8fca-6a5218028044', 2, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (80, N'eb93e099-b969-4404-8fca-6a5218028044', 3, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (81, N'eb93e099-b969-4404-8fca-6a5218028044', 6, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (87, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', 2, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (88, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', 3, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (89, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', 4, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (90, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', 5, NULL)
--GO
--INSERT [dbo].[User_Permission_Detail] ([UP_Id], [UP_User_Id], [UP_Permission_Id], [UP_Flag]) VALUES (91, N'7bd93525-a288-43fd-84f8-40ec4b7c50bd', 6, NULL)
--GO
SET IDENTITY_INSERT [dbo].[User_Permission_Detail] OFF
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
--ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
--REFERENCES [dbo].[AspNetUsers] ([Id])
--ON DELETE CASCADE
GO
--ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
--GO
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
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_Profile] FOREIGN KEY([UserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_Profile]
GO
