import { Component, OnDestroy, OnInit } from '@angular/core';
import { GameService } from '../services/gameService';
import { Player } from '../models/player';
import { Game, GameStatus } from '../models/game';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  player: Player = {
    nick: '',
    score: 0
  };

  opponent: Player = {
    nick: '',
    score: 0
  };

  game: Game = {
    status: 'NotStarted',
    round: 1,
    gameResult: null,
    roundResult: null
  }

  waitingForAnotherPlayer = false;

  constructor(private gameService: GameService) {
  }

  startOrJoinGame(): void {
    this.gameService.startOrJoinGame(this.player);
    this.game.status = 'Initiated'
  }

  get startButtonText(): string {
    return this.gameWasInitiatedByAnotherPlayer? "Join": "Start";
  }

  get gameWasInitiatedByAnotherPlayer(): boolean {
    return this.waitingForAnotherPlayer && this.game.status === 'NotStarted';
  }

  ngOnDestroy(): void {
    this.gameService.unsubscribe('WaitingForSecondPlayerToJoin');
    this.gameService.unsubscribe('StartTheGame');
  }

  ngOnInit(): void {
    this.gameService.subscribe('WaitingForSecondPlayerToJoin', () => {
      this.waitingForAnotherPlayer = true;
    })

    this.gameService.subscribe('StartTheGame', (opponentNick) => {
      this.opponent.nick = opponentNick;
      this.game.status= 'InProgress';
    })
  }

}
