import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { CategoryToDisplay } from '../models/category-to-display.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss'],
})
export class CategoryListComponent implements OnInit {
  totalCount?: number;
  list: number[]=[];
  categories$?: Observable<CategoryToDisplay[]>;
   pageNumber=1;
   pageSize=5;
  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }
  loadCategories() {
    this.categories$ = this.categoryService.getAllCategories(undefined,undefined,undefined,this.pageNumber,this.pageSize);
  }

  onSearch(query: string) {
    this.categories$ = this.categoryService.getAllCategories(query);
  }
  onDelete(id: string) {
    this.categoryService.deleteCategoryById(id).subscribe({
      next: () => {
        this.loadCategories();
      },
      error: (err) => {
        console.error('Error deleting category:', err);
      },
    });
  }
  sort(sortBy: string, sortDirection: string) {
    this.categories$ = this.categoryService.getAllCategories(
      undefined,
      sortBy,
      sortDirection
    );
  }


}
