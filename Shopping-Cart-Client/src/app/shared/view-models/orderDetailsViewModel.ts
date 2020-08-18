 import { OrderItemsViewModel } from './orderItemsViewModel';

export class OrderDetailsViewModel{
    orderId:number
    oderDate:Date
    totalAmount:number
    paymentMethod:string
    orderItems:OrderItemsViewModel[]
    customerId:number
    customerName:string
    Address:string
    contact:string
    
}