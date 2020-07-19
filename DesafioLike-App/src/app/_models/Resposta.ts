import { Usuario } from './Usuario';
import { Pergunta } from './Pergunta';

export interface Resposta {
    id: number;
    perguntaId: number;
    UserId: number;
    respostaOpcao: string;
}
