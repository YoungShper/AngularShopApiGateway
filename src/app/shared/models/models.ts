export interface ItemBase
{
  id: string;
  className: string;
  name: string;
}

export interface Article extends ItemBase {
  description: string;
}

export interface Product extends ItemBase {
  price: number;
  quantity: number;
  cartQuantity?: number;
  discountPrice: number;
  categoryId: string;
  description: string;
  protein: number;
  fats: number;
  carbs: number;
  calories: number;
}

export interface CategoryModel extends ItemBase {
}

export interface PagedData<T extends ItemBase> {
  items: T[];
  totalPages: number;
}

export interface AuthPayload {
  id: string;
  mail: string;
  isAdmin: boolean;
}

export interface User extends ItemBase {
  lastName: string;
  name: string;
  address: string;
  postalCode: string;
  isAdmin: boolean;
  city: string;
  mail: string;
  password: string;
}

export interface CartDataModel {
  id?: string;
  cartId?: string;
  isActual: boolean;
  products: Product[];
}

export interface CartModel {
  id?: string;                 // Guid? → string | undefined
  cartId?: string;
  userId: string;              // Guid → string
  productId: string;           // Guid → string
  quantity: number;           // int → number
  statusId?: string;           // Guid? → string | undefined
  createdOn?: string;          // DateTime? → string (ISO date)
  isActual: boolean;           // bool → boolean
  className: string;           // string
}

export interface UserProfile {
  goal: string;     // "gain", "loss", "maintain"
  weight: number;
  height: number;
  age: number;
}

