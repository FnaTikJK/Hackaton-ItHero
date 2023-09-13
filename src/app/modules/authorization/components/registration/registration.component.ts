import {
  ChangeDetectionStrategy,
  Component,
  DestroyRef,
  OnInit,
} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {BehaviorSubject, combineLatest, debounceTime, distinctUntilChanged, filter, tap} from "rxjs";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";
import {CompanyEntitiesService, ISpecialization} from "../../../../shared/services/company-entities.service";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegistrationComponent implements OnInit{

  protected specializations$ = this.companyEntitiesS.specializations$;
  protected specializationInput = new FormControl('');
  protected specializationNameFilter$ = new BehaviorSubject('');
  constructor(
    private destroyRef: DestroyRef,
    private companyEntitiesS: CompanyEntitiesService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  firstStep = new FormGroup({
    email: new FormControl('',
    [Validators.required, Validators.minLength(3), Validators.maxLength(15)]),
    password: new FormControl('',
    [Validators.required, Validators.minLength(3), Validators.maxLength(15),
    Validators.pattern('^[a-zA-Z0-9]+$')]),
    repeatPassword: new FormControl('',
    [Validators.required, Validators.minLength(3), Validators.maxLength(15), Validators.pattern('^[a-zA-Z0-9]+$')])
});

  secondStep = new FormGroup({
    name: new FormControl('',
      [Validators.required, Validators.pattern('[a-zA-Zа-яА-ЯёЁ]+')]),
    surname: new FormControl('',
      [Validators.required, Validators.pattern('[a-zA-Zа-яА-ЯёЁ]+')]),
    patronic: new FormControl('',
      [Validators.pattern('[a-zA-Zа-яА-ЯёЁ]+')])
  });

  thirdStep = new FormGroup({
    INN: new FormControl('',
      [Validators.required, Validators.pattern('^\\d+$')]),
    companyName: new FormControl('',
      [Validators.required]),
    specialization: new FormControl<ISpecialization[]>([], [Validators.required]),
    credentials: new FormControl('',
      [Validators.pattern('[a-zA-Zа-яА-ЯёЁ]+')]),
    phoneNumber: new FormControl('',
      [Validators.required]),
    email: new FormControl(''),
    sparcFile: new FormControl(null, [Validators.required]),
    registrationFile: new FormControl(null, [Validators.required]),
    egrulFile: new FormControl(null, [Validators.required]),
  });

  protected form = new FormGroup({
    firstStep: this.firstStep,
    secondStep: this.secondStep,
    thirdStep: this.thirdStep
    });

  ngOnInit(): void {
    this.initFirstForm();
    this.initThirdForm();
  }

  private initFirstForm(){
    combineLatest([
      this.form.controls.firstStep.controls['password'].valueChanges,
      this.form.controls.firstStep.controls['repeatPassword'].valueChanges
    ]).pipe(
      filter(() => !!this.form.controls.firstStep.controls['password'].value && !!this.form.controls.firstStep.controls['repeatPassword'].value),
      takeUntilDestroyed(this.destroyRef)
    )
      .subscribe(([fCurrent, sCurrent]) => {
        if (fCurrent !== sCurrent) {
          this.firstStep.controls['password'].setErrors({...this.firstStep.controls['password'].errors, mismatchedPasswords: true});
          this.firstStep.controls['repeatPassword'].setErrors({...this.firstStep.controls['repeatPassword'].errors, mismatchedPasswords: true});
        } else {
          const fNewErrors = {...this.firstStep.controls['password'].errors};
          if (this.firstStep.controls['password'].hasError('mismatchedPasswords')) { delete fNewErrors['mismatchedPasswords']; }
          const sNewErrors = {...this.firstStep.controls['repeatPassword'].errors};
          if (this.firstStep.controls['repeatPassword'].hasError('mismatchedPasswords')) { delete sNewErrors['mismatchedPasswords']; }
          this.firstStep.controls['password'].setErrors(Object.keys(fNewErrors).length ? fNewErrors : null);
          this.firstStep.controls['repeatPassword'].setErrors(Object.keys(sNewErrors).length ? sNewErrors : null);
          this.firstStep.updateValueAndValidity();
        }
      })
  }

  private initThirdForm(){
    this.specializationInput.valueChanges
      .pipe(
        debounceTime(100),
        distinctUntilChanged(),
        tap(v => this.specializationNameFilter$.next(v as string)),
        takeUntilDestroyed(this.destroyRef)
      ).subscribe();
  }

  protected addSpecialization(specializationID: number, specializations: ISpecialization[] | null){
    //@ts-ignore
    this.thirdStep.controls.specialization.setValue([...this.thirdStep.controls.specialization.value,
      specializations?.find(s => s.ID === specializationID)]);
    this.specializationInput.setValue('');
  }
  protected removeSpecialization(specializationID: number){
    this.thirdStep.controls.specialization.setValue([...(this.thirdStep.controls.specialization.value?.filter(spec =>
      spec.ID !== specializationID) ?? [])])
  }

  protected registrate() {
    // this.authS.registrate({login: this.form.controls['login'].value as string, password: this.form.controls['password'].value as string});
    // this.router.navigate(['../', 'login'], {relativeTo: this.route});
  }

}
