export interface ItemBase
{
  id: number;
  name: string;
  className: string;
}

export interface Article extends ItemBase {
  description: string;
}

export interface Product extends ItemBase {
  price: number;
  quantity: number;
  discountPrice: number;
  category_id: string;
  description: string;
}

export interface CategoryModel extends ItemBase {
}

export interface PagedData<T extends ItemBase> {
  items: T[];
  totalPages: number;
}
