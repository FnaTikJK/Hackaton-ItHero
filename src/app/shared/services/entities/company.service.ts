import { Injectable } from '@angular/core';
import {HttpService} from "../http.service";
import {BehaviorSubject, forkJoin, of, tap} from "rxjs";
import {IProfileData} from "./profile.service";

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  private _companies = new BehaviorSubject<ICompany[]>(null as unknown as ICompany[])
  constructor(
    private httpS: HttpService
  ) { }

  public createCompany$(company: ICompanyDTO, companyFiles: FormData) {
   return forkJoin([
     this.httpS.post('Companies', company),
     this.httpS.post('Statics/Documents/My', companyFiles)
   ]);
  }

  public getCompanies$(){
    return this._companies.value ? of(this._companies.value) : this.httpS.get<ICompany[]>('Companies')
      .pipe(
        tap(companies => this._companies.next(companies))
      );
  }
}

export interface ICompanyDTO {
  id: string;
  name: string;
  inn: string;
  kpp: string;
}

export interface ICompany {
  "id": string,
  "name": string,
  "inn": number,
  "kpp": number,
  "workers": IProfileData[]
}
