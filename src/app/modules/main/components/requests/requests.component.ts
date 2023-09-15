import {ChangeDetectionStrategy, ChangeDetectorRef, Component} from '@angular/core';
import {RequestsService} from "../../../../shared/services/entities/requests.service";
import {ActivatedRoute, Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {CreateRequestComponent} from "../create-request/create-request.component";

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
    private router: Router,
    private matDialog: MatDialog,
    private cdr: ChangeDetectorRef
  ) {}

  protected createNewRequest() {
    this.matDialog.open(CreateRequestComponent, {height: '500px', hasBackdrop: false})
      .afterClosed()
      .subscribe(v => {
        if(v) {
          this.requestsS.addRequest(v)
          this.requests$ = this.requestsS.get$();
          this.cdr.detectChanges();
        }
      });
  }

  protected openRequestPage(id: string | undefined) {
    this.router.navigate([id], {relativeTo: this.route});
  }
}
