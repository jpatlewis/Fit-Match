using Capstone.Web.Models;
using Capstone.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL.Interfaces
{
    public interface IWorkoutDAL
    {
        bool AddExercise(Exercise addExercise);
        Plan GetTraineePlan(int traineeID);
        int CreatePlan(Plan insertPlan);
        List<Workout> GetWorkouts(int planId);
        Workout GetWorkout(int workoutId);
        List<Workout> GetWorkoutsWithExercises(int planId);
        List<StrengthExercise> GetStrengthExercises(int workoutId);
        List<CardioExercise> GetCardioExercises(int workoutId);
        PopulatePlanViewModel GetPlanViewModel(int traineeID);
        List<Exercise> GetExercisesForTrainer(int TrainerID);
        bool CreateWorkout(string name, string notes, int planID);
        Plan GetPlan(int planID);
        bool AddStrengthToWorkout(StrengthExercise strengthExercise);
        bool AddCardioToWorkout(CardioExercise cardioExercise);
    }
}