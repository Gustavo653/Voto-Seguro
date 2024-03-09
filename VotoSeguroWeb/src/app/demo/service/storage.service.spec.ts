import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { StorageService } from './storage.service';
import { HttpClient } from '@angular/common/http';
import { RuntimeConfig } from '../api/base';
import { of } from 'rxjs';

describe('StorageService', () => {
    let service: StorageService;
    let httpClient: HttpClient;
    let httpTestingController: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [StorageService],
        });
        service = TestBed.inject(StorageService);
        httpClient = TestBed.inject(HttpClient);
        httpTestingController = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpTestingController.verify();
        localStorage.clear();
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should retrieve API URL from runtime config', () => {
        const apiUrl = 'https://api.example.com';
        const runtimeConfig: RuntimeConfig = {
            PARAM_API_URL: apiUrl,
        };

        const getAPIUrlSpy = spyOn(httpClient, 'get').and.returnValue(of(runtimeConfig));

        let result: string | undefined;
        service.getAPIURL().subscribe((url: string) => {
            result = url;
        });

        expect(getAPIUrlSpy).toHaveBeenCalled();
        expect(result).toBe(apiUrl);
    });

    it('should save data to local storage', () => {
        const key = 'testKey';
        const data = 'testData';

        service.saveData(key, data);

        expect(localStorage.getItem(key)).toBe(data);
    });

    it('should retrieve data from local storage', () => {
        const key = 'testKey';
        const data = 'testData';
        localStorage.setItem(key, data);

        const result = service.getData(key);

        expect(result).toBe(data);
    });

    it('should remove data from local storage', () => {
        const key = 'testKey';
        const data = 'testData';
        localStorage.setItem(key, data);

        service.removeData(key);

        expect(localStorage.getItem(key)).toBeNull();
    });

    it('should clear all data from local storage', () => {
        localStorage.setItem('key1', 'data1');
        localStorage.setItem('key2', 'data2');

        service.clearData();

        expect(localStorage.length).toBe(0);
    });
});
