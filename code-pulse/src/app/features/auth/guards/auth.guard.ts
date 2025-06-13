import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';
import { jwtDecode } from 'jwt-decode';
export const authGuard: CanActivateFn = (route, state) => {
  const cookieService = inject(CookieService);
  const authService = inject(AuthService);
  const router = inject(Router);

  const user = authService.getUser();
  //check if user is logged in or not.....c
  let token = cookieService.get('Authorization');

  if (token && user) {
    //check if token is not expired,,,,,decode the token....
    token = token.replace('Bearer', '');
    const decodedToken: any = jwtDecode(token);

    const expirationDate = decodedToken.exp * 1000;

    const currentTime = new Date().getTime();

    if (expirationDate < currentTime) {
      authService.logout();
      return router.createUrlTree(['/login'], {
        queryParams: {
          returnUrl: state.url,
        },
      });
    } else {
      //check roles....
      if (user.roles.includes('Writer')) {
        return true;
      } else {
        alert('UnAuthorised');
        return false;
      }
    }
  } else {
    //logout.
    authService.logout();
    return router.createUrlTree(['/login'], {
      queryParams: {
        returnUrl: state.url,
      },
    });
  }
};
