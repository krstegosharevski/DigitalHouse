import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
   animations: [
      trigger('scaleUp', [
        state('inactive', style({
          transform: 'scale(1)'
        })),
        state('active', style({
          transform: 'scale(1.02)' // Промени ја вредноста за поголемо зголемување
        })),
        transition('inactive <=> active', [
          animate('0.5s ease-out')
        ])
      ])
    ]
})
export class HomeComponent implements OnInit {

  state = 'inactive'; 
  
    @ViewChild('animatedDiv', { static: false }) animatedDiv!: ElementRef;
  
    constructor() { }
    ngOnInit(): void {
      
    }
  
    ngAfterViewInit(): void {
      this.observeScroll();
    }
    
    observeScroll() {
      const observer = new IntersectionObserver(
        (entries) => {
          entries.forEach(entry => {
            if (entry.isIntersecting) {
              this.state = 'active'; // Кога div ќе биде видлив, активирај анимација
            } else {
              this.state = 'inactive'; // Ако исчезне од екранот, ресетирај
            }
          });
        },
        { threshold: 0.5 } // Кога 50% од елементот е видлив, активирај
      );
  
      if (this.animatedDiv) {
        observer.observe(this.animatedDiv.nativeElement);
      }
    }

}
