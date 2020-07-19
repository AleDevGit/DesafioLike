import { Component, OnInit, TemplateRef } from '@angular/core';
import { CategoriaService } from '../_services/categoria.service';
import { Categoria } from '../_models/Categoria';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { templateJitUrl } from '@angular/compiler';
import { Template } from '@angular/compiler/src/render3/r3_ast';


@Component({
  selector: 'app-categorias',
  templateUrl: './categorias.component.html',
  styleUrls: ['./categorias.component.css']
})
export class CategoriasComponent implements OnInit {
  titulo = 'Categorias';
  categorias: Categoria[];
  categoria: Categoria;
  categoriasfiltradas: Categoria[];
  title = 'Categoria';
  umFiltroLista = '';
  registerForm: FormGroup;
  modoSalvar = 'post';

  constructor(
    private categoriaService: CategoriaService, private modalService: BsModalService,
    private fb: FormBuilder, private toastr: ToastrService) { }

  ngOnInit() {
    this.validation();
    this.getCategorias();
  }

  get filtroLista(): string{
    return this.umFiltroLista;
  }
  set filtroLista(value: string){
    this.umFiltroLista = value;
    this.categoriasfiltradas = this.filtroLista ? this.filtrarCategorias(this.filtroLista) : this.categorias;
  }

  openModal(template: any ){
    this.registerForm.reset();
    template.show();
  }

  filtrarCategorias(filtrarPor: string): Categoria[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.categorias.filter(
      categoria => categoria.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      );
    }

  validation(){
    this.registerForm = this.fb.group ({
      ativo: [''],
      descricao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]]
    });
  }

  salvarAlteracao(template: any){
    if (this.registerForm.valid){
      if (this.modoSalvar === 'post'){
        this.categoria = Object.assign({}, this.registerForm.value);
        this.categoria.ativo = false;
        this.categoriaService.cadastrarCategoria(this.categoria).subscribe(
        (novaCategoria: Categoria) => {
          template.hide();
          this.getCategorias();
          this.toastr.success(`Categoria: ${novaCategoria.descricao} criada com sucesso.`, ``);
        }, error => {
          this.toastr.error(`Erro ao criar a categoria: ${this.categoria.descricao}. ${error}`, ``);
        }
        );
      }else{
        this.categoria = Object.assign({id: this.categoria.id}, this.registerForm.value);
        this.categoriaService.alterarCategoria(this.categoria).subscribe(
          (novaCategoria: Categoria) => {
            template.hide();
            this.getCategorias();
            this.toastr.success(`Categoria: ${novaCategoria.descricao} editada com sucesso.`, ``);

          }, error => {
            this.toastr.error(`Erro ao editar a categoria: ${this.categoria.descricao}. ${error}`, ``);
          }
        );
      }
    }
  }

  getCategorias(){
    this.categoriaService.getCategorias().subscribe(
      (umCategorias: Categoria[]) => {
        this.categorias = umCategorias;
        this.categoriasfiltradas = umCategorias;
      }, error => {
        this.toastr.error(`Erro ao buscar categorias: ${error}.`);
      });
  }
  editarCategoria(categoria: Categoria, template: any){
    this.modoSalvar = 'put';
    this.openModal(template);
    this.categoria = categoria;
    this.registerForm.patchValue(categoria);
  }

  novaCategoria(template: any){
    this.modoSalvar = 'post';
    this.openModal(template);
  }
  ativarCategoria(categoria: Categoria, template: any){
    this.modoSalvar = 'put';
    categoria.ativo = true;
    this.categoria = categoria;
    this.registerForm.patchValue(categoria);
    this.editarStatusCategoria(template);
}
  desativarCategoria(categoria: Categoria, template: any){
    this.modoSalvar = 'put';
    this.categoria = categoria;
    categoria.ativo = false;
    this.registerForm.patchValue(categoria);
    this.editarStatusCategoria(template);
  }
  editarStatusCategoria(template: any){
    this.categoria = Object.assign({id: this.categoria.id}, this.registerForm.value);
    this.categoriaService.alterarCategoria(this.categoria).subscribe(
      (novaCategoria: Categoria) => {
        template.hide();
        this.getCategorias();
        if (novaCategoria.ativo) {
          this.toastr.success(`Categoria: ${novaCategoria.descricao} Ativado.`);
        }else{
          this.toastr.error(`Categoria: ${novaCategoria.descricao} Desativada.`);
      }
    }, error => {
      this.toastr.error(`Erro ao editar a categoria: ${this.categoria.descricao}.  ${error}`);
    });
  }
}
