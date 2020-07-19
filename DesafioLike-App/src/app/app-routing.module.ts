import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CategoriasComponent } from './categorias/categorias.component';
import { OpinioesComponent } from './opinioes/opinioes.component';
import { PerguntasComponent } from './perguntas/perguntas.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { AuthGuard } from './auth/auth.guard';
import { RegisterRoleComponent } from './admin/registerRole/registerRole.component';
import { AdminComponent } from './admin/admin.component';
import { UserroleComponent } from './admin/userrole/userrole.component';


const routes: Routes = [
{path: 'user', component: UserComponent,
  children: [
    {path: 'login', component: LoginComponent},
    {path: 'registration', component: RegistrationComponent}
  ]
},
{path: 'admin', component: AdminComponent,
children: [
  {path: 'registerrole', component: RegisterRoleComponent, canActivate: [AuthGuard]}
]},
{ path: 'user/:id/role', component: UserroleComponent, canActivate: [AuthGuard] },
{path: 'categorias', component: CategoriasComponent, canActivate: [AuthGuard]},
{path: 'opinioes', component: OpinioesComponent, canActivate: [AuthGuard]},
{path: 'perguntas', component: PerguntasComponent, canActivate: [AuthGuard]},
{path: 'dashboard', component: DashboardComponent},
{path: '', redirectTo: 'dashboard', pathMatch: 'full'},
{path: '**', redirectTo: 'dashboard', pathMatch: 'full'}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
