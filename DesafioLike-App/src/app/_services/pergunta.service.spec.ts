/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PerguntaService } from './pergunta.service';

describe('Service: Pergunta', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PerguntaService]
    });
  });

  it('should ...', inject([PerguntaService], (service: PerguntaService) => {
    expect(service).toBeTruthy();
  }));
});
