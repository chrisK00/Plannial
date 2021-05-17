import { Component, OnInit } from '@angular/core';
import { Message } from '../_models/message';
import { MessageService } from '../_services/message.service';


@Component({
  selector: 'app-message-component',
  templateUrl: './message-component.component.html',
  styleUrls: ['./message-component.component.css']
})
export class MessageComponentComponent implements OnInit {
  recipientId?: string;
  message?: string;
  token?: string;
  constructor(public messageService: MessageService) { }
  ngOnInit(): void {
  }

  send() {
    this.messageService.send(this.recipientId, this.message).then();
  }

  login() {
    console.log('hi');
    this.messageService.createHubConnection(this.token, this.recipientId);
    localStorage.setItem('token', this.token);
  }

}
