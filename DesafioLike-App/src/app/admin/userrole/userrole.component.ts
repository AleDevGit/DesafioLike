import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/_services/admin.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { Role } from 'src/app/_models/Role';
import { User } from 'src/app/_models/User';
import { UserRole } from 'src/app/_models/UserRole';

import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';

@Component({
  selector: 'app-userrole',
  templateUrl: './userrole.component.html',
  styleUrls: ['./userrole.component.css']
})
export class UserroleComponent implements OnInit {
  titulo = 'Permissão de usuários';
  registerForm: FormGroup;
  regra: Role = new Role();
  regras: Role[];
  nomeRegra = '';
  usuariosfiltrado: User;
  usuariosRegras: User[];
  usuarios: User[];
  listaUsuarios: User[];
  user: User;
  nomeUsuario: '';
  bodyDeletarUsuarioRegra = '';
  selectedValue: string;
  selectedOption: any;

  constructor(public fb: FormBuilder,
              private adminService: AdminService
            , private toastr: ToastrService
            , private router: ActivatedRoute
  ) { }

  ngOnInit() {
    this.validation();
    this.carregarRegra();
    this.carregarUsuarios();
    this.carregarPermissionado();
  }

  validation(){
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required]
    });
  }

  carregarRegra(){
    const idRegra = +this.router.snapshot.paramMap.get('id');
    this.adminService.buscarRegra(idRegra).subscribe(
      (regra: Role) => {
        this.regra = Object.assign({}, regra);
      });
  }

  carregarUsuarios(){
    const idRegra = +this.router.snapshot.paramMap.get('id');
    this.adminService.buscarUsuarios(idRegra).subscribe(
      (lisusuario: User[]) => {
        this.listaUsuarios = lisusuario;
      });
  }
  carregarPermissionado(){

    const idRegra = +this.router.snapshot.paramMap.get('id');
    this.adminService.buscarUsuarioRegra(idRegra).subscribe(
      (listaUsuarios: User[]) => {
        this.usuariosRegras = listaUsuarios;
      }, error => {
        const erro = error.Status;
      }
      );
  }

  openModal(template: any ){
    this.registerForm.reset();
    template.show();
  }

  excluirPermissao(user: User, role: Role, template: any){
    this.openModal(template);
    this.regra = role;
    this.user = user;
    this.bodyDeletarUsuarioRegra =
    `Tem certeza que deseja retirar a permissão de: ${this.regra.name}, para o Usuário: ${this.user.fullName}`;
  }

  confirmeExclusao(template: any) {
    this.adminService.excluirPermissao(this.regra.id, this.user.id).subscribe(
      () => {
        template.hide();
        this.carregarPermissionado();
        this.registerForm.reset();
        this.carregarUsuarios();
        this.toastr.success('Retirado com Sucesso');
      }, error => {
        const erro = error.Status;
      }
    );
  }

  onSelect(event: TypeaheadMatch): void {
    this.usuariosfiltrado = event.item;
  }

  cadastrarUsuario(){
    this.user = Object.assign({}, this.usuariosfiltrado);
    this.adminService.adicionarPermissao(this.user, this.regra.name).subscribe(
      () => {
        this.carregarPermissionado();
        this.registerForm.reset();
        this.carregarUsuarios();
        this.toastr.success('Adicionado com Sucesso');
      }, error => {
        const erro = error.Status;
      }
    );
  }
}
