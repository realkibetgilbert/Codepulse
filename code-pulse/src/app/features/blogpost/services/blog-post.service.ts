import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { Observable } from 'rxjs';
import { BlogPostToDisplay } from '../models/blog-post-to-display.model';
import { environment } from 'src/environments/environment.development';
import { UpdateBlogPost } from '../models/update-blogpost.model';
import { UrlHandlingStrategy } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class BlogPostService {
  constructor(private http: HttpClient) {}

  createBlogPost(model: AddBlogPost): Observable<BlogPostToDisplay> {
    return this.http.post<BlogPostToDisplay>(
      `${environment.localApiUrl}/BlogPost?addAuth=true`,
      model
    );
  }

  getBlogPosts(): Observable<BlogPostToDisplay[]> {
    return this.http.get<BlogPostToDisplay[]>(
      `${environment.localApiUrl}/BlogPost`
    );
  }

  getBlogPostById(id:string):Observable<BlogPostToDisplay>{
    return this.http.get<BlogPostToDisplay>(`${environment.localApiUrl}/BlogPost/${id}`);
  }
  
  getBlogPostByUrlHandle(urlHandle:string):Observable<BlogPostToDisplay>{
    return this.http.get<BlogPostToDisplay>(`${environment.localApiUrl}/BlogPost/${urlHandle}`);
  }
  updateBlogPost(id:string,model:UpdateBlogPost):Observable<BlogPostToDisplay>{
    return this.http.put<BlogPostToDisplay>(`${environment.localApiUrl}/BlogPost/${id}?addAuth=true`,model);
  }

  deleteBlogPost(id:string):Observable<BlogPostToDisplay>{
    return this.http.delete<BlogPostToDisplay>(`${environment.localApiUrl}/BlogPost/${id}?addAuth=true`)
  }


}
