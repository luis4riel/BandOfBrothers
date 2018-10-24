import { Component } from '@angular/core';

import 'rxjs/add/observable/of';
import 'rxjs/add/observable/throw';
import 'rxjs/add/observable/empty';
import 'rxjs/add/operator/takeUntil';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/delay';
import 'rxjs/add/operator/take';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/debounceTime';

@Component({
    selector: 'ndd-app',
    templateUrl: './app.component.html',
})
export class AppComponent {
    public showLoader: boolean = false;
}
