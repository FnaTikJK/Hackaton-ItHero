import {ChangeDetectionStrategy, Component, DestroyRef, OnInit} from '@angular/core';
import {ExecutorsService} from "../../../../shared/services/entities/executors.service";
import {FormControl} from "@angular/forms";
import {BehaviorSubject, debounceTime} from "rxjs";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-executors',
  templateUrl: './executors.component.html',
  styleUrls: ['./executors.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ExecutorsComponent implements OnInit{

  protected executors$ = this.executorsS.get$();
  protected searchFilter = new FormControl<string>('');
  protected nameFilter$ = new BehaviorSubject<string>('');

  protected p = 1;

    constructor(
      private executorsS: ExecutorsService,
      private destoyRef: DestroyRef,
      private router: Router,
      private route: ActivatedRoute
    ) {}

  ngOnInit(): void {
        this.searchFilter.valueChanges
          .pipe(
            debounceTime(300),
            takeUntilDestroyed(this.destoyRef),
          ).subscribe(v => this.nameFilter$.next(v as string))
    }

  openProfilePage(id: string) {
      this.router.navigate(['../profiles', id], {relativeTo: this.route})
  }
}
