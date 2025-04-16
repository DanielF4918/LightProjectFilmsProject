// evento-scripts.js
$(document).ready(function () {
    // Get URL routes from data attributes
    var getEventoByIdUrl = $('body').data('get-evento-url');
    var getAllClientsUrl = $('body').data('get-all-clients-url');

    // Load clients when page loads
    loadClients();

    // Initialize DataTable
    var table = $('#eventosTable').DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "info": true,
        "responsive": true,
        "language": {
            "search": "",
            "searchPlaceholder": "Search...",
            "emptyTable": "No events found"
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

    // View event details
    $('.view-evento').click(function () {
        var eventoId = $(this).data('id');
        console.log("View event ID:", eventoId);

        $.ajax({
            url: getEventoByIdUrl + '/' + eventoId,
            type: 'GET',
            success: function (evento) {
                console.log("Event data received:", evento);
                $('#view-idEvent').text(evento.idEvent);
                $('#view-eventName').text(evento.eventName);

                // Format dates for display
                const startDate = new Date(evento.startDate);
                const endDate = new Date(evento.endDate);
                $('#view-startDate').text(startDate.toLocaleDateString());
                $('#view-endDate').text(endDate.toLocaleDateString());

                $('#view-location').text(evento.location);
                $('#view-clientName').text(evento.clientName);
            },
            error: function (error) {
                console.error('Error fetching event data:', error);
                alert('Error fetching event data. Please try again.');
            }
        });
    });

    // Edit event
    $('.edit-evento').click(function () {
        var eventoId = $(this).data('id');
        console.log("Edit event ID:", eventoId);

        $.ajax({
            url: getEventoByIdUrl + '/' + eventoId,
            type: 'GET',
            success: function (evento) {
                console.log("Edit event data received:", evento);

                $('#edit-idEvent').val(evento.idEvent);
                $('#edit-eventName').val(evento.eventName);

                // Format dates for form inputs (YYYY-MM-DD)
                const startDate = new Date(evento.startDate);
                const endDate = new Date(evento.endDate);

                // ISO format but truncate the time portion
                const formattedStartDate = startDate.toISOString().split('T')[0];
                const formattedEndDate = endDate.toISOString().split('T')[0];

                $('#edit-startDate').val(formattedStartDate);
                $('#edit-endDate').val(formattedEndDate);

                $('#edit-location').val(evento.location);
                $('#edit-idClient').val(evento.idClient);
            },
            error: function (error) {
                console.error('Error fetching event data for edit:', error);
                alert('Error fetching event data. Please try again.');
            }
        });
    });

    // Delete event
    $('.delete-evento').click(function () {
        var eventoId = $(this).data('id');
        var eventoName = $(this).data('name');

        console.log("Delete event ID:", eventoId, "Name:", eventoName);

        $('#delete-idEvent').val(eventoId);
        $('#delete-eventoName').text(eventoName);
    });

    // Form validations
    $('#addEventoForm').submit(function (e) {
        if (!validateDates($('#startDate').val(), $('#endDate').val())) {
            e.preventDefault();
            alert('End date must be after start date.');
            return false;
        }
    });

    $('#editEventoForm').submit(function (e) {
        if (!validateDates($('#edit-startDate').val(), $('#edit-endDate').val())) {
            e.preventDefault();
            alert('End date must be after start date.');
            return false;
        }
    });

    function validateDates(startDate, endDate) {
        return new Date(startDate) <= new Date(endDate);
    }

    // Function to load clients into dropdowns
    function loadClients() {
        console.log("Loading clients from URL:", getAllClientsUrl);

        $.ajax({
            url: getAllClientsUrl,
            type: 'GET',
            success: function (clients) {
                console.log("Clients data received:", clients);
                populateClientDropdowns(clients);
            },
            error: function (error) {
                console.error('Error fetching clients data:', error);
                alert('Error loading client data. Please try again.');
            }
        });
    }

    // Function to populate client dropdowns
    function populateClientDropdowns(clients) {
        // Clear existing options except the default one
        $('#idClient').find('option:not(:first)').remove();
        $('#edit-idClient').find('option:not(:first)').remove();

        // Add client options to both dropdowns
        if (clients && clients.length > 0) {
            $.each(clients, function (i, client) {
                var clientName = client.firstName + ' ' + client.lastName;
                $('#idClient').append($('<option>', {
                    value: client.idClient,
                    text: clientName
                }));

                $('#edit-idClient').append($('<option>', {
                    value: client.idClient,
                    text: clientName
                }));
            });
            console.log("Client dropdowns populated with " + clients.length + " clients");
        } else {
            console.warn("No clients were returned from the API");
        }
    }
});