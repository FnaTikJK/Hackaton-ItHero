import {
  ChangeDetectionStrategy,
  Component,
  DestroyRef,
  OnInit,
} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {
  BehaviorSubject,
  combineLatest,
  debounceTime,
  distinctUntilChanged,
  filter, forkJoin, map, switchMap, take,
  tap
} from "rxjs";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";
import {CompanyEntitiesService, ISpecialization} from "../../../../shared/services/entities/company-entities.service";
import {AuthorizationService, IRegistrationCredentials} from "../../../../shared/services/authorization.service";
import {MatStepper} from "@angular/material/stepper";
import {IProfileData, IProfileDataDTO, ProfileService} from "../../../../shared/services/entities/profile.service";
import {CompanyService, ICompany, ICompanyDTO} from "../../../../shared/services/entities/company.service";
import {HttpService} from "../../../../shared/services/http.service";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegistrationComponent implements OnInit{

  protected specializations$ = this.companyEntitiesS.getSpecializations$();
  protected specializationInput = new FormControl('');
  protected specializationNameFilter$ = new BehaviorSubject('');
  protected companies$ = this.companyS.getCompanies$();

  protected roles$ = this.companyEntitiesS.getRoles$();
  constructor(
    private destroyRef: DestroyRef,
    private companyEntitiesS: CompanyEntitiesService,
    private companyS: CompanyService,
    private authS: AuthorizationService,
    private profileS: ProfileService,
    private route: ActivatedRoute,
    private router: Router,
    private httpS: HttpService
  ) {}

  firstStep = new FormGroup({
    login: new FormControl('',
    [Validators.required, Validators.minLength(3), Validators.maxLength(15)]),
    password: new FormControl('',
    [Validators.required, Validators.minLength(3), Validators.maxLength(15),
    Validators.pattern('^[a-zA-Z0-9]+$')]),
    repeatPassword: new FormControl('',
    [Validators.required, Validators.minLength(3), Validators.maxLength(15), Validators.pattern('^[a-zA-Z0-9]+$')]),
    role: new FormControl('', [Validators.required])
});

  secondStep = new FormGroup({
    firstName: new FormControl('',
      [Validators.required, Validators.pattern('[a-zA-Zа-яА-ЯёЁ]+')]),
    secondName: new FormControl('',
      [Validators.required, Validators.pattern('[a-zA-Zа-яА-ЯёЁ]+')]),
    thirdName: new FormControl('',
      [Validators.pattern('[a-zA-Zа-яА-ЯёЁ]+')]),
    specialization: new FormControl<ISpecialization[]>([], [Validators.required]),
    phoneNumber: new FormControl('',
      [Validators.required]),
    email: new FormControl(''),
    companyId: new FormControl<string>(null as unknown as string)
  });

  thirdStep = new FormGroup({
    INN: new FormControl('',
      [Validators.required, Validators.pattern('^\\d+$')]),
    KPP: new FormControl('',
      [Validators.required, Validators.pattern('^\\d+$')]),
    companyName: new FormControl('',
      [Validators.required]),
    credentials: new FormControl('',
      [Validators.pattern('[a-zA-Zа-яА-ЯёЁ]+')]),
    about: new FormControl('',
      [Validators.required]),
    sparcFile: new FormControl<File>(null as unknown as File, [Validators.required]),
    registrationFile: new FormControl<File>(null as unknown as File, [Validators.required]),
    egrulFile: new FormControl<File>(null as unknown as File, [Validators.required]),
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

  protected addSpecialization(specializationID: string, specializations: ISpecialization[] | null){
    //@ts-ignore
    this.secondStep.controls.specialization.setValue([...this.secondStep.controls.specialization.value,
      specializations?.find(s => s.id === specializationID)]);
    this.specializationInput.setValue('');
  }
  protected removeSpecialization(specializationID: string){
    this.secondStep.controls.specialization.setValue([...(this.secondStep.controls.specialization.value?.filter(spec =>
      spec.id !== specializationID) ?? [])])
  }

  protected registrate$(stepper: MatStepper) {
    const regCredentials = {...this.firstStep.value};
    delete regCredentials.repeatPassword;
    this.authS.registrate$(regCredentials as unknown as IRegistrationCredentials)
      .subscribe((res) => {
        stepper.next()
        localStorage.setItem('savedRole', JSON.stringify(res));
        this.authS.isLogged$.next(true);
      });
  }

  protected createProfile$(stepper: MatStepper) {
      const profileData = {
        ...this.secondStep.value,
        specialization: this.secondStep.value.specialization?.map(s => s.id)
      };
      const requests = [
        this.profileS.createProfile$(profileData as unknown as IProfileDataDTO)
      ];
      if (this.secondStep.value.companyId) {
        requests.push(this.companies$
          .pipe(
            map(comp => comp.find(c => c.id === this.secondStep.value.companyId)),
            take(1)
          ));
        requests.push(this.companyS.getDocuments$(this.secondStep.value.companyId));
      }

      forkJoin(requests)
        .subscribe(([profile, company, files]) => {
          if (company) {
            // this.s
          } else {
            stepper.next();
          }
        })
    }

  protected initCompany(stepper: MatStepper){
    if (this.secondStep.controls.companyId.value) {
      this.companies$.pipe(
        take(1),
        map(companies => companies.find(c => c.id === this.secondStep.controls.companyId.value)),
        switchMap(company => this.httpS.get<FormData>(`Statics/Documents/${company?.id}`)
          .pipe(
            map(files => [company, files])
          ))
      )
    } else {
      stepper.next();
    }
  }

  protected createCompany$(){
    if (this.secondStep.controls.companyId.value) {
      this.router.navigate(['../main'])
      return;
    } else {
      //@ts-ignore
      const company: ICompany = {name: this.thirdStep.value.companyName, inn: +this.thirdStep.value.INN , kpp: +this.thirdStep.value.KPP, about:  this.thirdStep.value.about}
      const formData = new FormData();
      formData.append( 'Spark',<File>this.thirdStep.value.sparcFile, 'Spark.doc');
      formData.append( 'Registration',<File>this.thirdStep.value.registrationFile, 'Registration.doc');
      formData.append( 'Egrul',<File>this.thirdStep.value.egrulFile, 'Egrul.doc');
      this.companyS.createCompany$(company, formData)
        .subscribe(() => this.router.navigate(['../main']));
    }
  }
}
