import {
  ChangeDetectionStrategy,
  Component,
  EnvironmentInjector,
  InjectionToken,
  Injector,
  ViewContainerRef
} from '@angular/core';
import {filter, map, switchMap, take} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {ExecutorsService} from "../../../../shared/services/entities/executors.service";
import {ChatService} from "../../../../shared/services/chat.service";
import {CreateRequestComponent} from "../create-request/create-request.component";
import {CompanyComponent} from "../company/company.component";
import {ChatComponent} from "../chat/chat.component";
import {HttpService} from "../../../../shared/services/http.service";
import {HttpClient, HttpHandler} from "@angular/common/http";
import {MainModule} from "../../main.module";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProfileComponent {

  protected reviews: IReview[] = [
    {
      reviewerName: 'Карпинский Никита Борисович',
      text: 'Могу порекомендовать данного исполнителя. Связались с ним, так как горели дедлайны. Выполнил всё качественно и в срок',
      rate: 5
    },
    {
      reviewerName: 'Димитров Анатолий Петрович',
      text: 'В наше время довольно сложно найти 1C-компанию, работающую качественно, а главное - по демократической цене. Данный исполнитель и его компания оставляют веру на лучшее',
      rate: 5
    }
  ]

  protected profiles$ = this.executorS.get$();

  protected profile$ = this.route.params
    .pipe(
      filter(params => params && params['id']),
      switchMap(params => this.profiles$
        .pipe(
          map(items => items.find(i => i['id'] === params['id']))
        )),
      take(1)
    );

  constructor(
    private route: ActivatedRoute,
    private executorS: ExecutorsService,
    private router: Router,
    private chatS: ChatService,
    private vcr: ViewContainerRef,
    private injector: Injector
  ) {}

  navigateToCompany(id: string | undefined) {
    this.router.navigate(['../../company', id], {relativeTo: this.route})
  }

  openChat(id : string | undefined) {
    if(!id) {return}
    this.chatS.createChat$({companionId: id})
      .pipe(
        switchMap(obj => this.chatS.openChat$(obj.id))
      )
      .subscribe(l => {
        this.vcr.createComponent(ChatComponent, {
          injector: Injector.create({
            providers: [
              {provide: idToken, useValue: id}
            ],
            parent: this.injector
          })
        } )
      })
  }
}

interface IReview{
  reviewerName: string;
  text: string;
  rate: number;
}

export const idToken = new InjectionToken('companionIDToken');
