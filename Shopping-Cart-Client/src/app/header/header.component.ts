import { Component, OnInit } from '@angular/core';
import { ProductviewService } from '../shared/services/productview.service';
import { UserRegistrationService } from '../shared/services/user-registration.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private userService:UserRegistrationService) {}

  ngOnInit(){

  }

}
