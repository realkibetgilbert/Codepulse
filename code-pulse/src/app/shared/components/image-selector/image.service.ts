import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BlogImageToDisplay } from './models/blog-image.model';
import { BlogPostToDisplay } from 'src/app/features/blogpost/models/blog-post-to-display.model';
import { environment } from 'src/environments/environment.development';
import { TitleStrategy } from '@angular/router';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http:HttpClient) { }

  selectedImage:BehaviorSubject<BlogImageToDisplay>=new BehaviorSubject<BlogImageToDisplay>({
    id:'',
    fileExtension:'',
    fileName:'',
    title:'',
    url:''
  });

  uploadImage(file:File,fileName:string,title:string):Observable<BlogImageToDisplay>{
     const formData= new FormData();
     formData.append('file',file);
     formData.append('fileName',fileName);
     formData.append('title',title);

     return this.http.post<BlogImageToDisplay>(`${environment.localApiUrl}/Image`,formData)
    }

    getAllImages():Observable<BlogImageToDisplay[]>{
    return this.http.get<BlogImageToDisplay[]>(`${environment.localApiUrl}/Image`)
    }

    selectImage(image:BlogImageToDisplay):void{
      this.selectedImage.next(image);
    }
    onSelectImage():Observable<BlogImageToDisplay>{
      return this.selectedImage.asObservable();
    }
}
