import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { ProblemDto } from '../_models/problemDto';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ProblemParams } from '../_models/problemParams';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProblemsService {

  baseUrl = environment.apiUrl
  paginatedResult: PaginatedResult<ProblemDto[]> = new PaginatedResult<ProblemDto[]>;

  constructor(private http: HttpClient) { }

  public getAllProblems(problemParams: ProblemParams) {
    let params = this.getPaginationHeaders(problemParams.pageNumber, problemParams.pageSize)

    return this.getPaginatedResult<ProblemDto[]>(this.baseUrl + "problem", params);
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams;
    params = params.append('pageNumber', pageNumber);
    params = params.append('pageSize', pageSize);
    return params;
  }

  private getPaginatedResult<T>(url: string, params: HttpParams) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>;

    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if (response.body) {
          paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if (pagination) {
          paginatedResult.pagination = JSON.parse(pagination);
        }
        return paginatedResult;
      })
    )
  }

  addNewProblem(formData: FormData): Observable<any> {
    return this.http.post(`${this.baseUrl}problem/report-problem`, formData)
  }

  deleteProblemPost(id: number){
    return this.http.delete(`${this.baseUrl}problem/delete/${id}`);
  }
}
