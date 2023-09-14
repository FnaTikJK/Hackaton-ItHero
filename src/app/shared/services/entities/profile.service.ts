import { Injectable } from '@angular/core';
import {HttpService} from "../http.service";
import {ISpecialization} from "./company-entities.service";

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(
    private httpS: HttpService
  ) { }

  public createProfile$(profileData: IProfileData) {
    return this.httpS.post('Profiles', profileData);
  }
}

export interface IProfileData{
  "secondName": string;
  "firstName": string;
  "thirdName": string;
  "specialization": number[];
  "phoneNumber": string;
  "email": string;
}
