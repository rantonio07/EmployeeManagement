// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$('#empleadosTable').DataTable({
    "ajax": {
        "url": "/api/empleados",
        "dataSrc": ""
    },
    "columns": [
        { "data": "nombre" },
        { "data": "apellido" },
        { "data": "cargo" },
        { "data": "salario" }
    ]
});