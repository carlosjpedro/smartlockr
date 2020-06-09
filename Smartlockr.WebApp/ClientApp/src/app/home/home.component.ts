import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private oauthService: OAuthService) {
  }

  public login() {
    this.oauthService.initLoginFlow();
  }

  public logout() {
    this.oauthService.logOut();
  }

  public get userName() {

    var claims = this.oauthService.getIdentityClaims();
    if (!claims) return null;

    return claims["given_name"];
  }
}
