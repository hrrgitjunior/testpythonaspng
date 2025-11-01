import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from "@angular/router";

class MyDto {
  name: string | any;
  file: File | any;
}

@Component({
  selector: 'upload-component',
  templateUrl: './upload.component.html'
})


export class UploadComponent implements OnInit {
  name: string | any = '';
  age: number | any = 0;
  file: File | any = null;


  constructor(private http: HttpClient) { }

  ngOnInit() { }

  uploadData(): void {
    
    const formData = new FormData();
    formData.append('name', this.file.name);
    formData.append('file', this.file);
    //event.preventDefault();
    console.log("=====uploadData FILE======", this.file);
    let myDto: MyDto = new MyDto();
    myDto.name = "AAA";
    myDto.file = this.file;
    //const headers = new HttpHeaders().set('Content-Type', 'multipart/form-data');
    this.http.post('/api/upload', formData, {}).subscribe(() => {
      // Handle success
    }, (error) => {
      // Handle error
    });
  }

  handleFileChange(event: any): void {
    console.log("=======", event.target.files.item(0));
    this.file = event.target.files.item(0);
  }
}
