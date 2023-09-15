import {ChangeDetectionStrategy, Component} from '@angular/core';
import {filter, map, Observable, of, switchMap, take} from "rxjs";
import {ActivatedRoute} from "@angular/router";
import {CompanyService, ICompany} from "../../../../shared/services/entities/company.service";
import {HttpService} from "../../../../shared/services/http.service";
import {DocumentsService} from "../../../../shared/services/entities/documents.service";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CompanyComponent {

  protected companies$ = this.companyS.getCompanies$();

  protected company$ = this.route.params
    .pipe(
      filter(params => params && params['id']),
      switchMap(params => this.companies$
        .pipe(
          map(items => items.find(i => i['id'] === params['id']) as ICompany)
        )),
      take(1)
    );

  // protected files$ = this.route.params
  //   .pipe(
  //     filter(params => params && params['id']),
  //     switchMap(params => of({
  //       sparc: this.domSanitizer.bypassSecurityTrustResourceUrl(`/../API/API.sln`),
  //       registration: this.domSanitizer.bypassSecurityTrustResourceUrl(`../API/API/Modules/StaticsModule/Files/Photos/Registration__${params['id']}.doc`),
  //       egrul: this.domSanitizer.bypassSecurityTrustResourceUrl(`../API/API/Modules/StaticsModule/Files/Photos/Egrul__${params['id']}.doc`)
  //     })),
  //     take(1)
  //   )

  constructor(
    private route: ActivatedRoute,
    private companyS: CompanyService,
    private httpS: HttpService,
    private documentsS: DocumentsService,
    private domSanitizer: DomSanitizer
  ) {}
}
