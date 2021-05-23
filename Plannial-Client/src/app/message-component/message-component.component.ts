import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { MessageService } from '../_services/message.service';


@Component({
  selector: 'app-message-component',
  templateUrl: './message-component.component.html',
  styleUrls: ['./message-component.component.css']
})
export class MessageComponentComponent implements OnInit {
  recipientId?: string;
  message?: string;
  user: User;
  constructor(public messageService: MessageService, private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  send() {
    this.messageService.send(this.recipientId, this.message).then();
  }

  connect() {
    this.messageService.createHubConnection(this.user, this.recipientId);

  }

}
