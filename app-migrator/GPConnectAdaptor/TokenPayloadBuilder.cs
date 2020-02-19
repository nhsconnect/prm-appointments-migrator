using System;
using System.Collections.Generic;
using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.Jwt;
using GPConnectAdaptor.Models.Slot;
using Identifier = GPConnectAdaptor.Models.Jwt.Identifier;
using JwtModel = GPConnectAdaptor.Models.Jwt.JwtModel;
using Name = GPConnectAdaptor.Models.Jwt.Name;

namespace GPConnectAdaptor
{
    public class TokenPayloadBuilder : ITokenPayloadBuilder
    {
        public JwtModel BuildPayload(Scope scope)
        {
            return new JwtModel()
            {
                iss = "https://orange.testlab.nhs.uk/",
                sub = "1",
                aud = "https://orange.testlab.nhs.uk/gpconnect-demonstrator/v1/fhir",
                exp = (int)(DateTime.UtcNow.AddMinutes(5) - new DateTime(1970, 1, 1)).TotalSeconds, //Epoch time
                iat = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds,
                reason_for_request = "directcare",
                requested_scope = scope == Scope.OrgRead ? "organization/*.read" : "patient/*.write",
                requesting_device = new Models.Jwt.RequestingDevice()
                {
                    resourceType = "Device",
                    identifier = new List<Identifier>()
                    {
                        new Identifier()
                        {
                            system = "https://orange.testlab.nhs.uk/gpconnect-demonstrator/Id/local-system-instance-id",
                            value = "gpcdemonstrator-1-orange"
                        }
                    },
                    model  = "GP Connect Demonstrator",
                    version = "1.2.3"
                },
                requesting_organization = new Models.Jwt.RequestingOrganization()
                {
                    resourceType = "Organization",
                    identifier = new List<Identifier>()
                    {
                        new Identifier()
                        {
                            system = "https://fhir.nhs.uk/Id/ods-organization-code",
                            value = "A11111"
                        }
                    },
                    name = "Consumer organisation name"
                },
                requesting_practitioner = new Models.Jwt.RequestingPractitioner()
                {
                    resourceType = "Practitioner",
                    id = "1",
                    identifier = new List<Identifier>()
                    {
                        new Identifier()
                        {
                            system = "https://fhir.nhs.uk/Id/sds-user-id",
                            value = "111111111111"
                        },
                        new Identifier()
                        {
                            system = "https://fhir.nhs.uk/Id/sds-role-profile-id",
                            value = "22222222222222"
                        },
                        new Identifier()
                        {
                            system = "https://orange.testlab.nhs.uk/gpconnect-demonstrator/Id/local-user-id",
                            value = "1"
                        }
                    },
                    name = new List<Models.Jwt.Name>()
                    {
                        new Models.Jwt.Name()
                        {
                            family = "Demonstrator",
                            given = new List<string>()
                            {
                                "GPConnect"
                            },
                            prefix = new List<string>()
                            {
                                "Dr"
                            }
                        }
                    }
                }
            };
        }
    }

    public enum Scope
    {
        OrgRead,
        PatientWrite
    }
}