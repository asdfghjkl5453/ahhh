USE [master]
GO
/****** Object:  Database [Grading]    Script Date: 2/23/2023 9:51:51 PM ******/
CREATE DATABASE [Grading]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Grading', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Grading.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Grading_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Grading_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Grading] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Grading].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Grading] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Grading] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Grading] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Grading] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Grading] SET ARITHABORT OFF 
GO
ALTER DATABASE [Grading] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Grading] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Grading] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Grading] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Grading] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Grading] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Grading] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Grading] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Grading] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Grading] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Grading] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Grading] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Grading] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Grading] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Grading] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Grading] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Grading] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Grading] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Grading] SET  MULTI_USER 
GO
ALTER DATABASE [Grading] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Grading] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Grading] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Grading] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Grading] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Grading] SET QUERY_STORE = OFF
GO
USE [Grading]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Grading]
GO
/****** Object:  Table [dbo].[tblStudent]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStudent](
	[lrn] [varchar](50) NOT NULL,
	[lname] [varchar](50) NULL,
	[fname] [varchar](50) NULL,
	[mname] [varchar](50) NULL,
	[program] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[contact] [varchar](50) NULL,
 CONSTRAINT [PK_tblStudent] PRIMARY KEY CLUSTERED 
(
	[lrn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblCourse]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCourse](
	[ccode] [varchar](50) NOT NULL,
	[cdesc] [varchar](50) NULL,
 CONSTRAINT [PK_tblCourse] PRIMARY KEY CLUSTERED 
(
	[ccode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblGrade]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblGrade](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[lrn] [varchar](50) NULL,
	[ccode] [varchar](50) NULL,
	[prelim] [decimal](18, 2) NULL,
	[midterm] [decimal](18, 2) NULL,
	[prefi] [decimal](18, 2) NULL,
	[final] [decimal](18, 2) NULL,
	[ave] [decimal](18, 2) NULL,
	[remarks] [varchar](50) NULL,
	[ay] [varchar](50) NULL,
 CONSTRAINT [PK_tblGrade] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vw_Grade]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vw_Grade] as SELECT        dbo.tblGrade.id, dbo.tblGrade.lrn, concat(dbo.tblStudent.lname, ', ' , dbo.tblStudent.fname, ' ' , dbo.tblStudent.mname) as fullname, dbo.tblStudent.program, dbo.tblGrade.ccode, dbo.tblCourse.cdesc, dbo.tblGrade.ay, dbo.tblGrade.prelim, 
                         dbo.tblGrade.midterm, dbo.tblGrade.prefi, dbo.tblGrade.final, dbo.tblGrade.ave, dbo.tblGrade.remarks
FROM            dbo.tblCourse INNER JOIN
                         dbo.tblGrade ON dbo.tblCourse.ccode = dbo.tblGrade.ccode INNER JOIN
                         dbo.tblStudent ON dbo.tblGrade.lrn = dbo.tblStudent.lrn
GO
/****** Object:  Table [dbo].[tblAY]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAY](
	[code] [varchar](50) NOT NULL,
	[year] [varchar](50) NULL,
	[term] [varchar](50) NULL,
 CONSTRAINT [PK_tblAY] PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[tblGrade] ADD  CONSTRAINT [DF_tblGrade_prelim]  DEFAULT ((0)) FOR [prelim]
GO
ALTER TABLE [dbo].[tblGrade] ADD  CONSTRAINT [DF_tblGrade_midterm]  DEFAULT ((0)) FOR [midterm]
GO
ALTER TABLE [dbo].[tblGrade] ADD  CONSTRAINT [DF_tblGrade_prefi]  DEFAULT ((0)) FOR [prefi]
GO
ALTER TABLE [dbo].[tblGrade] ADD  CONSTRAINT [DF_tblGrade_final]  DEFAULT ((0)) FOR [final]
GO
ALTER TABLE [dbo].[tblGrade] ADD  CONSTRAINT [DF_tblGrade_ave]  DEFAULT ((0)) FOR [ave]
GO
/****** Object:  StoredProcedure [dbo].[sp_classlist_add]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_classlist_add]
	-- Add the parameters for the stored procedure here
	@lrn varchar(50),
	@ccode varchar(50),
	@ay varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   insert into tblGrade (lrn,ccode,ay) values(@lrn, @ccode,@ay)
END

GO
/****** Object:  StoredProcedure [dbo].[sp_classlist_update]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_classlist_update]
	-- Add the parameters for the stored procedure here
	@id int,
	@lrn varchar(50),
	@ccode varchar(50),
	@ay varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   update tblGrade set lrn=@lrn,ccode=@ccode,ay=@ay where id = @id
END

GO
/****** Object:  StoredProcedure [dbo].[sp_course_add]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_course_add]
	-- Add the parameters for the stored procedure here
	@ccode varchar(50),
	@cdesc varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   insert into tblCourse (ccode,cdesc) values(@ccode,@cdesc)
END

GO
/****** Object:  StoredProcedure [dbo].[sp_course_update]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_course_update]
	-- Add the parameters for the stored procedure here
	@ccode varchar(50),
	@cdesc varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   update tblCourse set cdesc=@cdesc where ccode = @ccode
END

GO
/****** Object:  StoredProcedure [dbo].[sp_grade_update]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_grade_update]
	-- Add the parameters for the stored procedure here
	@id int,
	@prelim decimal(10,2),
	@midterm decimal(10,2),
	@prefi decimal(10,2),
	@final decimal(10,2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   update tblGrade set prelim=@prelim, midterm=@midterm, prefi=@prefi, final=@final where id = @id
END

GO
/****** Object:  StoredProcedure [dbo].[sp_student_add]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_student_add]
	-- Add the parameters for the stored procedure here
	@lrn varchar(50),
	@lname varchar(50),
	@fname varchar(50),
	@mname varchar(50),
	@program varchar(50),
	@address varchar(50),
	@contact varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblStudent(lrn, lname,fname, mname, program, address, contact)values(@lrn, @lname,@fname, @mname,@program, @address, @contact)
END

GO
/****** Object:  StoredProcedure [dbo].[sp_student_update]    Script Date: 2/23/2023 9:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_student_update]
	-- Add the parameters for the stored procedure here
	@lrn varchar(50),
	@lname varchar(50),
	@fname varchar(50),
	@mname varchar(50),
	@program varchar(50),
	@address varchar(50),
	@contact varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   update tblStudent set lname=@lname,fname=@fname, mname=@mname, program=@program, address=@address, contact=@contact where lrn = @lrn
END

GO
USE [master]
GO
ALTER DATABASE [Grading] SET  READ_WRITE 
GO
