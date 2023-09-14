import {ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {HttpService} from "../../../../shared/services/http.service";
import {IRequest, RequestsService} from "../../../../shared/services/entities/requests.service";
import {ActivatedRoute, Router} from "@angular/router";
import {filter, map, Observable, switchMap, take} from "rxjs";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {CompanyEntitiesService} from "../../../../shared/services/entities/company-entities.service";

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RequestComponent implements OnInit{

  protected requests$ = this.requestsS.requests$;

  protected request$ = this.route.params
    .pipe(
      filter(params => params && params['ID']),
      switchMap(params => this.requests$
        .pipe(
          map(items => items.find(i => i['ID'] === params['ID']))
        )),
      take(1)
    );

  protected specializations$ = this.companyEntitiesS.specializations$;

  protected form = new FormGroup({
    name: new FormControl<string>('', [Validators.required]),
    specialization: new FormControl<number>(-1, [Validators.required]),
    budget: new FormControl<number>(0, [Validators.required]),
    deadline: new FormControl<string>('', [Validators.required]),
    about: new FormControl<string>('', [Validators.required]),
    linkedProjects: new FormControl<number[]>([], [Validators.required])
  });

  constructor(
    private httpS: HttpService,
    private requestsS: RequestsService,
    private route: ActivatedRoute,
    private companyEntitiesS: CompanyEntitiesService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form.disable();
  }

  protected changeFormState() {
    if (this.form.disabled) { this.form.enable(); }
    else { this.form.disable() }
  }

  protected saveChanges(){

  }
}
