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
  ) { this.httpS.get<IRequest[]>('Applications/My').subscribe(reqs => this._requests.next(reqs)) }

  public createRequest$(request: IRequest) {
    return this.httpS.post('Applications/Create', request);
  }

  get$() {
    return this._requests.value ? this._requests : this.httpS.get<IRequest[]>('Applications/My');
  }

  addRequest(req: IRequest) {
    this._requests.next([...this._requests.value, req]);
  }
}


export interface IRequest  {
  "id": string,
  "title": string,
  "budget": 0,
  "text": string
}
