$(document).ready(function () {
    var getEmployeeByIdUrl = $('body').data('get-employee-url');

    // Initialize DataTable
    var table = $('#employeesTable').DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "info": true,
        "responsive": true,
        "language": {
            "search": "",
            "searchPlaceholder": "Search...",
            "emptyTable": "No employees found"
        },
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

    // View employee details
    $('.view-employee').click(function () {
        var employeeId = $(this).data('id');
        console.log("View employee ID:", employeeId);

        $.ajax({
            url: getEmployeeByIdUrl + '/' + employeeId,
            type: 'GET',
            success: function (employee) {
                $('#view-idEmployee').text(employee.idEmployee);
                $('#view-firstName').text(employee.firstName);
                $('#view-lastName').text(employee.lastName);
                $('#view-role').text(employee.role);
                $('#view-email').text(employee.email);
                $('#view-phone').text(employee.phone);
                $('#view-salaryPerEvent').text(employee.salaryPerEvent);
            },
            error: function (error) {
                console.error('Error fetching employee data:', error);
                alert('Error fetching employee data. Please try again.');
            }
        });
    });


    // Edit employee
    $('.edit-employee').click(function () {
        var employeeId = $(this).data('id');
        console.log("Edit employee ID:", employeeId);

        $.ajax({
            url: getEmployeeByIdUrl + '/' + employeeId,
            type: 'GET',
            success: function (employee) {
                $('#edit-idEmployee').val(employee.idEmployee);
                $('#edit-firstName').val(employee.firstName);
                $('#edit-lastName').val(employee.lastName);
                $('#edit-role').val(employee.role);
                $('#edit-email').val(employee.email);
                $('#edit-phone').val(employee.phone);
                $('#edit-salaryPerEvent').val(employee.salaryPerEvent);
            },
            error: function (error) {
                console.error('Error fetching employee data for edit:', error);
                alert('Error fetching employee data. Please try again.');
            }
        });
    });

    // Delete employee
    $('.delete-employee').click(function () {
        var employeeId = $(this).data('id');
        var employeeName = $(this).data('name');

        console.log("Delete employee ID:", employeeId, "Name:", employeeName);

        $('#delete-idEmployee').val(employeeId);
        $('#delete-employeeName').text(employeeName);
    });
});
