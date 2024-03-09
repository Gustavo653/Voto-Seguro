import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HomeComponent } from './home.component';
import { AuthService } from '../../service/auth.service';
import { Router } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { StorageService } from '../../service/storage.service';

describe('HomeComponent', () => {
    let component: HomeComponent;
    let fixture: ComponentFixture<HomeComponent>;
    let authService: AuthService;
    let router: Router;

    beforeEach(waitForAsync(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, HttpClientTestingModule],
            declarations: [HomeComponent],
            providers: [AuthService, StorageService],
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(HomeComponent);
        component = fixture.componentInstance;
        authService = TestBed.inject(AuthService);
        router = TestBed.inject(Router);
    });

    it('should create the HomeComponent', () => {
        expect(component).toBeTruthy();
    });
});
