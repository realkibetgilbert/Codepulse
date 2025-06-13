import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { EditcategoryComponent } from './features/category/editcategory/editcategory.component';
import { BlogpostListComponent } from './features/blogpost/blogpost-list/blogpost-list.component';
import { AddBlogpostComponent } from './features/blogpost/add-blogpost/add-blogpost.component';
import { EditBlogpostComponent } from './features/blogpost/edit-blogpost/edit-blogpost.component';
import { HomeComponent } from './features/public/home/home.component';
import { BlogDetailsComponent } from './features/public/blog-details/blog-details.component';
import { LoginComponent } from './features/auth/login/login.component';
import { authGuard } from './features/auth/guards/auth.guard';

const routes: Routes = [
 
      { path: '', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'blog/:url', component: BlogDetailsComponent },
      { path: 'admin/categories', component: CategoryListComponent,canActivate:[authGuard] },
      { path: 'admin/categories/add', component: AddCategoryComponent,canActivate:[authGuard] },
      { path: 'admin/categories/:id', component: EditcategoryComponent ,canActivate:[authGuard]},
      { path: 'admin/blogposts', component: BlogpostListComponent,canActivate:[authGuard] },
      { path: 'admin/blogposts/add', component: AddBlogpostComponent,canActivate:[authGuard] },
      { path: 'admin/blogposts/:id', component: EditBlogpostComponent,canActivate:[authGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
