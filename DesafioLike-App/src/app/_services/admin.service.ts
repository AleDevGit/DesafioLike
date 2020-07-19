import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Role } from '../_models/Role';
import { Observable } from 'rxjs';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  baseUrl = 'http://localhost:5000/api/Administration/';

  constructor( private http: HttpClient) { }

  cadastrarRegra(role: Role){
    return this.http.post(`${this.baseUrl}v1/cadastrarregra/`, role);
  }

  listarRegra(){
    return this.http.get(`${this.baseUrl}v1/obtertodosregra/`);
  }

  alterarRegra(role: Role){
    return this.http.put(`${this.baseUrl}v1/atualizarregra/${role.id}`, role);
  }

  excluirRegra(id: number){
    return this.http.delete(`${this.baseUrl}v1/excluirregra/${id}`);
  }

  buscarRegra(id: number): Observable<Role>{
    return this.http.get<Role>(`${this.baseUrl}v1/obterRegraPorId/${id}`);
  }

  buscarUsuarios(id: number): Observable<User[]>{
    return this.http.get<User[]>(`${this.baseUrl}v1/obterUsuarios/${id}`);
  }

  adicionarPermissao(userModel: User, name: string) {
     return this.http.put<User[]>(`${this.baseUrl}v1/adicionarPermissao/${name}`, userModel );
  }

  buscarUsuarioRegra(id: number): Observable<User[]>{
    return this.http.get<User[]>(`${this.baseUrl}v1/obterUsuarioRegraPorId/${id}`);
  }

  excluirPermissao(idRegra: number, idUser: number){
    return this.http.delete(`${this.baseUrl}v1/excluirpermissao/${idRegra}/${idUser}`);
  }
  
}
