// client-scripts.js
$(document).ready(function () {
    // Get URL routes from data attributes we'll add to the body
    var getClientByIdUrl = $('body').data('get-client-url');

    // Initialize DataTable
    var table = $('#clientsTable').DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "info": true,
        "responsive": true,
        "language": {
            "search": "",
            "searchPlaceholder": "Search...",
            "emptyTable": "No clients found"
        }
    });

    // Search functionality
    $('#searchButton').on('click', function () {
        table.search($('#searchInput').val()).draw();
    });

    $('#searchInput').on('keyup', function (e) {
        if (e.key === 'Enter') {
            table.search($(this).val()).draw();
        }
    });

    // View client details
    $('.view-client').click(function () {
        var clientId = $(this).data('id');

        $.ajax({
            url: getClientByIdUrl + '/' + clientId,
            type: 'GET',
            success: function (client) {
                $('#view-idClient').text(client.idClient);
                $('#view-firstName').text(client.firstName);
                $('#view-lastName').text(client.lastName);
                $('#view-phone').text(client.phone);
                $('#view-email').text(client.email);
                $('#view-location').text(client.location);
            },
            error: function (error) {
                console.error('Error fetching client data:', error);
                alert('Error fetching client data. Please try again.');
            }
        });
    });

    // Edit client
    $('.edit-client').click(function () {
        var clientId = $(this).data('id');

        $.ajax({
            url: getClientByIdUrl + '/' + clientId,
            type: 'GET',
            success: function (client) {
                $('#edit-idClient').val(client.idClient);
                $('#edit-firstName').val(client.firstName);
                $('#edit-lastName').val(client.lastName);
                $('#edit-phone').val(client.phone);
                $('#edit-email').val(client.email);
                $('#edit-location').val(client.location);
            },
            error: function (error) {
                console.error('Error fetching client data for edit:', error);
                alert('Error fetching client data. Please try again.');
            }
        });
    });

    // Delete client
    $('.delete-client').click(function () {
        var clientId = $(this).data('id');
        var clientName = $(this).data('name');

        $('#delete-idClient').val(clientId);
        $('#delete-clientName').text(clientName);
    });

    // Form validations
    $('#addClientForm').submit(function (e) {
        if (!validateEmail($('#email').val())) {
            e.preventDefault();
            alert('Invalid email format.');
        }
    });

    $('#editClientForm').submit(function (e) {
        if (!validateEmail($('#edit-email').val())) {
            e.preventDefault();
            alert('Invalid email format.');
        }
    });

    function validateEmail(email) {
        var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(email);
    }
});

