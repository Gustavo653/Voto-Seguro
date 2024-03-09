import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { StorageService } from './storage.service';

@Injectable({
    providedIn: 'root',
})
export class ItemService {
    constructor(private http: HttpClient, private storageService: StorageService) { }

    private getAPIURL(): Observable<string> {
        return this.storageService.getAPIURL();
    }

    getItems(): Observable<any> {
        return this.getAPIURL().pipe(
            switchMap((url) => {
                const apiUrl = `${url}/item`;
                return this.http.get(apiUrl);
            })
        );
    }

    createItem(data: any): Observable<any> {
        return this.getAPIURL().pipe(
            switchMap((url) => {
                const apiUrl = `${url}/item`;
                return this.http.post(apiUrl, data);
            })
        );
    }

    updateItem(id: string, data: any): Observable<any> {
        return this.getAPIURL().pipe(
            switchMap((url) => {
                const apiUrl = `${url}/item/${id}`;
                return this.http.put(apiUrl, data);
            })
        );
    }

    deleteItem(id: string): Observable<any> {
        return this.getAPIURL().pipe(
            switchMap((url) => {
                const apiUrl = `${url}/item/${id}`;
                return this.http.delete(apiUrl);
            })
        );
    }
}
