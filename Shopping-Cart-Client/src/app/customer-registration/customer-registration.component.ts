import { Component, OnInit } from '@angular/core';
import { UserRegistrationService } from '../shared/services/user-registration.service';
import { RegistrationViewModel } from '../shared/view-models/RegistrationViewModel';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-customer-registration',
  templateUrl: './customer-registration.component.html',
  styleUrls: ['./customer-registration.component.css']
})
export class CustomerRegistrationComponent implements OnInit {
  massage: string;  
  dataSaved = false;  
  Adduser:FormGroup;  

  constructor(private router: Router,public userRegistrationService:UserRegistrationService) { }  

  AddNewUser(registrationViewModel:RegistrationViewModel)  
  {  
      this.userRegistrationService.RegisterCustomer(registrationViewModel)
      .subscribe(  
        (data: any)=>  
        {      
          this.dataSaved = true;
        })  
  }  
  onFormSubmit() {  
    this.AddNewUser(this.Adduser.value); 
  }  
  
  clearform() {  
    debugger;  
    this.Adduser.controls['Email'].setValue("");  
    this.Adduser.controls['Password'].setValue("");  
    this.Adduser.controls['FirstName'].setValue("");  
    this.Adduser.controls['LastName'].setValue("");  
    this.Adduser.controls['Address'].setValue("");  
}
  
  ngOnInit() {  
    this.Adduser = new FormGroup({  

      email: new FormControl(), 
      password:new FormControl(),  
      firstName:new FormControl(),  
      lastName:new FormControl(),  
      address:new FormControl(),  
  });  

}  

}