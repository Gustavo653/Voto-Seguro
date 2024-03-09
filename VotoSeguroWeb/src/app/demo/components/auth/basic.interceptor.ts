import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../../service/auth.service';

@Injectable({
    providedIn: 'root',
})
export class BasicInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) {}

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        var token = this.authService.getToken();
        if (token != null) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}`,
                },
            });
        }
        return next.handle(request);
    }
}
