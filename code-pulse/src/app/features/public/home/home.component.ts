import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../../blogpost/services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPostToDisplay } from '../../blogpost/models/blog-post-to-display.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
blogs$?:Observable<BlogPostToDisplay[]>;
  constructor(private blogPostService:BlogPostService){

  }
  ngOnInit(): void {
    this.loadBlogPosts();
  }
private loadBlogPosts(){
this.blogs$=this.blogPostService.getBlogPosts();
}
}
