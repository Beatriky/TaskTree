import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

interface Task {
  id?: number;
  name: string;
  steps: Step[];
  expanded?: boolean;
}

interface Step {
  name: string;
  owner: string;
  subSteps?: Step[];
  expanded?: boolean;
}

@Component({
  selector: 'app-task-tree',
  templateUrl: './task-tree.component.html',
  styleUrls: ['./task-tree.component.css'],
})
export class TaskTreeComponent implements OnInit {
  tasks: Task[] = [];

  editedTask: Task | null = null;
  editedStep: Step | null = null;
  editedSubStep: Step | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.fetchTasks();
  }

  fetchTasks() {
    this.http.get<Task[]>('https://localhost:7090/api/tasks').subscribe(
      (tasks) => {
        this.tasks = tasks;

        if (this.tasks.length > 0) {
          this.tasks[0].expanded = false;
        }
      },
      (error) => {
        console.error('Error fetching tasks:', error);
      }
    );
  }
  toggleExpansion(task: Task) {
    task.expanded = !task.expanded;
  }

  toggleStepExpansion(step: Step) {
    step.expanded = !step.expanded;
  }
  
  createTask(taskName: string) {
    const newTask: Task = { name: taskName, steps: [] };
    this.http.post<Task>('https://localhost:7090/api/tasks', newTask)
      .subscribe(task => {
        this.tasks.push(task);
      }, error => {
        console.error('Error creating task:', error);
      });
  }

  deleteTask(task: Task) {
    this.http.delete(`https://localhost:7090/api/tasks/${task.id}`)
      .subscribe(() => {
        this.tasks = this.tasks.filter(t => t.id !== task.id);
      }, error => {
        console.error('Error deleting task:', error);
      });
  }

}

