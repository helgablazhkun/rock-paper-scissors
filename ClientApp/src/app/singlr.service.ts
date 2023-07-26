import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({ providedIn: 'root' })
export class SignalrService {
    constructor(
        ) { }

    declare hubConnection: signalR.HubConnection;

    startConnection = () => {
      this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7136/gamehub', {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets //to avoid cors issues
      })
      .build();

      this.hubConnection
      .start()
      .then(() => {
          console.log('Hub Connection Started!');
      })
      .catch((err: any) => console.log('Error while starting connection: ' + err))
  }

  askServer() {
    this.hubConnection.invoke("askServer", "hey")
        .catch(err => console.error(err));
  }

  askServerListener() {
    this.hubConnection.on("askServerResponse", (someText) => {
        console.log(someText);
  })
}

}