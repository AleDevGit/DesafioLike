/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RespostaService } from './resposta.service';

describe('Service: Resposta', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RespostaService]
    });
  });

  it('should ...', inject([RespostaService], (service: RespostaService) => {
    expect(service).toBeTruthy();
  }));
});
