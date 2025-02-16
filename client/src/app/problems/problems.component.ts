import { Component, OnInit } from '@angular/core';
import { ProblemsService } from '../_services/problems.service';
import { ProblemDto } from '../_models/problemDto';

@Component({
  selector: 'app-problems',
  templateUrl: './problems.component.html',
  styleUrls: ['./problems.component.css']
})
export class ProblemsComponent implements OnInit {

  problemData: ProblemDto = {
    email: '',
    name: '',
    context: '',
  };
  selectedFile?: File;

  constructor(private problemService: ProblemsService) {}
  ngOnInit(): void {
    
  }

  onFileSelected(event: any) {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }

  submitForm() {
    const formData = new FormData();
    formData.append('email', this.problemData.email);
    formData.append('name', this.problemData.name);
    formData.append('context', this.problemData.context);
    
    if (this.selectedFile) {
      formData.append('file', this.selectedFile);
    }

    this.problemService.addNewProblem(formData).subscribe({
      next: (response) => {
        console.log('Problem reported successfully:', response);
      },
      error: (error) => {
        console.error('Error reporting problem:', error);
      }
    });
  }

}
