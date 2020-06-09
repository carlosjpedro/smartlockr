import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OAuthService } from 'angular-oauth2-oidc';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-domain-list',
  templateUrl: './domain-list.component.html',
  styleUrls: ['./domain-list.component.css']
})
export class DomainListComponent {

  public domains: DomainInfo[]
  public newEmail: string
  private _http: HttpClient;
  private _baseUrl: string;
  private _oauthService: OAuthService;
  _signalRConnection: signalR.HubConnection;


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, oauthService: OAuthService) {
    this._baseUrl = baseUrl;
    this._http = http;
    this._oauthService = oauthService;
    this._signalRConnection = new signalR.HubConnectionBuilder().withUrl('https://localhost:44317/hub').build()
    this.updateDomainData();

    this._signalRConnection.on('update', (data) => {
      this.updateDomainData();
    })

    this._signalRConnection.start();
  }

  newEmailHandler() {
    var headers = new Headers();
    headers.set('Accept', 'text/json');
    headers.set('Authorization', 'Bearer ' + this._oauthService.getAccessToken())

    this._http.post(this._baseUrl + 'api/domain/' + this.newEmail, {
      headers: {
        'Authorization': 'Bearer ' + this._oauthService.getAccessToken()
      }
    })
      .subscribe((x: DomainInfo) => this.domains.push(x), err => console.log(err))
  }

  updateDomainData() {
    this._http.get<DomainInfo[]>(this._baseUrl + 'api/domain', { headers: { 'Authorization': 'Bearer ' + this._oauthService.getAccessToken() } })
      .subscribe(result => { this.domains = result }, console.error)
  }
}



interface DomainInfo {
  id: number,
  lastUpdated: Date
  isComplient: boolean,
  name: string
}
