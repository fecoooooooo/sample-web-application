import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { PokemonService } from '../../../../api/pokemon';

@Component({
  selector: 'app-edit-pokemon',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-pokemon.html',
  styleUrl: './edit-pokemon.scss',
})
export class EditPokemon {
  form!: FormGroup;
  isCreate: boolean = false;
  pokemonId: number | undefined;

  constructor(
    private fb: FormBuilder,
    private pokemonService: PokemonService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      name: ['', Validators.required],
      type: ['', Validators.required],
    });

    this.route.paramMap.subscribe((params) => {
      const idOrAction = params.get('id'); // lehet '42' vagy 'create'

      if (idOrAction === 'create') {
        this.isCreate = true;
      } else {
        this.isCreate = false;

        const pokeonId = Number(idOrAction);
        if (!isNaN(pokeonId)) {
          this.pokemonService.apiPokemonIdGet(pokeonId).subscribe((pokemon) => {
            this.pokemonId = pokemon.id;

            this.form.patchValue({
              name: pokemon.name,
              type: pokemon.type,
            });

            console.log(pokemon);
          });
        }
      }
    });
  }

  submit(): void {
    if (this.form.invalid) return;

    const pokemonToSend = this.form.value;

    if (this.isCreate) {
      this.pokemonService.apiPokemonPost(pokemonToSend).subscribe(() => {
        this.router.navigate(['/pokemons']);
      });
    } else {
      this.pokemonService
        .apiPokemonIdPut(this.pokemonId!, pokemonToSend)
        .subscribe(() => {
          this.router.navigate(['/pokemons']);
        });
    }
  }
}
