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
    public class ProfileSQLDAL : IProfileDAL
    {

        private string connectionString;

        public ProfileSQLDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Search for trainer(s) by last name (required) and first name (optional), calls the DB for users with trainer IDs- PC
        /// </summary>
        /// <param name="trainerFirstName"></param>
        /// <param name="trainerLastName"></param>
        /// <returns></returns>
        public List<User> TrainerProfileSearchFullName(string trainerFirstName, string trainerLastName)
        {
            List<User> SearchList = new List<User>();

            string SQLSearchString = "select first_name, last_name, email, user_info.trainer_id, user_location, trainer.price_per_hour FROM user_info " +
                "JOIN trainer on user_info.trainer_id = trainer.trainer_id WHERE first_name LIKE @first_name and last_name like @last_name and user_info.trainer_id IS NOT NULL and searchable = 1 ";

            //SQLSearchString += " JOIN trainer on user_info.trainer_id = trainer.trainer_id and user.trainer_id IS NOT NULL and searchable = 1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQLSearchString, conn))
                {
                    cmd.Parameters.AddWithValue("@last_name", "%" + trainerLastName + "%");

                    cmd.Parameters.AddWithValue("@first_name", "%" + trainerFirstName + "%");


                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User userToAdd = MapRowToUser(reader);

                        SearchList.Add(userToAdd);
                    }
                }

            }
            return SearchList;
        }

        public List<User> TrainerProfileSearchPartialName(string name)
        {
            List<User> SearchList = new List<User>();

            string SQLSearchString = "select first_name, last_name, email, user_info.trainer_id, user_location, trainer.price_per_hour from user_info" +
                " JOIN trainer on user_info.trainer_id = trainer.trainer_id WHERE first_name LIKE @name or last_name like @name and searchable = 1";

            SQLSearchString += " and user_info.trainer_id IS NOT NULL";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQLSearchString, conn))
                {
                    cmd.Parameters.AddWithValue("@name", "%" + name + "%");

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User userToAdd = MapRowToUser(reader);

                        SearchList.Add(userToAdd);
                    }
                }

            }
            return SearchList;
        }

        public List<User> TrainerProfileSearchPrice (double pricePerHour)
        {
            List<User> SearchList = new List<User>();

            string SQLSearchString = "select first_name, last_name, email, user_info.trainer_id, user_location, trainer.price_per_hour from user_info" +
                " JOIN trainer on user_info.trainer_id = trainer.trainer_id WHERE price_per_hour <= @price_per_hour and searchable = 1";

            SQLSearchString += " and user_info.trainer_id IS NOT NULL";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQLSearchString, conn))
                {
                    cmd.Parameters.AddWithValue("@price_per_hour", pricePerHour);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User userToAdd = MapRowToUser(reader);

                        SearchList.Add(userToAdd);
                    }
                }
            }
            return SearchList;
        }

        public List<User> TrainerProfileSearchLocation(int location)
        {
            List<User> SearchList = new List<User>();

            string SQLSearchString = "select first_name, last_name, email, user_info.trainer_id, user_location, trainer.price_per_hour from user_info" +
                " JOIN trainer on user_info.trainer_id = trainer.trainer_id WHERE user_location = @location and searchable = 1";

            SQLSearchString += " and user_info.trainer_id IS NOT NULL";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(SQLSearchString, conn))
                {
                    cmd.Parameters.AddWithValue("@location", location);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User userToAdd = MapRowToUser(reader);

                        SearchList.Add(userToAdd);
                    }
                }
            }
            return SearchList;
        }

        private User MapRowToUser(SqlDataReader reader)
        {
            return new User()
            {
                First_Name = Convert.ToString(reader["first_name"]),
                Last_Name = Convert.ToString(reader["last_name"]),
                Email = Convert.ToString(reader["email"]),
                Trainer_ID = Convert.ToInt32(reader["trainer_id"]),
                User_Location = Convert.ToString(reader["user_location"]),
                Price_Per_Hour = Convert.ToInt32(reader["price_per_hour"])
            };
        }
    }
}