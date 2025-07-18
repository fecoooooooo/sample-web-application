import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../../../api/user';

@Component({
  selector: 'app-edit-user',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-user.html',
  styleUrl: './edit-user.scss',
})
export class EditUser {
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      nameEN: ['', Validators.required],
      nameJP: ['', Validators.required],
      age: [null, [Validators.required, Validators.min(0)]],
    });
  }

  submit(): void {
    if (this.form.invalid) return;

    const newUser = this.form.value;
    this.userService.apiUserPost(newUser).subscribe(() => {
      this.router.navigate(['/users']);
    });
  }
}
