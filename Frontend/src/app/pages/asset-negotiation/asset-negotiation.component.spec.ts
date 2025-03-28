import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssetNegotiationComponent } from './asset-negotiation.component';

describe('AssetNegotiationComponent', () => {
  let component: AssetNegotiationComponent;
  let fixture: ComponentFixture<AssetNegotiationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssetNegotiationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssetNegotiationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
