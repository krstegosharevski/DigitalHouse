export interface Tariff{
    id:number,
    name: string,
    internetSpeed: number,
    conversationTime: string,
    sms: string,
    roamingInternet?: number,
    internationalNetworkCalls?: number,
    e_bill?: boolean,
    price: number,
    discount?: number
}