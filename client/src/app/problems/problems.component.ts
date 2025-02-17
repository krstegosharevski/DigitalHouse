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
  errorMessage: any;
  showSuccessBanner = false

  constructor(private problemService: ProblemsService) { }
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

        this.problemData = { email: '', name: '', context: '' };
        this.selectedFile = undefined;

        this.errorMessage = '';
        this.showSuccessBanner = true
      },
      error: (error) => {
        if (error.error?.message) {
          this.errorMessage = error.error.message;
          this.showSuccessBanner = false;
        } else {
          this.errorMessage = "All fields are required except picture which is optional!";
          this.showSuccessBanner = false;
        }
      }
    });
  }

}
