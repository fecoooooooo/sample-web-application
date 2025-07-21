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
import { FormsModule } from '@angular/forms';

export interface PokemonSelectDialogData {
  user: User;
}

@Component({
  selector: 'app-pokemon-select-modal',
  imports: [MatDialogModule, MatButtonModule, FormsModule],
  templateUrl: './pokemon-select-modal.html',
  styleUrl: './pokemon-select-modal.scss',
})
export class PokemonSelectModal {
  allPokemon: Pokemon[] = [];
  selectedPokemonIds: number[] = [];

  constructor(
    private pokemonService: PokemonService,
    public dialogRef: MatDialogRef<PokemonSelectModal>,
    @Inject(MAT_DIALOG_DATA) public data: PokemonSelectDialogData
  ) {}

  ngOnInit() {
    this.pokemonService.apiPokemonGet().subscribe((result) => {
      this.allPokemon = result;

      this.selectedPokemonIds =
        this.data.user?.pokemons?.map((p) => p.id!) ?? [];

      console.log(this.selectedPokemonIds);
    });
  }

  toggleSelection(pokemonId: number) {
    const index = this.selectedPokemonIds.indexOf(pokemonId);
    if (index === -1) {
      this.selectedPokemonIds.push(pokemonId);
    } else {
      this.selectedPokemonIds.splice(index, 1);
    }
  }

  onNoClick(): void {
    this.dialogRef.close(false);
  }

  onYesClick(): void {
    console.log(this.selectedPokemonIds);
    const selectedPokemons = this.allPokemon.filter((p) =>
      this.selectedPokemonIds.includes(p.id!)
    );
    this.dialogRef.close(selectedPokemons);
  }
}
