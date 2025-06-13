import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPostToDisplay } from '../models/blog-post-to-display.model';
import { CategoryService } from '../../category/services/category.service';
import { Observable } from 'rxjs';
import { CategoryToDisplay } from '../../category/models/category-to-display.model';
import { UpdateBlogPost } from '../models/update-blogpost.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.scss'],
})
export class EditBlogpostComponent implements OnInit {
  id: string | null = null;
  model?: BlogPostToDisplay;
  selectedCategories?:string[];
  categories$?:Observable<CategoryToDisplay[]>;
  isImageSelectorVisible:boolean=false;
  constructor(
    private route: ActivatedRoute,
    private blogPostService: BlogPostService,
    private categoryService:CategoryService,
    private router:Router,
    private imageService:ImageService
  ) {}
  ngOnInit(): void {
    this.categories$=this.categoryService.getAllCategories();
    this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response) => {
              this.model = response;
              this.selectedCategories=response.categories.map(X=>X.id);
            },
          });
        }
        this.imageService.onSelectImage().subscribe({
          next:(res)=>{
            if(this.model){
            this.model.featuredImageUrl=res.url;
            this.isImageSelectorVisible=false;
            }
          }
        })
      },
    });
  }
  onFormSubmit(){
    //CONVERT TO REQUEST OBJECT
  if(this.model && this.id ){
    var updateBlog:UpdateBlogPost={
     author:this.model.author,
     content:this.model.content,
     shortDescription:this.model.shortDescription,
     featuredImageUrl:this.model.featuredImageUrl,
     isVisible:this.model.isVisible,
     publishedDate:this.model.publishedDate,
     title:this.model.title,
     urlHandle:this.model.urlHandle,
     categories:this.selectedCategories??[]
    }
    this.blogPostService.updateBlogPost(this.id,updateBlog).subscribe({
      next:(response)=>{
     this.router.navigateByUrl('/admin/blogposts');
      }
    })
  }
  }
  openImageSelector():void{
   this.isImageSelectorVisible=true;

  }
  closeImageSelector(){
    this.isImageSelectorVisible=false;
  }
}
