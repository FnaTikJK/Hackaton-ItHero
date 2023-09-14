import { Injectable } from '@angular/core';
import {HttpService} from "./http.service";
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  public isLogged$ =
    new BehaviorSubject<boolean>(localStorage.getItem('savedRole') ? JSON.parse(localStorage.getItem('savedRole') as string).role : false);
  constructor(
    private httpS: HttpService
  ) { }

  public login$(credentials: ILoginCredentials){
    return this.httpS.post<{role: string}>('Accounts/Login', credentials);
  }

  public registrate$(credentials: IRegistrationCredentials){
    return this.httpS.post<{role: string}>('Accounts/Register', credentials);
  }

  public signOut$(){
    return this.httpS.post('Accounts/Logout', null);
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
