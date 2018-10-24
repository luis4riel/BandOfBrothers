import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Router, ActivatedRoute } from '@angular/router';
import { Destinatario } from '../../shared/destinatario.model';
import { DestinatarioResolveService } from '../../shared/destinatario.service';



@Component({
    templateUrl: './destinatario-detail.component.html',
})
export class DestinatarioDetailComponent implements OnInit, OnDestroy{
    public destinatario: Destinatario;
    public availabilityText: string = '';
    public dateResult: number;
    public isLoading: boolean = true;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: DestinatarioResolveService, private router: Router, private route: ActivatedRoute) {
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
    public ngOnInit(): void {
        this.resolver.onChanges
        .takeUntil(this.ngUnsubscribe)
        .subscribe((destinatario: Destinatario) => {
            this.isLoading = false;
            this.destinatario = Object.assign(new Destinatario(), destinatario);
        });
    }
    public redirect(): void {
        this.router.navigate(['/destinatarios'], { relativeTo: this.route });
    }

    public onEdit(): void {
        this.router.navigate(['edit'], { relativeTo: this.route });
    }
}
