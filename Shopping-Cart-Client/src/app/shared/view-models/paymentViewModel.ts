import { SelectedListViewModel } from './SelectedListViewModel';

export class PaymentViewModel{
    totalAmount:number
    orderDate:Date
    customerId:number
    token?: string;
    selectedListViewModel:SelectedListViewModel[]
    PaymentType:string
}