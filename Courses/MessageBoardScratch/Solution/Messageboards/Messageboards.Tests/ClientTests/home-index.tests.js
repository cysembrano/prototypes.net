/// <reference path="../scripts/jasmine.js" />
/// <reference path="../../messageboards.web/scripts/angular.js" />
/// <reference path="../../messageboards.web/scripts/angular-route.js" />
/// <reference path="../../messageboards.web/scripts/angular-mocks.js" />
/// <reference path="../../messageboards.web/js/home-index.js" />


describe("home-index Tests->", function () {

    beforeEach(function () {
        module("homeIndex");
    });

    var $httpBackend;

    beforeEach(inject(function ($injector) {
        $httpBackend = $injector.get("$httpBackend");
        $httpBackend.when("GET", "/api/v1/topics?includeReplies=true")
            .respond([
                {
                    title: "first title",
                    body: "some body",
                    id: 1,
                    created: "20050401"
                },
                {
                    title: "second title",
                    body: "another body",
                    id: 1,
                    created: "20050401"
                }
            ]);
    }));

    afterEach(function () {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    describe("dataService -> ", function () {
        it("can load topics", inject(function (dataService) {

            expect(dataService.topics).toEqual([]);

            $httpBackend.expectGET("/api/v1/topics?includeReplies=true");
            dataService.getTopics();
            $httpBackend.flush();
            expect(dataService.topics.length).toBeGreaterThan(0);
            expect(dataService.topics.length).toEqual(2);


        }));
    });

    describe("topicsController ->", function () {
        it("load data", inject(function ($controller, $http, dataService) {
            var theScope = {};
            $httpBackend.expectGET("/api/v1/topics?includeReplies=true");
            var ctrl = $controller("topicsController", {
                $scope: theScope,
                $http: $http,
                dataService: dataService
            });
            $httpBackend.flush();

            expect(ctrl).not.toBeNull();
            expect(theScope.data).toBeDefined();
        }));
    });
});