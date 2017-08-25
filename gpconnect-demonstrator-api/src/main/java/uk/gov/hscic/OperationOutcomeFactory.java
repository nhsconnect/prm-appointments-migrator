package uk.gov.hscic;

import ca.uhn.fhir.model.dstu2.composite.CodeableConceptDt;
import ca.uhn.fhir.model.dstu2.resource.OperationOutcome;
import ca.uhn.fhir.model.dstu2.resource.OperationOutcome.Issue;
import ca.uhn.fhir.model.dstu2.valueset.IssueSeverityEnum;
import ca.uhn.fhir.model.dstu2.valueset.IssueTypeEnum;
import ca.uhn.fhir.rest.server.exceptions.BaseServerResponseException;

public class OperationOutcomeFactory {

    private OperationOutcomeFactory() { }

    public static BaseServerResponseException buildOperationOutcomeException(BaseServerResponseException exception, String code, IssueTypeEnum issueTypeEnum ){
        
        return buildOperationOutcomeException(exception, code, issueTypeEnum, null);
    }
    
    public static BaseServerResponseException buildOperationOutcomeException(BaseServerResponseException exception, String code, IssueTypeEnum issueTypeEnum, String diagnostics ) {
        CodeableConceptDt codeableConceptDt = new CodeableConceptDt(SystemURL.VS_GPC_ERROR_WARNING_CODE, code)
                .setText(exception.getMessage());
        codeableConceptDt.getCodingFirstRep().setDisplay(code);

        OperationOutcome operationOutcome = new OperationOutcome();

        Issue ooIssue = new Issue();
        ooIssue.setSeverity(IssueSeverityEnum.ERROR)
               .setCode(issueTypeEnum)
               .setDetails(codeableConceptDt);
        
        if(diagnostics != null){
            ooIssue.setDiagnostics(diagnostics);
        }
        
        operationOutcome.addIssue(ooIssue);
        
        operationOutcome.getMeta()
                .addProfile(SystemURL.SD_GPC_OPERATIONOUTCOME);

        exception.setOperationOutcome(operationOutcome);
        return exception;
    }
}
