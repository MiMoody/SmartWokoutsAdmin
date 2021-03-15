
    let idWork = $('#mode').val();
    let url = '/Workouts/LoadWork';
    let dataWorkouts = {
        idWorkout: idWork
    };
    let obj = JSON.stringify(dataWorkouts)
    $.ajax({
        type: 'POST',
        url: url,
        contentType: 'application/json; charset=utf-8',
        data: obj,
        success: function (data) {
            $('#results').html(data);
        },
         error: function (data) {
            alert(data.responseText);
        }

    });

function Delete(item) {

    let idWork = $('#mode').val();
    let url = '/Workouts/LoadWork';
    let idExersice = item.id;
    let dataWorkouts = {
        idWorkout: idWork,
        idDeleteExercise: idExersice

    };
    let obj = JSON.stringify(dataWorkouts)
    $.ajax({
        type: 'POST',
        url: url,
        contentType: 'application/json; charset=utf-8',
        data: obj,
        success: function (data) {
            $('#results').html(data);
        },
        error: function (data) {
            alert(data.responseText);
        }

    });
};
function Add() {

    let idWork = $('#mode').val();
    let url = '/Workouts/LoadWork';
    let idExersice = $('#forDropDownList :selected').val();
    let dataWorkouts = {
        idWorkout: idWork,
        idAddExercise: idExersice
    };
    let obj = JSON.stringify(dataWorkouts)
    $.ajax({
        type: 'POST',
        url: url,
        contentType: 'application/json; charset=utf-8',
        data: obj,
        success: function (data) {
            $('#results').html(data);
        },
        error: function (data) {
            alert(data.responseText);
        }

    });
};


   

    