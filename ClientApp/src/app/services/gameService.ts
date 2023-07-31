import { Injectable } from "@angular/core";
import { Player } from "../models/player";
import { SignalrService } from "./signalrService";
import { GameEvent, Weapon } from "../models/game";

@Injectable({ providedIn: 'root' })
export class GameService {
    constructor(
      public signalrService: SignalrService
        ) { }

    startConnection = () => {
      this.signalrService.startConnection();
    }

    unsubscribe = (gameEvent: GameEvent) => {
      this.signalrService.unsubscribe(gameEvent);
    }

    testConnection = () => {
      this.signalrService.subscribe('AskServerResponse', (someText) => {
        console.log(someText);
      });

      this.signalrService.invoke('AskServer', 'hey');
    }

    startOrJoinGame = (player: Player) => {
      this.signalrService.invoke('JoinGame', player.nick);
    }

    makeChoice = (round: number, weapon: Weapon) => {
      this.signalrService.invoke('MakeChoice', round, weapon);
    }

    subscribe = (gameEvent: GameEvent, callback: ((...args: any[]) => void) | ((...args: any[]) => void)) => {
      this.signalrService.subscribe(gameEvent, callback);
    }
}