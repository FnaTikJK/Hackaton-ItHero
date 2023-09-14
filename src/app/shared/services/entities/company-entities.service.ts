import { Injectable } from '@angular/core';
import {BehaviorSubject, catchError, map, Observable, of, tap} from "rxjs";
import {isFirst} from "../../operators/operators";
import {HttpService} from "../http.service";

@Injectable({
  providedIn: 'root'
})
export class CompanyEntitiesService {

  private _roles$ = new BehaviorSubject<IRole[]>(null as unknown as IRole[]);
  private _specializations$ = new BehaviorSubject<ISpecialization[]>(null as unknown as ISpecialization[]);
  constructor(
    private httpS: HttpService
  ) { }

  getRoles$() {
    return this._roles$.value ? of(this._roles$.value) : this.httpS.get<ArrayLike<Object>>('Accounts/Roles')
      .pipe(
            map(roles => {
              //@ts-ignore
              const formedRoles = Object.entries(roles).map(([id, name]: [string, keyof typeof RolesTranslations]) =>
                ({id: +id, name: RolesTranslations[name]}));
              this._roles$.next(formedRoles);
              return formedRoles;
            }))
        }

  getSpecializations$() {
    return this._specializations$.value ? of(this._specializations$.value) : this.httpS.get<ISpecialization[]>('Specializations')
      .pipe(
        map(specs => {
          this._specializations$.next(specs);
          return specs;
        }))
  }
}

export interface ISpecialization {
  id: string;
  name: string;
}

export interface IRole {
  id: number;
  name: string;
}

enum RolesTranslations {
  "Admin"= "Администратор",
  "Manager"= "Менеджер",
  "Executor"= "Исполнитель",
  "Customer"= "Заказчик",
}



