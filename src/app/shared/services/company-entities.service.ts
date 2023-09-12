import { Injectable } from '@angular/core';
import {BehaviorSubject, catchError, Observable, of} from "rxjs";
import {isFirst} from "../operators/operators";
import {HttpService} from "./http.service";

@Injectable({
  providedIn: 'root'
})
export class CompanyEntitiesService {

  private _specializations$ = new BehaviorSubject<ISpecialization[]>([]);
  public specializations$: Observable<ISpecialization[]> = this._specializations$.asObservable()
    .pipe(
      isFirst(() => this.initSpecializations())
    );
  constructor(
    private httpS: HttpService
  ) { }

  private initSpecializations() {
    this.httpS.get<ISpecialization[]>('specializations')
      .pipe(
        catchError(err => of([
          {ID: 1, name: 'Сельхоз'},
          {ID: 2, name: 'Дрель-работы'},
          {ID: 3, name: 'Кокк'}
        ]))
      )
      .subscribe(specs => this._specializations$.next(specs))
  }
}

export interface ISpecialization {
  ID: number;
  name: string;
}



