import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { environment } from 'src/environments/environment';
import { FileUploadModule } from "ng2-file-upload"; 
import { MembersService } from 'src/app/_services/members.service';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input()
  member!: Member;
  upload!: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user!: User;

  constructor(private accountService:AccountService, private memberService:MembersService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user == user);
  }

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e :any){
    this.hasBaseDropZoneOver = e;
  }

  setMainPhoto(photo: Photo){
    this.memberService.setMainPhoto(photo.id).subscribe(() => {
      this.user.photoUrl = photo.url;
      this.accountService.setCurrentUser(this.user);
      this.member.photoUrl = photo.url;
      this.member.photos.forEach(p => {
        if(p.isMain) p.isMain = false;
        if(p.id == photo.id) p.isMain = true;
      })
    })
  }

  deletePhoto(photoId : Number){
    this.memberService.deletePhoto(photoId).subscribe(() => {
      this.member.photos = this.member.photos.filter(x => x.id !== photoId);
    })
  }

  initializeUploader(){
    this.upload = new FileUploader({
      url : this.baseUrl + 'users/add-photo',
      authToken : 'Bearer ' + this.user.tokenKey,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10*1024*1024
    });

    this.upload.onAfterAddingAll = (file) =>{
      file.withCredentials = false;
    }

    this.upload.onSuccessItem = (item, response, status, headers) => {
      if(response){
        const photo = JSON.parse(response);
        this.member.photos.push(photo);
      }
    }
  }
}