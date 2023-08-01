import { Component, Inject, Input } from '@angular/core';
import { Player } from '../models/player';
import { Game, Weapon, GameResult, GameStatistic } from '../models/game';
import { GameService } from '../services/gameService';
@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css']
})
export class GameBoardComponent {

  @Input() player!: Player;
  @Input() opponent!: Player;
  @Input() game!: Game;
  declare opponentWeapon: Weapon;
  declare playerWeapon: Weapon;
  declare gameStatistic: GameStatistic;
  opponentMadeChoice = false;
  opponentScore = 0;
  choiceIsMade = false;

  constructor(private gameService: GameService)
  {}

  makeChoice(weapon: Weapon) {
    if(this.waitingForOpponent){
      return;
    }

    this.choiceIsMade = true;
    this.opponentMadeChoice = false;
    this.playerWeapon = weapon;
    this.gameService.makeChoice(this.game.round, weapon)
  }

  get waitingForOpponent() {
    return this.choiceIsMade && !this.opponentMadeChoice;
  }

  ngOnDestroy(): void {
    this.gameService.unsubscribe('RoundEnd');
    this.gameService.unsubscribe('GameEnd');
  }

  ngOnInit(): void {
    this.gameService.subscribe('RoundEnd', ({playerResult, opponentWeapon}:{ playerResult: GameResult, opponentWeapon:Weapon }) => {
      this.opponentMadeChoice = true;
      if(playerResult === 'Win' ){
        this.player.score++;
      }
      else{
        if(playerResult !== 'Draw'){
          this.opponentScore++;
        }

      }

      this.opponentWeapon = opponentWeapon;
      this.game.round++;
      this.game.roundResult = playerResult;
      this.choiceIsMade = false;
    })

    this.gameService.subscribe('GameEnd', (gameStatistic: GameStatistic) => {
      this.game.status = 'Finished';
      this.game.gameResult = gameStatistic.playerResult;
      this.gameStatistic = gameStatistic;
    })
  }
}

