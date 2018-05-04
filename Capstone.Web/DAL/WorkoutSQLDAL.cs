using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace Capstone.Web.DAL
{
    public class WorkoutSQLDAL : IWorkoutDAL
    {
        private string connectionString;

        public WorkoutSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool AddExercise(Exercise addExercise)
        {
            //this is not finished
            string AddExerciseDAL = "INSERT INTO exercises (exercise_name, exercise_description, trainer_id, exercise_type_id, video_link) VALUES (@name, @description, @trainerID, @exerciseTypeID, @videolink)";
            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(AddExerciseDAL, conn);
                cmd.Parameters.AddWithValue("@name", addExercise.Name);
                cmd.Parameters.AddWithValue("@description", addExercise.Description);
                cmd.Parameters.AddWithValue("@trainerID", addExercise.TrainerID);
                cmd.Parameters.AddWithValue("@exerciseTypeID", addExercise.Type);
                cmd.Parameters.AddWithValue("@videolink", addExercise.VideoLink);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }

        public bool CreateWorkout(string name, string notes, int planID)
        {
            string AddPlanDAL = "INSERT INTO workout (workout_name, additional_notes, plan_id) VALUES (@name, @notes, @planID)";
            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(AddPlanDAL, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@notes", notes);
                cmd.Parameters.AddWithValue("@planID", planID);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }


        public bool AddCardioToWorkout(CardioExercise exercise)
        {
            bool check;
            string addCardio = "INSERT INTO cardio_exercise (exercise_id, duration, intensity, workout_id) VALUES (@exercise_id, @duration, @intensity, @workout_id)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(addCardio, conn);
                cmd.Parameters.AddWithValue("@exercise_id", exercise.ExerciseID);
                cmd.Parameters.AddWithValue("@duration", exercise.Duration);
                cmd.Parameters.AddWithValue("@intensity", exercise.Intensity);
                cmd.Parameters.AddWithValue("@workout_id", exercise.WorkoutID);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }


        public bool AddStrengthToWorkout(StrengthExercise exercise)
        {
            bool check;
            string addStrength = @"INSERT INTO strength_exercise (exercise_id, strength_reps, strength_sets, rest_time, workout_id) 
                                   VALUES (@exercise_id, @strength_reps, @strength_sets, @rest_time, @workout_id)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(addStrength, conn);
                cmd.Parameters.AddWithValue("@exercise_id", exercise.ExerciseID);
                cmd.Parameters.AddWithValue("@strength_reps", exercise.Reps);
                cmd.Parameters.AddWithValue("@strength_sets", exercise.Sets);
                cmd.Parameters.AddWithValue("@rest_time", exercise.Rest_time);
                cmd.Parameters.AddWithValue("@workout_id", exercise.WorkoutID);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }
            return check;
        }


        public int CreatePlan(Plan insertPlan)
        {
            string AddPlanDAL = @"INSERT INTO workout_plan (trainer_id, trainee_id, plan_notes, plan_name) OUTPUT INSERTED.plan_id VALUES (@trainer_id, @trainee_id, @plan_notes, @plan_name)";
            int id;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(AddPlanDAL, conn);
                cmd.Parameters.AddWithValue("@plan_name", insertPlan.PlanName);
                cmd.Parameters.AddWithValue("@trainee_id", insertPlan.ForTrainee);
                cmd.Parameters.AddWithValue("@trainer_id", insertPlan.ByTrainer);
                cmd.Parameters.AddWithValue("@plan_notes", insertPlan.Notes);

                id = (int)cmd.ExecuteScalar();
            }

            return id;
        }

        public Plan GetPlan(int planID)
        {
            Plan plan = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string SQL_Plans = @"SELECT workout_plan.*, user_info.first_name, user_info.last_name FROM workout_plan 
                                     JOIN user_info ON workout_plan.trainer_id = user_info.trainer_id
                                     WHERE plan_id = @PlanID";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_Plans, conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", planID);

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        plan = MapRowToPlan(reader);
                    }
                }
            }
            return plan;
        }

        public Plan GetTraineePlan(int traineeID)
        {
            Plan plan = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string SQL_Plans = @"SELECT workout_plan.*, user_info.first_name, user_info.last_name FROM workout_plan 
                                     JOIN user_info ON workout_plan.trainer_id = user_info.trainer_id
                                     WHERE trainee_id = @trainee_ID";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_Plans, conn))
                {
                    cmd.Parameters.AddWithValue("@trainee_ID", traineeID);

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        plan = MapRowToPlan(reader);
                    }
                }
            }
            return plan;
        }

        public List<Workout> GetWorkoutsWithExercises(int planId)
        {
            List<Workout> workouts = new List<Workout>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string SQL_Plans = "SELECT * from workout WHERE plan_id = @plan_Id";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_Plans, conn))
                {
                    cmd.Parameters.AddWithValue("@plan_Id", planId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Workout workout = MapRowToWorkout(reader);
                        workout.GetBig = GetStrengthExercises(workout.Id);
                        workout.RunningAndStuff = GetCardioExercises(workout.Id);

                        workouts.Add(workout);
                    }
                }
            }

            return workouts;
        }

        public List<StrengthExercise> GetStrengthExercises(int workoutId)
        {
            List<StrengthExercise> se = new List<StrengthExercise>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string SQL_Plans = @"SELECT * 
                                    from strength_exercise
                                    JOIN exercises ON strength_exercise.exercise_id = exercises.exercise_id
                                    WHERE workout_id = @workout_id";

                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_Plans, conn))
                {
                    cmd.Parameters.AddWithValue("@workout_Id", workoutId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        StrengthExercise exercise = MapRowToStrength(reader);

                        se.Add(exercise);
                    }
                }
            }

            return se;
        }

        public List<CardioExercise> GetCardioExercises(int workoutId)
        {
            List<CardioExercise> ce = new List<CardioExercise>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string SQL_Plans = @"SELECT * 
                                    from cardio_exercise
                                    JOIN exercises ON cardio_exercise.exercise_id = exercises.exercise_id
                                    WHERE workout_id = @workout_id";

                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_Plans, conn))
                {
                    cmd.Parameters.AddWithValue("@workout_Id", workoutId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CardioExercise exercise = MapRowToCardio(reader);

                        ce.Add(exercise);
                    }
                }
            }

            return ce;
        }

        public List<Workout> GetWorkouts(int planID)
        {
            List<Workout> WorkoutsList = new List<Workout>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string SQL_WorkOutList = "SELECT * from workout WHERE plan_id = @plan_id";

                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_WorkOutList, conn))
                {
                    cmd.Parameters.AddWithValue("@plan_id", planID);

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        Workout workoutToAdd = MapRowToWorkout(reader);

                        WorkoutsList.Add(workoutToAdd);
                    }
                }
            }
            return WorkoutsList;
        }

        public Workout GetWorkout(int workoutId)
        {
            Workout workout = null;

            string GetWorkoutSQL = @"SELECT * FROM workout WHERE workout_id = @workoutId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(GetWorkoutSQL, conn))
                {
                    cmd.Parameters.AddWithValue("@workoutId", workoutId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                       workout = MapRowToWorkout(reader);
                       workout.GetBig = GetStrengthExercises(workoutId);
                       workout.RunningAndStuff = GetCardioExercises(workoutId);
                    }
                }
            }

            return workout;
        }

        public PopulatePlanViewModel GetPlanViewModel(int traineeID)
        {
            PopulatePlanViewModel plan = new PopulatePlanViewModel();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string SQL_Plans = "SELECT plan_id, plan_name from workout_plan WHERE trainee_id = @trainee_ID";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQL_Plans, conn))
                {
                    cmd.Parameters.AddWithValue("@trainee_ID", traineeID);

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        PopulatePlanViewModel planToAdd = MapRowToPlanViewModel(reader);

                        plan = planToAdd;
                    }
                }
            }
            return plan;
        }

        public List<User> GetClientNames(int TrainerID)
        {
            throw new NotImplementedException();
        }

        public List<Exercise> GetExercisesForTrainer(int TrainerID)
        {
            List<Exercise> ExercisesByTrainer = new List<Exercise>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string SQLExercises = "SELECT exercise_id, exercise_name, exercise_description, video_link, exercise_type_id FROM exercises WHERE trainer_id = @trainer_id OR trainer_id = 0";

                using (SqlCommand cmd = new SqlCommand(SQLExercises, conn))
                {
                    cmd.Parameters.AddWithValue("@trainer_id", TrainerID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Exercise ExerciseToAdd = MapRowToExercise(reader);

                        ExercisesByTrainer.Add(ExerciseToAdd);
                    }
                }
            }

            return ExercisesByTrainer;

        }

        private Exercise MapRowToExercise(SqlDataReader reader)
        {
            return new Exercise()
            {
                Name = Convert.ToString(reader["exercise_name"]),
                Type = Convert.ToInt32(reader["exercise_type_id"]),
                Description = Convert.ToString(reader["exercise_description"]),
                ExerciseID = Convert.ToInt32(reader["exercise_id"]),
                VideoLink = Convert.ToString(reader["video_link"])
            };
        }

        private User MapRowToUser(SqlDataReader reader)
        {
            return new User()
            {
                First_Name = Convert.ToString(reader["first_name"]),
                Last_Name = Convert.ToString(reader["last_name"]),
            };
        }

        private PopulatePlanViewModel MapRowToPlanViewModel(SqlDataReader reader)
        {
            return new PopulatePlanViewModel()
            {
                PlanID = Convert.ToInt32(reader["plan_id"]),
                PlanName = Convert.ToString(reader["plan_name"]),
            };
        }


        private Plan MapRowToPlan(SqlDataReader reader)
        {
            return new Plan()
            {
                Id = Convert.ToInt32(reader["plan_id"]),
                ByTrainer = Convert.ToInt32(reader["trainer_id"]),
                ForTrainee = Convert.ToInt32(reader["trainee_id"]),
                PlanName = Convert.ToString(reader["plan_name"]),
                Notes = Convert.ToString(reader["plan_notes"]),
                TrainerFirstName = Convert.ToString(reader["first_name"]),
                TrainerLastName = Convert.ToString(reader["last_name"])
            };
        }

        private Plan MapRowToTraineePlan(SqlDataReader reader)
        {
            return new Plan()
            {
                Id = Convert.ToInt32(reader["plan_id"]),
                ByTrainer = Convert.ToInt32(reader["trainer_id"]),
                ForTrainee = Convert.ToInt32(reader["trainee_id"]),
                PlanName = Convert.ToString(reader["plan_name"]),
                Notes = Convert.ToString(reader["plan_notes"]),
                TrainerFirstName = Convert.ToString(reader["first_name"]),
                TrainerLastName = Convert.ToString(reader["last_name"])
            };
        }

        private Workout MapRowToWorkout(SqlDataReader reader)
        {
            return new Workout()
            {
                Id = Convert.ToInt32(reader["workout_id"]),
                Name = Convert.ToString(reader["workout_name"]),
                Notes = Convert.ToString(reader["additional_notes"]),
                Plan_Id = Convert.ToInt32(reader["plan_id"])
            };
        }

        private StrengthExercise MapRowToStrength(SqlDataReader reader)
        {
            return new StrengthExercise()
            {
                ExerciseID = Convert.ToInt32(reader["exercise_id"]),
                Name = Convert.ToString(reader["exercise_name"]),
                Type = Convert.ToInt32(reader["exercise_type_id"]),
                Description = Convert.ToString(reader["exercise_description"]),
                VideoLink = Convert.ToString(reader["video_link"]),
                TrainerID = Convert.ToInt32(reader["trainer_id"]),
                Reps = Convert.ToInt32(reader["strength_reps"]),
                Sets = Convert.ToInt32(reader["strength_sets"]),
                Rest_time = Convert.ToInt32(reader["rest_time"]),
                Strength_id = Convert.ToInt32(reader["strength_id"])
            };
        }

        private CardioExercise MapRowToCardio(SqlDataReader reader)
        {
            return new CardioExercise()
            {
                ExerciseID = Convert.ToInt32(reader["exercise_id"]),
                Name = Convert.ToString(reader["exercise_name"]),
                Type = Convert.ToInt32(reader["exercise_type_id"]),
                Description = Convert.ToString(reader["exercise_description"]),
                VideoLink = Convert.ToString(reader["video_link"]),
                TrainerID = Convert.ToInt32(reader["trainer_id"]),
                Intensity = Convert.ToInt32(reader["intensity"]),
                Duration = Convert.ToInt32(reader["duration"]),
                Cardio_id = Convert.ToInt32(reader["cardio_id"]),
            };
        }
    }
}