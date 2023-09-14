import { Injectable } from '@angular/core';
import {HttpService} from "../http.service";
import {forkJoin} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(
    private httpS: HttpService
  ) { }

  public createCompany$(company: ICompany, companyFiles: FormData) {
   return forkJoin([
     this.httpS.post('Companies', company),
     this.httpS.post('Statics/Documents/My', companyFiles)
   ]);
  }
}

export interface ICompany {
  name: string;
  inn: string;
  kpp: string;
}
