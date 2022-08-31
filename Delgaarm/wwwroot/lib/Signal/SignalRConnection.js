

var connection = new signalR.HubConnectionBuilder().withUrl("/mymsgHub").withAutomaticReconnect().build();

connection.start();

document.getElementById("Sentletter").addEventListener("click", function () {

    // $("#Sentletter").on('click', function (e) {
        //e.preventDefault();
        //  کنترول اینکه حتمن بک نامه انتخاب شده باشد
        if ($("#LetterId").html().trim() == 0) {
            $("#divmsg").removeClass('hidden');
            $("#divmsg").addClass('alert-danger');
            $("#divmsg").html("هیج نامه یی جهت ارسال به کاربان انتخاب نشده است");
            return false;
        }

        if (LetterType == 1) {
            // کنترول اینکه حتمن شخصی جهت ارسال نامه انتخاب شده باشد
            if ($("#SelectedItemToUser").val() == "" || $("#SelectedItemToUser").val() == '[]') {
                $("#divmsg").removeClass('hidden');
                $("#divmsg").addClass('alert-danger');
                $("#divmsg").html("هیج شخصی جهت ارسال نامه انتخاب نشده است");
                return false;
            }

        }
        $.ajax({
            type: "Post",
            url: '/UserArea/Draft/SentLetter',
            data: {
                'LetterId': $("#LetterId").html().trim(),
                'SelectedItemToUser': $("#SelectedItemToUser").val(),
                'LetterType': LetterType,
                'MainLetterId': MainLetterId
            }
        }).done(function (result) {
            if (result.status == 'noletterselected') {
                $("#divmsg").removeClass('hidden');
                $("#divmsg").addClass('alert-danger');
                $("#divmsg").html("هیچ نامه ای انتخاب نشده است");
            }
            if (result.status == 'nouserselected') {
                $("#divmsg").removeClass('hidden');
                $("#divmsg").addClass('alert-danger');
                $("#divmsg").html("هیچ شخصی جهت ارسال نامه انتخاب نشده است");
            }
            if (result.status == 'error') {
                $("#divmsg").removeClass('hidden');
                $("#divmsg").addClass('alert-danger');
                $("#divmsg").html("در ارسال نامه مشکلی به وجود آمده است");
            }
            if (result.status == 'Ok') {
                var userList = $("#SelectedItemToUser").val();
                connection.invoke("SentLetter", userList).catch(function (err) {
                    return console.error(err.tostring());
                });

                swal({
                    title: "ارسال نامه",
                    text: "نامه شما با موفقیت ارسال شد!",
                    type: 'success',
                    showCancelButton: false,
                    allowOutsideClick: false,
                    confirmButtonColor: "green",
                    confirmButtonText: "باشه",
                }).then(function () {
                    window.location.reload();
                });
            }

        });
    //});

    
});