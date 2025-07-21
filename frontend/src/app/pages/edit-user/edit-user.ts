import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User, UserService } from '../../../../api/user';

@Component({
  selector: 'app-edit-user',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-user.html',
  styleUrl: './edit-user.scss',
})
export class EditUser {
  form!: FormGroup;
  idOrAction: string | null = null;
  isCreate: boolean = false;
  userId: number | undefined;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      nameEN: ['', Validators.required],
      nameJP: ['', Validators.required],
      age: [null, [Validators.required, Validators.min(0)]],
    });

    this.route.paramMap.subscribe((params) => {
      this.idOrAction = params.get('id'); // lehet '42' vagy 'create'
      console.log(this.idOrAction);

      if (this.idOrAction === 'create') {
        this.isCreate = true;
      } else {
        this.isCreate = false;

        const userId = Number(this.idOrAction);
        if (!isNaN(userId)) {
          this.userService.apiUserIdGet(userId).subscribe((user) => {
            this.userId = user.id;

            this.form.patchValue({
              nameEN: user.nameEN,
              nameJP: user.nameJP,
              age: user.age,
            });
          });
        }
      }
    });
  }

  submit(): void {
    if (this.form.invalid) return;

    const userToSend = this.form.value;

    if (this.isCreate) {
      this.userService.apiUserPost(userToSend).subscribe(() => {
        this.router.navigate(['/users']);
      });
    } else {
      this.userService.apiUserIdPut(this.userId!, userToSend).subscribe(() => {
        this.router.navigate(['/users']);
      });
    }
  }
}
