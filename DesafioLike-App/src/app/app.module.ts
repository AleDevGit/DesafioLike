import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TooltipModule} from 'ngx-bootstrap/tooltip';
import { BsDropdownModule} from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { ModalModule} from 'ngx-bootstrap/modal/';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { CategoriaService } from './_services/categoria.service';

import { AppComponent } from './app.component';
import { CategoriasComponent } from './categorias/categorias.component';
import { NavComponent } from './nav/nav.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { PerguntasComponent } from './perguntas/perguntas.component';
import { TituloComponent } from './_shared/titulo/titulo.component';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { AdminComponent } from './admin/admin.component';
import { UserroleComponent } from './admin/userrole/userrole.component';
import { RegisterRoleComponent } from './admin/registerRole/registerRole.component';
import { LoginComponent } from './user/login/login.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { FooterComponent } from './footer/footer.component';
import { OpinioesComponent } from './opinioes/opinioes.component';
import { PerguntaService } from './_services/pergunta.service';
import { RespostaService } from './_services/resposta.service';



@NgModule({
   declarations: [
      AppComponent,
      CategoriasComponent,
      NavComponent,
      DashboardComponent,
      UsuariosComponent,
      PerguntasComponent,
      TituloComponent,
      UserComponent,
      RegistrationComponent,
      AdminComponent,
      RegisterRoleComponent,
      LoginComponent,
      UserroleComponent,
      FooterComponent,
      OpinioesComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      ToastrModule.forRoot(),
      BrowserAnimationsModule,
      ToastrModule.forRoot(),
      AppRoutingModule,
      TypeaheadModule.forRoot(),
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BsDatepickerModule.forRoot()
   ],
   providers: [
      PerguntaService,
      CategoriaService,
      RespostaService,
      {
         provide: HTTP_INTERCEPTORS,
         useClass: AuthInterceptor,
         multi: true
      }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
