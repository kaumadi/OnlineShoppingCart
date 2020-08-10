import { SelectedListViewModel } from './SelectedListViewModel';

export class CheckoutViewModel{
    customerId:number
    userName:string
    token?: string;
    selectedListViewModel:SelectedListViewModel[]
}