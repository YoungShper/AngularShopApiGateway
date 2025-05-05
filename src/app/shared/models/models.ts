export interface ItemBase
{
  id: number;
  name: string;
  className: string;
}

export interface Article extends ItemBase {

}

export interface Product extends ItemBase {
  price: number;
  image: string;
  description: string;
  category: string;
}
