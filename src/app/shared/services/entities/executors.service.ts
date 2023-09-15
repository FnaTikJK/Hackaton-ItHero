import { Injectable } from '@angular/core';
import {BehaviorSubject, map, of, tap} from "rxjs";
import {IRequest} from "./requests.service";
import {HttpService} from "../http.service";
import {IProfileData} from "./profile.service";

@Injectable({
  providedIn: 'root'
})
export class ExecutorsService {

  private _executors$ = new BehaviorSubject<IProfileData[]>(null as unknown as IProfileData[]);
  constructor(
    private httpS: HttpService
  ) { }

  get$() {
    return this._executors$.value ? of(this._executors$.value) : this.httpS.get<{profiles: IProfileData[]}>('Search?Take=10')
      .pipe(
        map(executors => executors.profiles),
        tap(executors => this._executors$.next(executors))
      );
  }
}
