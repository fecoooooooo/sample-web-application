import { Routes } from '@angular/router';
import { ListPokemons } from './pages/list-pokemons/list-pokemons';
import { ListUsers } from './pages/list-users/list-users';
import { EditUser } from './pages/edit-user/edit-user';
import { EditPokemon } from './pages/edit-pokemon/edit-pokemon';

export const routes: Routes = [
  { path: 'pokemons', component: ListPokemons },
  { path: 'users', component: ListUsers },
  { path: 'users/:id', component: EditUser },
  { path: 'pokemons/:id', component: EditPokemon },
];
