$(document).ready(function () {
    // Get URL routes from data attributes we'll add to the body
    var getEquipmentByIdUrl = $('body').data('get-equipment-url');

    // Initialize DataTable
    var table = $('#equipmentTable').DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "info": true,
        "responsive": true,
        "language": {
            "search": "",
            "searchPlaceholder": "Search...",
            "emptyTable": "No equipment found"
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

    // View equipment details
    $(document).on('click', '.view-equipment', function () {
        var equipmentId = $(this).data('id');
        console.log("View equipment ID:", equipmentId);

        $.ajax({
            url: getEquipmentByIdUrl + '/' + equipmentId,
            type: 'GET',
            success: function (equipment) {
                console.log("Equipment data received:", equipment);
                $('#view-idEquipment').text(equipment.idEquipment);
                $('#view-equipmentName').text(equipment.equipmentName);
                $('#view-description').text(equipment.description);
                $('#view-category').text(equipment.category);
                $('#view-dailyRate').text("$" + equipment.dailyRate);
            },
            error: function (error) {
                console.error('Error fetching equipment data:', error);
                alert('Error fetching equipment data. Please try again.');
            }
        });
    });

    // Edit equipment
    $(document).on('click', '.edit-equipment', function () {
        var equipmentId = $(this).data('id');
        console.log("Edit equipment ID:", equipmentId);

        $.ajax({
            url: getEquipmentByIdUrl + '/' + equipmentId,
            type: 'GET',
            success: function (equipment) {
                console.log("Edit equipment data received:", equipment);
                $('#edit-idEquipment').val(equipment.idEquipment);
                $('#edit-equipmentName').val(equipment.equipmentName);
                $('#edit-description').val(equipment.description);
                $('#edit-category').val(equipment.category);
                $('#edit-dailyRate').val(equipment.dailyRate);
            },
            error: function (error) {
                console.error('Error fetching equipment data for edit:', error);
                alert('Error fetching equipment data. Please try again.');
            }
        });
    });

    // Delete equipment
    $(document).on('click', '.delete-equipment', function () {
        var equipmentId = $(this).data('id');
        var equipmentName = $(this).data('name');

        console.log("Delete equipment ID:", equipmentId, "Name:", equipmentName);

        $('#delete-idEquipment').val(equipmentId);
        $('#delete-equipmentName').text(equipmentName);
    });

    // Form validations
    $('#addEquipmentForm').submit(function (e) {
        if ($('#equipmentName').val().trim() === "" || $('#category').val().trim() === "" || $('#dailyRate').val().trim() === "") {
            e.preventDefault();
            alert('All fields are required.');
        }
    });

    $('#editEquipmentForm').submit(function (e) {
        if ($('#edit-equipmentName').val().trim() === "" || $('#edit-category').val().trim() === "" || $('#edit-dailyRate').val().trim() === "") {
            e.preventDefault();
            alert('All fields are required.');
        }
    });
});