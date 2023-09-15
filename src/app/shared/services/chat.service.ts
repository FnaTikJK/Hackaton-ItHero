import { Injectable } from '@angular/core';
import {HttpService} from "./http.service";
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(
    private httpS: HttpService
  ) { }

  public messages = new BehaviorSubject<string[]>([]);
  createChat$(obj: {"companionId": string}){
    return this.httpS.post<{id: string}>('Chats', obj);
  }

  openChat$(id: string) {
    return this.httpS.get<string>(`Chats/${id}`);
  }

  sendMess$(id: string, mess: string) {
    return this.httpS.post(`Chats/${id}`, {message: mess});
  }

  addMess$(mess: string) {
    this.messages.next([...this.messages.value, mess]);
  }
}
