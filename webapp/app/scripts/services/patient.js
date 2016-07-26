'use strict';

angular.module('gpConnect')
  .factory('PatientService', function ($http, EnvConfig) {

    var findAllSummaries = function () {
      return $http.get('/api/patients');
    };

    var getSummary = function (patientId) {
      return $http.post(EnvConfig.restUrlPrefix+'/Patient/$gpc.getcarerecord',
                '{"resourceType" : "Parameters","parameter" : [{"name" : "patientNHSNumber","valueIdentifier" : { "value" : "'+patientId+'" }},{"name" : "recordSection","valueString" : "SUM"},{"name" : "timePeriod","valuePeriod" : { "start" : "2015", "end" : "2016" }}]}',
                {
                    headers: {
                        'Ssp-From': EnvConfig.fromASID,
                        'Ssp-To': EnvConfig.toASID,
                        'Ssp-InteractionID': "urn:nhs:names:services:gpconnect:fhir:operation:gpc.getcarerecord"
                    }
                }
              );
    };
    
    var getPatientFhirId = function (patientId) {
      var response;
      return $http.get(EnvConfig.restUrlPrefix+'/Patient?identifier=http://fhir.nhs.net/Id/nhs-number|'+patientId,
                {
                    headers: {
                        'Ssp-From': EnvConfig.fromASID,
                        'Ssp-To': EnvConfig.toASID,
                        'Ssp-InteractionID': "urn:nhs:names:services:gpconnect:fhir:rest:search:patient"
                    }
                }
                ).then(function(response) {
         return response.data.entry[0].resource.id;
      });
    };
    
    var getFhirPatient = function (patientId) {
      var response;
      return $http.get(EnvConfig.restUrlPrefix+'/Patient?identifier=http://fhir.nhs.net/Id/nhs-number|'+patientId,
                {
                    headers: {
                        'Ssp-From': EnvConfig.fromASID,
                        'Ssp-To': EnvConfig.toASID,
                        'Ssp-InteractionID': "urn:nhs:names:services:gpconnect:fhir:rest:search:patient"
                    }
                }
                ).then(function(response) {
         return response.data.entry[0].resource;
      });
    };

    return {
      findAllSummaries: findAllSummaries,
      getSummary: getSummary,
      getPatientFhirId: getPatientFhirId,
      getFhirPatient: getFhirPatient
    };

  });
