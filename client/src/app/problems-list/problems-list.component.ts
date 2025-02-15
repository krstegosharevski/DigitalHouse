import { Component, OnInit } from '@angular/core';
import { ProblemDto } from '../_models/problemDto';
import { Pagination } from '../_models/pagination';
import { ProblemParams } from '../_models/problemParams';
import { ProblemsService } from '../_services/problems.service';
import { ActivatedRoute } from '@angular/router';
declare var bootstrap: any;
@Component({
  selector: 'app-problems-list',
  templateUrl: './problems-list.component.html',
  styleUrls: ['./problems-list.component.css']
})
export class ProblemsListComponent implements OnInit {

  problems: ProblemDto[] = [];
  pagination: Pagination | undefined
  productParams: ProblemParams = new ProblemParams();
  selectedProblem: any = null;


  constructor(private route : ActivatedRoute,
              private problemService: ProblemsService){ }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.loadProblems();
    });
  }

  loadProblems(): void{
    this.problemService.getAllProblems(this.productParams).subscribe({
      next: (response) => {
        if(response.result && response.pagination){
          this.problems = response.result;
          this.pagination = response.pagination;
        }
      },
      error: (err) => console.error("Error loading products", err)
    });
  }


  pageChanged(event: any){
    if(this.productParams && this.productParams.pageNumber !== event.page){
      this.productParams.pageNumber = event.page;
      this.loadProblems();
    }
  }

  
  openModal(problem: any) {
    this.selectedProblem = problem;
    var myModal = new bootstrap.Modal(document.getElementById('problemModal'));
    myModal.show();
  }

}
