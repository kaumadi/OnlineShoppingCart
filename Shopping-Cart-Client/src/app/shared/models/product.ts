import { Category } from './category'

export class Product {
    ProductId:number
    ProductName:string 
    UnitPrice:number
    UnitsInStock:number
    Description:string
    ImagePath:string

    Categories:Category
  }