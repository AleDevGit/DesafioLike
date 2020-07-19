/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { OpinioesComponent } from './opinioes.component';

describe('OpinioesComponent', () => {
  let component: OpinioesComponent;
  let fixture: ComponentFixture<OpinioesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpinioesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpinioesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
