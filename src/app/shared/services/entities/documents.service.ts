import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class DocumentsService {

  constructor(
    private httpS: HttpClient
  ) { }

  getDocuments$(id: string){
    return this.httpS.get(`https://localhost:4200/api/Statics/Documents/${id}`, {responseType: 'arraybuffer'});
  }
}
