import { Component, Inject } from '@angular/core';
import { Pokemon, PokemonService } from '../../../../api/pokemon';
import { subscribe } from 'diagnostics_channel';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { User } from '../../../../api/user';
import { MatButtonModule } from '@angular/material/button';

export interface PokemonSelectDialogData {
  user: User;
}

@Component({
  selector: 'app-pokemon-select-modal',
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './pokemon-select-modal.html',
  styleUrl: './pokemon-select-modal.scss',
})
export class PokemonSelectModal {
  allPokemon: Pokemon[] = [];

  constructor(
    private pokemonService: PokemonService,
    public dialogRef: MatDialogRef<PokemonSelectModal>,
    @Inject(MAT_DIALOG_DATA) public data: PokemonSelectDialogData
  ) {}

  ngOnInit() {
    this.pokemonService.apiPokemonGet().subscribe((result) => {
      this.allPokemon = result;
    });
  }

  hasPokemon(pokemonId: number | undefined) {
    if (pokemonId === undefined) {
      return false;
    } else {
      console.log(
        pokemonId +
          ' ' +
          this.data.user?.pokemons?.some((p) => p.id == pokemonId)
      );
      return this.data.user?.pokemons?.some((p) => p.id == pokemonId) ?? false;
    }
  }

  onNoClick(): void {
    this.dialogRef.close(false);
  }

  onYesClick(): void {
    this.dialogRef.close(true);
  }
}
