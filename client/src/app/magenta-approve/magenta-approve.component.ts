import { Component, OnInit } from '@angular/core';
import { Magenta1 } from '../_models/magenta1';
import { Magenta1Service } from '../_services/magenta1.service';

@Component({
  selector: 'app-magenta-approve',
  templateUrl: './magenta-approve.component.html',
  styleUrls: ['./magenta-approve.component.css']
})
export class MagentaApproveComponent implements OnInit {

  magenta1 : Magenta1[] = [];

  constructor(private magenta1Service : Magenta1Service) { }

  ngOnInit(): void {
    this.fetchMagenta1s()
  }

  fetchMagenta1s(){
    this.magenta1Service.getAllMagenta1().subscribe({
      next : (response) => {
        this.magenta1 = response
      },
      error : (err) => console.log(err)
    })
  }

  // add method when click on button approve to be approved magenta1 for the user.
  approveMagenta1(id : number){
    this.magenta1Service.approveMagenta1(id).subscribe({
      next: (response) => {
        this.fetchMagenta1s();
      },
      error : (err) => console.log(err)
    })
  }

}
