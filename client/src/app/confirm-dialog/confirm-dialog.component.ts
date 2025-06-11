import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmation-dialog',
  template: `
    <h1 mat-dialog-title class="dialog-title">Are you sure?</h1>
    <div mat-dialog-content class="dialog-content">
      <!-- <p>Your shopping cart will be cancelled</p> -->
    </div>
    <div mat-dialog-actions class="dialog-actions">
      <button mat-button (click)="closeDialog(false)" class="no-button">No</button>
      <button mat-button (click)="closeDialog(true)" cdkFocusInitial class="yes-button">Yes</button>
    </div>
  `,
  styles: [`
    .dialog-title {
      color: #333;
      font-weight: 500;
      padding: 16px 24px;
      margin: 0;
      border-bottom: 1px solid #eee;
    }
    
    .dialog-content {
      padding: 20px 24px;
      color: #666;
      font-size: 16px;
    }
    
    .dialog-actions {
      display: flex;
      justify-content: flex-end;
      padding: 8px 16px;
      gap: 8px;
    }
    
    .no-button {
      color: #666;
    }
    
    .yes-button {
      color: white;
      background-color: #f44336;
    }
    
    .yes-button:hover {
      background-color: #d32f2f; 
    }
  `]
})
export class ConfirmationDialogComponent {
  constructor(private dialogRef: MatDialogRef<ConfirmationDialogComponent>) {}

  closeDialog(result: boolean): void {
    this.dialogRef.close(result);
  }
}