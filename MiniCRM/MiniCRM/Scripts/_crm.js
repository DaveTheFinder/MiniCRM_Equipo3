var app = angular.module("crm", ["ngResource", "ngRoute"]);

app.factory('contactoService', function ($resource) {
    return $resource('/api/contactos/:id',
        { id: '@Id' },
        {
            update: { method: 'PUT' }
        });
});

app.factory('anotacionService', function ($resource) {
    return $resource('/api/anotaciones/:id',
        { id: '@Id' },
        {
            update: { method: 'PUT' }
        });
});


app.controller("mainController", function ($scope) {

});

app.controller("contactoController", function ($scope, contactoService) {

    $scope.title = '';
    $scope.errors = [];

    $scope.contactos = contactoService.query();

    $scope.contacto = {
        Id: 0,
        Nombre: '',
        Correo: '',
        Telefono: 0,
    };

    $scope.selectContacto = function (dpmt) {
        $scope.contacto = dpmt;
        $scope.showUpdateDialog();
    };

    $scope.deleteContacto = function (contacto) {
        contactoService.remove(contacto, $scope.refreshData, $scope.errorMessage);
    };

    $scope.refreshData = function () {
        $scope.contactos = contactoService.query();
        $('#modal-dialog').modal('hide');
    };

    $scope.errorMessage = function (response) {
        var errors = [];
        for (var key in response.data.ModelState) {
            for (var i = 0; i < response.data.ModelState[key].length; i++) {
                errors.push(response.data.ModelState[key][i]);
            }
        }
        $scope.errors = errors;
    };

    $scope.clearErrors = function () {
        $scope.errors = [];
    };

    $scope.showUpdateDialog = function () {
        $scope.clearErrors();
        $scope.title = 'Actualiza Contacto';
        $('#modal-dialog').modal('show');
    };

    $scope.showAddDialog = function () {
        $scope.clearErrors();
        $scope.clearCurrentContacto();
        $scope.title = 'Agrega Contacto';
        $('#modal-dialog').modal('show');
    };

    $scope.saveContacto = function () {
        if ($scope.contacto.Id > 0) {
            contactoService.update($scope.contacto, $scope.refreshData, $scope.errorMessage);
        }
        else {
            contactoService.save($scope.contacto, $scope.refreshData, $scope.errorMessage);
        }

    };

    $scope.clearCurrentContacto = function () {
        $scope.contacto = { Id: 0, Name: '' };

    };
});

app.controller("anotacionController", function ($scope, anotacionService) {

    $scope.anotaciones = anotacionService.query();

    $scope.anotacion = {
        Id: 0,
        Fecha: '',
        Descripcion: '',
        ContactoId: 0
    };

    $scope.deleteAnotacion = function (anotacion) {
        anotacionService.remove(anotacion, $scope.refreshData);
    };

    $scope.refreshData = function () {
        $scope.anotaciones = anotacionService.query();
    };

    $scope.showAddDialog = function () {
        $('#modal-dialog').modal('show');
    };

    $scope.saveAnotacion = function () {
        anotacionService.save($scope.anotacion, $scope.refreshData);
        $('#modal-dialog').modal('hide');

        $scope.clearCurrentAnotacion();
    };

    $scope.clearCurrentAnotacion = function () {
        $scope.anotacion = { Id: 0, Name: '', Position: '', DepartmentId: 0, };
    };

});

app.config(function ($routeProvider) {
    $routeProvider.when('/', {
        controller: 'mainController'
    }).when('/anotaciones', {
        templateUrl: '/Content/Views/Anotaciones.html',
        controller: 'anotacionController'
    }).when('/contactos', {
        templateUrl: '/Content/Views/Contactos.html',
        controller: 'contactoController'
    });
});