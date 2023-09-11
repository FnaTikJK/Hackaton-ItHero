import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpRequest} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private url = 'localhost:4200';
  constructor(
    private httpClient: HttpClient
  ) {}

  public get(method: string) {
    return this.httpClient.get(`https://${this.url}/api/${method}`);
  }

  public post(method: string, body: any, options?: { headers: HttpHeaders | {[p: string] : string} }) {
    return this.httpClient.post(`https://${this.url}/api/${method}`, body, options);
  }

  public put(method: string, body: any, options?: { headers: HttpHeaders | {[p: string] : string} }) {
    return this.httpClient.put(`https://${this.url}/api/${method}`, body, options);
  }

  public patch(method: string, body: any, options?: { headers: HttpHeaders | {[p: string] : string} }) {
    return this.httpClient.patch(`https://${this.url}/api/${method}`, body, options);
  }

  public delete(method: string, options?: { headers: HttpHeaders | {[p: string] : string} }) {
    return this.httpClient.delete(`https://${this.url}/api/${method}`, options);
  }

}
