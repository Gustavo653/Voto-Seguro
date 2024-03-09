import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { RuntimeConfig } from '../api/base';

@Injectable()
export class StorageService {
    constructor(private http: HttpClient) {}

    public getAPIURL(): Observable<string> {
        return this.http.get('assets/json/runtime.json').pipe(
            map((x: Object) => {
                const config = x as RuntimeConfig;
                return config.PARAM_API_URL;
            })
        );
    }

    public saveData(key: string, data: string) {
        localStorage.setItem(key, data);
    }

    public getData(key: string) {
        return localStorage.getItem(key);
    }

    public removeData(key:string) {
        localStorage.removeItem(key);
    }

    public clearData() {
        localStorage.clear();
    }
}
