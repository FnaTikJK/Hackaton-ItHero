import {ChangeDetectionStrategy, Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AuthorizationService, ILoginCredentials} from "../../../../shared/services/authorization.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent {

  constructor(
    private authS: AuthorizationService,
    private router: Router
  ) {}

  protected form = new FormGroup({
    login: new FormControl('',
      [Validators.required, Validators.minLength(3), Validators.maxLength(15)]),
    password: new FormControl('',
      [Validators.required, Validators.minLength(3), Validators.maxLength(15),
        Validators.pattern('^[a-zA-Z0-9]+$')])
  });

  protected login$(){
    this.authS.login$(this.form.value as ILoginCredentials)
      .subscribe(res => console.log('Успех'))
  }
}
