USE [master]
GO
/****** Object:  Database [BasicWeb]    Script Date: 9/20/2023 1:54:30 PM ******/
CREATE DATABASE [BasicWeb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BasicWeb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQL2017\MSSQL\DATA\BasicWeb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BasicWeb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQL2017\MSSQL\DATA\BasicWeb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BasicWeb] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BasicWeb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BasicWeb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BasicWeb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BasicWeb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BasicWeb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BasicWeb] SET ARITHABORT OFF 
GO
ALTER DATABASE [BasicWeb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BasicWeb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BasicWeb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BasicWeb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BasicWeb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BasicWeb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BasicWeb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BasicWeb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BasicWeb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BasicWeb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BasicWeb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BasicWeb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BasicWeb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BasicWeb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BasicWeb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BasicWeb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BasicWeb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BasicWeb] SET RECOVERY FULL 
GO
ALTER DATABASE [BasicWeb] SET  MULTI_USER 
GO
ALTER DATABASE [BasicWeb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BasicWeb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BasicWeb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BasicWeb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BasicWeb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BasicWeb', N'ON'
GO
ALTER DATABASE [BasicWeb] SET QUERY_STORE = OFF
GO
USE [BasicWeb]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/20/2023 1:54:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[PassWord] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Tel] [nvarchar](50) NULL,
	[Disabled] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetUsersbyEmail]    Script Date: 9/20/2023 1:54:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[GetUsersbyEmail]
@searchstring nvarchar(50)
as
begin
select * from Users where Email like '%' + @searchstring + '%'
end
GO
/****** Object:  StoredProcedure [dbo].[pcd_CheckID]    Script Date: 9/20/2023 1:54:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pcd_CheckID]
@userID nvarchar(50)
as begin
SELECT COUNT(*) AS count
FROM Users
WHERE UserID = @userID
end
GO
/****** Object:  StoredProcedure [dbo].[pcd_DeleteUsers]    Script Date: 9/20/2023 1:54:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pcd_DeleteUsers]
@userID nvarchar(50)
as 
begin
delete from Users where UserID = @userID
end
GO
/****** Object:  StoredProcedure [dbo].[pcd_GetUsers]    Script Date: 9/20/2023 1:54:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[pcd_GetUsers]
as
begin
select * from Users
end
GO
/****** Object:  StoredProcedure [dbo].[pcd_GetUsersById]    Script Date: 9/20/2023 1:54:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pcd_GetUsersById]
@userID nvarchar(50)
 as 
 begin
 select *from Users where UserID = @userID
 end
GO
/****** Object:  StoredProcedure [dbo].[pcd_SaveUsers]    Script Date: 9/20/2023 1:54:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pcd_SaveUsers]
@userid nvarchar(50),
@username nvarchar(50),
@password nvarchar(50),
@email nvarchar(50),
@tel nvarchar,
@disabled tinyint
as
begin 
insert into Users(UserID, UserName,Password, Email,Tel,Disabled) values(@userid,@username, @password,@email, @tel, @disabled)
end

GO
/****** Object:  StoredProcedure [dbo].[pcd_UpdateUser]    Script Date: 9/20/2023 1:54:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pcd_UpdateUser]
 @userid nvarchar(50),
@username nvarchar(50),
@password nvarchar(50),
@email nvarchar(50),
@tel nvarchar(50),
@disabled tinyint
as
begin 
update Users set 
UserName = @username,
Password = @password,
Email= @email,
Tel= @tel,
Disabled = @disabled
from Users where UserID = @userid
end


GO
USE [master]
GO
ALTER DATABASE [BasicWeb] SET  READ_WRITE 
GO
