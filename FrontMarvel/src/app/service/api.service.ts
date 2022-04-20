import { PersonagemModel } from '../model/personagens.model';
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  apiUrl = 'https://localhost:44331';

  constructor(
    private httpClient: HttpClient) { }

  public getAllPersonagens()
  {
    return this.httpClient.get(`${this.apiUrl}/api/Personagem/getListaPersonagens`)
  }

  public getPersonagens(person: PersonagemModel)
  {
    return this.httpClient.get(`${this.apiUrl}/api/Personagem/getBuscaPersonagens?nomePersonagem=${person.nome}`)
  }

}
