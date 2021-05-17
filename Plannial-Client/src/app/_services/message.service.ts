import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { take } from 'rxjs/operators';
import { Message } from '../_models/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  messageHubUrl = `${environment.hubUrl}message`;
  private hubConnection: HubConnection;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor() { }

  createHubConnection(token: string, otherUserId: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.messageHubUrl + '?user=' + otherUserId, {
        accessTokenFactory: () => token
      }).withAutomaticReconnect().build()

    this.hubConnection.start().catch(e => console.log(e));

    this.hubConnection.on('MessageThread', messages => {
      console.log(messages);;
      this.messageThreadSource.next(messages);
    })

    this.hubConnection.on('NewMessage', message => {
      console.log(message);
      this.messageThread$.pipe(take(1)).subscribe(messages => {
        this.messageThreadSource.next([...messages, message])
      })
    })
  }

  stopHubConnection() {
    this.hubConnection.stop();
  }

  async send(recipientId: string, content: string) {
    console.log(recipientId);
    console.log(content);
    return this.hubConnection.invoke('AddMessage', { recipientId, content })
      .catch(e => console.log(e));
  }
}


