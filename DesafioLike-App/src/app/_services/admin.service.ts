import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Role } from '../_models/Role';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = 'http://localhost:5000/api/Administration/';

constructor( private http: HttpClient) { }

cadastrarRole(role: Role){
  return this.http.post(`${this.baseUrl}v1/cadastrar/`, role);
}

listarRegras(){
  return this.http.get(`${this.baseUrl}v1/Obtertodos/`);
}


alterarRole(role: Role){
  return this.http.put(`${this.baseUrl}v1/atualizar/${role.id}`, role);
}

}
