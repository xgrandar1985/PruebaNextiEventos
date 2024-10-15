import { Component, OnInit } from '@angular/core';
import { EventoService, Evento } from '../../services/evento.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-evento-form',
  templateUrl: './evento-form.component.html',
})
export class EventoFormComponent implements OnInit {
  evento: Evento = {
    id: 0,
    fecha: '',
    lugar: '',
    descripcion: '',
    precio: 0,
    desabilitado: false
  };

  constructor(
    private eventoService: EventoService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id) {
      this.eventoService.getEventoById(id).subscribe((data) => {
        this.evento = data;
      });
    }
  }

  saveEvento(): void {
    if (this.evento.id) {
      this.eventoService.updateEvento(this.evento.id, this.evento).subscribe(() => {
        this.router.navigate(['/eventos']);
      });
    } else {
      this.eventoService.createEvento(this.evento).subscribe(() => {
        this.router.navigate(['/eventos']);
      });
    }
  }
}
