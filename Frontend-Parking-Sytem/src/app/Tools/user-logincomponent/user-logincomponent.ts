import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';
import { AuthService } from '../../../Services/auth-service';

@Component({
  selector: 'app-user-logincomponent',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgIf,MatTableModule, MatPaginatorModule,MatSortModule],
  templateUrl: './user-logincomponent.html',
  styleUrl: './user-logincomponent.css',
})
export class UserLogincomponent {
  
  loginForm!: FormGroup;
  
  ngOnInit(): void {
    this.createForm();
  }

  private createForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required]),
      password: new FormControl('', Validators.required),
    });
  }
  
  constructor(
    private authService: AuthService,
    private router: Router
  ) {
  }

  onSubmit() {
      this.authService.login(this.loginForm.value).subscribe(()=>{
        this.router.navigate([`/parkingstructur`]);
        this.loginForm.reset();
      });
  }
}
