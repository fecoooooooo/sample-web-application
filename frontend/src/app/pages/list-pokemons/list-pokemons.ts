import { Component } from '@angular/core';
import { Pokemon, PokemonService } from '../../../../api/pokemon';
import { RouterModule } from '@angular/router';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ConfirmModal } from '../../components/confirm-modal/confirm-modal';

@Component({
  selector: 'app-list-pokemons',
  imports: [RouterModule, MatDialogModule],
  templateUrl: './list-pokemons.html',
  styleUrl: './list-pokemons.scss',
})
export class ListPokemons {
  allPokemon: Pokemon[] = [];
  selectedPokemonId: number = -1;

  constructor(
    private pokemonService: PokemonService,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.loadData();
  }

  confirmDelete(id: number | undefined): void {
    if (id === undefined) return;

    this.selectedPokemonId = id;
    const dialogRef = this.dialog.open(ConfirmModal, {
      width: '250px',
      data: {
        title: 'Confirm delete pokemon',
        message:
          'Are you sure you want to delete pokemon with id: ' +
          this.selectedPokemonId,
      },
    });

    dialogRef.afterClosed().subscribe((result: boolean) => {
      if (result === true) {
        this.pokemonService
          .apiPokemonIdDelete(this.selectedPokemonId)
          .subscribe(() => {
            this.loadData();
          });
      } else {
        console.log('Cancelled');
      }
    });
  }

  loadData() {
    this.pokemonService.apiPokemonGet().subscribe((result) => {
      this.allPokemon = result;
    });
  }
}
