import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SocketService {

  private socket = new WebSocket('wss://localhost:7250/Chats');
  constructor() {
    this.initSocket();
  }

  private initSocket() {
    this.socket.onmessage = (ev) => {

    }
  }
  sendMsg(data: Object) {
    this.socket.send(JSON.stringify(data));
  }
}
