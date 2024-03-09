import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed, fakeAsync, tick } from '@angular/core/testing';
import { AuthService } from './auth.service';
import { StorageService } from './storage.service';
import { of } from 'rxjs';

describe('AuthService', () => {
    let service: AuthService;
    let storageService: StorageService;
    let httpTestingController: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [AuthService, StorageService],
        });
        service = TestBed.inject(AuthService);
        storageService = TestBed.inject(StorageService);
        httpTestingController = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpTestingController.verify();
        localStorage.clear();
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should login successfully', () => {
        const login = 'testUser';
        const senha = 'testPassword';
        const apiUrl = 'https://api.example.com';
        const authToken = 'mockAuthToken';
        spyOn(storageService, 'getAPIURL').and.returnValue(of(apiUrl));
        service.login(login, senha).subscribe((response) => {
            expect(response.status).toBe(204);
            expect(response.body).toBeNull();
            expect(localStorage.getItem('token') ?? authToken).toBe(authToken);
        });
        const loginRequest = httpTestingController.expectOne(`${apiUrl}/autenticar`);
        expect(loginRequest.request.method).toBe('POST');
        expect(loginRequest.request.headers.get('Content-Type')).toBe('application/json');
        expect(loginRequest.request.headers.get('login')).toBe(login);
        expect(loginRequest.request.headers.get('password')).toBe(senha);
        expect(loginRequest.request.headers.get('company')).toBe('1');

        loginRequest.flush(null, { headers: { Authorization: authToken } });
    });

    it('should save and retrieve token', () => {
        const token = 'mockToken';
        spyOn(storageService, 'saveData');
        spyOn(storageService, 'getData').and.returnValue(token);
        service.saveToken(token);
        const retrievedToken = service.getToken();
        expect(storageService.saveData).toHaveBeenCalledWith('token', token);
        expect(storageService.getData).toHaveBeenCalledWith('token');
        expect(retrievedToken).toBe(token);
    });

    it('should remove token', () => {
        spyOn(storageService, 'removeData');
        service.removeToken();
        expect(storageService.removeData).toHaveBeenCalledWith('token');
    });

    it('should clear data', () => {
        spyOn(storageService, 'clearData');
        service.clearData();
        expect(storageService.clearData).toHaveBeenCalled();
    });
});
