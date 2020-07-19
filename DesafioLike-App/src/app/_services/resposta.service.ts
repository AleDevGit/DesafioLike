import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Resposta } from '../_models/Resposta';

@Injectable({
  providedIn: 'root'
})
export class RespostaService {
  baseUrl = 'http://localhost:5000/api/resposta/';
  jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient) { }

  cadastrarResposta(resposta: Resposta){
    console.log('Passou');
    const jwt = localStorage.getItem('token');
    const jwtDecoded = this.jwtHelper.decodeToken(jwt);
    resposta.UserId = jwtDecoded.nameid;
    return this.http.post(`${this.baseUrl}v1/adicionar/`, resposta);
  }

}
