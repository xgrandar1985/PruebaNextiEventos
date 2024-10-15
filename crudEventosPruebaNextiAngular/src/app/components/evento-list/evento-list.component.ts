import { Component, OnInit } from '@angular/core';
import { EventoService, Evento } from '../../services/evento.service';

@Component({
  selector: 'app-evento-list',
  templateUrl: './evento-list.component.html',
})
export class EventoListComponent implements OnInit {
  eventos: Evento[] = [];

  constructor(private eventoService: EventoService) {}

  ngOnInit(): void {
    this.loadEventos();
  }

  loadEventos(): void {
    this.eventoService.getEventos().subscribe((data) => {
      this.eventos = data;
    });
  }

  /*deleteEvento(id: number): void {
    this.eventoService.deleteEvento(id).subscribe(() => {
      this.loadEventos();  
    });
  }*/

    deleteEvento(id: number): void {
      if (confirm('Estas seguro de que deseas eliminar este evento')) {
        this.eventoService.deleteEvento(id).subscribe(
          () => {
            this.eventos = this.eventos.filter(evento => evento.id !== id);
            alert('Evento eliminado con exito.');
          },
          (error) => {
            console.error('Error al eliminar el evento', error);
          }
        );
      }
    }

}
