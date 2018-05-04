using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class UserSQLDAL : IUserDAL
    {
        private string connectionString;

        public UserSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //non-trainer registration function
        public bool RegisterUser(User newUser)
        {
            string RegisterUserSQL = "INSERT INTO user_info (email, password, first_name, last_name, user_location, salt) VALUES (@email, @password, @FirstName, @LastName, @Location, @salt)";
            bool check;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(RegisterUserSQL, conn);
                cmd.Parameters.AddWithValue("@Email", newUser.Email);
                cmd.Parameters.AddWithValue("@UserID", newUser.User_ID);
                cmd.Parameters.AddWithValue("@Location", newUser.User_Location);
                cmd.Parameters.AddWithValue("@FirstName", newUser.First_Name);
                cmd.Parameters.AddWithValue("@LastName", newUser.Last_Name);
                cmd.Parameters.AddWithValue("@password", newUser.Password);
                cmd.Parameters.AddWithValue("@salt", newUser.Salt);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
    
            }
                return check;
        }

        //trainer registration function
        public bool RegisterUser(Trainer trainMaster)
        {
            //string delimitedCerts = DelimitedList(trainMaster.ListCertifications);

            bool check;

            string CreateUserSQL = "INSERT INTO user_info (email, password, salt, trainer_id, first_name, last_name, user_location) " +
                "VALUES (@email, @password, @salt, @trainer_id, @first_name, @last_name, @user_location)";

            string createProfileSQL = "INSERT INTO trainer (price_per_hour, certifications, experience, client_success_stories, exercise_philosophy, additional_notes, searchable) " +
                "OUTPUT inserted.trainer_id VALUES (@price_per_hour, @certifications, @experience, @client_success_stories, @exercise_philosophy, @additional_notes, 1)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd2 = new SqlCommand(createProfileSQL, conn);
                
                cmd2.Parameters.AddWithValue("@price_per_hour", trainMaster.Price_Per_Hour);
                cmd2.Parameters.AddWithValue("@exercise_philosophy", trainMaster.exercise_Philosophy);
                cmd2.Parameters.AddWithValue("@additional_notes", trainMaster.Additional_notes);
                cmd2.Parameters.AddWithValue("@experience", trainMaster.experience);
                cmd2.Parameters.AddWithValue("@certifications", trainMaster.Certifications);
                cmd2.Parameters.AddWithValue("@client_success_stories", trainMaster.Client_Success_Stories);

                int mostRecent = (int)(cmd2.ExecuteScalar()); 

                SqlCommand cmd = new SqlCommand(CreateUserSQL, conn);
                cmd.Parameters.AddWithValue("@email", trainMaster.Email);
                cmd.Parameters.AddWithValue("@password", trainMaster.Password);
                cmd.Parameters.AddWithValue("@salt", trainMaster.Salt);
                cmd.Parameters.AddWithValue("@first_name", trainMaster.First_Name);
                cmd.Parameters.AddWithValue("@last_name", trainMaster.Last_Name);
                cmd.Parameters.AddWithValue("@trainer_id", mostRecent);
                cmd.Parameters.AddWithValue("@user_location", trainMaster.User_Location);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
                   
            }

            return check;
        }

        //login function
        public User GetCurrentUser(string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                User result = conn.QueryFirstOrDefault<User>("Select * FROM user_info WHERE email = @emailValue", new { emailValue = email });
                return result;
            }
        }

        public Trainer GetTrainer(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Trainer result = conn.QueryFirstOrDefault<Trainer>("Select trainer.trainer_id, user_info.first_name, user_info.last_name, price_per_hour, experience, searchable, client_success_stories, exercise_philosophy, certifications, additional_notes, user_location FROM trainer JOIN user_info on user_info.trainer_id = trainer.trainer_id WHERE trainer.trainer_id = @trainerID", new { trainerID = ID });
                return result;
            }
        }

        private string DelimitedList (List<string> list)
        {
            string delimitedList = "";

            foreach(string item in list)
            {
                delimitedList += item + "|";
            }

            return delimitedList;
        }

        public bool UpdateTrainer(Trainer update)
        {
            bool check;
            string UpdateTrainerSQL = "UPDATE trainer SET price_per_hour = @price_per_hour, certifications = @certifications, experience = @experience, " +
                "client_success_stories = @client_success_stories, exercise_philosophy = @exercise_philosophy, additional_notes = @additional_notes WHERE trainer_id = @trainer_id";

            string UpdateLocationSQL = "UPDATE user_info SET user_location = @user_location WHERE user_id = @userID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(UpdateTrainerSQL, conn);
                cmd.Parameters.AddWithValue("@price_per_hour", update.Price_Per_Hour);
                cmd.Parameters.AddWithValue("@certifications", update.Certifications);
                cmd.Parameters.AddWithValue("@experience", update.experience);
                cmd.Parameters.AddWithValue("@client_success_stories", update.Client_Success_Stories);
                cmd.Parameters.AddWithValue("@exercise_philosophy", update.exercise_Philosophy);
                cmd.Parameters.AddWithValue("@additional_notes", update.Additional_notes);
                cmd.Parameters.AddWithValue("@searchable", update.Searchable);
                cmd.Parameters.AddWithValue("@trainer_id", update.Trainer_ID);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;

                SqlCommand cmd1 = new SqlCommand(UpdateLocationSQL, conn);
                cmd1.Parameters.AddWithValue("@user_location", update.User_Location);
                cmd1.Parameters.AddWithValue("@userID", update.User_ID);

                check = cmd1.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }

        public bool SwitchAccess(int trainerID, string flipBit)
        {
            bool check;
            string AccessTrainerSQL = "UPDATE trainer SET searchable = @flipBit WHERE trainer_id = @trainerID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(AccessTrainerSQL, conn);
                cmd.Parameters.AddWithValue("@trainerID", trainerID);
                cmd.Parameters.AddWithValue("@flipbit", flipBit);

                check = cmd.ExecuteNonQuery() > 0 ? true : false;
            }

            return check;
        }

        public Exercise GetExercise(int ExerciseID)
        {
            Exercise AExercise = new Exercise();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string SQLOneExercise = "SELECT exercise_id, exercise_name, exercise_description, video_link, exercise_type_id FROM exercises WHERE exercise_id = @exercise_id";

                using (SqlCommand cmd = new SqlCommand(SQLOneExercise, conn))
                {
                    cmd.Parameters.AddWithValue("@exercise_id", ExerciseID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Exercise add = MapRowToExercise(reader);

                        AExercise = add;
                    }
                }
            }

            return AExercise;
        }

        public bool MatchWithTrainer(int trainer, int trainee)
        {
            bool isMatched = false;

            string MatchTrainerSQL = @"BEGIN
                                        IF NOT EXISTS (SELECT * FROM Trainer_Trainee
                                        WHERE trainer_id = @trainerID
                                        AND trainee_id = @traineeID)
                                        BEGIN
                                        INSERT INTO Trainer_Trainee (trainer_id, trainee_id)
                                        VALUES (@trainerID, @traineeID)
                                        END
                                        END";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(MatchTrainerSQL, conn);
                cmd.Parameters.AddWithValue("@trainerID", trainer);
                cmd.Parameters.AddWithValue("@traineeID", trainee);

                isMatched = cmd.ExecuteNonQuery() > 0 ? true : false;


            }

            return isMatched;
        }

        public List<User> GetClients (int trainerID)
        {
            List<User> ClientList = new List<User>();

            string ClientSelectSQL = @"Select user_info.first_name, user_info.last_name, user_info.user_id 
                                       FROM user_info 
                                       JOIN trainer_trainee ON user_info.user_id = trainer_trainee.trainee_id 
                                       WHERE trainer_trainee.trainer_id = @trainer_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(ClientSelectSQL, conn);

                cmd.Parameters.AddWithValue("@trainer_id", trainerID);

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    User userToAdd = MapRowToUser(reader);

                    ClientList.Add(userToAdd);
                }
            }

            return ClientList;
        }

        public List<User> GetClientsWithoutPlans(int trainerID)
        {
            List<User> ClientList = new List<User>();

            string ClientSelectSQL = @"SELECT tt.*, wp.*, ui.first_name, ui.last_name, ui.user_id
                                       FROM Trainer_Trainee tt
                                       LEFT JOIN workout_plan wp ON tt.trainee_id = wp.trainee_id
                                       JOIN user_info ui ON tt.trainee_id = ui.user_id
                                       WHERE wp.plan_id IS NULL AND tt.trainer_id = @trainer_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(ClientSelectSQL, conn);

                cmd.Parameters.AddWithValue("@trainer_id", trainerID);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User userToAdd = MapRowToUser(reader);

                    ClientList.Add(userToAdd);
                }
            }

            return ClientList;
        }

        public List<User> GetClientsWithPlans(int trainerID)
        {
            List<User> ClientList = new List<User>();

            string ClientSelectSQL = @"SELECT tt.*, wp.*, ui.first_name, ui.last_name, ui.user_id
                                       FROM Trainer_Trainee tt
                                       JOIN workout_plan wp ON tt.trainee_id = wp.trainee_id
                                       JOIN user_info ui ON tt.trainee_id = ui.user_id
                                       WHERE tt.trainer_id = @trainer_id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(ClientSelectSQL, conn);

                cmd.Parameters.AddWithValue("@trainer_id", trainerID);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    User userToAdd = MapRowToUser(reader);
                    userToAdd.PlanId = Convert.ToInt32(reader["plan_id"]);

                    ClientList.Add(userToAdd);
                }
            }

            return ClientList;
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
                User_ID = Convert.ToInt32(reader["user_id"]),
                First_Name = Convert.ToString(reader["first_name"]),
                Last_Name = Convert.ToString(reader["last_name"]),
            };
        }
    }
}