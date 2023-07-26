import { Component, OnDestroy, OnInit } from '@angular/core';
import { SignalrService } from './singlr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit, OnDestroy {
  /**
   *
   */
  constructor(
    public signalrService: SignalrService
  )
  {}

  title = 'app';

  ngOnInit(): void {
    this.signalrService.startConnection();

    setTimeout(() => {
      this.signalrService.askServerListener();
      this.signalrService.askServer();
    }, 2000);
  }

  ngOnDestroy() {
    this.signalrService.hubConnection.off("askServerResponse");
  }
}
