import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListPokemons } from './list-pokemons';

describe('ListPokemons', () => {
  let component: ListPokemons;
  let fixture: ComponentFixture<ListPokemons>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListPokemons]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListPokemons);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
