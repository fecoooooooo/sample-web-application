import { Component } from '@angular/core';
import { Pokemon, PokemonService } from '../../../../api/pokemon';

@Component({
  selector: 'app-list-pokemons',
  imports: [],
  templateUrl: './list-pokemons.html',
  styleUrl: './list-pokemons.scss',
})
export class ListPokemons {
  allPokemon: Pokemon[] = [];

  constructor(private pokemonService: PokemonService) {}

  ngOnInit(): void {
    this.pokemonService.apiPokemonGet().subscribe((result) => {
      this.allPokemon = result;
      console.log(result);
    });
  }
}
