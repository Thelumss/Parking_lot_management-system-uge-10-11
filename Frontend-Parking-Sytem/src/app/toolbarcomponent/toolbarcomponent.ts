import { Component, NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Router } from '@angular/router';
import { AuthService } from '../Services/auth-service';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-toolbarcomponent',
  standalone: true,
  imports: [
    MatSidenavModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatCardModule,
    NgIf,
    FormsModule
],
  templateUrl: './toolbarcomponent.html',
  styleUrl: './toolbarcomponent.css',
})
export class Toolbarcomponent {
  hideToolbar = false;

onButtonClick(buttonName: string) {
    this.router.navigate([buttonName.toLocaleLowerCase()]);
  }
    constructor(public router: Router,private authService: AuthService) {
  }
logout(){
  this.authService.logout();
}
}
