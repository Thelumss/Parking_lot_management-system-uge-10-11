import { Component, effect, OnDestroy, signal } from '@angular/core';
import { Toolbarcomponent } from "./toolbarcomponent/toolbarcomponent";
import { UserLogincomponent } from "./Tools/user-logincomponent/user-logincomponent";
import { CommonModule, NgIf } from '@angular/common';
import { AuthService } from './Services/auth-service';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [Toolbarcomponent, UserLogincomponent, NgIf, RouterOutlet,FormsModule,CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnDestroy {
  protected readonly title = signal('Parking Sytem');

  isLoggedIn = signal(false);

constructor(private authService: AuthService) {
    this.authService.loggedIn.subscribe(status => this.isLoggedIn.set(status));
  }

  ngOnDestroy(): void {
    //this.subscription.unsubscribe();
  }
}
