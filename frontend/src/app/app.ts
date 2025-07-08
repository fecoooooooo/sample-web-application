import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PokemonService } from '../../api/user';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  protected title = 'frontend';

  constructor(private pokemonService: PokemonService) {}

  ngOnInit(): void {
    this.pokemonService.apiPokemonGetPokemonsGet().subscribe((result) => {
      console.log(result);
    });
  }
}
