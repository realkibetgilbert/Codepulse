import { CategoryToDisplay } from "../../category/models/category-to-display.model";

export interface BlogPostToDisplay{
    id:string;
    title:string;
    shortDescription:string;
    content:string;
    featuredImageUrl:string;
    urlHandle:string;
    author:string;
    publishedDate:Date;
    isVisible:boolean;
    categories:CategoryToDisplay[];
}