import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavoriteListButtonComponent } from './favorite-list-button.component';

describe('FavoriteListButtonComponent', () => {
  let component: FavoriteListButtonComponent;
  let fixture: ComponentFixture<FavoriteListButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FavoriteListButtonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FavoriteListButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
