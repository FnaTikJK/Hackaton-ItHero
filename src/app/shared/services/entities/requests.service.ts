import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {HttpService} from "../http.service";

@Injectable({
  providedIn: 'root'
})
export class RequestsService {

  private _requests = new BehaviorSubject<IRequest[]>([]);
  public requests$ = this._requests.asObservable();

  constructor(
    private httpS: HttpService
  ) { this.httpS.get<IRequest[]>('Requests').subscribe(reqs => this._requests.next(reqs)) }

  public createRequest$(request: IRequest) {
    return this.httpS.post('Requests', request);
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
