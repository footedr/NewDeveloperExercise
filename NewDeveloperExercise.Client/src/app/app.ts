import { HttpClient } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { map } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('NewDeveloperExercise');
  private http = inject(HttpClient);

  constructor() {
    this.getWeather();
  }

  getWeather() {
    const forecast = this.http.get<any>(`/api/weatherforecast`);

    forecast.pipe(map(f => console.log(f.summary)));
  }
}
