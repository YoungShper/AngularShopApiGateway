import {Component, Input, OnInit} from '@angular/core';
import {CategoryModel, Product} from '../../shared/models/models';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {HttpService} from '../../services/http.service';
import {ActivatedRoute} from '@angular/router';
import {NgForOf, NgIf} from '@angular/common';
import {forkJoin} from 'rxjs';

@Component({
  selector: 'app-product-edit',
  imports: [
    ReactiveFormsModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './product-edit.component.html',
  styleUrl: './product-edit.component.scss'
})
export class ProductEditComponent implements OnInit {
  product: Product | null = null;
  form!: FormGroup;
  isEditMode: boolean = false;
  id!: string;
  action!: string;
  categories!: CategoryModel[];
  isDragging: any;
  previewUrl: any;

  constructor(private fb: FormBuilder, private http: HttpService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = params.get('id')!;
      this.action = params.get('action')!;
      this.isEditMode = this.action === 'edit';

      // Параллельно загрузим данные и категории
      forkJoin({
        product: this.http.getDataByID<Product>('products', this.id),
        categories: this.http.getData<CategoryModel>('categories')
      }).subscribe(({product, categories}) => {
        this.product = product;
        this.categories = categories;
        this.initForm();
      });
    });
  }


  initForm(): void {
    this.form = this.fb.group({
      id: [this.product?.id],
      name: [this.product?.name || '', Validators.required],
      className: [this.product?.className || '', Validators.required],
      price: [this.product?.price || 0, Validators.required],
      quantity: [this.product?.quantity || 0, Validators.required],
      discountPrice: [this.product?.discountPrice || null],
      categoryId: [this.product?.categoryId || '', Validators.required],
      description: [this.product?.description || ''],
      protein: [this.product?.protein || 0],
      fats: [this.product?.fats || 0],
      carbs: [this.product?.carbs || 0],
      calories: [this.product?.calories || 0]
    });
  }
  onSubmit() {
    if (this.form.invalid) return;
    const productData = this.form.value as Product;

    if (this.isEditMode && this.product) {
      this.http.updateData(`products`, productData).subscribe(response => {
        console.log('Updated:', response);
      });
    } else {
      this.http.createData('products', productData).subscribe(response => {
        console.log('Created:', response);
      });
    }
  }

  onDragOver($event: DragEvent) {
    
  }

  onDragLeave($event: DragEvent) {
    
  }

  onDrop($event: DragEvent) {
    
  }

  onFileSelected($event: Event) {
    
  }
}
