import { Component, OnInit } from '@angular/core';

import { SocialAuthService, GoogleLoginProvider, SocialUser } from '@abacritt/angularx-social-login';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {
  title = 'google-auth-ang';

  socialUser!: SocialUser;
  isLoggedIn?: boolean;
  files: File[] = [];

  constructor(private socialAuthService: SocialAuthService) {
  }

  ngOnInit() {
    this.socialAuthService.authState.subscribe((user) => {
      this.socialUser = user;
      this.isLoggedIn = user != null;
      //Remove this log record
      console.log(this.socialUser);
    });
  }

  loginWithGoogle(): void {
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  logOut(): void {
    this.socialAuthService.signOut();
  }

  onFileSelected(event: any) {
    if (event.target.files.length > 0) {
      for(var i = 0; i < event.target.files.length; i++) {
        console.log(event.target.files[i].name);
        this.files.push(event.target.files[i]);
      }
    }
  }
}
