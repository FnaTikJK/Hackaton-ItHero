import {ChangeDetectionStrategy, Component} from '@angular/core';
import {SocketService} from "./shared/socket.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  constructor(
    private socketS: SocketService
  ) {}
}
