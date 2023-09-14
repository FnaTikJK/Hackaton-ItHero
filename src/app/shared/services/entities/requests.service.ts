import { Injectable } from '@angular/core';
import {BehaviorSubject, of, tap} from "rxjs";
import {HttpService} from "../http.service";

@Injectable({
  providedIn: 'root'
})
export class RequestsService {

  private _requests = new BehaviorSubject<IRequest[]>(null as unknown as IRequest[]);

  constructor(
    private httpS: HttpService
  ) { this.httpS.get<IRequest[]>('Requests').subscribe(reqs => this._requests.next(reqs)) }

  public createRequest$(request: IRequest) {
    return this.httpS.post('Requests', request);
  }

  get$() {
    return this._requests.value ? of(this._requests.value) : this.httpS.get<IRequest[]>('Requests')
      .pipe(
        tap(reqs => this._requests.next(reqs))
      );
  }
}


export interface IRequest {
  ID?: number;
  name: string;
  specialization: number;
  budget: number;
  deadline: string | Date;
  about: string;
  linkedProjects: number[];
}
