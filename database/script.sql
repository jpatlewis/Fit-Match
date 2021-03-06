CREATE DATABASE [TrainerMatch]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TrainerMatch', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\TrainerMatch.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TrainerMatch_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\TrainerMatch_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TrainerMatch] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TrainerMatch].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TrainerMatch] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TrainerMatch] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TrainerMatch] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TrainerMatch] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TrainerMatch] SET ARITHABORT OFF 
GO
ALTER DATABASE [TrainerMatch] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TrainerMatch] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TrainerMatch] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TrainerMatch] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TrainerMatch] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TrainerMatch] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TrainerMatch] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TrainerMatch] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TrainerMatch] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TrainerMatch] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TrainerMatch] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TrainerMatch] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TrainerMatch] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TrainerMatch] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TrainerMatch] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TrainerMatch] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TrainerMatch] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TrainerMatch] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TrainerMatch] SET  MULTI_USER 
GO
ALTER DATABASE [TrainerMatch] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TrainerMatch] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TrainerMatch] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TrainerMatch] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TrainerMatch] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TrainerMatch] SET QUERY_STORE = OFF
GO
USE [TrainerMatch]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [TrainerMatch]
GO
/****** Object:  Table [dbo].[cardio_exercise]    Script Date: 4/30/2018 1:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cardio_exercise](
	[exercise_id] [int] NULL,
	[duration] [int] NOT NULL,
	[intensity] [int] NOT NULL,
	[cardio_id] [int] IDENTITY(1,1) NOT NULL,
	[workout_id] [int] NULL,
 CONSTRAINT [pk_cardio_id] PRIMARY KEY CLUSTERED 
(
	[cardio_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[exercise_type]    Script Date: 4/30/2018 1:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[exercise_type](
	[exercise_type_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_exercise_type] PRIMARY KEY CLUSTERED 
(
	[exercise_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[exercises]    Script Date: 4/30/2018 1:17:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[exercises](
	[exercise_id] [int] IDENTITY(1,1) NOT NULL,
	[exercise_name] [varchar](30) NOT NULL,
	[exercise_description] [varchar](60) NOT NULL,
	[video_link] [varchar](1000) NOT NULL,
	[trainer_id] [int] NOT NULL,
	[exercise_type_id] [int] NOT NULL,
 CONSTRAINT [pk_exercise_id] PRIMARY KEY CLUSTERED 
(
	[exercise_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageHistory]    Script Date: 4/30/2018 1:17:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageHistory](
	[Message_id] [int] IDENTITY(1,1) NOT NULL,
	[MessageContent] [varchar](200) NOT NULL,
	[trainer_id] [int] NOT NULL,
	[trainee_id] [int] NOT NULL,
 CONSTRAINT [pk_Message_id] PRIMARY KEY CLUSTERED 
(
	[Message_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[strength_exercise]    Script Date: 4/30/2018 1:17:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[strength_exercise](
	[exercise_id] [int] NULL,
	[strength_reps] [int] NOT NULL,
	[strength_sets] [int] NOT NULL,
	[rest_time] [int] NOT NULL,
	[strength_id] [int] IDENTITY(1,1) NOT NULL,
	[workout_id] [int] NULL,
 CONSTRAINT [pk_strength_id] PRIMARY KEY CLUSTERED 
(
	[strength_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[trainer]    Script Date: 4/30/2018 1:17:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[trainer](
	[trainer_id] [int] IDENTITY(1,1) NOT NULL,
	[price_per_hour] [int] NOT NULL,
	[certifications] [varchar](60) NULL,
	[experience] [varchar](60) NULL,
	[client_success_stories] [varchar](60) NULL,
	[exercise_philosophy] [varchar](60) NULL,
	[additional_notes] [varchar](60) NULL,
	[searchable] [bit] NULL,
 CONSTRAINT [pk_trainer_id] PRIMARY KEY CLUSTERED 
(
	[trainer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_info]    Script Date: 4/30/2018 1:17:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_info](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](30) NOT NULL,
	[password] [varchar](30) NOT NULL,
	[salt] [varchar](30) NOT NULL,
	[trainer_id] [int] NULL,
	[first_name] [varchar](30) NOT NULL,
	[last_name] [varchar](30) NOT NULL,
	[user_location] [varchar](30) NULL,
 CONSTRAINT [pk_user_id] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workout]    Script Date: 4/30/2018 1:17:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workout](
	[workout_id] [int] IDENTITY(1,1) NOT NULL,
	[workout_name] [varchar](30) NOT NULL,
	[additional_notes] [varchar](60) NULL,
	[plan_id] [int] NOT NULL,
 CONSTRAINT [pk_workout_id] PRIMARY KEY CLUSTERED 
(
	[workout_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workout_plan]    Script Date: 4/30/2018 1:17:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workout_plan](
	[plan_id] [int] IDENTITY(1,1) NOT NULL,
	[trainer_id] [int] NOT NULL,
	[trainee_id] [int] NOT NULL,
	[plan_notes] [varchar](60) NULL,
	[plan_name] [varchar](30) NULL,
 CONSTRAINT [pk_plan_id] PRIMARY KEY CLUSTERED 
(
	[plan_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[exercises] ADD  CONSTRAINT [DF_exercises_exercise_type_id]  DEFAULT ((1)) FOR [exercise_type_id]
GO
ALTER TABLE [dbo].[cardio_exercise]  WITH CHECK ADD  CONSTRAINT [fk_workout_id] FOREIGN KEY([workout_id])
REFERENCES [dbo].[workout] ([workout_id])
GO
ALTER TABLE [dbo].[cardio_exercise] CHECK CONSTRAINT [fk_workout_id]
GO
ALTER TABLE [dbo].[exercises]  WITH CHECK ADD  CONSTRAINT [FK_exercise_exercise_type] FOREIGN KEY([exercise_type_id])
REFERENCES [dbo].[exercise_type] ([exercise_type_id])
GO
ALTER TABLE [dbo].[exercises] CHECK CONSTRAINT [FK_exercise_exercise_type]
GO
ALTER TABLE [dbo].[exercises]  WITH CHECK ADD  CONSTRAINT [FK_exercises_trainer] FOREIGN KEY([trainer_id])
REFERENCES [dbo].[trainer] ([trainer_id])
GO
ALTER TABLE [dbo].[exercises] CHECK CONSTRAINT [FK_exercises_trainer]
GO
ALTER TABLE [dbo].[MessageHistory]  WITH CHECK ADD  CONSTRAINT [fk_trainee_id] FOREIGN KEY([trainee_id])
REFERENCES [dbo].[user_info] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MessageHistory] CHECK CONSTRAINT [fk_trainee_id]
GO
ALTER TABLE [dbo].[MessageHistory]  WITH CHECK ADD  CONSTRAINT [fk_trainer_id] FOREIGN KEY([trainer_id])
REFERENCES [dbo].[trainer] ([trainer_id])
GO
ALTER TABLE [dbo].[MessageHistory] CHECK CONSTRAINT [fk_trainer_id]
GO
ALTER TABLE [dbo].[strength_exercise]  WITH CHECK ADD  CONSTRAINT [fk_s_e_id] FOREIGN KEY([exercise_id])
REFERENCES [dbo].[exercises] ([exercise_id])
GO
ALTER TABLE [dbo].[strength_exercise] CHECK CONSTRAINT [fk_s_e_id]
GO
ALTER TABLE [dbo].[strength_exercise]  WITH CHECK ADD  CONSTRAINT [fk_strength_workout_id] FOREIGN KEY([workout_id])
REFERENCES [dbo].[workout] ([workout_id])
GO
ALTER TABLE [dbo].[strength_exercise] CHECK CONSTRAINT [fk_strength_workout_id]
GO
ALTER TABLE [dbo].[user_info]  WITH CHECK ADD  CONSTRAINT [fk_user_trainer_id] FOREIGN KEY([trainer_id])
REFERENCES [dbo].[trainer] ([trainer_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[user_info] CHECK CONSTRAINT [fk_user_trainer_id]
GO
ALTER TABLE [dbo].[workout]  WITH CHECK ADD  CONSTRAINT [fk_plan_id] FOREIGN KEY([plan_id])
REFERENCES [dbo].[workout_plan] ([plan_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[workout] CHECK CONSTRAINT [fk_plan_id]
GO
ALTER TABLE [dbo].[workout_plan]  WITH CHECK ADD  CONSTRAINT [fk_trainee_plan_id] FOREIGN KEY([trainee_id])
REFERENCES [dbo].[user_info] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[workout_plan] CHECK CONSTRAINT [fk_trainee_plan_id]
GO
ALTER TABLE [dbo].[workout_plan]  WITH CHECK ADD  CONSTRAINT [fk_trainer_plan_id] FOREIGN KEY([trainer_id])
REFERENCES [dbo].[trainer] ([trainer_id])
GO
ALTER TABLE [dbo].[workout_plan] CHECK CONSTRAINT [fk_trainer_plan_id]
GO
USE [master]
GO
ALTER DATABASE [TrainerMatch] SET  READ_WRITE 
GO
