import {ChangeDetectionStrategy, Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {CompanyEntitiesService} from "../../../../shared/services/entities/company-entities.service";
import {ActivatedRoute, Router} from "@angular/router";
import {IRequest, RequestsService} from "../../../../shared/services/entities/requests.service";

@Component({
  selector: 'app-create-request',
  templateUrl: './create-request.component.html',
  styleUrls: ['./create-request.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreateRequestComponent {

  protected form = new FormGroup({
    name: new FormControl<string>('', [Validators.required]),
    specialization: new FormControl<number>(-1, [Validators.required]),
    budget: new FormControl<number>(0, [Validators.required]),
    deadline: new FormControl<string>('', [Validators.required]),
    about: new FormControl<string>('', [Validators.required]),
    linkedProjects: new FormControl<number[]>([])
  });

  protected specializations$ = this.companyEntityS.getSpecializations$();
  protected requests$ = this.requestsS.get$();

  constructor(
    private companyEntityS: CompanyEntitiesService,
    private requestsS: RequestsService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  createNewRequest() {
    this.requestsS.createRequest$(this.form.value as IRequest)
      .subscribe((res) => this.router.navigate(['../requests'], {relativeTo: this.route}));
  }

  returnToRequestsPage() {
    this.router.navigate(['../requests'], {relativeTo: this.route});
  }
}
