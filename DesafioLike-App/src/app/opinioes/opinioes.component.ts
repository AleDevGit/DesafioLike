import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';
import { PerguntaService } from 'src/app/_services/pergunta.service';
import { Pergunta } from '../_models/Pergunta';
import { RespostaService } from '../_services/resposta.service';
import { Resposta } from '../_models/Resposta';


@Component({
  selector: 'app-opinioes',
  templateUrl: './opinioes.component.html',
  styleUrls: ['./opinioes.component.css']
})
export class OpinioesComponent implements OnInit {
  resposta: Resposta;
  pergunta: Pergunta;
  constructor(private authService: AuthService, public router: Router,
              private perguntaService: PerguntaService, private respostaService: RespostaService) { }

  ngOnInit() {
    this.buscarPergunta();

  }
  buscarPergunta(){
     this.perguntaService.getProximaPergunta().subscribe(
      (novaPergunta: Pergunta) => {
        this.pergunta = novaPergunta;
    }, error => {
      console.log(`Erro ao buscar Pergunta.  ${error}`);
    });
  }
  OpcaoEscolhida(pergunta: Pergunta, opcao: string){

    this.resposta = Object.assign({perguntaId: pergunta.id, respostaOpcao: opcao});
    this.respostaService.cadastrarResposta(this.resposta).subscribe(
      () => {
        this.buscarPergunta();
    }, error => {
      console.log(`Erro ao Buscar Pergunta.  ${error}`);
    });
  }

}
