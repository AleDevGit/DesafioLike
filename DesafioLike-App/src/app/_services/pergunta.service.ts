import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Pergunta } from '../_models/Pergunta';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PerguntaService {
  baseUrl = 'http://localhost:5000/api/pergunta/';

  constructor( private http: HttpClient){
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

}
