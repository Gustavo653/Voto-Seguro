import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';
import { AuthService } from '../demo/service/auth.service';
import { Router } from '@angular/router';
import { MenuRoutes } from '../demo/api/base';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html',
    styles: [
        `
            .user-profile {
                display: flex;
                align-items: center;
                margin-top: 30px;
            }
        `,
    ],
})
export class AppMenuComponent implements OnInit {
    model: any[] = MenuRoutes.filter((x) => {
        const role = this.authService.getRole();
        const routeRoles = x.role;
        if (role === 'admin') {
            return true;
        }
        if (routeRoles && Array.isArray(routeRoles)) {
            return routeRoles.some((routeRole) => routeRole === role);
        }
        return false;
    });

    constructor(public layoutService: LayoutService, private authService: AuthService, private router: Router) {}

    public logout(): void {
        this.authService.clearData();
        this.router.navigate(['/login']);
    }

    public showName(): string {
        const user = this.authService.getFirstName();
        const username = typeof user === 'string' ? user.toUpperCase() : 'USER';
        return username;
    }

    ngOnInit() {}
}
