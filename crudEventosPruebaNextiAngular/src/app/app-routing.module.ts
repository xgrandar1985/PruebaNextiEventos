import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventoFormComponent } from './components/evento-form/evento-form.component';
import { EventoListComponent } from './components/evento-list/evento-list.component';


const routes: Routes = [
  { path: 'eventos', component: EventoListComponent },
  { path: 'evento/edit/:id', component: EventoFormComponent },
  { path: 'evento/new', component: EventoFormComponent },
  { path: '', redirectTo: '/eventos', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
