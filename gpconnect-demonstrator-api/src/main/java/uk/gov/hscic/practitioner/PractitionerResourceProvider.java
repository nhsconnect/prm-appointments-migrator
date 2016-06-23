package uk.gov.hscic.practitioner;

import ca.uhn.fhir.model.dstu2.composite.CodeableConceptDt;
import ca.uhn.fhir.model.dstu2.composite.CodingDt;
import ca.uhn.fhir.model.dstu2.composite.HumanNameDt;
import ca.uhn.fhir.model.dstu2.composite.IdentifierDt;
import ca.uhn.fhir.model.dstu2.composite.ResourceReferenceDt;
import ca.uhn.fhir.model.dstu2.resource.OperationOutcome;
import ca.uhn.fhir.model.dstu2.resource.Practitioner;
import ca.uhn.fhir.model.dstu2.resource.Practitioner.PractitionerRole;
import ca.uhn.fhir.model.dstu2.valueset.AdministrativeGenderEnum;
import ca.uhn.fhir.model.dstu2.valueset.IssueSeverityEnum;
import ca.uhn.fhir.model.dstu2.valueset.NameUseEnum;
import ca.uhn.fhir.model.primitive.IdDt;
import ca.uhn.fhir.rest.annotation.IdParam;
import ca.uhn.fhir.rest.annotation.Read;
import ca.uhn.fhir.rest.server.IResourceProvider;
import ca.uhn.fhir.rest.server.exceptions.InternalErrorException;
import org.springframework.context.ApplicationContext;
import uk.gov.hscic.common.types.RepoSource;
import uk.gov.hscic.common.types.RepoSourceType;
import uk.gov.hscic.practitioner.model.PractitionerDetails;
import uk.gov.hscic.practitioner.search.PractitionerSearch;
import uk.gov.hscic.practitioner.search.PractitionerSearchFactory;

public class PractitionerResourceProvider  implements IResourceProvider {
    
    ApplicationContext applicationContext;
    
    public PractitionerResourceProvider(ApplicationContext applicationContext){
        this.applicationContext = applicationContext;
    }
    
    @Override
    public Class<Practitioner> getResourceType() {
        return Practitioner.class;
    }
    
    @Read()
    public Practitioner getPractitionerById(@IdParam IdDt practitionerId) {
        
        RepoSource sourceType = RepoSourceType.fromString(null);
        PractitionerSearch practitionerSearch = applicationContext.getBean(PractitionerSearchFactory.class).select(sourceType);
        PractitionerDetails practitionerDetails = practitionerSearch.findPractitionerDetails(practitionerId.getIdPart());

        if(practitionerDetails == null){
            OperationOutcome operationalOutcome = new OperationOutcome();
            operationalOutcome.addIssue().setSeverity(IssueSeverityEnum.ERROR).setDetails("No practitioner details found for practitioner ID: "+practitionerId.getIdPart());
            throw new InternalErrorException("No practitioner details found for practitioner ID: "+practitionerId.getIdPart(), operationalOutcome);
        }
        
        Practitioner practitioner = new Practitioner();
        
        practitioner.addIdentifier(new IdentifierDt("http://fhir.nhs.net/Id/sds-user-id", practitionerDetails.getUserId()));
        practitioner.addIdentifier(new IdentifierDt("http://fhir.nhs.net/Id/sds-role-profile-id", practitionerDetails.getRoleId()));
        
        HumanNameDt name = new HumanNameDt();
        name.addFamily(practitionerDetails.getNameFamily());
        name.addGiven(practitionerDetails.getNameGiven());
        name.addPrefix(practitionerDetails.getNamePrefix());
        name.setUse(NameUseEnum.USUAL);
        practitioner.setName(name);
        
        switch(practitionerDetails.getGender().toLowerCase()){
            case "male" : practitioner.setGender(AdministrativeGenderEnum.MALE); break;
            case "female" : practitioner.setGender(AdministrativeGenderEnum.FEMALE); break;
            case "other" : practitioner.setGender(AdministrativeGenderEnum.OTHER); break;
            default : practitioner.setGender(AdministrativeGenderEnum.UNKNOWN); break;
        }
        
        CodingDt roleCoding = new CodingDt();
        roleCoding.setSystem("http://fhir.nhs.net/ValueSet/sds-job-role-name-1-0");
        roleCoding.setCode(practitionerDetails.getRoleCode());
        roleCoding.setDisplay(practitionerDetails.getRoleDisplay());
        CodeableConceptDt roleCodableConcept = new CodeableConceptDt();
        roleCodableConcept.addCoding(roleCoding);
        PractitionerRole practitionerRole = practitioner.addPractitionerRole();
        practitionerRole.setRole(roleCodableConcept);
        
        practitionerRole.setManagingOrganization(new ResourceReferenceDt("Organization/"+practitionerDetails.getOrganizationId())); // Associated Organisation
        
        CodingDt comCoding = new CodingDt();
        comCoding.setSystem("http://fhir.nhs.net/ValueSet/human-language-1-0");
        comCoding.setCode(practitionerDetails.getComCode());
        comCoding.setDisplay(practitionerDetails.getComDisplay());
        practitioner.addCommunication().addCoding(comCoding);
        
        return practitioner;
    }
}