import { AuthConfig } from 'angular-oauth2-oidc'

export const authCodeFlowConfig: AuthConfig = {
  issuer: 'https://localhost:5001',
  redirectUri: window.location.origin + '/callback',
  clientId: "angular-domain-verify",
  responseType: 'code',
  scope: 'openid profile domainapi',
  showDebugInformation: true,
};
