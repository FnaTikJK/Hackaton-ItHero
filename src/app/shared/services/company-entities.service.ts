import { Injectable } from '@angular/core';
import {BehaviorSubject, catchError, Observable, of} from "rxjs";
import {isFirst} from "../operators/operators";
import {HttpService} from "./http.service";

@Injectable({
  providedIn: 'root'
})
export class CompanyEntitiesService {

  private _roles$ = new BehaviorSubject<IRole[]>([]);
  private _specializations$ = new BehaviorSubject<ISpecialization[]>([]);
  public specializations$: Observable<ISpecialization[]> = this._specializations$.asObservable()
    .pipe(
      isFirst(() => this.initSpecializations())
    );
  public roles$: Observable<ISpecialization[]> = this._roles$.asObservable()
    .pipe(
      isFirst(() => this.initRoles())
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

  private initRoles() {
    this.httpS.get<ArrayLike<Object>>('Accounts/Roles')
      .subscribe((rolesObj: Object) => {
        this._roles$.next(
          Object.entries(rolesObj).map(([id, name] : [string, keyof typeof RolesTranslations]) =>
            ({ID: +id, name: RolesTranslations[name]}))
        )
      })
  }
}

export interface ISpecialization {
  ID: number;
  name: string;
}

export interface IRole {
  ID: number;
  name: string;
}

enum RolesTranslations {
  "Admin"= "Администратор",
  "Manager"= "Менеджер",
  "Executor"= "Исполнитель",
  "Customer"= "Заказчик",
}



