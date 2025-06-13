import {
  HttpClient,
  HttpClientJsonpModule,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { CategoryToDisplay } from '../models/category-to-display.model';
import { environment } from 'src/environments/environment.development';
import { UpdateCategoryRequestDto } from '../models/update-category-request.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient, private cookieService: CookieService) {}

  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.http.post<void>(
      `${environment.localApiUrl}/Category?addAuth=true`,
      model
    );
  }

  getAllCategories(
    query?: string,
    sortBy?: string,
    sortDirection?: string,
    pageNumber?: number,
    pageSize?: number
  ): Observable<CategoryToDisplay[]> {
    let params = new HttpParams();
    if (query) {
      params = params.set('query', query);
    }
    if (sortBy) {
      params = params.set('sortBy', sortBy);
    }
    if (sortDirection) {
      params = params.set('sortDirection', sortDirection);
    }
    if (pageNumber) {
      params = params.set('pageNumber', pageNumber);
    }
    if (pageSize) {
      params = params.set('pageSize', pageSize);
    }
    return this.http.get<CategoryToDisplay[]>(
      `${environment.localApiUrl}/Category`,
      {
        params: params,
      }
    );
  }

  getCategoryById(id: string): Observable<CategoryToDisplay> {
    return this.http.get<CategoryToDisplay>(
      `${environment.localApiUrl}/Category/${id}`
    );
  }

  updateCategory(
    id: string,
    model: UpdateCategoryRequestDto
  ): Observable<CategoryToDisplay> {
    return this.http.put<CategoryToDisplay>(
      `${environment.localApiUrl}/Category/${id}?addAuth=true`,
      model
    );
  }

  deleteCategoryById(id: string): Observable<CategoryToDisplay> {
    return this.http.delete<CategoryToDisplay>(
      `${environment.localApiUrl}/Category/${id}?addAuth=true`
    );
  }

  getCategoryCount(): Observable<number> {
    return this.http.get<number>(`${environment.localApiUrl}/Category/Count`);
  }
}
