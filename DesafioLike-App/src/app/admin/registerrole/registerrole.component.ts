import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { Role } from 'src/app/_models/Role';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-registerrole',
  templateUrl: './registerrole.component.html',
  styleUrls: ['./registerrole.component.css']
})
export class RegisterRoleComponent implements OnInit {
  titulo = 'Regras';
  registerForm: FormGroup;
  regras: Role[];
  regra: Role;
  umFiltroLista = '';
  regrasfiltradas: Role[];
  modoSalvar = 'post';

  constructor(public fb: FormBuilder, private toastr: ToastrService,
              private adminService: AdminService, public router: Router) { }

  ngOnInit() {
    this.validation();
    this.getRegras();
  }

  get filtroLista(): string{
    return this.umFiltroLista;
  }
  set filtroLista(value: string){
    this.umFiltroLista = value;
    this.regrasfiltradas = this.filtroLista ? this.filtrarRegras(this.filtroLista) : this.regras;
  }

  filtrarRegras(filtrarPor: string): Role[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.regras.filter(
      role => role.name.toLocaleLowerCase().indexOf(filtrarPor) !== -1
      );
    }

  validation(){
    this.registerForm = this.fb.group({
      name: ['', Validators.required]
    });
  }
  openModal(template: any ){
    this.registerForm.reset();
    template.show();
  }

  getRegras(){
    this.adminService.listarRegras().subscribe(
      (umRegras: Role[]) => {
        console.log(umRegras);
        this.regras = umRegras;
        this.regrasfiltradas = umRegras;
      }, error => {
        this.toastr.error(`Erro ao buscar regras: ${error}.`);
      });
  }

  novaRegra(template: any){
    this.modoSalvar = 'post';
    this.openModal(template);
  }

  cadastrarRegra(template: any){
    if (this.registerForm.valid){
      if (this.modoSalvar === 'post'){
        this.regra = Object.assign({}, this.registerForm.value);
        this.adminService.cadastrarRole(this.regra).subscribe(
          () => {
            template.hide();
            this.getRegras();
            this.toastr.success(`Regra: ${this.regra.name} criada com sucesso.`, ``);
          },
          error => {
            const erro = error.error;
            erro.forEach(element => {
              switch (element.code){
                case 'DuplicateUserName':
                  this.toastr.error('Cadastro duplicado');
                  break;
                default:
                  this.toastr.error(`Erro no cadastro! CODE: ${element.code}`);
                  break;
              }
            });
          }
        );
      } else {
        this.regra = Object.assign({id: this.regra.id}, this.registerForm.value);
        this.adminService.alterarRole(this.regra).subscribe(
          () => {
            template.hide();
            this.getRegras();
            this.toastr.success(`Regra: ${this.regra.name} editada com sucesso.`, ``);
          },
          error => {
            const erro = error.error;
            erro.forEach(element => {
              switch (element.code){
                case 'DuplicateUserName':
                  this.toastr.error('Cadastro duplicado');
                  break;
                default:
                  this.toastr.error(`Erro no cadastro! CODE: ${element.code}`);
                  break;
              }
            });
          }
        );

      }
    }
  }

  editarRegra(regra: Role, template: any){
    console.log(regra);
    this.modoSalvar = 'put';
    this.openModal(template);
    this.regra = regra;
    this.registerForm.patchValue(regra);
  }

}
