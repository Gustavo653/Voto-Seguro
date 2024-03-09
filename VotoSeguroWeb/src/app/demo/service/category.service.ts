import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { StorageService } from './storage.service';

@Injectable({
    providedIn: 'root',
})
export class CategoryService {
    constructor(private http: HttpClient, private storageService: StorageService) { }

    private getAPIURL(): Observable<string> {
        return this.storageService.getAPIURL();
    }

    getCategories(): Observable<any> {
        return this.getAPIURL().pipe(
            switchMap((url) => {
                const apiUrl = `${url}/category`;
                return this.http.get(apiUrl);
            })
        );
    }

    createCategory(data: any): Observable<any> {
        return this.getAPIURL().pipe(
            switchMap((url) => {
                const apiUrl = `${url}/category`;
                return this.http.post(apiUrl, data);
            })
        );
    }

    updateCategory(id: string, data: any): Observable<any> {
        return this.getAPIURL().pipe(
            switchMap((url) => {
                const apiUrl = `${url}/category/${id}`;
                return this.http.put(apiUrl, data);
            })
        );
    }

    deleteCategory(id: string): Observable<any> {
        return this.getAPIURL().pipe(
            switchMap((url) => {
                const apiUrl = `${url}/category/${id}`;
                return this.http.delete(apiUrl);
            })
        );
    }
}
