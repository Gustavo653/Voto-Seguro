import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppMenuComponent } from './app.menu.component';
import { LayoutService } from './service/app.layout.service';
import { AuthService } from '../demo/service/auth.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';

describe('AppMenuComponent', () => {
    let component: AppMenuComponent;
    let fixture: ComponentFixture<AppMenuComponent>;
    let layoutService: LayoutService;
    let authService: AuthService;
    let router: Router;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [AppMenuComponent],
            providers: [
                {
                    provide: LayoutService,
                    useValue: jasmine.createSpyObj('LayoutService', ['onOverlaySubmenuOpen']),
                },
                {
                    provide: AuthService,
                    useValue: jasmine.createSpyObj('AuthService', {
                        logout: of(undefined),
                        clearData: jasmine.createSpy(),
                        getUser: jasmine.createSpy(),
                    }),
                },
                {
                    provide: Router,
                    useValue: jasmine.createSpyObj('Router', ['navigate']),
                },
            ],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(AppMenuComponent);
        component = fixture.componentInstance;
        layoutService = TestBed.inject(LayoutService);
        authService = TestBed.inject(AuthService);
        router = TestBed.inject(Router);
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should call logout and clear data on logout', () => {
        component.logout();
        expect(authService.logout).toHaveBeenCalled();
        expect(authService.clearData).toHaveBeenCalled();
        expect(router.navigate).toHaveBeenCalledWith(['/login']);
    });

    it('should return username in uppercase', () => {
        const username = 'user';
        const result = component.showName();
        expect(result).toBe(username.toUpperCase());
    });
});
