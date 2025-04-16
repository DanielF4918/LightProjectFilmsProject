$(document).ready(function () {
    var baseUrl = window.location.origin;
    var getEventsUrl = "http://localhost:5163/api/Evento";
    var deleteRentalUrl = baseUrl + "/Rental/Delete";
    var getRentalDetailsUrl = baseUrl + "/Rental/GetRentalDetails";

    // Cargar eventos en el dropdown cuando se abre el modal de creación
    $('#createRentalModal').on('show.bs.modal', function () {
        $.ajax({
            url: getEventsUrl,
            type: "GET",
            success: function (events) {
                console.log("Eventos recibidos:", events);
                var eventSelect = $("#eventId");
                eventSelect.empty();
                eventSelect.append('<option value="">Select Event</option>');

                events.forEach(event => {
                    eventSelect.append(`<option value="${event.idEvent}" data-event-name="${event.eventName}">${event.eventName}</option>`);
                });
            },
            error: function () {
                console.error("Error fetching events.");
            }
        });
    });

    // Capturar `EventName` y almacenarlo en el campo oculto al seleccionar en el dropdown
    $('#eventId').on('change', function () {
        var selectedEventName = $("#eventId option:selected").attr("data-event-name");
        console.log("Evento seleccionado:", selectedEventName);
        $("#eventName").val(selectedEventName);
    });

    // Manejo del botón Delete para abrir el modal con el ID correcto
    $(".delete-rental").on("click", function () {
        var rentalId = $(this).data("id");
        console.log("ID a eliminar:", rentalId);
        $("#delete-idRental").val(rentalId);
        $("#deleteRentalModal").modal("show");
    });

    // Manejo del botón View Rental para cargar detalles desde el controlador del frontend
    $(".view-rental").on("click", function () {
        var rentalId = $(this).data("id");
        console.log("Cargando detalles para ID:", rentalId);

        $.ajax({
            url: `${getRentalDetailsUrl}/${rentalId}`,
            type: "GET",
            success: function (rentalData) {
                console.log("Detalles del rental recibidos:", rentalData);

                $("#view-idRental").text(rentalData.idRental || "N/A");
                $("#view-eventName").text(rentalData.eventName || "N/A");
                $("#view-rentalDate").text(rentalData.rentalDate || "N/A");
                $("#view-returnDate").text(rentalData.returnDate || "N/A");
                $("#view-totalCost").text(rentalData.totalCost ? `$${rentalData.totalCost}` : "N/A");
            },
            error: function (xhr) {
                console.error("Error obteniendo detalles del rental:", xhr.responseText);
                alert("Error obteniendo los detalles. Inténtalo de nuevo.");
            }
        });
    });

    // Cargar datos en el modal de edición al hacer clic en un botón de edición
    $(".edit-rental").on("click", function () {
        var rentalId = $(this).data("id");
        var eventId = $(this).data("event-id");
        var eventName = $(this).data("event-name");
        var rentalDate = $(this).data("rental-date");
        var returnDate = $(this).data("return-date");
        var totalCost = $(this).data("total-cost");

        console.log("Datos cargados para edición antes de conversión:", { rentalId, eventId, eventName, rentalDate, returnDate, totalCost });

        // Convertir formato de fecha a YYYY-MM-DD
        rentalDate = convertirFecha(rentalDate);
        returnDate = convertirFecha(returnDate);

        console.log("Datos cargados después de conversión:", { rentalDate, returnDate });

        // Asignar valores antes de abrir el modal
        $("#edit-idRental").val(rentalId);
        $("#view-eventName").text(rentalData.IdEvent || "N/A");
        $("#edit-rentalDate").val(rentalDate);
        $("#edit-returnDate").val(returnDate);
        $("#edit-totalCost").val(totalCost);

        cargarEventosEdicion(eventId);
        $("#editRentalModal").modal("show");
    });

    // Función para convertir fecha M/D/YYYY a YYYY-MM-DD
    function convertirFecha(fecha) {
        if (!fecha) return "";

        var partes = fecha.split("/");
        if (partes.length === 3) {
            var mes = partes[0].padStart(2, "0");
            var dia = partes[1].padStart(2, "0");
            var año = partes[2];
            return `${año}-${mes}-${dia}`;
        }
        return fecha;
    }

    // Cargar eventos en el dropdown cuando se abre el modal de edición
    function cargarEventosEdicion(eventIdSeleccionado) {
        $.ajax({
            url: getEventsUrl,
            type: "GET",
            success: function (events) {
                console.log("Eventos recibidos para edición:", events);
                var eventSelect = $("#edit-eventId");
                eventSelect.empty();
                eventSelect.append('<option value="">Select Event</option>');

                events.forEach(event => {
                    var isSelected = event.idEvent == eventIdSeleccionado ? "selected" : "";
                    eventSelect.append(`<option value="${event.idEvent}" data-event-name="${event.eventName}" ${isSelected}>${event.eventName}</option>`);
                });

                eventSelect.on("change", function () {
                    var selectedEventName = $("#edit-eventId option:selected").attr("data-event-name");
                    console.log("Evento seleccionado para edición:", selectedEventName);
                    $("#edit-eventName").val(selectedEventName);
                });
            },
            error: function () {
                console.error("Error fetching events.");
            }
        });
    }

    // Search functionality
    $('#searchButton').on('click', function () {
        table.search($('#searchInput').val()).draw();
    });

    $('#searchInput').on('keyup', function (e) {
        if (e.key === 'Enter') {
            table.search($(this).val()).draw();
        }
    });
});