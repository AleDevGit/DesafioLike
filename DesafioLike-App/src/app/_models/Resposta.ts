import { Usuario } from './Usuario';
import { Pergunta } from './Pergunta';

export interface Resposta {
    id: number;
    perguntaId: number;
    usuarioId: number;
    respostaOpcao: string;
}
