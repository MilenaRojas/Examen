// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    GetMajors();
    LoadStudents();

    $(document).on('submit', '#student-entry-form', function () {

        if ($('#id').val() == null) {

            Add();
        } else {

            Update();
        }
        return false;
    });




});
function Add() {

    var student = {
        name: $('#name').val(),
        email: $('#email').val(),
        password: $('#password').val(),
        majorId: parseInt($('#major').val())

    };

    var major = {

        id: parseInt($('#major').val()),
        name: $('#major').find('option:selected').text()

    };

    student.major = major;

    if (student != null) {

        $.ajax({
            url: "/Students/Post",
            data: JSON.stringify(student), //converte la variable estudiante en tipo json
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                // alert("resultado: "+result);
                $('#result').text("Added successfully");
                //document.getElementById("result").style.color = "green";
                $('#result').css('color', 'green');
                $('#name').val('');
                $('#email').val('');
                $('#password').val('');
                $('#major').val($("#major option:first").val());
            },
            error: function (errorMessage) {
                if (errorMessage === "no connection") {
                    $('#result').text("Error en la conexión.");
                }
                $('#result').text("User not added");
                $('#result').css('color', 'red');
                $('#password').val('');
            }
        });

    }


}

function Clear() {

    $('#name').val('');
    $('#email').val('');
    $('#password').val('');
    $('#major').val($("#major option:first").val());

}

function GetMajors() {

    $.ajax({
        url: "/Major/Get",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //llenar el dropdowns (select)
            var html = '';
            $.each(result, function (key, item) {
                html += '<option value="' + item.id + '">' + item.name + '</option>';
            });
            $('#major').append(html);

        },
        error: function (errorMessage) {
            // alert("Error");
            alert(errorMessage.responseText);
        }
    });
}

function GetStudentById(id_student) {

    var id = 0;
    $.ajax({
        url: "/Students/GetById",
        type: "GET",
        data: { id: id_student },
        success: function (result) {

            $('#id').val(result.id);
            $('#name').val(result.name);
            $('#email').val(result.email);
            //$('#password').val(result.password);
            $('#major').val(result.major.id);
        },
        error: function (errorMessage) {
            if (errorMessage === "no connection") {
                $('#result').text("Error en la conexión.");
            }
            $('#result').text("User not added");
            $('#result').css('color', 'red');
            $('#password').val('');
        }
    });
}

function LoadStudents() {

    $.ajax({
        url: "/Students/Get",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            dataSet = new Array();
            $.each(result, function (key, item) {

                data = [
                    item.id,
                    item.name,
                    item.email,
                    item.major.name,
                    '<td><a href="#about" onclick="GetStudentById(\'' + item.id + '\')">Edit</a> | <a href="#" onclick="Delete(' + item.id + ')">Delete</a></td>'
                ];

                dataSet.push(data);
            });
            $('#students-table').DataTable({
                "searching": true,
                data: dataSet,
                "bDestroy": true
            });

            /*
                    success: function (result) {
            
                        var html = '';
                        $.each(result, function (key, item) {
            
                            html += '<tr>';
                            html += '<td>' + item.id + '</td>';
                            html += '<td>' + item.name + '</td>';
                            html += '<td>' + item.email + '</td>';
                            html += '<td>' + item.major.name + '</td>';
                            html += '<td><a href="#about" onclick="GetStudentByEmail(\'' + item.email + '\')">Edit</a> | <a href="#" onclick="Delete(' + item.id + ')">Delete</a></td>';
                            html += '</tr>';
                        });
            
                        $('#students-tbody').html(html);,*/
        },
        error: function (errorMessage) {
            // alert("Error");
            alert(errorMessage.responseText);
        }
    });

}

function loadData() {
    $.ajax({
        url: "/Home/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            dataSet = new Array();
            var html = '';
            $.each(result, function (key, item) {

                data = [
                    item.StudentId,
                    item.Name,
                    item.Age,
                    item.Nationality,
                    item.Major,
                    '<td><a href="#" onclick="return GetById(' + item.StudentId + ')">Edit</a> | <a href="#" onclick="Delele(' + item.StudentId + ')">Delete</a></td>'
                ];

                dataSet.push(data);
            });
            $('#table').DataTable({
                "searching": true,
                data: dataSet,
                "bDestroy": true
            });

        },
        error: function (errorMessage) {
            alert(errorMessage.responseText);
        }
    })

}



function Update() {

    var student = {
        id: $('#id').val(),
        name: $('#name').val(),
        email: $('#email').val(),
        password: $('#password').val(),
        majorId: parseInt($('#major').val())

    };

    var major = {

        id: parseInt($('#major').val()),
        name: $('#major').find('option:selected').text()

    };

    student.major = major;

    if (student != null) {

        $.ajax({
            url: "/Students/Put",
            data: JSON.stringify(student), //converte la variable estudiante en tipo json
            type: "PUT",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {

                $('#result').text("Updated successfully");
                $('#result').css('color', 'green');
                $('#name').val('');
                $('#email').val('');
                $('#password').val('');
                $('#major').val($("#major option:first").val());
            },
            error: function (errorMessage) {
                if (errorMessage === "no connection") {
                    $('#result').text("Error en la conexión.");
                }
                $('#result').text("User not added");
                $('#result').css('color', 'red');
                $('#password').val('');
            }
        });

    }
}


function Delete(id_student) {

    var id = 0;
    $.ajax({
        url: "/Students/Delete",
        type: "DELETE",
        data: { id: id_student },
        success: function (result) {
            LoadStudents();
        },
        error: function (errorMessage) {
            if (errorMessage === "no connection") {
                $('#result').text("Error en la conexión.");
            }
        }
    });
}