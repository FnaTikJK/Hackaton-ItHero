import { Injectable } from '@angular/core';
import {HttpService} from "../http.service";
import {ISpecialization} from "./company-entities.service";
import {ICompany, ICompanyDTO} from "./company.service";

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(
    private httpS: HttpService
  ) { }

  public createProfile$(profileData: IProfileDataDTO) {
    return this.httpS.post('Profiles', profileData);
  }
}

export interface IProfileData{
  "id": string;
  "secondName": string;
  "firstName": string;
  "thirdName": string;
  "specialization": ISpecialization[];
  company: ICompany;
  "phoneNumber": string;
  "email": string;
}

export interface IProfileDataDTO{
  "secondName": string;
  "firstName": string;
  "thirdName": string;
  "specialization": string[];
  companyId: string;
  "phoneNumber": string;
  "email": string;
}

