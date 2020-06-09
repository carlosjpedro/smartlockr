"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.authCodeFlowConfig = {
    issuer: 'https://localhost:5001',
    redirectUri: window.location.origin + '/callback',
    clientId: "angular-domain-verify",
    responseType: 'code',
    scope: 'openid profile domainapi',
    showDebugInformation: true,
};
//# sourceMappingURL=authCodeFlowConfig.js.map