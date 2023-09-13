import { Injectable } from '@angular/core';
import {HttpService} from "./http.service";

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  constructor(
    private httpS: HttpService
  ) { }

  public login$(credentials: ILoginCredentials){
    return this.httpS.post('Accounts/Login', credentials);
  }

  public registrate$(credentials: IRegistrationCredentials){
    return this.httpS.post('Accounts/Register', credentials);
  }
}

export interface ILoginCredentials{
  login: string;
  password: string;
}

export interface IRegistrationCredentials{
  login: string;
  password: string;
  role: number;
}
