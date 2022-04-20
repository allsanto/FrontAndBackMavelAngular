import { ApiService } from './../service/api.service';
import { Component, OnInit } from '@angular/core';
import { PersonagemModel } from '../model/personagens.model';
import { retry } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit{
  person: PersonagemModel = new PersonagemModel();
  personagens: PersonagemModel;

  constructor(private personService: ApiService){}

  ngOnInit(){
    this.listarPersonagens();
  }

  load(){
    (sessionStorage.refresh = 'true' || !sessionStorage.refresh) && location.reload()
    sessionStorage.refresh = false;
  }


  listarPersonagens() {
      this.personService.getAllPersonagens().subscribe((data: PersonagemModel) => {
      this.personagens = data;
      console.log('O data que recebemos', data);
      console.log('Variavel preenchida', this.personagens);
        }, err => {
          console.log('Erro ao listar Personagens', err)
        }
      )
    }

    buscarPersonagens() {
      this.personService.getPersonagens(this.person).subscribe((data: PersonagemModel) => {
        this.personagens = data;
        console.log('O data que recebemos', data);
        console.log('Variavel preenchida', this.personagens);
          }, err => {
            console.log('Erro ao listar Personagens', err)
          }
        )
    }

    recarregar() {
      this.listarPersonagens();
    }
}

