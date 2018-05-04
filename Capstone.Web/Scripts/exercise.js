$(document).ready(function () {

    $("button[name='strengthExerciseButton'").on("click", function (event) {
        var exerciseToAdd = $("#strengthExercises").find(":selected").val();
        var cardioExerciseToAdd = $("#cardioExercises").find(":selected").val();
        var workoutToAddTo = $("#workoutID").val();
        var sets = $("#sets").val();
        var reps = $("#reps").val();

        var service = new ExerciseService();
        service.addStrength(exerciseToAdd, workoutToAddTo, sets, reps, ExerciseAdded);
        event.preventDefault();
        location.reload(true);
    })

    $("button[name='cardioExerciseButton'").on("click", function (event) {
        var exerciseToAdd = $("#cardioExercises").find(":selected").val();
        var workoutToAddTo = $("#workoutID").val();
        var duration = $("#duration").val();
        var intensity = $("#intensity").val();

        var service = new ExerciseService();
        service.addCardio(exerciseToAdd, workoutToAddTo, duration, intensity, ExerciseAdded)
        event.preventDefault();
        location.reload(true);
    })

    function ExerciseAdded(exercises) {
        $("#exerciseTable").empty();
        console.log(exercises);
        for (i = 0; i < exercises.RunningAndStuff.length; i++) {
            var item = exercises.RunningAndStuff[i];
            var tr = $("<tr scope='row'>");

            var clock = $("<i>");
            clock.addClass("far");
            clock.addClass("fa-clock");
            clock.addClass("exercise-icon");

            var resultCell = $("<td>");
            var title = $("<h3>").text(item.Name);
            resultCell.addClass("margin-top");
            resultCell.addClass("padding-top");
            resultCell.append(title);
            resultCell.append(clock);
            tr.append(resultCell);

            $("#exerciseTable").append(tr);
        }
        for (i = 0; i < exercises.GetBig.length; i++) {
            var item = exercises.GetBig[i];
            var tr = $("<tr scope='row'>");
            var resultCell = $("<td>").text(item.Name);
            resultCell.addClass("margin-top");
            resultCell.addClass("padding-top");
            tr.append(resultCell);
            $("#exerciseTable").append(tr);
        }
    }

    function ExerciseService() {
        const strengthRoot = "/Trainer/AddStrengthExercise";
        const cardioRoot = "/Trainer/AddCardioExercise";

        this.addStrength = function (exerciseToAdd, workoutToAddTo, sets, reps, successCallback) {
            $.ajax({
                url: strengthRoot,
                method: "GET",
                data: {
                    "exerciseID": exerciseToAdd,
                    "workoutID": workoutToAddTo,
                    "sets": sets,
                    "reps": reps
                }
            }).done(function (data) {
                //successCallback(data);
            }).fail(function (xhr, status, error) {
                console.error("Error occured while adding exercise", error);
            })
        };

        this.addCardio = function (exerciseToAdd, workoutToAddTo, duration, intensity, successCallback) {
            $.ajax({
                url: cardioRoot,
                method: "GET",
                data: {
                    "exerciseID": exerciseToAdd,
                    "workoutID": workoutToAddTo,
                    "duration": duration,
                    "intensity": intensity
                }
            }).done(function (data) {
                //successCallback(data);
            }).fail(function (xhr, status, error) {
                console.error("Error occured while adding exercise", error);
            })
        }
    }
});