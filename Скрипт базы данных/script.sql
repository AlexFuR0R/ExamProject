USE [master]
GO
/****** Object:  Database [QualificationCourses]    Script Date: 29.04.2025 11:57:52 ******/
CREATE DATABASE [QualificationCourses]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QualificationCourses', FILENAME = N'D:\SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QualificationCourses.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QualificationCourses_log', FILENAME = N'D:\SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QualificationCourses_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QualificationCourses] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QualificationCourses].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QualificationCourses] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QualificationCourses] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QualificationCourses] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QualificationCourses] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QualificationCourses] SET ARITHABORT OFF 
GO
ALTER DATABASE [QualificationCourses] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QualificationCourses] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QualificationCourses] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QualificationCourses] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QualificationCourses] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QualificationCourses] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QualificationCourses] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QualificationCourses] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QualificationCourses] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QualificationCourses] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QualificationCourses] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QualificationCourses] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QualificationCourses] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QualificationCourses] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QualificationCourses] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QualificationCourses] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [QualificationCourses] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QualificationCourses] SET RECOVERY FULL 
GO
ALTER DATABASE [QualificationCourses] SET  MULTI_USER 
GO
ALTER DATABASE [QualificationCourses] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QualificationCourses] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QualificationCourses] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QualificationCourses] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QualificationCourses] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QualificationCourses] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QualificationCourses', N'ON'
GO
ALTER DATABASE [QualificationCourses] SET QUERY_STORE = ON
GO
ALTER DATABASE [QualificationCourses] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QualificationCourses]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 29.04.2025 11:57:52 ******/
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
/****** Object:  Table [dbo].[Attendances]    Script Date: 29.04.2025 11:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendances](
	[AttendanceId] [int] IDENTITY(1,1) NOT NULL,
	[ScheduleId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[IsPresent] [bit] NOT NULL,
	[Notes] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[AttendanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Certifications]    Script Date: 29.04.2025 11:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Certifications](
	[CertificationId] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationId] [int] NOT NULL,
	[TestScore] [int] NULL,
	[IssueDate] [datetime2](7) NULL,
	[CertificateNumber] [nvarchar](50) NULL,
	[CertificatePdf] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CertificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 29.04.2025 11:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Topic] [nvarchar](100) NULL,
	[TeacherId] [int] NULL,
	[DurationHours] [int] NOT NULL,
	[Price] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registrations]    Script Date: 29.04.2025 11:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registrations](
	[RegistrationId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[RegistrationDate] [datetime2](7) NULL,
	[Status] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[RegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 29.04.2025 11:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[ScheduleId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[Classroom] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 29.04.2025 11:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Attendances] ADD  DEFAULT ((0)) FOR [IsPresent]
GO
ALTER TABLE [dbo].[Certifications] ADD  DEFAULT (getdate()) FOR [IssueDate]
GO
ALTER TABLE [dbo].[Registrations] ADD  DEFAULT (getdate()) FOR [RegistrationDate]
GO
ALTER TABLE [dbo].[Registrations] ADD  DEFAULT ('Active') FOR [Status]
GO
ALTER TABLE [dbo].[Attendances]  WITH CHECK ADD  CONSTRAINT [FK_Attendances_Schedules] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedules] ([ScheduleId])
GO
ALTER TABLE [dbo].[Attendances] CHECK CONSTRAINT [FK_Attendances_Schedules]
GO
ALTER TABLE [dbo].[Attendances]  WITH CHECK ADD  CONSTRAINT [FK_Attendances_Users] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Attendances] CHECK CONSTRAINT [FK_Attendances_Users]
GO
ALTER TABLE [dbo].[Certifications]  WITH CHECK ADD  CONSTRAINT [FK_Certifications_Registrations] FOREIGN KEY([RegistrationId])
REFERENCES [dbo].[Registrations] ([RegistrationId])
GO
ALTER TABLE [dbo].[Certifications] CHECK CONSTRAINT [FK_Certifications_Registrations]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Registrations]  WITH CHECK ADD  CONSTRAINT [FK_Registrations_Courses] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([CourseId])
GO
ALTER TABLE [dbo].[Registrations] CHECK CONSTRAINT [FK_Registrations_Courses]
GO
ALTER TABLE [dbo].[Registrations]  WITH CHECK ADD  CONSTRAINT [FK_Registrations_Users] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Registrations] CHECK CONSTRAINT [FK_Registrations_Users]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([CourseId])
GO
ALTER TABLE [dbo].[Registrations]  WITH CHECK ADD CHECK  (([Status]='Cancelled' OR [Status]='Completed' OR [Status]='Active'))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Role]='EducationDepartment' OR [Role]='Teacher' OR [Role]='Student'))
GO
USE [master]
GO
ALTER DATABASE [QualificationCourses] SET  READ_WRITE 
GO
