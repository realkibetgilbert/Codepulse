import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPostService } from '../../blogpost/services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPostToDisplay } from '../../blogpost/models/blog-post-to-display.model';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.scss'],
})
export class BlogDetailsComponent implements OnInit {
  url: string | null = null;
  blogPost$?:Observable<BlogPostToDisplay>;
  constructor(private route: ActivatedRoute, private service:BlogPostService) {}
  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.url = params.get('url');
      },
    });
    if (this.url) {
      this.loadBlog(this.url);
    }
  }
  private loadBlog(url: string) 
  {
   this.blogPost$= this.service.getBlogPostByUrlHandle(url);
      
  }
}
