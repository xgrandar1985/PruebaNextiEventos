import { Component, OnInit } from '@angular/core';
import { EventoService } from '../../services/evento.service'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-evento-list',
  templateUrl: './evento-list.component.html',
})
export class EventoListComponent implements OnInit {
  eventos: any[] = [];
  page: number = 1; // Página actual
  itemsPerPage: number = 5; // Items por página

  constructor(private eventoService: EventoService, private router: Router) {}

  ngOnInit(): void {
    this.getEventos();
  }

  getEventos(): void {
    this.eventoService.getEventos().subscribe(
      (data) => {
        this.eventos = data;
      },
      (error) => {
        console.error('Error al obtener los eventos', error);
      }
    );
  }

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
