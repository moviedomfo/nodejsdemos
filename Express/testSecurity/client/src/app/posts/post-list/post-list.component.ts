import { Component, OnInit, OnDestroy } from '@angular/core';
import {PostsAndCommentsService} from './../../service/posts-and-comments.service'

import { Subscription } from 'rxjs/Subscription';
import { Post } from "../../model/post";
import { HttpHelpersService } from "../../service/http-helpers.service";
@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit,OnDestroy {

  //aqui podria haber usado un Observable : ej :  postsSub$ : Observable<Post[]>;
 postsSub$ : Subscription;
 private posts :Post[];
  
 error: any;
  constructor(private postService :PostsAndCommentsService,authService:HttpHelpersService) { }

  ngOnInit() {
    

    this.postsSub$ = this.postService
    .retriveAllPostService()
    .subscribe(
      posts => {
        this.posts = posts;
      },
      err => this.error = err
    );

  }
  ngOnDestroy() {
    this.postsSub$.unsubscribe();
  }
}
