import { Component } from "@angular/core";
import { OAuthService } from "angular-oauth2-oidc";

@Component({
  selector: 'callback',
  templateUrl: './callback.component.html'
})
export class CallbackComponent {
  private _token: string;
  constructor(private oauthService: OAuthService) {
    this._token = oauthService.getAccessToken();
  }
}
