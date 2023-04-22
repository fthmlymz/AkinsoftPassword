import {Component, OnInit} from '@angular/core';
import {MypasswordsService} from "../../../service/mypasswords/mypasswords.service";
import {MyPasswordModel} from "../../../models/MyPasswordModel";
import {Router} from "@angular/router";

@Component({
  selector: 'app-mypasswords',
  templateUrl: './mypasswords.component.html',
  styleUrls: ['./mypasswords.component.scss']
})
export class MypasswordsComponent implements OnInit {

  constructor(private service: MypasswordsService, private route: Router) { }
  passwordList: MyPasswordModel[] = [];
  clonedPasswordList: { [s: string]: MyPasswordModel } = {};
  columns: [] | any;
  rowCounter: number = 0;


  ngOnInit() {
    this.getMyPasswords();

    this.columns = [
      {field: 'id', header: 'id'},
      {field: 'description', header: 'description'},
      {field: 'url', header: 'url'},
      {field: 'category', header: 'category'},
      {field: 'userName', header: 'userName'},
      {field: 'password', header: 'password'},
    ]
  }

  getMyPasswords() {
    this.service.getMyPasswords()
      .subscribe({
        next: (data: any) => {
          console.log(data);
          this.passwordList = data?.data;
        },
        complete: () => {
          console.log("completed")
        },
        error: (err) => {
          console.log(err)
        }
      })
  }
  addPassword(password: MyPasswordModel, index: number) {
    this.service.postPassword(password)
      .subscribe({
        next: (response: any) => {
          this.passwordList.push(response.data);
          this.passwordList = [...this.passwordList];
        },
        error: (e) => {
          console.log(e)
          //this.messageService.add({severity: 'error', summary: 'Hata', detail: `kayıt yapılamadı \n${e}`, life: constants.TOAST_ERROR_LIFETIME});
          //this.messageService.clear('c');
        },
        complete: () => {
          //this.messageService.add({severity: 'success', summary: 'Başarılı', detail: `${company.name} isimli şirket kaydedildi.`, life: constants.TOAST_SUCCESS_LIFETIME});
          //this.messageService.clear('c');
          this.passwordList[index] = this.clonedPasswordList[password.id];
          delete this.clonedPasswordList[password.id];
        }
      });
  }

  logout() {
    console.log('logout islemleri');
    this.route.navigate(['/auth/login']);
  }





  newRow() {
    return {
      id: ' '+this.rowCounter++,
      userName: '',
      description: '',
      url: '',
      category: '',
      password: ''
    };
  }
  onRowEditInit(password: MyPasswordModel) {
    this.clonedPasswordList[password.id] = { ...password };
  }
  onRowEditSave(password: MyPasswordModel, index: number) {
    console.log(password)
    if (!password.id.toString().indexOf(' ')){
      this.addPassword(password, index);
    } else if (password.id.toString().indexOf(' ')) {
      //this.putPassword(password);
    }
  }
  onRowEditCancel(password: MyPasswordModel, index: number) {
    this.passwordList[index] = this.clonedPasswordList[password.id];
    delete this.clonedPasswordList[password.id];
  }
}
