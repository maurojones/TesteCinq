(function () {
    "use strict";

    function productListCtrl(productResource) {
        var vm = this;

        vm.searchCriteria = "GDN"; // could be bound to a user input on the front end

        productResource.query(function (data) {
			vm.products = data;
			for (var i = 0; i < vm.products.length; i++) {
				vm.products[i].priceUS = vm.products[i].price;
				vm.products[i].priceEUR = vm.products[i].price * 0.9;
			}
        });
    }

    angular
        .module("productManagement")
        .controller("ProductListCtrl",
                     productListCtrl,
                     ["productResource",
                         productListCtrl]);
}());
