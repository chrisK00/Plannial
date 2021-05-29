import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { take } from 'rxjs/operators';
import { Message } from '../_models/message';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  messageHubUrl = `${environment.hubUrl}message`;
  private hubConnection: HubConnection;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor() { }

  createHubConnection(user: User, otherUserId: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.messageHubUrl + '?user=' + otherUserId, {
        accessTokenFactory: () => user.token
      }).withAutomaticReconnect().configureLogging(LogLevel.Information).build();

    this.hubConnection.start().catch(e => console.log(e));

    this.hubConnection.on('MessageThread', messages => {
      console.log(messages);
      this.messageThreadSource.next(messages);
    });

    this.hubConnection.on('NewMessage', message => {
      console.log(message);
      this.messageThread$.pipe(take(1)).subscribe(messages => {
        this.messageThreadSource.next([...messages, message]);
      });
    });
  }

  stopHubConnection() {
    if (this.hubConnection?.connectionId) {
      this.hubConnection.stop();
      console.log('Stopped hub connection');
    }
  }

  async send(recipientEmail: string, content: string) {
    console.log(content);
    return this.hubConnection.invoke('AddMessage', { recipientEmail, content })
      .catch(e => console.log(e));
  }
}


