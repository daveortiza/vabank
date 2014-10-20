﻿(function () {
    'use strict';

    angular.module('vabank.webapp')
        .config(registerRoutes);

    registerRoutes.$inject = ['$stateProvider'];

    function registerRoutes($stateProvider) {
        $stateProvider
            .state('customer', {
                url: '/',
                'abstract': true,
                templateUrl: '/Client/app/areas/customer/customer.html',
                controller: 'customerController',
                data: {
                    title: 'VaBank - Мой кабинет',
                    subtitle: 'Мой кабинет',
                    access: {
                        allowAnonymous: false,
                        roles: ['Customer']
                    }
                },
            })
            .state('customer.cabinet', {
                url: '',
                templateUrl: '/Client/app/areas/customer/cabinet/cabinet.html',
            })
            .state('customer.profile', {
                url: 'profile',
                'abstract': true,
                templateUrl: '/Client/app/areas/customer/profile/profile.html',
                data: {
                    title: 'VaBank - Профиль',
                    subtitle: 'Мой профиль'
                },
            })
            .state('customer.profile.edit', {
                url: '',
                templateUrl: '/Client/app/areas/customer/profile/profile-edit.html',
                controller: 'profileEditController'
            })
            .state('customer.profile.changePassword', {
                url: '/change-password',
                templateUrl: '/Client/app/areas/customer/profile/profile-change-pwd.html',
                controller: 'changePasswordController'
            })
            .state('customer.cards', {
                url: 'cards',
                'abstract': true,
                templateUrl: '/Client/app/areas/customer/cards/cards.html',
                data: {
                    title: 'VaBank - Платежные карты',
                    subtitle: 'Мои платежные карты'
                },
            })
            .state('customer.cards.list', {
                url: '',
                templateUrl: '/Client/app/areas/customer/cards/cards-list.html',
                controller: 'cardsListController'
            })
            .state('customer.cards.statement', {
                url: '/statement',
                templateUrl: '/Client/app/areas/customer/cards/cards-statement.html',
                controller: 'cardsStatementController'
            })
            .state('customer.cards.transfer', {
                url: '/transfer',
                templateUrl: '/Client/app/areas/customer/cards/cards-transfer.html',
                controller: 'cardsTransferController'
            });
    }

})();