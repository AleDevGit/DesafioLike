import { Injectable } from '@angular/core';
import { HttpClientModule, HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Categoria } from '../_models/Categoria';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {
  baseUrl = 'http://localhost:5000/api/categoria/';
  constructor( private http: HttpClient){
   }

  getCategorias(): Observable<Categoria[]>{
    return this.http.get<Categoria[]>(`${this.baseUrl}v1/obtertodos/`);
  }

  getCategoriaId(id: number): Observable<Categoria>{
    return this.http.get<Categoria>(`${this.baseUrl}v1/obterPorId/${id}`);
  }

  cadastrarCategoria(categoria: Categoria){
    return this.http.post(`${this.baseUrl}v1/cadastrar/`, categoria);
  }
  alterarCategoria(categoria: Categoria){
    return this.http.put(`${this.baseUrl}v1/atualizar/${categoria.id}`, categoria);
  }

}
