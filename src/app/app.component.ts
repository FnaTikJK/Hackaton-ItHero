import {ChangeDetectionStrategy, Component} from '@angular/core';
import {SocketService} from "./shared/services/socket.service";
import {AuthorizationService} from "./shared/services/authorization.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppComponent {
  constructor(
    private socketS: SocketService,
    private authS: AuthorizationService,
    private router: Router
  ) {}

  protected signOut$() {
    this.authS.signOut$()
      .subscribe(res => this.router.navigate(['/authorization']))
  }
}
