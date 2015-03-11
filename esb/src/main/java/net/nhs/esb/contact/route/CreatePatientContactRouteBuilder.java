package net.nhs.esb.contact.route;

import net.nhs.esb.contact.model.ContactUpdate;
import net.nhs.esb.openehr.route.CompositionCreateParameters;
import org.apache.camel.ExchangePattern;
import org.apache.camel.component.cxf.common.message.CxfConstants;
import org.apache.camel.spring.SpringRouteBuilder;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

/**
 */
@Component
public class CreatePatientContactRouteBuilder extends SpringRouteBuilder {

    @Value("${openehr.contactsTemplate}")
    private String contactTemplate;

    @Autowired
    private CompositionCreateParameters compositionCreateParameters;

    @Override
    public void configure() throws Exception {

        from("direct:createPatientContactComposition").routeId("openEhrCreatePatientContactComposition")
                .convertBodyTo(ContactUpdate.class)
                .setHeader("composition", simple("${body.content}"))
                .to("direct:openEhrCreatePatientContactComposition");

        from("direct:openEhrCreatePatientContactComposition")
                .to("direct:setHeaders")
                .to("direct:createSession")
                .to("direct:getEhrId")
                .setExchangePattern(ExchangePattern.InOut)
                .setHeader(CxfConstants.CAMEL_CXF_RS_USING_HTTP_API, constant(Boolean.FALSE))
                .setHeader(CxfConstants.OPERATION_NAME, constant("createComposition"))
                .setHeader("template", constant(contactTemplate))
                .bean(compositionCreateParameters)
                .to("cxfrs:bean:rsOpenEhr")
                .end();
    }
}