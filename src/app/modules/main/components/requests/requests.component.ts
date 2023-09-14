import {ChangeDetectionStrategy, Component} from '@angular/core';
import {RequestsService} from "../../../../shared/services/entities/requests.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RequestsComponent {

  protected requests$ = this.requestsS.get$();

  constructor(
    private requestsS: RequestsService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  protected createNewRequest() {
    this.router.navigate(['../create-request'], {relativeTo: this.route});
  }

  protected openRequestPage(id: number | undefined) {
    this.router.navigate([id], {relativeTo: this.route});
  }


}
