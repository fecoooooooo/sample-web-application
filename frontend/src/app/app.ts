import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PokemonService } from '../../api/pokemon';
import { UserService } from '../../api/user';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  protected title = 'frontend';

  constructor(
    private pokemonService: PokemonService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.pokemonService.apiPokemonGet().subscribe((result) => {
      console.log(result);
    });

    this.userService.apiUserGet().subscribe((result) => {
      console.log(result);
    });
  }
}
