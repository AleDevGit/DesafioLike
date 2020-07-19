import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Pergunta } from '../_models/Pergunta';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class PerguntaService {
  baseUrl = 'http://localhost:5000/api/pergunta/';
  jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient){
    }

  getPerguntas(): Observable<Pergunta[]>{
    return this.http.get<Pergunta[]>(`${this.baseUrl}v1/obtertodos/`);
  }

  getPerguntaId(id: number): Observable<Pergunta>{
    return this.http.get<Pergunta>(`${this.baseUrl}v1/obterPorId/${id}`);
  }

  cadastrarPergunta(pergunta: Pergunta){
    return this.http.post(`${this.baseUrl}v1/adicionar/`, pergunta);
  }

  alterarPergunta(pergunta: Pergunta){
    return this.http.put(`${this.baseUrl}v1/atualizar/${pergunta.id}`, pergunta);
  }

  getProximaPergunta(): Observable<Pergunta>{
    const jwt = localStorage.getItem('token');
    const jwtDecoded = this.jwtHelper.decodeToken(jwt);
    return this.http.get<Pergunta>(`${this.baseUrl}v1/obterpergunta/${jwtDecoded.nameid}`);
  }

}
