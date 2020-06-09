import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { AuthConfig } from 'angular-oauth2-oidc';
import { authCodeFlowConfig } from './authCodeFlowConfig';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor(private oauthService: OAuthService) {
    this.oauthService.configure(authCodeFlowConfig);
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
  }
  title = 'app';
}
