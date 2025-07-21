import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Pokemon, User, UserDto, UserService } from '../../../../api/user';
import { MatDialog } from '@angular/material/dialog';
import { PokemonSelectModal } from '../../components/pokemon-select-modal/pokemon-select-modal';

@Component({
  selector: 'app-edit-user',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-user.html',
  styleUrl: './edit-user.scss',
})
export class EditUser {
  form!: FormGroup;
  isCreate: boolean = false;
  user: User | undefined;
  userId: number = -1;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      nameEN: ['', Validators.required],
      nameJP: ['', Validators.required],
      age: [null, [Validators.required, Validators.min(0)]],
    });

    this.route.paramMap.subscribe((params) => {
      const idOrAction = params.get('id');

      if (idOrAction === 'create') {
        this.isCreate = true;
      } else {
        this.isCreate = false;

        this.userId = Number(idOrAction);
        if (!isNaN(this.userId)) {
          this.userService.apiUserIdGet(this.userId).subscribe((user) => {
            this.user = user;

            this.form.patchValue({
              nameEN: user.nameEN,
              nameJP: user.nameJP,
              age: user.age,
            });

            console.log(user);
          });
        }
      }
    });
  }

  editPokemons() {
    const dialogRef = this.dialog.open(PokemonSelectModal, {
      width: '300px',
      data: {
        user: this.user,
      },
    });

    dialogRef.afterClosed().subscribe((result: Pokemon[] | null) => {
      if (result === null) {
        console.log('Cancelled');
      } else {
        this.user!.pokemons = result;
      }
    });
  }

  getPokemonNames() {
    if (this.user?.pokemons === null || this.user?.pokemons === undefined)
      return '';
    else return this.user.pokemons.map((p) => p.name).join(', ');
  }

  submit(): void {
    if (this.form.invalid) return;

    let userDto: UserDto = {};
    userDto.nameEN = this.form.value.nameEN;
    userDto.nameJP = this.form.value.nameJP;
    userDto.age = this.form.value.age;
    userDto.pokemonIds = this.user!.pokemons!.map((p) => p.id).filter(
      (id): id is number => id !== undefined
    );

    if (this.isCreate) {
      this.userService.apiUserPost(userDto).subscribe(() => {
        this.router.navigate(['/users']);
      });
    } else {
      this.userService.apiUserIdPut(this.userId, userDto).subscribe(() => {
        this.router.navigate(['/users']);
      });
    }
  }
}
