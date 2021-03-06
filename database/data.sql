USE [TrainerMatch]
GO
SET IDENTITY_INSERT [dbo].[trainer] ON 

INSERT [dbo].[trainer] ([trainer_id], [price_per_hour], [certifications], [experience], [client_success_stories], [exercise_philosophy], [additional_notes], [searchable]) VALUES (1, 1, N'string', N'0', N'dfgdfgdfgdf', N'7987987', N'vzdfgfdg', 1)
SET IDENTITY_INSERT [dbo].[trainer] OFF
SET IDENTITY_INSERT [dbo].[exercise_type] ON 

INSERT [dbo].[exercise_type] ([exercise_type_id], [name]) VALUES (1, N'Cardio')
INSERT [dbo].[exercise_type] ([exercise_type_id], [name]) VALUES (2, N'Strength')
SET IDENTITY_INSERT [dbo].[exercise_type] OFF
SET IDENTITY_INSERT [dbo].[exercises] ON 

INSERT [dbo].[exercises] ([exercise_id], [exercise_name], [exercise_description], [video_link], [trainer_id], [exercise_type_id]) VALUES (1, N'Bench Press', N'Lay on back and lift bar', N'https://www.youtube.com/watch?v=uVp27_BdCJM', 1, 2)
INSERT [dbo].[exercises] ([exercise_id], [exercise_name], [exercise_description], [video_link], [trainer_id], [exercise_type_id]) VALUES (2, N'Shoulder Press', N'Stand up and lift bar over head', N'', 1, 2)
INSERT [dbo].[exercises] ([exercise_id], [exercise_name], [exercise_description], [video_link], [trainer_id], [exercise_type_id]) VALUES (3, N'Dead Lift', N'Bend knees and lift bar up from ground', N'', 1, 2)
INSERT [dbo].[exercises] ([exercise_id], [exercise_name], [exercise_description], [video_link], [trainer_id], [exercise_type_id]) VALUES (4, N'Running', N'Run', N'', 1, 1)
INSERT [dbo].[exercises] ([exercise_id], [exercise_name], [exercise_description], [video_link], [trainer_id], [exercise_type_id]) VALUES (5, N'Swimming', N'Swim', N'', 1, 1)
INSERT [dbo].[exercises] ([exercise_id], [exercise_name], [exercise_description], [video_link], [trainer_id], [exercise_type_id]) VALUES (6, N'Boxing', N'punch enemy in face', N'', 1, 1)
SET IDENTITY_INSERT [dbo].[exercises] OFF
INSERT [dbo].[cardio_exercise] ([exercise_id], [duration], [intensity]) VALUES (4, 30, 6)
INSERT [dbo].[cardio_exercise] ([exercise_id], [duration], [intensity]) VALUES (5, 60, 6)
INSERT [dbo].[cardio_exercise] ([exercise_id], [duration], [intensity]) VALUES (6, 15, 10)
INSERT [dbo].[strength_exercise] ([exercise_id], [strength_reps], [strength_sets], [rest_time]) VALUES (1, 5, 5, 30)
INSERT [dbo].[strength_exercise] ([exercise_id], [strength_reps], [strength_sets], [rest_time]) VALUES (2, 8, 3, 30)
INSERT [dbo].[strength_exercise] ([exercise_id], [strength_reps], [strength_sets], [rest_time]) VALUES (3, 3, 6, 30)
SET IDENTITY_INSERT [dbo].[user_info] ON 

INSERT [dbo].[user_info] ([user_id], [email], [password], [salt], [trainer_id], [first_name], [last_name], [user_location]) VALUES (4, N'STEVE@STEVE.STEVE', N'GhGEYZPzZUL/meP7QBG7AdzSUps=', N'hTGEtoqUE6AiZ+DF', 1, N'steve', N'steve', N'ffff')
INSERT [dbo].[user_info] ([user_id], [email], [password], [salt], [trainer_id], [first_name], [last_name], [user_location]) VALUES (34, N't@t.t', N'oHW+bz4BimgHNkmsCrXHSyZD5HA=', N'5M2GAARGGHowjHWd', NULL, N't', N't', N't')
SET IDENTITY_INSERT [dbo].[user_info] OFF
