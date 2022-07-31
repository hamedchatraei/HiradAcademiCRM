USE [master]
GO
/****** Object:  Database [HiradCRM_DB]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE DATABASE [HiradCRM_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HiradCRM_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HiradCRM_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HiradCRM_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HiradCRM_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [HiradCRM_DB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HiradCRM_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HiradCRM_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HiradCRM_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HiradCRM_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HiradCRM_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HiradCRM_DB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [HiradCRM_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [HiradCRM_DB] SET  MULTI_USER 
GO
ALTER DATABASE [HiradCRM_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HiradCRM_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HiradCRM_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HiradCRM_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HiradCRM_DB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'HiradCRM_DB', N'ON'
GO
ALTER DATABASE [HiradCRM_DB] SET QUERY_STORE = OFF
GO
USE [HiradCRM_DB]
GO
/****** Object:  Table [dbo].[Advisers]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Advisers](
	[AdviserId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Family] [nvarchar](200) NOT NULL,
	[JobMobile] [nvarchar](11) NOT NULL,
	[PersonalMobile] [nvarchar](11) NOT NULL,
	[Study] [nvarchar](200) NOT NULL,
	[AccountNumber] [nvarchar](16) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Advisers] PRIMARY KEY CLUSTERED 
(
	[AdviserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[StudyId] [int] NOT NULL,
	[GradeId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Family] [nvarchar](200) NOT NULL,
	[FatherName] [nvarchar](200) NOT NULL,
	[Mobile1] [nvarchar](11) NOT NULL,
	[Mobile2] [nvarchar](11) NOT NULL,
	[Mobile3] [nvarchar](11) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[City] [nvarchar](200) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pays]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pays](
	[PayId] [int] IDENTITY(1,1) NOT NULL,
	[RegisterId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[PayDate] [datetime2](7) NOT NULL,
	[PursuitCode] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Pays] PRIMARY KEY CLUSTERED 
(
	[PayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewPay]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewPay]
AS
SELECT       dbo.Pays.PayId, dbo.Pays.RegisterId, dbo.Students.StudentId, dbo.Advisers.AdviserId, dbo.Students.Name + ' ' + dbo.Students.Family AS StudentNameFamily, dbo.Advisers.Name + ' ' + dbo.Advisers.Family AS AdviserNameFamily, 
                         dbo.Pays.Amount, dbo.Pays.PayDate, dbo.Pays.PursuitCode, dbo.Pays.Description
FROM            dbo.Pays CROSS JOIN
                         dbo.Advisers CROSS JOIN
                         dbo.Students
GO
/****** Object:  Table [dbo].[AdviserType]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdviserType](
	[AdviserTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](max) NULL,
 CONSTRAINT [PK_AdviserType] PRIMARY KEY CLUSTERED 
(
	[AdviserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegisterSAdvisers]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisterSAdvisers](
	[RAId] [int] IDENTITY(1,1) NOT NULL,
	[RegisterId] [int] NOT NULL,
	[AdviserId] [int] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[AdviserTypeId] [int] NOT NULL,
 CONSTRAINT [PK_RegisterSAdvisers] PRIMARY KEY CLUSTERED 
(
	[RAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewRegistersAdviserAndAdviserType]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewRegistersAdviserAndAdviserType]
AS
SELECT       dbo.RegisterSAdvisers.RegisterId, dbo.Advisers.AdviserId, dbo.Advisers.ParentId, dbo.Advisers.Name + ' ' + dbo.Advisers.Family AS AdviserNameFamily, dbo.Advisers.JobMobile, dbo.Advisers.PersonalMobile, dbo.Advisers.Study, 
                         dbo.Advisers.AccountNumber, dbo.RegisterSAdvisers.StartDate, dbo.RegisterSAdvisers.EndDate, dbo.RegisterSAdvisers.IsActive, dbo.RegisterSAdvisers.AdviserTypeId, dbo.AdviserType.Type AS AdviserType
FROM            dbo.Advisers INNER JOIN
                         dbo.RegisterSAdvisers ON dbo.Advisers.AdviserId = dbo.RegisterSAdvisers.AdviserId INNER JOIN
                         dbo.AdviserType ON dbo.RegisterSAdvisers.AdviserTypeId = dbo.AdviserType.AdviserTypeId
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grades](
	[GradeId] [int] IDENTITY(1,1) NOT NULL,
	[GradeTitle] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Grades] PRIMARY KEY CLUSTERED 
(
	[GradeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Studies]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Studies](
	[StudyId] [int] IDENTITY(1,1) NOT NULL,
	[StudyTitle] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Studies] PRIMARY KEY CLUSTERED 
(
	[StudyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registers]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registers](
	[RegisterId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[AdviserId] [int] NOT NULL,
	[PlanId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[RegisterDate] [datetime2](7) NOT NULL,
	[Discount] [int] NOT NULL,
	[FinalAmount] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[PayTypeId] [int] NOT NULL,
	[IsCancel] [bit] NOT NULL,
 CONSTRAINT [PK_Registers] PRIMARY KEY CLUSTERED 
(
	[RegisterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewStudents]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewStudents]
AS
SELECT       dbo.Students.StudentId, dbo.Grades.GradeId, dbo.Studies.StudyId, dbo.Grades.GradeTitle AS Grade, dbo.Studies.StudyTitle AS Study, dbo.Students.Name, dbo.Students.Family, dbo.Students.FatherName, dbo.Students.Mobile1, 
                         dbo.Students.Mobile2, dbo.Students.Mobile3, dbo.Students.PhoneNumber, dbo.Students.City, dbo.Registers.RegisterId, dbo.Students.IsDelete
FROM            dbo.Students INNER JOIN
                         dbo.Grades ON dbo.Students.GradeId = dbo.Grades.GradeId INNER JOIN
                         dbo.Studies ON dbo.Students.StudyId = dbo.Studies.StudyId INNER JOIN
                         dbo.Registers ON dbo.Students.StudentId = dbo.Registers.StudentId
GO
/****** Object:  Table [dbo].[AdviserGroups]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdviserGroups](
	[AdviserGroupId] [int] IDENTITY(1,1) NOT NULL,
	[AdviserId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[GroupAdviserGroupId] [int] NULL,
 CONSTRAINT [PK_AdviserGroups] PRIMARY KEY CLUSTERED 
(
	[AdviserGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewAdvisers]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewAdvisers]
AS
SELECT       dbo.Advisers.AdviserId, dbo.AdviserGroups.GroupId, dbo.Advisers.Name, dbo.Advisers.Family, dbo.Advisers.JobMobile, dbo.Advisers.PersonalMobile, dbo.Advisers.Study, dbo.Advisers.AccountNumber, dbo.Advisers.IsDelete
FROM            dbo.Advisers LEFT OUTER JOIN
                         dbo.AdviserGroups ON dbo.Advisers.AdviserId = dbo.AdviserGroups.AdviserId
GO
/****** Object:  View [dbo].[ViewUnRegisteredStudents]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewUnRegisteredStudents]
AS
SELECT       dbo.Students.StudentId, dbo.Grades.GradeId, dbo.Studies.StudyId, dbo.Registers.RegisterId, dbo.Grades.GradeTitle AS Grade, dbo.Studies.StudyTitle AS Study, dbo.Students.Name, dbo.Students.Family, dbo.Students.FatherName, 
                         dbo.Students.Mobile1, dbo.Students.Mobile2, dbo.Students.Mobile3, dbo.Students.PhoneNumber, dbo.Students.City, dbo.Students.IsDelete, dbo.Registers.IsCancel
FROM            dbo.Students INNER JOIN
                         dbo.Studies ON dbo.Students.StudyId = dbo.Studies.StudyId INNER JOIN
                         dbo.Grades ON dbo.Students.GradeId = dbo.Grades.GradeId LEFT OUTER JOIN
                         dbo.Registers ON dbo.Students.StudentId = dbo.Registers.StudentId
GO
/****** Object:  Table [dbo].[UnregisteredCalls]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnregisteredCalls](
	[UnregisteredCallsId] [int] IDENTITY(1,1) NOT NULL,
	[AdviserId] [int] NOT NULL,
	[CallDate] [datetime2](7) NOT NULL,
	[CallTime] [time](7) NOT NULL,
	[Subject] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsCall] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_UnregisteredCalls] PRIMARY KEY CLUSTERED 
(
	[UnregisteredCallsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewUnregisteredCalls]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewUnregisteredCalls]
AS
SELECT       dbo.UnregisteredCalls.UnregisteredCallsId, dbo.Advisers.AdviserId, dbo.Students.StudentId, dbo.UnregisteredCalls.CallDate, dbo.UnregisteredCalls.CallTime, dbo.UnregisteredCalls.Subject, dbo.UnregisteredCalls.Description, 
                         dbo.UnregisteredCalls.IsCall, dbo.Advisers.Name + ' ' + dbo.Advisers.Family AS AdviserNameFamily
FROM            dbo.UnregisteredCalls INNER JOIN
                         dbo.Advisers ON dbo.UnregisteredCalls.AdviserId = dbo.Advisers.AdviserId INNER JOIN
                         dbo.Students ON dbo.UnregisteredCalls.StudentId = dbo.Students.StudentId
GO
/****** Object:  Table [dbo].[PayTypes]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayTypes](
	[PayTypeId] [int] IDENTITY(1,1) NOT NULL,
	[PayTypeTitle] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PayTypes] PRIMARY KEY CLUSTERED 
(
	[PayTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[Origin] [nvarchar](200) NOT NULL,
	[CourseDate] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CourseTitle] [nvarchar](200) NOT NULL,
	[PercentAmount] [int] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plans]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plans](
	[PlanId] [int] IDENTITY(1,1) NOT NULL,
	[PlanTitle] [nvarchar](200) NOT NULL,
	[Amount] [int] NOT NULL,
	[Length] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Plans] PRIMARY KEY CLUSTERED 
(
	[PlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewRegisterAdviser]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewRegisterAdviser]
AS
SELECT       dbo.Registers.RegisterId, dbo.Students.StudentId, dbo.Advisers.AdviserId, dbo.Plans.PlanId, dbo.Courses.CourseId, dbo.Registers.RegisterDate, dbo.Registers.Discount, dbo.Registers.FinalAmount, 
                         dbo.Students.Name + ' ' + dbo.Students.Family AS StudentNameFamily, dbo.Advisers.Name + ' ' + dbo.Advisers.Family AS AdviserNameFamily, dbo.Plans.PlanTitle, dbo.Courses.CourseTitle, dbo.PayTypes.PayTypeId, 
                         dbo.PayTypes.PayTypeTitle
FROM            dbo.Registers INNER JOIN
                         dbo.Advisers ON dbo.Registers.AdviserId = dbo.Advisers.AdviserId INNER JOIN
                         dbo.Students ON dbo.Registers.StudentId = dbo.Students.StudentId INNER JOIN
                         dbo.Plans ON dbo.Registers.PlanId = dbo.Plans.PlanId INNER JOIN
                         dbo.Courses ON dbo.Registers.CourseId = dbo.Courses.CourseId INNER JOIN
                         dbo.PayTypes ON dbo.Registers.PayTypeId = dbo.PayTypes.PayTypeId
GO
/****** Object:  View [dbo].[ViewInformationRegisterAdviser]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewInformationRegisterAdviser]
AS
SELECT       dbo.Registers.RegisterId, dbo.Students.StudentId, dbo.RegisterSAdvisers.StartDate, dbo.RegisterSAdvisers.EndDate, dbo.RegisterSAdvisers.IsActive, dbo.Students.Name + ' ' + dbo.Students.Family AS StudentNameFamily, 
                         dbo.RegisterSAdvisers.AdviserId, dbo.AdviserType.AdviserTypeId, dbo.AdviserType.Type, dbo.Registers.PlanId, dbo.Registers.CourseId
FROM            dbo.RegisterSAdvisers INNER JOIN
                         dbo.Registers ON dbo.RegisterSAdvisers.RegisterId = dbo.Registers.RegisterId INNER JOIN
                         dbo.Students ON dbo.Registers.StudentId = dbo.Students.StudentId INNER JOIN
                         dbo.AdviserType ON dbo.RegisterSAdvisers.AdviserTypeId = dbo.AdviserType.AdviserTypeId
GO
/****** Object:  View [dbo].[ViewPayByRegister]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewPayByRegister]
AS
SELECT       dbo.RegisterSAdvisers.RegisterId, dbo.RegisterSAdvisers.AdviserId, dbo.RegisterSAdvisers.StartDate, dbo.RegisterSAdvisers.EndDate, dbo.RegisterSAdvisers.IsActive, dbo.RegisterSAdvisers.AdviserTypeId, dbo.Pays.PayId, 
                         dbo.Pays.Amount, dbo.Pays.PayDate, dbo.Pays.PursuitCode, dbo.Pays.Description, dbo.Courses.CourseId, dbo.Plans.PlanId, dbo.Courses.PercentAmount
FROM            dbo.RegisterSAdvisers INNER JOIN
                         dbo.Registers ON dbo.RegisterSAdvisers.RegisterId = dbo.Registers.RegisterId INNER JOIN
                         dbo.Pays ON dbo.Registers.RegisterId = dbo.Pays.RegisterId INNER JOIN
                         dbo.Courses ON dbo.Registers.CourseId = dbo.Courses.CourseId INNER JOIN
                         dbo.Plans ON dbo.Registers.PlanId = dbo.Plans.PlanId
WHERE        (dbo.RegisterSAdvisers.AdviserTypeId = 2)
GO
/****** Object:  Table [dbo].[AdviserInvoices]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdviserInvoices](
	[AdviserInvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[AdviserId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Amount] [int] NOT NULL,
	[TotalAmount] [int] NOT NULL,
	[PaySalaryDate] [datetime2](7) NOT NULL,
	[SalaryTypeId] [int] NOT NULL,
 CONSTRAINT [PK_AdviserInvoices] PRIMARY KEY CLUSTERED 
(
	[AdviserInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalaryTypes]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalaryTypes](
	[SalaryTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
 CONSTRAINT [PK_SalaryTypes] PRIMARY KEY CLUSTERED 
(
	[SalaryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewAdviserInvoice]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewAdviserInvoice]
AS
SELECT       dbo.AdviserInvoices.AdviserId, dbo.AdviserInvoices.Description, dbo.AdviserInvoices.Amount, dbo.AdviserInvoices.TotalAmount, dbo.AdviserInvoices.PaySalaryDate, dbo.SalaryTypes.SalaryTypeId, dbo.SalaryTypes.Title, 
                         dbo.Advisers.Name + ' ' + dbo.Advisers.Family AS AdviserNameFamily
FROM            dbo.AdviserInvoices INNER JOIN
                         dbo.SalaryTypes ON dbo.AdviserInvoices.SalaryTypeId = dbo.SalaryTypes.SalaryTypeId INNER JOIN
                         dbo.Advisers ON dbo.AdviserInvoices.AdviserId = dbo.Advisers.AdviserId
GO
/****** Object:  Table [dbo].[Cheques]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cheques](
	[ChequeId] [int] IDENTITY(1,1) NOT NULL,
	[RegisterId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[ChequeDate] [datetime2](7) NOT NULL,
	[Owner] [nvarchar](50) NOT NULL,
	[ChequeNumber] [nvarchar](50) NOT NULL,
	[AccountNumber] [nvarchar](50) NOT NULL,
	[ForWho] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[IsPass] [bit] NOT NULL,
 CONSTRAINT [PK_Cheques] PRIMARY KEY CLUSTERED 
(
	[ChequeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewChequeByRegister]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewChequeByRegister]
AS
SELECT       dbo.RegisterSAdvisers.RegisterId, dbo.RegisterSAdvisers.AdviserId, dbo.RegisterSAdvisers.StartDate, dbo.RegisterSAdvisers.EndDate, dbo.RegisterSAdvisers.IsActive, dbo.RegisterSAdvisers.AdviserTypeId, dbo.Cheques.ChequeId, 
                         dbo.Cheques.Amount, dbo.Cheques.ChequeDate, dbo.Cheques.Owner, dbo.Cheques.ChequeNumber, dbo.Cheques.AccountNumber, dbo.Cheques.ForWho, dbo.Cheques.Description, dbo.Cheques.IsPass, dbo.Courses.CourseId, 
                         dbo.Plans.PlanId, dbo.Courses.PercentAmount
FROM            dbo.RegisterSAdvisers INNER JOIN
                         dbo.Registers ON dbo.RegisterSAdvisers.RegisterId = dbo.Registers.RegisterId INNER JOIN
                         dbo.Cheques ON dbo.Registers.RegisterId = dbo.Cheques.RegisterId INNER JOIN
                         dbo.Courses ON dbo.Registers.CourseId = dbo.Courses.CourseId INNER JOIN
                         dbo.Plans ON dbo.Registers.PlanId = dbo.Plans.PlanId
WHERE        (dbo.RegisterSAdvisers.AdviserTypeId = 2)
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttributePlans]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttributePlans](
	[APId] [int] IDENTITY(1,1) NOT NULL,
	[PlanId] [int] NOT NULL,
	[PlanAttributeId] [int] NOT NULL,
 CONSTRAINT [PK_AttributePlans] PRIMARY KEY CLUSTERED 
(
	[APId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Calls]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calls](
	[CallId] [int] IDENTITY(1,1) NOT NULL,
	[RegisterId] [int] NOT NULL,
	[CallDate] [datetime2](7) NOT NULL,
	[CallTime] [time](7) NOT NULL,
	[Subject] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsCall] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Calls] PRIMARY KEY CLUSTERED 
(
	[CallId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FollowUps]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FollowUps](
	[FollowUpId] [int] IDENTITY(1,1) NOT NULL,
	[RegisterId] [int] NOT NULL,
	[FollowUpDateTime] [datetime2](7) NOT NULL,
	[Subject] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsFollowedUp] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_FollowUps] PRIMARY KEY CLUSTERED 
(
	[FollowUpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupAdvisers]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupAdvisers](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[GroupTitle] [nvarchar](200) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_GroupAdvisers] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Installments]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Installments](
	[InstallmentId] [int] IDENTITY(1,1) NOT NULL,
	[RegisterId] [int] NOT NULL,
	[InstallmentDate] [datetime2](7) NOT NULL,
	[Amount] [int] NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsPayed] [bit] NOT NULL,
 CONSTRAINT [PK_Installments] PRIMARY KEY CLUSTERED 
(
	[InstallmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionId] [int] IDENTITY(1,1) NOT NULL,
	[PermissionTitle] [nvarchar](200) NOT NULL,
	[ParentID] [int] NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlanAttributes]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanAttributes](
	[PlanAttributeId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NULL,
 CONSTRAINT [PK_PlanAttributes] PRIMARY KEY CLUSTERED 
(
	[PlanAttributeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[RP_Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[RP_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleTitle] [nvarchar](200) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StartDayOfMonths]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StartDayOfMonths](
	[SdmId] [int] IDENTITY(1,1) NOT NULL,
	[SdmValue] [int] NOT NULL,
 CONSTRAINT [PK_StartDayOfMonths] PRIMARY KEY CLUSTERED 
(
	[SdmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnregisteredFollowUps]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnregisteredFollowUps](
	[UnregisteredFollowUpId] [int] IDENTITY(1,1) NOT NULL,
	[AdviserId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[FollowUpDateTime] [datetime2](7) NOT NULL,
	[Subject] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsFollowedUp] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_UnregisteredFollowUps] PRIMARY KEY CLUSTERED 
(
	[UnregisteredFollowUpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UR_ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UR_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[UserAliasName] [nvarchar](200) NOT NULL,
	[Mobile] [nvarchar](11) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[UserAvatar] [nvarchar](200) NULL,
	[RegisterDate] [datetime2](7) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220419191527_mig_initialDataBase', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220514064828_mig_AddAdviserTypeForRegisterAdviser', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220516234225_mig_AddStartDayOfMonthToSettingsEntity', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220523125843_mig_UpdateTableRegistersAdviserEndTimeBecomeNullable', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220528071655_mig_AddUnregisteeredCallsAndUnregisteredFollowUp', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220528073756_mig_UpdateUnregisteredCalls', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220529124213_mig_UpdateCourseModel', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220601000236_mig_AddPayType', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220602215341_mig_UpdateRegistersAdviser', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220605094838_mig_AddSalaryAndSalaryTypeAndAdviserInvoice', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220605104909_mig_AddPercentAmountToCourse', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220605142038_mig_UpdateAdviserInvoiceAndRelationItWithSalaryTypeAndRemoveSalary', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220605142152_mig_RemoveSalary', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220611221322_mig_AddInstallment', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220611221546_mig_AddIsPayedToInstallment', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220617175358_mig_AddIsCancelToStudentEntity', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220617180842_mig_AddIsCancelToRegisterEntityAndRemoveIsCanceleFromStudentEntity', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220623114538_mig_UpdatePlan', N'6.0.4')
GO
SET IDENTITY_INSERT [dbo].[AdviserGroups] ON 

INSERT [dbo].[AdviserGroups] ([AdviserGroupId], [AdviserId], [GroupId], [GroupAdviserGroupId]) VALUES (1, 1, 2, NULL)
INSERT [dbo].[AdviserGroups] ([AdviserGroupId], [AdviserId], [GroupId], [GroupAdviserGroupId]) VALUES (2, 5, 2, NULL)
INSERT [dbo].[AdviserGroups] ([AdviserGroupId], [AdviserId], [GroupId], [GroupAdviserGroupId]) VALUES (3, 5, 3, NULL)
INSERT [dbo].[AdviserGroups] ([AdviserGroupId], [AdviserId], [GroupId], [GroupAdviserGroupId]) VALUES (4, 3, 2, NULL)
INSERT [dbo].[AdviserGroups] ([AdviserGroupId], [AdviserId], [GroupId], [GroupAdviserGroupId]) VALUES (5, 4, 4, NULL)
INSERT [dbo].[AdviserGroups] ([AdviserGroupId], [AdviserId], [GroupId], [GroupAdviserGroupId]) VALUES (6, 4, 3, NULL)
INSERT [dbo].[AdviserGroups] ([AdviserGroupId], [AdviserId], [GroupId], [GroupAdviserGroupId]) VALUES (7, 4, 5, NULL)
INSERT [dbo].[AdviserGroups] ([AdviserGroupId], [AdviserId], [GroupId], [GroupAdviserGroupId]) VALUES (31, 8, 2, NULL)
INSERT [dbo].[AdviserGroups] ([AdviserGroupId], [AdviserId], [GroupId], [GroupAdviserGroupId]) VALUES (32, 8, 4, NULL)
INSERT [dbo].[AdviserGroups] ([AdviserGroupId], [AdviserId], [GroupId], [GroupAdviserGroupId]) VALUES (33, 19, 2, NULL)
INSERT [dbo].[AdviserGroups] ([AdviserGroupId], [AdviserId], [GroupId], [GroupAdviserGroupId]) VALUES (34, 16, 2, NULL)
SET IDENTITY_INSERT [dbo].[AdviserGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[AdviserInvoices] ON 

INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (1, 1, N'دستی', 5000000, -5000000, CAST(N'2022-05-11T05:00:27.0000000' AS DateTime2), 5)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (73, 1, N'اردیبهشت و خرداد 1401', 2350000, -2650000, CAST(N'2022-06-07T19:23:59.6054381' AS DateTime2), 1)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (74, 3, N'اردیبهشت و خرداد 1401', 0, 0, CAST(N'2022-06-07T19:23:59.6162754' AS DateTime2), 1)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (75, 5, N'اردیبهشت و خرداد 1401', 0, 0, CAST(N'2022-06-07T19:23:59.6250443' AS DateTime2), 1)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (76, 1, N'اردیبهشت و خرداد 1401', 2350000, -300000, CAST(N'2022-06-08T13:49:54.6129909' AS DateTime2), 1)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (77, 3, N'اردیبهشت و خرداد 1401', 0, 0, CAST(N'2022-06-08T13:49:56.8154543' AS DateTime2), 1)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (78, 5, N'اردیبهشت و خرداد 1401', 0, 0, CAST(N'2022-06-08T13:49:56.8262930' AS DateTime2), 1)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (79, 1, N'اردیبهشت و خرداد 1401', 2350000, 2050000, CAST(N'2022-06-09T11:28:17.0330020' AS DateTime2), 1)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (80, 3, N'اردیبهشت و خرداد 1401', 0, 0, CAST(N'2022-06-09T11:28:21.0140763' AS DateTime2), 1)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (81, 5, N'اردیبهشت و خرداد 1401', 0, 0, CAST(N'2022-06-09T11:28:23.6524936' AS DateTime2), 1)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (82, 1, N'اردیبهشت و خرداد 1401', 2350000, 4400000, CAST(N'2022-06-10T14:08:49.7451236' AS DateTime2), 1)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (83, 3, N'اردیبهشت و خرداد 1401', 0, 0, CAST(N'2022-06-10T14:08:51.3963111' AS DateTime2), 1)
INSERT [dbo].[AdviserInvoices] ([AdviserInvoiceId], [AdviserId], [Description], [Amount], [TotalAmount], [PaySalaryDate], [SalaryTypeId]) VALUES (84, 5, N'اردیبهشت و خرداد 1401', 0, 0, CAST(N'2022-06-10T14:08:51.4053360' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[AdviserInvoices] OFF
GO
SET IDENTITY_INSERT [dbo].[Advisers] ON 

INSERT [dbo].[Advisers] ([AdviserId], [ParentId], [Name], [Family], [JobMobile], [PersonalMobile], [Study], [AccountNumber], [IsDelete]) VALUES (1, NULL, N'حامد', N'چترایی', N'09120601285', N'09138771172', N'انسانی', N'001', 0)
INSERT [dbo].[Advisers] ([AdviserId], [ParentId], [Name], [Family], [JobMobile], [PersonalMobile], [Study], [AccountNumber], [IsDelete]) VALUES (3, 1, N'مریم', N'محمدی', N'09120601288', N'09139729707', N'انسانی', N'002', 0)
INSERT [dbo].[Advisers] ([AdviserId], [ParentId], [Name], [Family], [JobMobile], [PersonalMobile], [Study], [AccountNumber], [IsDelete]) VALUES (4, NULL, N'سارا', N'توشقانیان', N'09126554587', N'09112352645', N'روانشناسی', N'22365', 0)
INSERT [dbo].[Advisers] ([AdviserId], [ParentId], [Name], [Family], [JobMobile], [PersonalMobile], [Study], [AccountNumber], [IsDelete]) VALUES (5, 1, N'نیلوفر', N'شریفی', N'09120601282', N'09132564799', N'تجربی', N'2145', 0)
INSERT [dbo].[Advisers] ([AdviserId], [ParentId], [Name], [Family], [JobMobile], [PersonalMobile], [Study], [AccountNumber], [IsDelete]) VALUES (8, NULL, N'فرینوش', N'زارع شیرازی', N'09120601289', N'09902253366', N'ریاضی', N'0004', 1)
INSERT [dbo].[Advisers] ([AdviserId], [ParentId], [Name], [Family], [JobMobile], [PersonalMobile], [Study], [AccountNumber], [IsDelete]) VALUES (16, 1, N'سحر', N'صادقی', N'09120601281', N'09102543694', N'کامپیوتر', N'0003', 0)
INSERT [dbo].[Advisers] ([AdviserId], [ParentId], [Name], [Family], [JobMobile], [PersonalMobile], [Study], [AccountNumber], [IsDelete]) VALUES (19, 1, N'سعید', N'محمدی', N'09120661288', N'09135264789', N'انسانی', N'0006', 1)
SET IDENTITY_INSERT [dbo].[Advisers] OFF
GO
SET IDENTITY_INSERT [dbo].[AdviserType] ON 

INSERT [dbo].[AdviserType] ([AdviserTypeId], [Type]) VALUES (2, N'جذب')
INSERT [dbo].[AdviserType] ([AdviserTypeId], [Type]) VALUES (3, N'برنامه ریزی')
INSERT [dbo].[AdviserType] ([AdviserTypeId], [Type]) VALUES (4, N'روانشناسی')
INSERT [dbo].[AdviserType] ([AdviserTypeId], [Type]) VALUES (5, N'تغذیه')
SET IDENTITY_INSERT [dbo].[AdviserType] OFF
GO
SET IDENTITY_INSERT [dbo].[AttributePlans] ON 

INSERT [dbo].[AttributePlans] ([APId], [PlanId], [PlanAttributeId]) VALUES (1, 5, 1)
SET IDENTITY_INSERT [dbo].[AttributePlans] OFF
GO
SET IDENTITY_INSERT [dbo].[Calls] ON 

INSERT [dbo].[Calls] ([CallId], [RegisterId], [CallDate], [CallTime], [Subject], [Description], [IsCall], [IsDelete]) VALUES (1, 25, CAST(N'2022-06-11T05:00:27.0000000' AS DateTime2), CAST(N'00:16:00' AS Time), N'تماس اول', N'ندارد', 1, 0)
INSERT [dbo].[Calls] ([CallId], [RegisterId], [CallDate], [CallTime], [Subject], [Description], [IsCall], [IsDelete]) VALUES (2, 19, CAST(N'2022-06-13T05:00:27.0000000' AS DateTime2), CAST(N'00:17:35' AS Time), N'تماس اول', N'ن', 0, 0)
SET IDENTITY_INSERT [dbo].[Calls] OFF
GO
SET IDENTITY_INSERT [dbo].[Cheques] ON 

INSERT [dbo].[Cheques] ([ChequeId], [RegisterId], [Amount], [ChequeDate], [Owner], [ChequeNumber], [AccountNumber], [ForWho], [Description], [IsPass]) VALUES (1, 25, 6500000, CAST(N'2022-06-11T05:00:27.0000000' AS DateTime2), N'آقای جهانگیری', N'31586665', N'01112145512', N'هیراد ذاکر', N'ندارد', 1)
SET IDENTITY_INSERT [dbo].[Cheques] OFF
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([CourseId], [Origin], [CourseDate], [Description], [IsDelete], [CourseTitle], [PercentAmount]) VALUES (1, N'رادیو ایران', CAST(N'2022-03-04T00:00:00.0000000' AS DateTime2), N'برنامه فوق العاده', 0, N'گزینه یک', 10)
INSERT [dbo].[Courses] ([CourseId], [Origin], [CourseDate], [Description], [IsDelete], [CourseTitle], [PercentAmount]) VALUES (2, N'تلویزیون اصفهان', CAST(N'2022-05-11T00:00:00.0000000' AS DateTime2), N'با رتبه برتر ها', 0, N'زنده رود', 10)
INSERT [dbo].[Courses] ([CourseId], [Origin], [CourseDate], [Description], [IsDelete], [CourseTitle], [PercentAmount]) VALUES (3, N'رادیو جوان', CAST(N'2022-06-20T00:00:00.0000000' AS DateTime2), N'ندارد', 1, N'برتر شو', 10)
SET IDENTITY_INSERT [dbo].[Courses] OFF
GO
SET IDENTITY_INSERT [dbo].[Grades] ON 

INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (1, N'اول')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (2, N'دوم')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (3, N'سوم')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (4, N'چهارم')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (5, N'پنجم')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (6, N'ششم')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (7, N'هفتم')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (8, N'هشتم')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (9, N'نهم')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (10, N'دهم')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (11, N'یازدهم')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (12, N'دوازدهم')
INSERT [dbo].[Grades] ([GradeId], [GradeTitle]) VALUES (13, N'فارغ التحصیل')
SET IDENTITY_INSERT [dbo].[Grades] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupAdvisers] ON 

INSERT [dbo].[GroupAdvisers] ([GroupId], [ParentId], [GroupTitle], [IsDelete]) VALUES (2, NULL, N'جذب', 0)
INSERT [dbo].[GroupAdvisers] ([GroupId], [ParentId], [GroupTitle], [IsDelete]) VALUES (3, NULL, N'برنامه ریزی', 0)
INSERT [dbo].[GroupAdvisers] ([GroupId], [ParentId], [GroupTitle], [IsDelete]) VALUES (4, NULL, N'روانشناسی', 0)
INSERT [dbo].[GroupAdvisers] ([GroupId], [ParentId], [GroupTitle], [IsDelete]) VALUES (5, NULL, N'تغذیه', 0)
SET IDENTITY_INSERT [dbo].[GroupAdvisers] OFF
GO
SET IDENTITY_INSERT [dbo].[Installments] ON 

INSERT [dbo].[Installments] ([InstallmentId], [RegisterId], [InstallmentDate], [Amount], [Description], [IsPayed]) VALUES (1, 19, CAST(N'2022-06-11T05:00:27.0000000' AS DateTime2), 2000000, N'ندارد', 0)
SET IDENTITY_INSERT [dbo].[Installments] OFF
GO
SET IDENTITY_INSERT [dbo].[Pays] ON 

INSERT [dbo].[Pays] ([PayId], [RegisterId], [Amount], [PayDate], [PursuitCode], [Description]) VALUES (10, 19, 4000000, CAST(N'2021-03-20T05:00:27.0000000' AS DateTime2), N'12365', N'no')
INSERT [dbo].[Pays] ([PayId], [RegisterId], [Amount], [PayDate], [PursuitCode], [Description]) VALUES (11, 20, 8500000, CAST(N'2021-03-20T05:00:27.0000000' AS DateTime2), N'112233', N'no')
INSERT [dbo].[Pays] ([PayId], [RegisterId], [Amount], [PayDate], [PursuitCode], [Description]) VALUES (12, 21, 6500000, CAST(N'2022-05-20T05:00:27.0000000' AS DateTime2), N'114422', N'no')
INSERT [dbo].[Pays] ([PayId], [RegisterId], [Amount], [PayDate], [PursuitCode], [Description]) VALUES (13, 23, 8500000, CAST(N'2022-05-20T05:00:27.0000000' AS DateTime2), N'552236', N'no')
INSERT [dbo].[Pays] ([PayId], [RegisterId], [Amount], [PayDate], [PursuitCode], [Description]) VALUES (15, 19, 4500000, CAST(N'2022-05-20T17:00:27.0000000' AS DateTime2), N'4455214', N'no')
SET IDENTITY_INSERT [dbo].[Pays] OFF
GO
SET IDENTITY_INSERT [dbo].[PayTypes] ON 

INSERT [dbo].[PayTypes] ([PayTypeId], [PayTypeTitle]) VALUES (1, N'نقد')
INSERT [dbo].[PayTypes] ([PayTypeId], [PayTypeTitle]) VALUES (2, N'قسط')
INSERT [dbo].[PayTypes] ([PayTypeId], [PayTypeTitle]) VALUES (3, N'چک')
SET IDENTITY_INSERT [dbo].[PayTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[PlanAttributes] ON 

INSERT [dbo].[PlanAttributes] ([PlanAttributeId], [Title]) VALUES (1, N'برنامه ریزی روزانه')
INSERT [dbo].[PlanAttributes] ([PlanAttributeId], [Title]) VALUES (2, N'گزارش کار شبانه')
INSERT [dbo].[PlanAttributes] ([PlanAttributeId], [Title]) VALUES (3, N'آزمون')
INSERT [dbo].[PlanAttributes] ([PlanAttributeId], [Title]) VALUES (4, N'تحلیل آزمون')
INSERT [dbo].[PlanAttributes] ([PlanAttributeId], [Title]) VALUES (5, N'وبینار')
INSERT [dbo].[PlanAttributes] ([PlanAttributeId], [Title]) VALUES (6, N'کارشناسی منابع آموزشی')
SET IDENTITY_INSERT [dbo].[PlanAttributes] OFF
GO
SET IDENTITY_INSERT [dbo].[Plans] ON 

INSERT [dbo].[Plans] ([PlanId], [PlanTitle], [Amount], [Length], [IsDelete]) VALUES (1, N'طرح وی آی پی', 8500000, 12, 0)
INSERT [dbo].[Plans] ([PlanId], [PlanTitle], [Amount], [Length], [IsDelete]) VALUES (2, N'طرح A+', 6500000, 12, 0)
INSERT [dbo].[Plans] ([PlanId], [PlanTitle], [Amount], [Length], [IsDelete]) VALUES (3, N'A', 4500000, 12, 0)
INSERT [dbo].[Plans] ([PlanId], [PlanTitle], [Amount], [Length], [IsDelete]) VALUES (4, N'B', 3000000, 12, 0)
INSERT [dbo].[Plans] ([PlanId], [PlanTitle], [Amount], [Length], [IsDelete]) VALUES (5, N'C', 2000000, 12, 0)
SET IDENTITY_INSERT [dbo].[Plans] OFF
GO
SET IDENTITY_INSERT [dbo].[Registers] ON 

INSERT [dbo].[Registers] ([RegisterId], [StudentId], [AdviserId], [PlanId], [CourseId], [RegisterDate], [Discount], [FinalAmount], [IsDelete], [PayTypeId], [IsCancel]) VALUES (19, 2, 1, 1, 1, CAST(N'2021-03-20T05:00:27.0000000' AS DateTime2), 1000000, 8500000, 0, 2, 0)
INSERT [dbo].[Registers] ([RegisterId], [StudentId], [AdviserId], [PlanId], [CourseId], [RegisterDate], [Discount], [FinalAmount], [IsDelete], [PayTypeId], [IsCancel]) VALUES (20, 3, 5, 1, 1, CAST(N'2021-03-20T05:00:27.0000000' AS DateTime2), 0, 8500000, 0, 1, 0)
INSERT [dbo].[Registers] ([RegisterId], [StudentId], [AdviserId], [PlanId], [CourseId], [RegisterDate], [Discount], [FinalAmount], [IsDelete], [PayTypeId], [IsCancel]) VALUES (21, 4, 1, 2, 1, CAST(N'2022-05-11T05:00:27.0000000' AS DateTime2), 0, 6500000, 0, 1, 0)
INSERT [dbo].[Registers] ([RegisterId], [StudentId], [AdviserId], [PlanId], [CourseId], [RegisterDate], [Discount], [FinalAmount], [IsDelete], [PayTypeId], [IsCancel]) VALUES (23, 6, 1, 1, 2, CAST(N'2022-05-11T05:00:27.0000000' AS DateTime2), 0, 8500000, 0, 1, 0)
INSERT [dbo].[Registers] ([RegisterId], [StudentId], [AdviserId], [PlanId], [CourseId], [RegisterDate], [Discount], [FinalAmount], [IsDelete], [PayTypeId], [IsCancel]) VALUES (25, 2, 3, 2, 1, CAST(N'2022-05-11T05:00:27.0000000' AS DateTime2), 0, 6500000, 0, 3, 0)
SET IDENTITY_INSERT [dbo].[Registers] OFF
GO
SET IDENTITY_INSERT [dbo].[RegisterSAdvisers] ON 

INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (17, 19, 1, CAST(N'2021-03-20T05:00:27.0000000' AS DateTime2), CAST(N'2023-06-11T05:00:27.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (18, 19, 5, CAST(N'2021-03-22T05:00:27.0000000' AS DateTime2), CAST(N'2023-06-11T05:00:27.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (19, 19, 4, CAST(N'2021-03-20T05:00:27.0000000' AS DateTime2), CAST(N'2023-06-11T05:00:27.0000000' AS DateTime2), 1, 4)
INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (20, 19, 4, CAST(N'2021-03-20T05:00:27.0000000' AS DateTime2), CAST(N'2023-06-11T05:00:27.0000000' AS DateTime2), 1, 5)
INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (21, 20, 5, CAST(N'2021-03-20T05:00:27.0000000' AS DateTime2), CAST(N'2023-06-11T05:00:27.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (22, 20, 5, CAST(N'2021-03-20T05:00:27.0000000' AS DateTime2), CAST(N'2021-03-20T05:00:27.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (23, 21, 1, CAST(N'2022-05-11T05:00:27.0000000' AS DateTime2), CAST(N'2023-05-11T05:00:27.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (24, 21, 5, CAST(N'2022-05-11T05:00:27.0000000' AS DateTime2), CAST(N'2023-05-11T05:00:27.0000000' AS DateTime2), 1, 3)
INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (25, 21, 4, CAST(N'2022-05-11T05:00:27.0000000' AS DateTime2), CAST(N'2023-05-11T05:00:27.0000000' AS DateTime2), 1, 4)
INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (26, 23, 1, CAST(N'2022-05-11T05:00:27.0000000' AS DateTime2), CAST(N'2023-05-11T05:00:27.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (27, 25, 3, CAST(N'2022-05-11T05:00:27.0000000' AS DateTime2), CAST(N'2023-05-11T05:00:27.0000000' AS DateTime2), 1, 2)
INSERT [dbo].[RegisterSAdvisers] ([RAId], [RegisterId], [AdviserId], [StartDate], [EndDate], [IsActive], [AdviserTypeId]) VALUES (28, 25, 4, CAST(N'2022-05-15T05:00:27.0000000' AS DateTime2), CAST(N'2023-05-11T05:00:27.0000000' AS DateTime2), 1, 3)
SET IDENTITY_INSERT [dbo].[RegisterSAdvisers] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleTitle], [IsDelete]) VALUES (2, N'مدیر کل', 0)
INSERT [dbo].[Roles] ([RoleId], [RoleTitle], [IsDelete]) VALUES (3, N'منشی', 0)
INSERT [dbo].[Roles] ([RoleId], [RoleTitle], [IsDelete]) VALUES (4, N'مشاور', 0)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[SalaryTypes] ON 

INSERT [dbo].[SalaryTypes] ([SalaryTypeId], [Title]) VALUES (1, N'پورسانت')
INSERT [dbo].[SalaryTypes] ([SalaryTypeId], [Title]) VALUES (2, N'حقوق')
INSERT [dbo].[SalaryTypes] ([SalaryTypeId], [Title]) VALUES (3, N'تشویقی')
INSERT [dbo].[SalaryTypes] ([SalaryTypeId], [Title]) VALUES (4, N'جریمه')
INSERT [dbo].[SalaryTypes] ([SalaryTypeId], [Title]) VALUES (5, N'وجه')
SET IDENTITY_INSERT [dbo].[SalaryTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[StartDayOfMonths] ON 

INSERT [dbo].[StartDayOfMonths] ([SdmId], [SdmValue]) VALUES (1, 10)
SET IDENTITY_INSERT [dbo].[StartDayOfMonths] OFF
GO
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (2, 1, 10, N'علی', N'نیمایی', N'حسن', N'09111235687', N'09125473256', N'09135478214', N'03454585796', N'تهران', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (3, 3, 12, N'اصغر', N'فرهادی', N'علی', N'09102546978', N'09142367854', N'09124587447', N'0214568521', N'تهران', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (4, 2, 11, N'مبینا', N'مبینایی', N'پدر', N'09152327548', N'09153247215', N'09158893654', N'05152479658', N'مشهد', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (6, 4, 13, N'علی اکبر', N'اسماعیل زاده', N'غلامرضا', N'09133335648', N'09133335648', N'09133335687', N'03142235647', N'اصفهان', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (7, 1, 10, N'سحر', N'صادقی', N'علی', N'09162458923', N'09163254789', N'09162351478', N'06123547812', N'ارمیه', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (8, 2, 13, N'نیما', N'شاهرخ شاهی', N'اصغر', N'09172223366', N'09174445569', N'09174441569', N'07123654789', N'شیراز', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (9, 1, 10, N'مینا', N'مینایی', N'اکبر', N'09162253669', N'09162265589', N'09162278547', N'06129856478', N'تبریز', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (11, 1, 10, N'شیما', N'شیمایی', N'شهروز', N'09121112569', N'09122225687', N'09126665895', N'0214562589', N'تهران', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (12, 1, 10, N'امیر', N'امیری', N'امیرعلی', N'09144445566', N'09146665544', N'09143332211', N'04123698521', N'قم', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (13, 1, 10, N'شهروز', N'علیایی', N'علی', N'09188887456', N'09189996655', N'09187774455', N'08142563987', N'یزد', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (14, 1, 10, N'لیلا', N'لیلایی', N'ابو لیلا', N'09172225544', N'09173336698', N'09174445896', N'07154447856', N'خوزستان', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (15, 2, 11, N'مجتبی', N'امیری', N'امیر', N'09114447788', N'09112223366', N'09115559988', N'0115569988', N'گیلان', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (16, 4, 12, N'ایمان', N'مومنی', N'امین', N'09142568745', N'09146982534', N'09142365874', N'04112569853', N'لرستان', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (17, 3, 10, N'علیرضا', N'محمدی', N'محمد', N'09132562458', N'09385567458', N'09102335846', N'03142235687', N'اصفهان', 0)
INSERT [dbo].[Students] ([StudentId], [StudyId], [GradeId], [Name], [Family], [FatherName], [Mobile1], [Mobile2], [Mobile3], [PhoneNumber], [City], [IsDelete]) VALUES (18, 1, 12, N'هانیه', N'امینی', N'اصغر', N'09352262458', N'09386521411', N'09142336541', N'05112336655', N'شیراز', 0)
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
SET IDENTITY_INSERT [dbo].[Studies] ON 

INSERT [dbo].[Studies] ([StudyId], [StudyTitle]) VALUES (1, N'تجربی')
INSERT [dbo].[Studies] ([StudyId], [StudyTitle]) VALUES (2, N'ریاضی')
INSERT [dbo].[Studies] ([StudyId], [StudyTitle]) VALUES (3, N'انسانی')
INSERT [dbo].[Studies] ([StudyId], [StudyTitle]) VALUES (4, N'فنی')
SET IDENTITY_INSERT [dbo].[Studies] OFF
GO
SET IDENTITY_INSERT [dbo].[UnregisteredCalls] ON 

INSERT [dbo].[UnregisteredCalls] ([UnregisteredCallsId], [AdviserId], [CallDate], [CallTime], [Subject], [Description], [IsCall], [IsDelete], [StudentId]) VALUES (2, 1, CAST(N'2022-05-05T05:00:27.0000000' AS DateTime2), CAST(N'00:15:30' AS Time), N'تماس اول', N'قرار شد با والیدنش تماس گررفته بشه', 1, 0, 12)
INSERT [dbo].[UnregisteredCalls] ([UnregisteredCallsId], [AdviserId], [CallDate], [CallTime], [Subject], [Description], [IsCall], [IsDelete], [StudentId]) VALUES (4, 3, CAST(N'2022-05-06T05:00:27.0000000' AS DateTime2), CAST(N'00:05:30' AS Time), N'تماس اول', N'گفت قبلا صحبت کردم نمیخام', 1, 0, 12)
SET IDENTITY_INSERT [dbo].[UnregisteredCalls] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([UR_ID], [UserId], [RoleId]) VALUES (8, 3, 4)
INSERT [dbo].[UserRoles] ([UR_ID], [UserId], [RoleId]) VALUES (9, 4, 4)
INSERT [dbo].[UserRoles] ([UR_ID], [UserId], [RoleId]) VALUES (10, 2, 4)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [UserName], [UserAliasName], [Mobile], [Password], [IsActive], [UserAvatar], [RegisterDate], [IsDelete]) VALUES (2, N'sahar_sadeghi', N'سحر صادقی', N'09120601281', N'B5-9C-67-BF-19-6A-47-58-19-1E-42-F7-66-70-CE-BA', 1, N'f41674faac4649f1b901ab89bcf48116.jpg', CAST(N'2022-06-21T20:22:35.1203061' AS DateTime2), 0)
INSERT [dbo].[Users] ([UserId], [UserName], [UserAliasName], [Mobile], [Password], [IsActive], [UserAvatar], [RegisterDate], [IsDelete]) VALUES (3, N'farinush_zare', N'فرینوش زارع شیرازی', N'09120601289', N'B5-9C-67-BF-19-6A-47-58-19-1E-42-F7-66-70-CE-BA', 1, N'2e1b475d45a94e379139b9ec42c47c2d.png', CAST(N'2022-06-21T20:24:02.9137844' AS DateTime2), 0)
INSERT [dbo].[Users] ([UserId], [UserName], [UserAliasName], [Mobile], [Password], [IsActive], [UserAvatar], [RegisterDate], [IsDelete]) VALUES (4, N'saeid_mohamadi', N'سعید محمدی', N'09120661288', N'B5-9C-67-BF-19-6A-47-58-19-1E-42-F7-66-70-CE-BA', 1, N'9b7f634808284647a2ec6c2d36cbcc93.png', CAST(N'2022-06-22T16:04:35.8662422' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_AdviserGroups_AdviserId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_AdviserGroups_AdviserId] ON [dbo].[AdviserGroups]
(
	[AdviserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AdviserGroups_GroupAdviserGroupId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_AdviserGroups_GroupAdviserGroupId] ON [dbo].[AdviserGroups]
(
	[GroupAdviserGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AdviserInvoices_AdviserId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_AdviserInvoices_AdviserId] ON [dbo].[AdviserInvoices]
(
	[AdviserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AdviserInvoices_SalaryTypeId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_AdviserInvoices_SalaryTypeId] ON [dbo].[AdviserInvoices]
(
	[SalaryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Advisers_ParentId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Advisers_ParentId] ON [dbo].[Advisers]
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AttributePlans_PlanAttributeId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_AttributePlans_PlanAttributeId] ON [dbo].[AttributePlans]
(
	[PlanAttributeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AttributePlans_PlanId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_AttributePlans_PlanId] ON [dbo].[AttributePlans]
(
	[PlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Calls_RegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Calls_RegisterId] ON [dbo].[Calls]
(
	[RegisterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cheques_RegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cheques_RegisterId] ON [dbo].[Cheques]
(
	[RegisterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FollowUps_RegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_FollowUps_RegisterId] ON [dbo].[FollowUps]
(
	[RegisterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GroupAdvisers_ParentId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_GroupAdvisers_ParentId] ON [dbo].[GroupAdvisers]
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Installments_RegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Installments_RegisterId] ON [dbo].[Installments]
(
	[RegisterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Pays_RegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Pays_RegisterId] ON [dbo].[Pays]
(
	[RegisterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Permissions_ParentID]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Permissions_ParentID] ON [dbo].[Permissions]
(
	[ParentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Registers_AdviserId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Registers_AdviserId] ON [dbo].[Registers]
(
	[AdviserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Registers_CourseId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Registers_CourseId] ON [dbo].[Registers]
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Registers_PayTypeId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Registers_PayTypeId] ON [dbo].[Registers]
(
	[PayTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Registers_PlanId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Registers_PlanId] ON [dbo].[Registers]
(
	[PlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Registers_StudentId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Registers_StudentId] ON [dbo].[Registers]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RegisterSAdvisers_AdviserId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_RegisterSAdvisers_AdviserId] ON [dbo].[RegisterSAdvisers]
(
	[AdviserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RegisterSAdvisers_AdviserTypeId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_RegisterSAdvisers_AdviserTypeId] ON [dbo].[RegisterSAdvisers]
(
	[AdviserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RegisterSAdvisers_RegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_RegisterSAdvisers_RegisterId] ON [dbo].[RegisterSAdvisers]
(
	[RegisterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RolePermissions_PermissionId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_RolePermissions_PermissionId] ON [dbo].[RolePermissions]
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RolePermissions_RoleId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_RolePermissions_RoleId] ON [dbo].[RolePermissions]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Students_GradeId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Students_GradeId] ON [dbo].[Students]
(
	[GradeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Students_StudyId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_Students_StudyId] ON [dbo].[Students]
(
	[StudyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UnregisteredCalls_AdviserId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_UnregisteredCalls_AdviserId] ON [dbo].[UnregisteredCalls]
(
	[AdviserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UnregisteredCalls_StudentId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_UnregisteredCalls_StudentId] ON [dbo].[UnregisteredCalls]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UnregisteredFollowUps_AdviserId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_UnregisteredFollowUps_AdviserId] ON [dbo].[UnregisteredFollowUps]
(
	[AdviserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UnregisteredFollowUps_StudentId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_UnregisteredFollowUps_StudentId] ON [dbo].[UnregisteredFollowUps]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_UserId]    Script Date: 6/23/2022 4:57:04 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_UserId] ON [dbo].[UserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdviserInvoices] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [PaySalaryDate]
GO
ALTER TABLE [dbo].[AdviserInvoices] ADD  DEFAULT ((0)) FOR [SalaryTypeId]
GO
ALTER TABLE [dbo].[Courses] ADD  DEFAULT (N'') FOR [CourseTitle]
GO
ALTER TABLE [dbo].[Courses] ADD  DEFAULT ((0)) FOR [PercentAmount]
GO
ALTER TABLE [dbo].[Installments] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsPayed]
GO
ALTER TABLE [dbo].[Registers] ADD  DEFAULT ((0)) FOR [PayTypeId]
GO
ALTER TABLE [dbo].[Registers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsCancel]
GO
ALTER TABLE [dbo].[RegisterSAdvisers] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [EndDate]
GO
ALTER TABLE [dbo].[RegisterSAdvisers] ADD  DEFAULT ((0)) FOR [AdviserTypeId]
GO
ALTER TABLE [dbo].[UnregisteredCalls] ADD  DEFAULT ((0)) FOR [StudentId]
GO
ALTER TABLE [dbo].[AdviserGroups]  WITH CHECK ADD  CONSTRAINT [FK_AdviserGroups_Advisers_AdviserId] FOREIGN KEY([AdviserId])
REFERENCES [dbo].[Advisers] ([AdviserId])
GO
ALTER TABLE [dbo].[AdviserGroups] CHECK CONSTRAINT [FK_AdviserGroups_Advisers_AdviserId]
GO
ALTER TABLE [dbo].[AdviserGroups]  WITH CHECK ADD  CONSTRAINT [FK_AdviserGroups_GroupAdvisers_GroupAdviserGroupId] FOREIGN KEY([GroupAdviserGroupId])
REFERENCES [dbo].[GroupAdvisers] ([GroupId])
GO
ALTER TABLE [dbo].[AdviserGroups] CHECK CONSTRAINT [FK_AdviserGroups_GroupAdvisers_GroupAdviserGroupId]
GO
ALTER TABLE [dbo].[AdviserInvoices]  WITH CHECK ADD  CONSTRAINT [FK_AdviserInvoices_Advisers_AdviserId] FOREIGN KEY([AdviserId])
REFERENCES [dbo].[Advisers] ([AdviserId])
GO
ALTER TABLE [dbo].[AdviserInvoices] CHECK CONSTRAINT [FK_AdviserInvoices_Advisers_AdviserId]
GO
ALTER TABLE [dbo].[AdviserInvoices]  WITH CHECK ADD  CONSTRAINT [FK_AdviserInvoices_SalaryTypes_SalaryTypeId] FOREIGN KEY([SalaryTypeId])
REFERENCES [dbo].[SalaryTypes] ([SalaryTypeId])
GO
ALTER TABLE [dbo].[AdviserInvoices] CHECK CONSTRAINT [FK_AdviserInvoices_SalaryTypes_SalaryTypeId]
GO
ALTER TABLE [dbo].[Advisers]  WITH CHECK ADD  CONSTRAINT [FK_Advisers_Advisers_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Advisers] ([AdviserId])
GO
ALTER TABLE [dbo].[Advisers] CHECK CONSTRAINT [FK_Advisers_Advisers_ParentId]
GO
ALTER TABLE [dbo].[AttributePlans]  WITH CHECK ADD  CONSTRAINT [FK_AttributePlans_PlanAttributes_PlanAttributeId] FOREIGN KEY([PlanAttributeId])
REFERENCES [dbo].[PlanAttributes] ([PlanAttributeId])
GO
ALTER TABLE [dbo].[AttributePlans] CHECK CONSTRAINT [FK_AttributePlans_PlanAttributes_PlanAttributeId]
GO
ALTER TABLE [dbo].[AttributePlans]  WITH CHECK ADD  CONSTRAINT [FK_AttributePlans_Plans_PlanId] FOREIGN KEY([PlanId])
REFERENCES [dbo].[Plans] ([PlanId])
GO
ALTER TABLE [dbo].[AttributePlans] CHECK CONSTRAINT [FK_AttributePlans_Plans_PlanId]
GO
ALTER TABLE [dbo].[Calls]  WITH CHECK ADD  CONSTRAINT [FK_Calls_Registers_RegisterId] FOREIGN KEY([RegisterId])
REFERENCES [dbo].[Registers] ([RegisterId])
GO
ALTER TABLE [dbo].[Calls] CHECK CONSTRAINT [FK_Calls_Registers_RegisterId]
GO
ALTER TABLE [dbo].[Cheques]  WITH CHECK ADD  CONSTRAINT [FK_Cheques_Registers_RegisterId] FOREIGN KEY([RegisterId])
REFERENCES [dbo].[Registers] ([RegisterId])
GO
ALTER TABLE [dbo].[Cheques] CHECK CONSTRAINT [FK_Cheques_Registers_RegisterId]
GO
ALTER TABLE [dbo].[FollowUps]  WITH CHECK ADD  CONSTRAINT [FK_FollowUps_Registers_RegisterId] FOREIGN KEY([RegisterId])
REFERENCES [dbo].[Registers] ([RegisterId])
GO
ALTER TABLE [dbo].[FollowUps] CHECK CONSTRAINT [FK_FollowUps_Registers_RegisterId]
GO
ALTER TABLE [dbo].[GroupAdvisers]  WITH CHECK ADD  CONSTRAINT [FK_GroupAdvisers_GroupAdvisers_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[GroupAdvisers] ([GroupId])
GO
ALTER TABLE [dbo].[GroupAdvisers] CHECK CONSTRAINT [FK_GroupAdvisers_GroupAdvisers_ParentId]
GO
ALTER TABLE [dbo].[Installments]  WITH CHECK ADD  CONSTRAINT [FK_Installments_Registers_RegisterId] FOREIGN KEY([RegisterId])
REFERENCES [dbo].[Registers] ([RegisterId])
GO
ALTER TABLE [dbo].[Installments] CHECK CONSTRAINT [FK_Installments_Registers_RegisterId]
GO
ALTER TABLE [dbo].[Pays]  WITH CHECK ADD  CONSTRAINT [FK_Pays_Registers_RegisterId] FOREIGN KEY([RegisterId])
REFERENCES [dbo].[Registers] ([RegisterId])
GO
ALTER TABLE [dbo].[Pays] CHECK CONSTRAINT [FK_Pays_Registers_RegisterId]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Permissions_ParentID] FOREIGN KEY([ParentID])
REFERENCES [dbo].[Permissions] ([PermissionId])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Permissions_ParentID]
GO
ALTER TABLE [dbo].[Registers]  WITH CHECK ADD  CONSTRAINT [FK_Registers_Advisers_AdviserId] FOREIGN KEY([AdviserId])
REFERENCES [dbo].[Advisers] ([AdviserId])
GO
ALTER TABLE [dbo].[Registers] CHECK CONSTRAINT [FK_Registers_Advisers_AdviserId]
GO
ALTER TABLE [dbo].[Registers]  WITH CHECK ADD  CONSTRAINT [FK_Registers_Courses_CourseId] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([CourseId])
GO
ALTER TABLE [dbo].[Registers] CHECK CONSTRAINT [FK_Registers_Courses_CourseId]
GO
ALTER TABLE [dbo].[Registers]  WITH CHECK ADD  CONSTRAINT [FK_Registers_PayTypes_PayTypeId] FOREIGN KEY([PayTypeId])
REFERENCES [dbo].[PayTypes] ([PayTypeId])
GO
ALTER TABLE [dbo].[Registers] CHECK CONSTRAINT [FK_Registers_PayTypes_PayTypeId]
GO
ALTER TABLE [dbo].[Registers]  WITH CHECK ADD  CONSTRAINT [FK_Registers_Plans_PlanId] FOREIGN KEY([PlanId])
REFERENCES [dbo].[Plans] ([PlanId])
GO
ALTER TABLE [dbo].[Registers] CHECK CONSTRAINT [FK_Registers_Plans_PlanId]
GO
ALTER TABLE [dbo].[Registers]  WITH CHECK ADD  CONSTRAINT [FK_Registers_Students_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[Registers] CHECK CONSTRAINT [FK_Registers_Students_StudentId]
GO
ALTER TABLE [dbo].[RegisterSAdvisers]  WITH CHECK ADD  CONSTRAINT [FK_RegisterSAdvisers_Advisers_AdviserId] FOREIGN KEY([AdviserId])
REFERENCES [dbo].[Advisers] ([AdviserId])
GO
ALTER TABLE [dbo].[RegisterSAdvisers] CHECK CONSTRAINT [FK_RegisterSAdvisers_Advisers_AdviserId]
GO
ALTER TABLE [dbo].[RegisterSAdvisers]  WITH CHECK ADD  CONSTRAINT [FK_RegisterSAdvisers_AdviserType_AdviserTypeId] FOREIGN KEY([AdviserTypeId])
REFERENCES [dbo].[AdviserType] ([AdviserTypeId])
GO
ALTER TABLE [dbo].[RegisterSAdvisers] CHECK CONSTRAINT [FK_RegisterSAdvisers_AdviserType_AdviserTypeId]
GO
ALTER TABLE [dbo].[RegisterSAdvisers]  WITH CHECK ADD  CONSTRAINT [FK_RegisterSAdvisers_Registers_RegisterId] FOREIGN KEY([RegisterId])
REFERENCES [dbo].[Registers] ([RegisterId])
GO
ALTER TABLE [dbo].[RegisterSAdvisers] CHECK CONSTRAINT [FK_RegisterSAdvisers_Registers_RegisterId]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Permissions_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([PermissionId])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Permissions_PermissionId]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Roles_RoleId]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Grades_GradeId] FOREIGN KEY([GradeId])
REFERENCES [dbo].[Grades] ([GradeId])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Grades_GradeId]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Studies_StudyId] FOREIGN KEY([StudyId])
REFERENCES [dbo].[Studies] ([StudyId])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Studies_StudyId]
GO
ALTER TABLE [dbo].[UnregisteredCalls]  WITH CHECK ADD  CONSTRAINT [FK_UnregisteredCalls_Advisers_AdviserId] FOREIGN KEY([AdviserId])
REFERENCES [dbo].[Advisers] ([AdviserId])
GO
ALTER TABLE [dbo].[UnregisteredCalls] CHECK CONSTRAINT [FK_UnregisteredCalls_Advisers_AdviserId]
GO
ALTER TABLE [dbo].[UnregisteredCalls]  WITH CHECK ADD  CONSTRAINT [FK_UnregisteredCalls_Students_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[UnregisteredCalls] CHECK CONSTRAINT [FK_UnregisteredCalls_Students_StudentId]
GO
ALTER TABLE [dbo].[UnregisteredFollowUps]  WITH CHECK ADD  CONSTRAINT [FK_UnregisteredFollowUps_Advisers_AdviserId] FOREIGN KEY([AdviserId])
REFERENCES [dbo].[Advisers] ([AdviserId])
GO
ALTER TABLE [dbo].[UnregisteredFollowUps] CHECK CONSTRAINT [FK_UnregisteredFollowUps_Advisers_AdviserId]
GO
ALTER TABLE [dbo].[UnregisteredFollowUps]  WITH CHECK ADD  CONSTRAINT [FK_UnregisteredFollowUps_Students_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([StudentId])
GO
ALTER TABLE [dbo].[UnregisteredFollowUps] CHECK CONSTRAINT [FK_UnregisteredFollowUps_Students_StudentId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
/****** Object:  StoredProcedure [dbo].[GetAllChequeByRegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllChequeByRegisterId]
	@registerId					INT
AS
BEGIN
	SELECT				*
	FROM				ViewChequeByRegister
	WHERE
	RegisterId		=	@registerId
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllPayByRegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllPayByRegisterId]
	@registerId					INT
AS
BEGIN
	SELECT				*
	FROM				ViewPayByRegister
	WHERE
	RegisterId		=	@registerId
END
GO
/****** Object:  StoredProcedure [dbo].[GetCallByRegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCallByRegisterId] 
	@registerId				INT
AS
BEGIN
	SELECT					*
	FROM					Calls
	WHERE
	RegisterId			=	@registerId
END
GO
/****** Object:  StoredProcedure [dbo].[GetChequeByRegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetChequeByRegisterId]
	@registerId			INT,
	@adviserId			INT
AS
BEGIN
	SELECT				*
	FROM				ViewChequeByRegister
	WHERE
	RegisterId		=	@registerId
	AND
	AdviserId		=	@adviserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetDebtorStudents]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDebtorStudents]
	@registerId			INT
AS
BEGIN
	SELECT				*
	FROM				Pays
	WHERE
	RegisterId		=	@registerId
	AND
	Amount         !=	0
END
GO
/****** Object:  StoredProcedure [dbo].[GetInformationRegisterByAdviserId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInformationRegisterByAdviserId]
	@adviserId			INT
AS
BEGIN
	SELECT				*
	FROM				ViewRegistersAdviserAndAdviserType
	WHERE
	AdviserId		=	@adviserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetInstallmentByRegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetInstallmentByRegisterId]
	@registerId			INT
AS
BEGIN
	SELECT				*
	FROM				Installments
	WHERE
	RegisterId		=	@registerId
END
GO
/****** Object:  StoredProcedure [dbo].[getInvoices]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getInvoices]

AS
BEGIN
	SELECT			*
	FROM			AdviserInvoices
END
GO
/****** Object:  StoredProcedure [dbo].[GetLastRegisterByStudentId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLastRegisterByStudentId]
	@studentId				INT
AS
BEGIN
	SELECT					TOP 1 *
	FROM					Registers
	WHERE
	StudentId			=	@studentId
	ORDER BY				RegisterId DESC
END
GO
/****** Object:  StoredProcedure [dbo].[GetPayByAdviserId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPayByAdviserId]
	@adviserId			INT
AS
BEGIN
	SELECT				*
	FROM				ViewPayByRegister
	WHERE
	AdviserId		=	@adviserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetPayByRegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPayByRegisterId]
	@registerId			INT,
	@adviserId			INT
AS
BEGIN
	SELECT				*
	FROM				ViewPayByRegister
	WHERE
	RegisterId		=	@registerId
	AND
	AdviserId		=	@adviserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegisterByAdviserId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRegisterByAdviserId]
	@AdviserId			INT
AS
BEGIN
	SELECT				*
	FROM				RegisterSAdvisers
	WHERE
	AdviserId		=	@AdviserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegisterByCourseId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRegisterByCourseId]
	@courseId				INT
AS
BEGIN
	SELECT					*
	FROM					ViewInformationRegisterAdviser
	WHERE
	CourseId			=	@courseId
	AND
	AdviserTypeId		=	2
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegisterByPlanId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRegisterByPlanId]
	@planId				INT
AS
BEGIN
	SELECT					*
	FROM					ViewInformationRegisterAdviser
	WHERE
	PlanId				=	@planId
	AND
	AdviserTypeId		=	2
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegistersByAdviserId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRegistersByAdviserId]
	@adviserId			INT
AS
BEGIN
	SELECT				*
	FROM				ViewInformationRegisterAdviser
	WHERE
	AdviserId		=	@adviserId
	AND
	AdviserTypeId	!= 2
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegistersByAdviserIdWithPlanAndCourse]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRegistersByAdviserIdWithPlanAndCourse]
	@adviserId				INT
AS
BEGIN
	SELECT				*
	FROM				ViewInformationRegisterAdviser
	WHERE
	AdviserId		=	@adviserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegistersByStudentId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRegistersByStudentId]
	@studentId		INT
AS
BEGIN
	SELECT			*
	FROM			Registers
	WHERE	
	StudentId	=	@studentId
END

GO
/****** Object:  StoredProcedure [dbo].[GetStartDayOfMonth]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStartDayOfMonth]
	@startDay		INT			OUTPUT
AS
BEGIN
	SELECT			@startDay = SdmValue
	FROM						StartDayOfMonths
END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentsAdviser]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentsAdviser]
	@registerId			INT
AS
BEGIN
	SELECT				*
	FROM				ViewRegistersAdviserAndAdviserType
	WHERE
	RegisterId		=	@registerId
END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentsRegisters]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentsRegisters]
	@studentId			INT
AS
BEGIN
	SELECT				*
	FROM				Registers
	WHERE
	StudentId		=	@studentId
END
GO
/****** Object:  StoredProcedure [dbo].[ShowAdviserInvoices]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowAdviserInvoices]
	@adviserId			INT
AS
BEGIN
	SELECT				*
	FROM				ViewAdviserInvoice
	WHERE
	AdviserId		=	@adviserId
END
GO
/****** Object:  StoredProcedure [dbo].[ShowAdvisers]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowAdvisers]

AS
BEGIN
	SELECT			*
	FROM			ViewAdvisers
	WHERE
	IsDelete	=	'false'
END
GO
/****** Object:  StoredProcedure [dbo].[ShowCanceledStudents]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowCanceledStudents]

AS
BEGIN
	SELECT				*
	FROM				ViewUnRegisteredStudents
	WHERE
	IsCancel		=	'true'
END
GO
/****** Object:  StoredProcedure [dbo].[ShowCourse]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowCourse]

AS
BEGIN
	SELECT				*
	FROM				Courses
	WHERE
	IsDelete		=	'false'
END
GO
/****** Object:  StoredProcedure [dbo].[ShowDeletedAdvisers]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowDeletedAdvisers]

AS
BEGIN
	SELECT			*
	FROM			ViewAdvisers
	WHERE
	IsDelete	=	'true'
END
GO
/****** Object:  StoredProcedure [dbo].[ShowDeletedCourse]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowDeletedCourse]

AS
BEGIN
	SELECT				*
	FROM				Courses
	WHERE
	IsDelete		=	'true'
END
GO
/****** Object:  StoredProcedure [dbo].[ShowDeletedStudents]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowDeletedStudents]

AS
BEGIN
	SELECT				*
	FROM				ViewUnRegisteredStudents
	WHERE
	IsDelete		=	'true'
END
GO
/****** Object:  StoredProcedure [dbo].[ShowInformationAdviserInvoice]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowInformationAdviserInvoice]
AS
BEGIN
	SELECT			*
	FROM			ViewAdviserInvoice
END
GO
/****** Object:  StoredProcedure [dbo].[ShowInformationRegisterByRegisterId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowInformationRegisterByRegisterId]
	@registerId				INT
AS
BEGIN
	SELECT					*
	FROM					ViewRegistersAdviserAndAdviserType
	WHERE
	RegisterId			=	@registerId
END
GO
/****** Object:  StoredProcedure [dbo].[ShowRegisterByAdviserId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowRegisterByAdviserId]
	@adviserId			INT
AS
BEGIN
	SELECT				*
	FROM				ViewRegisterAdviser
	WHERE
	AdviserId		=	@adviserId
END
GO
/****** Object:  StoredProcedure [dbo].[ShowStudents]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowStudents]
	
AS
BEGIN
	SELECT				*
	FROM				ViewStudents
	WHERE
	IsDelete		=	'false'
END
GO
/****** Object:  StoredProcedure [dbo].[ShowUnregisteredCallsByStudentId]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowUnregisteredCallsByStudentId]
	@studentId				INT
AS
BEGIN
	SELECT					*
	FROM					ViewUnregisteredCalls
	WHERE
	StudentId			=	@studentId
END
GO
/****** Object:  StoredProcedure [dbo].[ShowUnregisteredStudents]    Script Date: 6/23/2022 4:57:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowUnregisteredStudents]

AS
BEGIN
	SELECT			*
	FROM			ViewUnRegisteredStudents
	WHERE
	RegisterId     IS NULL
	AND
	IsDelete		=	'false'
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[2] 2[24] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AdviserInvoices"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 214
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SalaryTypes"
            Begin Extent = 
               Top = 6
               Left = 252
               Bottom = 102
               Right = 422
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Advisers"
            Begin Extent = 
               Top = 6
               Left = 460
               Bottom = 136
               Right = 638
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewAdviserInvoice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewAdviserInvoice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[8] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Advisers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "AdviserGroups"
            Begin Extent = 
               Top = 6
               Left = 254
               Bottom = 136
               Right = 458
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 18
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewAdvisers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewAdvisers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RegisterSAdvisers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Registers"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Cheques"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 632
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "Courses"
            Begin Extent = 
               Top = 6
               Left = 670
               Bottom = 136
               Right = 843
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Plans"
            Begin Extent = 
               Top = 6
               Left = 881
               Bottom = 136
               Right = 1051
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 19
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
        ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewChequeByRegister'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewChequeByRegister'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewChequeByRegister'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[13] 2[5] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RegisterSAdvisers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Registers"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Students"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdviserType"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 234
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewInformationRegisterAdviser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewInformationRegisterAdviser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewInformationRegisterAdviser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Pays"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Advisers"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Students"
            Begin Extent = 
               Top = 6
               Left = 462
               Bottom = 136
               Right = 632
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[9] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RegisterSAdvisers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Registers"
            Begin Extent = 
               Top = 132
               Left = 246
               Bottom = 262
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Pays"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Courses"
            Begin Extent = 
               Top = 6
               Left = 662
               Bottom = 136
               Right = 832
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Plans"
            Begin Extent = 
               Top = 6
               Left = 870
               Bottom = 136
               Right = 1040
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin Crite' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPayByRegister'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'riaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 2085
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPayByRegister'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewPayByRegister'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[43] 4[17] 2[13] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Registers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "Advisers"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Students"
            Begin Extent = 
               Top = 6
               Left = 462
               Bottom = 136
               Right = 632
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Plans"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Courses"
            Begin Extent = 
               Top = 77
               Left = 638
               Bottom = 207
               Right = 808
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "PayTypes"
            Begin Extent = 
               Top = 6
               Left = 846
               Bottom = 102
               Right = 1016
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 15
         Width = 284
         Width = 1500
         Width = 1500
     ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewRegisterAdviser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'    Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewRegisterAdviser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewRegisterAdviser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[7] 2[25] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Advisers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "RegisterSAdvisers"
            Begin Extent = 
               Top = 6
               Left = 462
               Bottom = 136
               Right = 632
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "AdviserType"
            Begin Extent = 
               Top = 6
               Left = 254
               Bottom = 102
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 14
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewRegistersAdviserAndAdviserType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewRegistersAdviserAndAdviserType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[17] 2[9] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Students"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "Grades"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 102
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Studies"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 102
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Registers"
            Begin Extent = 
               Top = 6
               Left = 662
               Bottom = 136
               Right = 832
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 15
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         Sor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewStudents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'tOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewStudents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewStudents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UnregisteredCalls"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 229
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "Advisers"
            Begin Extent = 
               Top = 6
               Left = 267
               Bottom = 136
               Right = 445
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Students"
            Begin Extent = 
               Top = 6
               Left = 483
               Bottom = 136
               Right = 653
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUnregisteredCalls'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUnregisteredCalls'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[9] 2[8] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Students"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "Studies"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 102
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Grades"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 102
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Registers"
            Begin Extent = 
               Top = 6
               Left = 662
               Bottom = 136
               Right = 832
            End
            DisplayFlags = 280
            TopColumn = 7
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 17
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue =' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUnRegisteredStudents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUnRegisteredStudents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ViewUnRegisteredStudents'
GO
USE [master]
GO
ALTER DATABASE [HiradCRM_DB] SET  READ_WRITE 
GO
