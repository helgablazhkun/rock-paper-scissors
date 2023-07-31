import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({ providedIn: 'root' })
export class SignalrService {
    constructor(
        ) { }

    declare hubConnection: signalR.HubConnection;
    private hubMethods: string[] = [];

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

  invoke = (methodName: string, ...args: any[]) => {
    this.hubConnection.invoke(methodName, ...args)
        .catch(err => console.error(err));
  }

  subscribe = (methodName: string, callback: (...args: any[]) => void) => {
    if(!this.hubMethods.includes(methodName)){
        this.hubMethods.push(methodName);
    }

    this.hubConnection.on(methodName, callback);
  }

  unsubscribe(methodName: string) {
    this.hubConnection.off(methodName);
  };

}