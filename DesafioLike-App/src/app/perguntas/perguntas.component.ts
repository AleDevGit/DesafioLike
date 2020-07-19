import { Component, OnInit } from '@angular/core';
import { Pergunta } from '../_models/Pergunta';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap/modal';
import { PerguntaService } from '../_services/pergunta.service';
import { ToastrService } from 'ngx-toastr';
import { CategoriaService } from '../_services/categoria.service';
import { Categoria } from '../_models/Categoria';

@Component({
  selector: 'app-perguntas',
  templateUrl: './perguntas.component.html',
  styleUrls: ['./perguntas.component.css']
})
export class PerguntasComponent implements OnInit {
  titulo = 'Perguntas';
  perguntas: Pergunta[];
  categorias: Categoria[];
  pergunta: Pergunta;
  perguntasfiltradas: Pergunta[];
  umFiltroLista = '';
  registerForm: FormGroup;
  modoSalvar = 'post';
  constructor( private perguntaService: PerguntaService,
               private categoriaService: CategoriaService, private modalService: BsModalService,
               private fb: FormBuilder, private toastr: ToastrService) { }

    ngOnInit() {
      this.validation();
      this.getPerguntas();
    }
    get filtroLista(): string{
      return this.umFiltroLista;
    }
    set filtroLista(value: string){
      this.umFiltroLista = value;
      this.perguntasfiltradas = this.filtroLista ? this.filtrarPerguntas(this.filtroLista) : this.perguntas;
    }
    openModal(template: any ){
      this.registerForm.reset();
      template.show();
    }

    filtrarPerguntas(filtrarPor: string): Pergunta[]{
      filtrarPor = filtrarPor.toLocaleLowerCase();
      return this.perguntas.filter(
        pergunta => pergunta.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      );
    }

    validation(){
      this.registerForm = this.fb.group ({
        ativo: [''],
        descricao: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
        categoria: ['', [Validators.required]],
        opcaoRespostaB: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
        opcaoRespostaA: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]]
      });
    }

    salvarAlteracao(template: any){
      if (this.registerForm.valid){
        if (this.modoSalvar === 'post'){
          this.pergunta = Object.assign({}, this.registerForm.value);
          this.pergunta.categoriaId =   this.registerForm.get('categoria').value;
          this.pergunta.opcao1 = this.registerForm.get('opcaoRespostaA').value;
          this.pergunta.opcao2 = this.registerForm.get('opcaoRespostaB').value;
          this.pergunta.ativo = false;
          this.perguntaService.cadastrarPergunta(this.pergunta).subscribe(
          (novaPergunta: Pergunta) => {
            template.hide();
            this.getPerguntas();
            this.toastr.success(`Pergunta: ${novaPergunta.descricao} criada com sucesso.`, ``);
          }, error => {
            this.toastr.error(`Erro ao criar a Pergunta: ${this.pergunta.descricao}. ${error}`, ``);
          }
          );
        }else{
          this.pergunta = Object.assign({id: this.pergunta.id}, this.registerForm.value);
          this.perguntaService.alterarPergunta(this.pergunta).subscribe(
            (novaPergunta: Pergunta) => {
              template.hide();
              this.getPerguntas();
              this.toastr.success(`Pergunta: ${novaPergunta.descricao} editada com sucesso.`, ``);
            }, error => {
              this.toastr.error(`Erro ao editar a pergunta: ${this.pergunta.descricao}. ${error}`, ``);
            }
          );
        }
      }
    }

    getPerguntas(){
      this.perguntaService.getPerguntas().subscribe(
        (umPerguntas: Pergunta[]) => {
          this.perguntas = umPerguntas;
          this.perguntasfiltradas = umPerguntas;
        }, error => {
          this.toastr.error(`Erro ao buscar perguntas: ${error}.`);
        });
    }

    editarPergunta(pergunta: Pergunta, template: any){
      this.modoSalvar = 'put';
      this.openModal(template);
      this.pergunta = pergunta;
      this.registerForm.patchValue(pergunta);
    }

    novaPergunta(template: any){
      this.modoSalvar = 'post';
      this.openModal(template);
      this.getCategorias();
    }

    getCategorias(){
      this.categoriaService.getCategorias().subscribe(
        (umCategorias: Categoria[]) => {
          this.categorias = umCategorias;
        }, error => {
          this.toastr.error(`Erro ao buscar categorias: ${error}.`);
        });
    }

    ativarPergunta(pergunta: Pergunta, template: any){
      this.modoSalvar = 'put';
      pergunta.ativo = true;
      this.pergunta = pergunta;
      this.registerForm.patchValue(pergunta);
      this.editarStatusPergunta(template);
  }
    desativarPergunta(pergunta: Pergunta, template: any){
      this.modoSalvar = 'put';
      this.pergunta = pergunta;
      pergunta.ativo = false;
      this.registerForm.patchValue(pergunta);
      this.editarStatusPergunta(template);
    }
    editarStatusPergunta(template: any){
      this.pergunta = Object.assign({id: this.pergunta.id}, this.registerForm.value);
      this.perguntaService.alterarPergunta(this.pergunta).subscribe(
        (novaPergunta: Pergunta) => {
          template.hide();
          this.getPerguntas();
          if (novaPergunta.ativo) {
            this.toastr.success(`Pergunta: ${novaPergunta.descricao} Ativado.`);
          }else{
            this.toastr.error(`Pergunta: ${novaPergunta.descricao} Desativada.`);
        }
      }, error => {
        this.toastr.error(`Erro ao editar a pergunta: ${this.pergunta.descricao}.  ${error}`);
      });
    }

}
