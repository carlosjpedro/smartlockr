import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-domain-list',
  templateUrl: './domain-list.component.html',
  styleUrls: ['./domain-list.component.css']
})
export class DomainListComponent {

  public domains: DomainInfo[]
  public newEmail: string
  http: HttpClient;
  baseUrl: string;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
    this.http.get<DomainInfo[]>(baseUrl + 'api/domain')
      .subscribe(x => { this.domains = x }, console.error)
  }


  newEmailHandler(value) {
    this.http.post(this.baseUrl + 'api/domain/' + this.newEmail, {}).subscribe((x: DomainInfo) => this.domains.push(x), err => console.log(err))
  }
}

interface DomainInfo {
  id: number,
  lastUpdated: Date
  isComplient: boolean,
  name: string
}
