package uk.gov.hscic.auth;

import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.cert.X509Certificate;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
import java.util.List;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.apache.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpMethod;
import org.springframework.web.method.HandlerMethod;
import org.springframework.web.servlet.ModelAndView;
import org.springframework.web.servlet.mvc.method.AbstractHandlerMethodAdapter;
import org.springframework.web.servlet.mvc.method.annotation.RequestMappingHandlerAdapter;

/**
 * <p>This class authenticates requests by ensuring the certificate provided is in the trusted jks.</p>
 */
public final class SignedHandler extends AbstractHandlerMethodAdapter {
    private static final Logger LOG = Logger.getLogger(SignedHandler.class);

    @Autowired
    private RequestMappingHandlerAdapter baseHandlerAdapter;

    private final List<X509Certificate> knownCerts = new ArrayList<>();

    public SignedHandler(KeyStore keyStore) throws KeyStoreException {
        setOrder(-1);

        for (String alias : Collections.list(keyStore.aliases())) {
            if (keyStore.isCertificateEntry(alias)) {
                knownCerts.add((X509Certificate) keyStore.getCertificate(alias));
            }
        }
    }

    @Override
    public ModelAndView handleInternal(HttpServletRequest request, HttpServletResponse response, HandlerMethod handlerMethod) throws Exception {
        try {
            if (request.isSecure() && !HttpMethod.OPTIONS.name().equals(request.getMethod())) {
                X509Certificate[] certs = (X509Certificate[]) request.getAttribute("javax.servlet.request.X509Certificate");

                if (null == certs) {
                    throw new CertificateException("No certificate found!", 496);
                }

                X509Certificate x509Certificate = certs[0];

                if (!knownCerts.contains(x509Certificate)) {
                    throw new CertificateException("Provided certificate is not in trusted list!", 495);
                } else { // Otherwise, check the expiry
                    knownCerts.stream()
                            .filter(cert -> x509Certificate.equals(cert))
                            .peek(cert -> LOG.info("Certificate valid until: " + cert.getNotAfter()))
                            .filter(cert -> new Date().before(cert.getNotAfter()))
                            .findAny()
                            .orElseThrow(() -> new CertificateException("Provided certificate has expired!", 495));
                }
            }

            return this.baseHandlerAdapter.handle(request, response, handlerMethod);
        } catch (CertificateException certificateException) {
            StringBuilder requestURL = new StringBuilder(request.getRequestURL());
            String queryString = request.getQueryString();

            if (null != queryString) {
                requestURL.append('?').append(queryString);
            }

            LOG.error("Bad signature detected for " + request.getMethod() + " to " + requestURL, certificateException);
            response.setStatus(certificateException.getStatusCode());
            return null;
        }
    }

    @Override
    protected boolean supportsInternal(HandlerMethod handlerMethod) {
        return baseHandlerAdapter.supports(handlerMethod);
    }

    @Override
    protected long getLastModifiedInternal(HttpServletRequest request, HandlerMethod handlerMethod) {
        return baseHandlerAdapter.getLastModified(request, handlerMethod);
    }
}