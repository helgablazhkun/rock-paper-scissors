import { Component, OnDestroy, OnInit } from '@angular/core';
import { GameService } from './services/gameService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit, OnDestroy {

  constructor(
    public gameService: GameService
  )
  {}

  title = 'Rock Paper Scissors';

  ngOnInit(): void {
    this.gameService.startConnection();

    //TODO: remove
    setTimeout(() => {
      this.gameService.testConnection();
    }, 2000);
  }

  ngOnDestroy() {
    this.gameService.unsubscribe('AskServerResponse');
  }
}
