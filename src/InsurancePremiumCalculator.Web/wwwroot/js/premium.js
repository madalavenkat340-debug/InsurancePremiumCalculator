$(function() {
    function showErrors(errors) {
        $('#errors').html('');
        if (errors && errors.length) {
            $('#errors').html(errors.map(e => '<div>' + e + '</div>').join(''));
        }
    }

    function postCalculate() {
        var form = $('#premiumForm');
        var data = form.serialize();
        $.ajax({
            url: '/Premium/Calculate',
            method: 'POST',
            data: data,
            success: function(resp) {
                if (resp.success) {
                    $('#premiumValue').text(resp.premiumAmount);
                    showErrors([]);
                } else {
                    $('#premiumValue').text('-');
                    showErrors(resp.errors || ['Unknown error']);
                }
            },
            error: function(xhr) {
                var body = xhr.responseJSON;
                if (body && body.errors) {
                    showErrors(body.errors);
                } else {
                    showErrors(['Error calculating premium']);
                }
            }
        });
    }

    // Trigger when Occupation changes
    $('#Occupation').on('change', function() {
        // Only calculate if all fields have values
        var name = $('#Name').val();
        var age = $('#AgeNextBirthday').val();
        var dob = $('#DateOfBirth').val();
        var occ = $('#Occupation').val();
        var death = $('#DeathCoverAmount').val();

        if (name && age && dob && occ && death) {
            postCalculate();
        }
    });

    // Also allow explicit calculate
    $('#btnCalculate').on('click', function() {
        postCalculate();
    });
});
