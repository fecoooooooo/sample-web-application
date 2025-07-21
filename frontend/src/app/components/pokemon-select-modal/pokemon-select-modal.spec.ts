import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PokemonSelectModal } from './pokemon-select-modal';

describe('PokemonSelectModal', () => {
  let component: PokemonSelectModal;
  let fixture: ComponentFixture<PokemonSelectModal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PokemonSelectModal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PokemonSelectModal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
