import {Component, Inject} from '@angular/core';
import {ENTER} from "@angular/cdk/keycodes";
import {ChatService} from "../../../../shared/services/chat.service";
import {idToken} from "../profile/profile.component";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {

  messages$ = this.chatS.messages;
  constructor(
    private chatS: ChatService,
    @Inject(idToken) private companionID: string
  ) {}
  sendMess(mess: string, ev: KeyboardEvent) {
    if(mess.trim().length && ev.keyCode === ENTER) {
      this.chatS.addMess$(mess);
      // this.chatS.sendMess$(this.companionID, mess)
      //   .subscribe()
    }
  }
}
