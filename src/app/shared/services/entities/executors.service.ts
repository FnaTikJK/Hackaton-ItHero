import { Injectable } from '@angular/core';
import {BehaviorSubject, of, tap} from "rxjs";
import {IRequest} from "./requests.service";
import {HttpService} from "../http.service";

@Injectable({
  providedIn: 'root'
})
export class ExecutorsService {

  private _executors$ = new BehaviorSubject<any>(null);
  constructor(
    private httpS: HttpService
  ) { }

  get$() {
    return this._executors$.value ? of(this._executors$.value) : this.httpS.get('Executors')
      .pipe(
        tap(executors => this._executors$.next(executors))
      );
  }
}
