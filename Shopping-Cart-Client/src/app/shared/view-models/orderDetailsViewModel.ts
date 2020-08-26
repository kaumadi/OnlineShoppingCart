 import { OrderItemsViewModel } from './orderItemsViewModel';

export class OrderDetailsViewModel{
    orderId:number
    orderDate:Date
    totalAmount:number
    paymentMethod:string
    orderItemsViewModel:OrderItemsViewModel[]
    customerId:number
    firstName:string
    lastName:string
    address:string
    contact:string
    
}